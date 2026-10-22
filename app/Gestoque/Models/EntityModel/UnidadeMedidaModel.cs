using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace Gestoque.Models
{
    public class UnidadeMedidaModel
    {
        #region Atributos

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }

        #endregion

        #region Métodos

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var db = new ContextoBD())
            {
                ret = db.UnidadesMedida.Count();
            }

            return ret;
        }

        public static List<UnidadeMedidaModel> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            var ret = new List<UnidadeMedidaModel>();

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
                       "select * from unidade_medida" + filtroWhere +
                       " order by nome" + paginacao;

                ret = db.Database.Connection.Query<UnidadeMedidaModel>(sql).ToList();
            }

            return ret;
        }

        public static UnidadeMedidaModel RecuperarPeloId(int id)
        {
            UnidadeMedidaModel ret = null;

            using (var db = new ContextoBD())
            {
                ret = db.UnidadesMedida.Find(id);
            }


            return ret;
        }
        
        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            try { 
            if (RecuperarPeloId(id) != null)
            {
                using (var db = new ContextoBD())
                {
                    var unidadeMedida = new UnidadeMedidaModel { Id = id };
                    db.UnidadesMedida.Attach(unidadeMedida);
                    db.Entry(unidadeMedida).State = EntityState.Deleted;
                    db.SaveChanges();
                    ret = true;
                }
            }
            }catch (Exception ex)
            {
                ret = false;
            }

            return ret;
        }

        public int Salvar()
        {
            var ret = 0;

            var model = RecuperarPeloId(this.Id);

            using (var db = new ContextoBD())
            {
                if (model == null)
                {
                    db.UnidadesMedida.Add(this);
                }
                else
                {
                    db.UnidadesMedida.Attach(this);
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