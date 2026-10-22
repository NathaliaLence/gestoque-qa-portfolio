using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Gestoque.Models
{
    public class PerfilModel
    {
        #region Atributos

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public virtual List<UsuarioModel> Usuarios { get; set; }

        #endregion

        #region Métodos
        

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var db = new ContextoBD())
            {
                ret = db.PerfisUsuarios.Count();
            }

            return ret;
        }

        public static List<PerfilModel> RecuperarLista(int pagina, int tamPagina, string filtro = "")
        {
            var ret = new List<PerfilModel>();

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
                        " from perfil" +
                        filtroWhere +
                        " order by nome" +
                        paginacao;

                ret = db.Database.Connection.Query<PerfilModel>(sql).ToList();
            }

            return ret;
        }

        public static List<PerfilModel> RecuperarListaAtivos()
        {
            var ret = new List<PerfilModel>();

            using (var db = new ContextoBD())
            {
                ret = db.PerfisUsuarios
                    .Where(x => x.Ativo)
                    .OrderBy(x => x.Nome)
                    .ToList();
            }

            return ret;
        }

        public static PerfilModel RecuperarPeloId(int id)
        {
            PerfilModel ret = null;

            using (var db = new ContextoBD())
            {
                ret = db.PerfisUsuarios
                    .Include(x => x.Usuarios)
                    .Where(x => x.Id == id)
                    .SingleOrDefault();
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
                    var perfil = new PerfilModel { Id = id };
                    db.PerfisUsuarios.Attach(perfil);
                    db.Entry(perfil).State = EntityState.Deleted;
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

            using (var db = new ContextoBD())
            {
                var model = db.PerfisUsuarios
                    .Include(x => x.Usuarios)
                    .Where(x => x.Id == this.Id)
                    .SingleOrDefault();

                if (model == null)
                {
                    if (this.Usuarios != null && this.Usuarios.Count > 0) //usuarios diferente de null
                    {
                        foreach (var usuario in this.Usuarios) //p cada um dos usuarios
                        {
                            db.Usuarios.Attach(usuario); //pegar usuario e dizer que já esta dentro do bd
                            db.Entry(usuario).State = EntityState.Unchanged; //mas usuario n foi modificado
                        }
                    }

                    db.PerfisUsuarios.Add(this); //incluir 
                }
                else
                {
                    
                    model.Nome = this.Nome;
                    model.Ativo = this.Ativo;

                    //desmarcar usuario do perfil
                    if (this.Usuarios != null) 
                    {
                        //remover
                        //buscar tds usuarios no bd que n estao na lista dos usuarios em memoria
                        foreach (var usuario in model.Usuarios.FindAll(x => !this.Usuarios.Exists(u => u.Id == x.Id)))
                        {
                            model.Usuarios.Remove(usuario);
                        }
                        //incluir
                        //buscar tds usuarios da lista de usuarios em memoria que n existe no bd
                        foreach (var usuario in this.Usuarios.FindAll(x => x.Id > 0 && !model.Usuarios.Exists(u => u.Id == x.Id)))
                        {
                            db.Usuarios.Attach(usuario); //usuario já esta no bd
                            db.Entry(usuario).State = EntityState.Unchanged;//n inclua esse usuario no bd
                            model.Usuarios.Add(usuario); //add
                        }
                    }
                }

                db.SaveChanges();
                ret = this.Id;
            }

            return ret;
        }

        #endregion
    }
}