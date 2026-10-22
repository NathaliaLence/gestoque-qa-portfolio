using Dapper;
using Gestoque.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Gestoque.Models
{
    public class UsuarioModel
    {
        #region Atributos

        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public virtual List<PerfilModel> Perfis { get; set; }

        #endregion

        #region Métodos

        public static UsuarioModel ValidarUsuario(String login, String senha) //logica para buscar no banco
        {
            UsuarioModel ret = null;
            senha = CriptoHelper.HashMD5(senha);
            
            using (var db = new ContextoBD())
            {
                ret = db.Usuarios
                    .Include(x => x.Perfis)
                    .Where(x => x.Login == login && x.Senha == senha)
                    .SingleOrDefault();
            }

            return ret;
        }

        public static int RecuperarQuantidade()
        {
            var ret = 0;

            using (var db = new ContextoBD())
            {
                ret = db.Usuarios.Count();
            }

            return ret;
        }

        public static List<UsuarioModel> RecuperarLista(int pagina = -1, int tamPagina = -1, string filtro = "")
        {
            var ret = new List<UsuarioModel>();

            using (var db = new ContextoBD())
            {
                var pos = (pagina - 1) * tamPagina;
                var paginacao = "";
                if (pagina > 0 && tamPagina > 0)
                {
                    paginacao = string.Format(" offset {0} rows fetch next {1} rows only",
                    pos, tamPagina);
                }

                var filtroWhere = "";
                if (!string.IsNullOrEmpty(filtro))
                {
                    filtroWhere = string.Format(" where lower(nome) like '%{0}%'", filtro.ToLower());
                }

                string sql;

                if (pagina == -1 || tamPagina == -1)
                {
                    sql =
                        "select *" +
                        "from usuario" +
                        filtroWhere +
                        " order by nome";
                }

                else
                {
                    sql =
                        "select *" +
                        " from usuario" +
                        filtroWhere +
                        " order by nome" +
                        paginacao;
                        
                }

                ret = db.Database.Connection.Query<UsuarioModel>(sql).ToList();
            }

            return ret;
        }

        public static UsuarioModel RecuperarPeloId(int id)
        {
            UsuarioModel ret = null;

            using (var db = new ContextoBD())
            {
                ret = db.Usuarios.Find(id);
            }

            return ret;
        }

        public static UsuarioModel RecuperarPeloLogin(string login)
        {
            UsuarioModel ret = null;

            using (var db = new ContextoBD())
            {
                ret = db.Usuarios
                    .Where(x => x.Login == login)
                    .SingleOrDefault();
            }

            return ret;
        }

        public static bool ExcluirPeloId(int id)
        {
            var ret = false;
            try
            {
                if (RecuperarPeloId(id) != null)
                {
                    using (var db = new ContextoBD())
                    {
                        var usuario = new UsuarioModel { Id = id };
                        db.Usuarios.Attach(usuario);
                        db.Entry(usuario).State = EntityState.Deleted;
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

        public int Salvar()
        {
            var ret = 0;

            var model = RecuperarPeloId(this.Id);

            using (var db = new ContextoBD())
            {
                if (model == null)
                {
                    if (!string.IsNullOrEmpty(this.Senha))
                    {
                        this.Senha = CriptoHelper.HashMD5(this.Senha);
                    }
                    db.Usuarios.Add(this);
                }
                else
                {
                    db.Usuarios.Attach(this);
                    db.Entry(this).State = EntityState.Modified;

                    if (string.IsNullOrEmpty(this.Senha))
                    {
                        db.Entry(this).Property(x => x.Senha).IsModified = false;
                    }
                    else
                    {
                        this.Senha = CriptoHelper.HashMD5(this.Senha);
                    }
                }

                db.SaveChanges();
                ret = this.Id;
            }

            return ret;
        }

        public string RecuperarStringNomePerfis()
        {
            var ret = string.Empty;
            if (this.Perfis != null && this.Perfis.Count > 0)
            {
                var perfis = this.Perfis.Select(x => x.Nome);
                ret = string.Join(";", perfis);
            }

            return ret;
        }

        public bool ValidarSenhaAtual(string senhaAtual)
        {
            var ret = false;

            using (var db = new ContextoBD())
            {
                senhaAtual = CriptoHelper.HashMD5(senhaAtual);

                ret = db.Usuarios
                  .Where(x => x.Senha == senhaAtual && x.Id == this.Id)
                  .Any();
            }

            return ret;
        }

        public bool AlterarSenha(string novaSenha)
        {
            var ret = true;

            using (var db = new ContextoBD())
            {
                //var sql = "update usuario set senha = @senha where id = @id";
                //var parametros = new { id = this.Id, senha = CriptoHelper.HashMD5(novaSenha) };
                //ret = db.Database.Connection.Query<UsuarioModel>(sql, parametros).Any();
                this.Senha = CriptoHelper.HashMD5(novaSenha);
                db.Usuarios.Attach(this);
                db.Entry(this).Property(x => x.Senha).IsModified = true;
                db.SaveChanges();

            }

            return ret;
        }

        #endregion
    }
}