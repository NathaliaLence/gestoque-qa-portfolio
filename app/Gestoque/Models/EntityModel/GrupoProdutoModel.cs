using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Gestoque.Models
{
    public class GrupoProdutoModel
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
                ret = db.GruposProdutos.Count();
            }

            return ret;
        }

        //função de listar
        public static List<GrupoProdutoModel> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            var ret = new List<GrupoProdutoModel>() ;

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
               " from grupo_produto" +
               filtroWhere +
               " order by nome" +
               paginacao;

                ret = db.Database.Connection.Query<GrupoProdutoModel>(sql).ToList();
            }

            return ret;
        }

        //função que recupera pelo Id
        public static GrupoProdutoModel RecuperarPeloId(int id)
        {
            GrupoProdutoModel ret = null;

            using (var db = new ContextoBD())
            {
                ret = db.GruposProdutos.Find(id);
            }


            return ret;
        }

        //função de excluir
        public static bool ExcluirPeloId(int id)
        {
            var ret = true;

            try
            {

                if (RecuperarPeloId(id) != null)
                {
                    using (var db = new ContextoBD())
                    {
                        var grupo = new GrupoProdutoModel { Id = id };
                        db.GruposProdutos.Attach(grupo);
                        db.Entry(grupo).State = EntityState.Deleted;
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
                    db.GruposProdutos.Add(this);

                }
                else //se não atualiza no banco de dados

                {
                    db.GruposProdutos.Attach(this);
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

