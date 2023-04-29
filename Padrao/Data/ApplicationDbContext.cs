using Microsoft.EntityFrameworkCore;
using Padrao.Models;

namespace Padrao.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // propriedades definindo as tabelas de dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Usuario_Dado> Usuario_Dados { get; set; }

    }
}
