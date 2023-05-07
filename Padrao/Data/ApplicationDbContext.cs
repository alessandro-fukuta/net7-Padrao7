using Microsoft.EntityFrameworkCore;
using Oficina7.Models;

namespace Oficina7.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // propriedades definindo as tabelas de dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Usuario_Dado> Usuario_Dados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }


    }
}
