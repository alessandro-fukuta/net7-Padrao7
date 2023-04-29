using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padrao.Data;
using Padrao.Models;
using RestSharp;
using System.Diagnostics;

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
            string Mensagem = "";

            var cookie = Request.Cookies["asplogin"];
            if (cookie != null)
            {
                Mensagem = "Olá, " + cookie;
                Publica.Logado = true;
                Publica.Login_Usuario = cookie;
                //Response.Redirect("Index");
            } else
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
            return View();
        }

        public IActionResult _Sair()
        {
            Response.Cookies.Delete("asplogin");
            return Redirect("Index");

        }

        [HttpPost]
		public async Task<IActionResult> Logar([Bind("NomeUsuario")] Usuario users, string pNomeUsuario)
		{
            /*
			var us = await _context.Usuarios
				.FirstOrDefaultAsync(m => m.NomeUsuario == pNomeUsuario);
			if (us == null)
			{
				return NotFound();
			}
            */


            // salvar cookie de login se deu certo
            string x = users.NomeUsuario;

            SalvarCookie( "asplogin" ,users.NomeUsuario);


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
                Publica.Login_NomeCompleto = usuarios.NomeCompleto;
                Publica.Login_Email = usuarios.Email;
                string Mensagem = "Você se registrou em nosso site, segue o link para você confirmar o seu e-mail";
                await emailSender.SendEmailAsync(usuarios.Email, "REGISTRO DE CONTA", Mensagem);
                return Redirect("RegSucesso");
			} 
            return View("_Registrar");
			

		}

        public IActionResult RegSucesso()
        {
            //return PartialView("RegSucesso");
           return View();
		}

		public IActionResult Privacy()
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