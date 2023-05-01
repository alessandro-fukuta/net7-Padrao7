﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padrao.Data;
using Padrao.Models;
using RestSharp;
using Padrao.Functions;
using System.Diagnostics;
using System.Security.Policy;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Padrao.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender emailSender;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IEmailSender emailSender)
        {
            _logger = logger;
            _context = context;
            this.emailSender = emailSender;
        }
        public IActionResult Index()
        {
            string? Mensagem = Publica.Mensagem1;

            var cookie = Request.Cookies["asplogin"];
            if (cookie != null)
            {
                Mensagem = "Olá, " + cookie;
                Publica.Logado = true;
                Publica.Login_Usuario = cookie;
                //Response.Redirect("Index");
            }
            else
            {
                Publica.Logado = false;
                Publica.Login_Usuario = "";
                Mensagem = "Fazer Login";
            }

            ViewBag.Mensagem = Mensagem;

            return View();
        }

        public IActionResult _Login()
        {
            ViewBag.Mensagem = "Fazer Login";
            return View();
        }

        public IActionResult _Sair()
        {
            Response.Cookies.Delete("asplogin");
            return Redirect("Index");

        }

        [HttpGet]
        public IActionResult Logar()
        {
            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Logar([Bind("NomeUsuario")] Usuario users, string pUsuario, string pSenha)
        {
            var us = await _context.Usuarios
                  .FirstOrDefaultAsync(m => m.NomeUsuario == pUsuario && m.Senha == pSenha);

            if (us == null)
            {
                Publica.Mensagem1 = "Usuário ou Senha inválido.";
                return Redirect("Logar");
            }

            Publica.Mensagem1 = "";
            // salvar cookie de login se deu certo
            string? x = us.NomeUsuario;
            SalvarCookie("asplogin", x);

            return Redirect("index");
        }
        public IActionResult _Registrar()
        {

            return View();
        }



        // registro (salvar)
        [HttpPost]
        public async Task<IActionResult> Create([Bind("NomeCompleto,NomeUsuario,Celular, Email, Senha, ConfirmeSenha")] Usuario usuarios)
        {
            if (ModelState.IsValid)
            {

                if (_context.Usuarios.FirstOrDefault(t => t.Email == usuarios.Email) != null)
                {
                    return Redirect("RegErro");
                }

                if (_context.Usuarios.FirstOrDefault(t => t.NomeUsuario == usuarios.NomeUsuario) != null)
                {
                    return Redirect("RegErro");
                }


                /*
                var usr = from u in _context.Usuarios where u.NomeUsuario == usuarios.NomeUsuario select u;
                
                if (usr.FirstOrDefault(t => t.NomeUsuario == usuarios.NomeUsuario) != null)
                {
                    return Redirect("RegErro");

                }
                */

                // gerando numero aleatorio de autenticaçao

                Funcoes fun = new Funcoes();
                int Id_Autenticacao = fun.GeraID();

                // PROXIMA ETAPA, SALVAR NO BD ESSE CODIGO DE AUTENTICAÇAO

                usuarios.Autenticacao = Id_Autenticacao;
                usuarios.EmailValidado = false;
                usuarios.Cadastro = DateTime.Now;
                usuarios.Chave = "0";
                usuarios.Administrador = false;

                _context.Add(usuarios);
                await _context.SaveChangesAsync();

                Publica.Login_NomeCompleto = usuarios.NomeCompleto;
                Publica.Login_Usuario = usuarios.NomeUsuario;
                Publica.Login_Email = usuarios.Email;
                string Mensagem =
                      "<html>" +
                      "<head>" +
                      "</head>" +
                      "<body>" +
                         "<h4>Olá, " + Publica.Login_NomeCompleto + " </h4>" +
                         "<hr/>" +
                         "<h4>Você se registrou em nosso site, segue o link para você confirmar o seu e - mail</h4>" +
                         "<h4>ESSE EMAIL É PARTICULAR, O CÓDIGO ABAIXO É SUA SEGURANÇA</h4>" +
                         "<h4>SEU CÓDIGO DE AUTENTICAÇÃO É:</h4> <h3 style='color: blue;'> " + Id_Autenticacao.ToString() + " </h3>" +
                         "<hr/" +
                         "<h4>Acesse o link abaixo</4>" +
                         "<a href='https://localhost:7253/Home/AutenticarEmail'>Autenticar E-mail</a>" +
                         "</body>" +
                         "</html>";
                await emailSender.SendEmailAsync(usuarios.Email, "REGISTRO DE CONTA", Mensagem);
                return Redirect("RegSucesso");
            }
            return View("_Registrar");

        }
        [HttpPost]
        public async Task<IActionResult> IrAutenticar()
        {
            return Redirect("AutenticarEmail");
        }

        [HttpGet]
        public IActionResult AutenticarEmail()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AutenticarEmail([Bind("NomeCompleto,NomeUsuario,Celular, Email, Senha, ConfirmeSenha")] Usuario usuarios, string pCodigoAutenticacao, string pEmail)
        {
            var users = await _context.Usuarios.FirstOrDefaultAsync(t => t.NomeUsuario == Publica.Login_Usuario);
            Publica.Login_Email = pEmail;
            // pegar por usuario
            string Pesquisa;
            if (string.IsNullOrEmpty(pEmail))
            {
                // users = await _context.Usuarios.FirstOrDefaultAsync(t => t.NomeUsuario == Publica.Login_Usuario);
            }
            else
            {
                users = await _context.Usuarios.FirstOrDefaultAsync(t => t.Email == pEmail);
            }



            if (users != null)
            {
                if (int.Parse(pCodigoAutenticacao) == users.Autenticacao)
                {
                    users.EmailValidado = true;
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                    return Redirect("AutenticarEmailSucesso");
                }
            }

            return View();
        }

        public IActionResult AutenticarEmailSucesso()
        {
            return View();
        }

        public IActionResult AutenticarEmailErro()
        {
            return View();
        }
        public IActionResult RegSucesso()
        {
            //return PartialView("RegSucesso");
            return View();
        }
        public IActionResult RegErro()
        {
            return View();
        }
        public IActionResult RecuperarAcesso()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AlterarSenha()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AlterarSenha([Bind("NomeUsuario,Email")] Usuario usuarios, string pUsuario, string pSenhaAtual, string pNovaSenha, string pReNovaSenha)
        {
            if(pNovaSenha != pReNovaSenha) {
                Publica.Mensagem1 = "Senhas não conferem";
                return View();
            }
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> RecuperarAcesso([Bind("NomeUsuario,Email")] Usuario usuarios, string pUsuario, string pEmail)
        {
            bool Achou = false;
            string Mensagem = "";
            string? Email = "";
            string? vUsuario = "";
            string? vSenha = "";
            Funcoes fun = new Funcoes();
            int Id_Autenticacao = fun.GeraID();



            if (!string.IsNullOrEmpty(pUsuario))
            {
                var users = await _context.Usuarios.FirstOrDefaultAsync(m => m.NomeUsuario == pUsuario);
                if (users != null)
                {
                    Achou = true;
                    vUsuario = pUsuario;
                    Email = users.Email;
                    vSenha = users.Senha;
                    users.Autenticacao = Id_Autenticacao;
                    users.EmailValidado = false;

                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
            }
            else if (!string.IsNullOrEmpty(pEmail))
            {
                var users = await _context.Usuarios.FirstOrDefaultAsync(m => m.Email == pEmail);
                if (users != null)
                {
                    Achou = true;
                    Email = users.Email;
                    vUsuario = users.NomeUsuario;
                    vSenha = users.Senha;

                    users.Autenticacao = Id_Autenticacao;
                    users.EmailValidado = false;

                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }

            }

            if (Achou)
            {

                Mensagem =
                    "<html>" +
                    "<head>" +
                    "</head>" +
                    "<body>" +
                       "<h4>Você solicitou a recuperação de seu acesso.</h4>" +
                       "<h4>Abaixo segue os dados de acesso ao sistema:</h4>" +
                       "<h4 style='color:blue;'>Usuário:" + vUsuario + "</h4>" +
                       "<h4 style='color:blue;'>Email..:" + Email + "</h4>" +
                       "<h4 style='color:blue;'>Senha..:" + vSenha + "</h4>" +
                       "<h4>ESSE EMAIL É PARTICULAR, O CÓDIGO ABAIXO É SUA SEGURANÇA</h4>" +
                       "<h4>SEU CÓDIGO DE AUTENTICAÇÃO É:</h4> <h3 style='color: blue;'> " + Id_Autenticacao.ToString() + " </h3>" +
                       "<hr/" +
                       "<h4>Acesse o link abaixo</4>" +
                       "<a href='https://localhost:7253/Home/AutenticarEmail'>Autenticar E-mail</a>" +
                    "</body>" +
                    "</html>";
                await emailSender.SendEmailAsync(Email, "Recuperação de Acesso", Mensagem);
                return Redirect("RegRecuperarSucesso");
            }

            return Redirect("RegRecuperarErro");

        }

        [HttpPost]
        public async Task<IActionResult> RegErro(string? pRetornar)
        {
            return Redirect("_Registrar");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult RegRecuperarErro()
        {
            return View();
        }
        public IActionResult RegRecuperarSucesso()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private void SalvarCookie(string NomeCookie, string pValor)
        {
            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(10),

            };

            Response.Cookies.Append(NomeCookie, pValor, cookie);
        }


        // enviando email
        [HttpPost]
        public async Task<IActionResult> EnviarEmail(string email, string subject, string message)
        {
            await emailSender.SendEmailAsync(email, subject, message);
            return View();
        }

    }
}