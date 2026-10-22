using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Gestoque.Models
{
    public class MarcaProdutoModel
    {
        #region Atributos

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        #endregion

        #region Métodos

        //obter a quantidade de registros no bd
        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var db = new ContextoBD())
            {
                ret = db.MarcasProdutos.Count();
            }

            return ret;
        }

        //função de listar
        public static List<MarcaProdutoModel> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            var ret = new List<MarcaProdutoModel>();

            using (var db = new ContextoBD())
            {
                var filtroWhere = "";
                if (!string.IsNullOrEmpty(filtro))
                {
                    filtroWhere = string.Format(" where lower(nome) like '%{0}%'", filtro.ToLower());
                }

                var pos = (pagina - 1) * tamPagina;
                var paginacao = "";
                if (pagina > 0 && tamPagina > 0)
                {
                    paginacao = string.Format(" offset {0} rows fetch next {1} rows only",
                    pos, tamPagina);
                }

                var sql = 
               "select *" +
               " from marca_produto" +
               filtroWhere +
               " order by nome" +
               paginacao;

                ret = db.Database.Connection.Query<MarcaProdutoModel>(sql).ToList();
            }

            return ret;
        }

        //função que recupera pelo Id
        public static MarcaProdutoModel RecuperarPeloId(int id)
        {
            MarcaProdutoModel ret = null;

            using (var db = new ContextoBD())
            {
                ret = db.MarcasProdutos.Find(id);
            }

            return ret;
        }

        //função de excluir
        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            try { 
            if (RecuperarPeloId(id) != null)
            {
                using (var db = new ContextoBD())
                {
                    var marcas = new MarcaProdutoModel { Id = id };
                    db.MarcasProdutos.Attach(marcas);
                    db.Entry(marcas).State = EntityState.Deleted;
                    db.SaveChanges();
                    ret = true;
                }
            }
            }catch(Exception ex)
            {
                ret = false; 
            }

            return ret;
        }

        //função de salvar ou atualizar
        public int Salvar()
        {
            var ret = 0;

            var model = RecuperarPeloId(this.Id);

            using (var db = new ContextoBD())
            {
                if (model == null) //se for nulo insere no banco de dados
                {
                    db.MarcasProdutos.Add(this);

                }
                else //se não atualiza no banco de dados
                {
                    db.MarcasProdutos.Attach(this);
                    db.Entry(this).State = EntityState.Modified;
                }

                db.SaveChanges();
                ret = this.Id;
            }

            return ret;
        }

        #endregion
    }
}