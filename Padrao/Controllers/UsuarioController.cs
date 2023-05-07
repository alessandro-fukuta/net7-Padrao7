using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oficina7.Data;
using Oficina7.Models;

namespace Oficina7.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) 
        {
            var users = _context.Usuarios.Find(id);
            if (users == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.txtEmail = users.Email;

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,NomeCompleto,NomeUsuario,Email,Celular,CPF,RG,Endereco_Rua,Endereco_Bairro,Endereco_Cidade,Endereco_Estado,Endereco_Cep,Endereco_Numero,Senha,ConfirmeSenha,Nascimento")] Usuario user)
        {
               
                string x = user.Celular;

                _context.Update(user);
                await _context.SaveChangesAsync();

            return Redirect("Edit");
        }
    }
}
