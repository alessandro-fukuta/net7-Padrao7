using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oficina7.Data;
using Oficina7.Models;
using X.PagedList;

namespace Oficina7.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TelSortParm = sortOrder == "Telefone" ? "telefone_desc" : "Telefone";
            if(searchString != null)
            {
                page = 1;
            } else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var clientes = from s in _context.Clientes select s;
            if (!String.IsNullOrEmpty(searchString))
            {
       
                clientes = clientes.Where(s => s.nome.Contains(searchString));

            }

            switch (sortOrder)
            {
                case "name_desc":
                    clientes = clientes.OrderBy(s => s.nome);
                    break;
                case "telefone_desc":
                    clientes = clientes.OrderBy(s => s.telefone);
                    break;
                default:
                    clientes = clientes.OrderBy(s => s.nome);
                    break;
            }

            int pageSize = 16;
            int pageNumber = (page ?? 1);
             

            /*
             * retorna a lista de clientes para a pagina view
            return View(await clientes.ToListAsync()); 
            */

            return View(clientes.ToPagedList(pageNumber, pageSize));

        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,nome,Endereco,bairro,cidade,estado,cep,telefone,celular,email,cpf,rg,nascimento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.ClienteId = 0;
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,nome,Endereco,bairro,cidade,estado,cep,telefone,celular,email,cpf,rg,nascimento")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}
