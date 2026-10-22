using Gestoque.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gestoque.Controllers
{
    public class ContaController : Controller
    {
        [AllowAnonymous] //qualquer um pode acessar
        public ActionResult Login(String returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel login, string returnUrl)
        {
            ViewBag.Mensagem = null;

            //validar se a entrada do usuario esta correta
            if (!ModelState.IsValid)
            {//se os dados que o usuario digitou ainda esta invalido retorna para tela de login
                return View(login);
            }
            var usuario = UsuarioModel.ValidarUsuario(login.Usuario, login.Senha);
            if (usuario!= null)
            {   //se o usuario for encontrado vai ser autenticado
                // cria e criptografa o ticket de autenticação
                var tiket = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(
                    1, usuario.Nome, DateTime.Now, DateTime.Now.AddHours(12), login.LembrarMe, usuario.Id + "|" + usuario.RecuperarStringNomePerfis())); //seção tera 12h de duração
                //cria o cookie como o conteudo do tiket
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, tiket);
                //responde ao browser e adiciona o cookie
                Response.Cookies.Add(cookie);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                //ModelState.AddModelError("", "Login inválido.");
                ViewBag.Mensagem = true;

            }

            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        { //quando deslogado é direcionado para pg Index
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Conta");
        }

        [AllowAnonymous]
        public ActionResult AlterarSenhaUsuario(AlteracaoSenhaUsuarioViewModel model)
        {
            ViewBag.Mensagem = null;

            if (HttpContext.Request.HttpMethod.ToUpper() == "POST")
            {
                var usuarioLogado = (HttpContext.User as AplicacaoPrincipal);
                var alterou = true;
                if (usuarioLogado != null)
                {
                    if (!usuarioLogado.Dados.ValidarSenhaAtual(model.SenhaAtual))
                    {
                        ModelState.AddModelError("SenhaAtual", "A senha atual não confere.");
                    }
                    else
                    {
                        alterou = usuarioLogado.Dados.AlterarSenha(model.NovaSenha);

                        if (alterou)
                        {
                            ViewBag.Mensagem = new string[] { "ok", "Senha alterada." };
                        }
                        else
                        {
                            ViewBag.Mensagem = new string[] { "erro", "Não foi possível alterar a senha." };
                        }
                    }
                }

                return View();
            }
            else
            {
                ModelState.Clear();
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult RedefinirSenha(int id)
        {
            var usuario = UsuarioModel.RecuperarPeloId(id); //identificar o usuario
            if (usuario == null) //se usuario n existir
            {
                id = -1;
            }

            var model = new NovaSenhaViewModel() { Usuario = id }; // carregar id do usuario

            ViewBag.Mensagem = null;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RedefinirSenha(NovaSenhaViewModel model)
        {
            ViewBag.Mensagem = null; //se n houver problemas mensagem nao aparece

            if (!ModelState.IsValid) //se n estiver valido
            {
                return View(model); //retorna somente a view
            }
            //se a validacao for bem sucedida, procura o usuario pelo id
            var usuario = UsuarioModel.RecuperarPeloId(model.Usuario);
            if (usuario != null)
            {
                //achando o usuario altera a senha 
                var ok = usuario.AlterarSenha(model.Senha);
                //exibi a mensagem
                ViewBag.Mensagem = ok ? ViewBag.Mensagem = true : ViewBag.Mensagem = false;
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult EsqueciMinhaSenha(EsqueciMinhaSenhaViewModel model)
        {
            if (HttpContext.Request.HttpMethod.ToUpper() == "GET") //se for GET
            {
                ViewBag.EmailEnviado = false; //email n foi enviado
                ModelState.Clear(); //limpar para n exibir mensagens
            }
            else //quando estiver no post
            {
                if (!ModelState.IsValid) //login em branco: mostra a mensagem de validação
                {
                    return View(model);
                }

                var usuario = UsuarioModel.RecuperarPeloLogin(model.Login); //identificar o usuario
                if (usuario != null) //se n for nulo
                {
                    EnviarEmailRedefinicaoSenha(usuario);
                }

                // Mesma resposta para login existente ou não, para não revelar quais logins existem
                ViewBag.EmailEnviado = true;
            }

            return View(model);
        }

        private void EnviarEmailRedefinicaoSenha(UsuarioModel usuario)
        {  //passar o id e o protocolo de  requisicao
            var callbackUrl = Url.Action("RedefinirSenha", "Conta", new { id = usuario.Id }, protocol: Request.Url.Scheme);

            // Ambiente local: o e-mail é gravado como arquivo .eml em App_Data/emails,
            // em vez de ser enviado. Nenhuma credencial fica no código.
            var pastaEmails = Server.MapPath("~/App_Data/emails");
            System.IO.Directory.CreateDirectory(pastaEmails);
            var client = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = pastaEmails
            };

            var mensagem = new MailMessage();
            mensagem.From = new MailAddress(ConfigurationManager.AppSettings["EmailOrigem"], "Gestoque"); //nome do email
            mensagem.To.Add(usuario.Email); //para quem deve ser enviado
            mensagem.Subject = "Redefinição de senha"; //assunto do email
            mensagem.Body = string.Format("Redefina a sua senha <a href='{0}'>aqui</a>", callbackUrl); //corpo do emaiil
            mensagem.IsBodyHtml = true; //indicação que email é um html

            client.Send(mensagem); //envia 

        }
    }
}
    