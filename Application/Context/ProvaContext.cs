using Application.Entities;
using Application.Services.ProductsServices;
using Application.Services.UsuariosService;
using Microsoft.EntityFrameworkCore;

namespace Application.Context
{
    public class ProvaContext : DbContext
    {
        private ProductsService product;

        public ProvaContext(DbContextOptions<ProvaContext> options)
            : base(options)
        { }      

        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }  

    }
}
