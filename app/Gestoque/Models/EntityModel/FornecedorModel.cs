using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Gestoque.Models
{
    public class FornecedorModel
    {
        #region Atributos

        public int Id { get; set; }
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string NumDocumento { get; set; }
        public TipoPessoa Tipo { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public bool Ativo { get; set; }

        #endregion

        #region Métodos

        //obter a quantidade de registros no bd
        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var db = new ContextoBD())
            {
                ret = db.Fornecedores.Count();
            }

            return ret;
        }

        //função de listar
        public static List<FornecedorModel> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            var ret = new List<FornecedorModel>();
            try
            {
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
                        "select id, nome, tipo, telefone, contato, logradouro, numero, complemento, cep, ativo, estado, cidade," +
                        "razao_social as RazaoSocial, num_documento as NumDocumento from fornecedor" + filtroWhere + " order by nome" + paginacao;

                    ret = db.Database.Connection.Query<FornecedorModel>(sql).ToList();
                }
            }
            catch(Exception ex)
            {

            }


            return ret;
        }

        //função que recupera pelo Id
        public static FornecedorModel RecuperarPeloId(int id)
        {
            FornecedorModel ret = null;

            using (var db = new ContextoBD())
            {
                ret = db.Fornecedores.Find(id);
            }

            return ret;
        }

        //função de excluir
        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            try
            {
                if (RecuperarPeloId(id) != null)
                {
                    using (var db = new ContextoBD())
                    {
                        var fornecedor = new FornecedorModel { Id = id };
                        db.Fornecedores.Attach(fornecedor);
                        db.Entry(fornecedor).State = EntityState.Deleted;
                        db.SaveChanges();
                        ret = true;
                    }
                }
            }
            catch(Exception ex)
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
                if (model == null)
                {
                    db.Fornecedores.Add(this);
                }
                else
                {
                    db.Fornecedores.Attach(this);
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
