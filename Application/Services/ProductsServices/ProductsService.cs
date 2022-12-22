using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductsServices
{
    public class ProductsService : IProductsService
    {
        private readonly ProvaContext _context;

        public ProductsService(ProvaContext context)
        {
            _context = context;
        }

        public (bool, string) Add(Produtos produtos)
        {
            var result = _context.Produtos.Any(x => x.Nome == produtos.Nome);

            if (result)
            {
                return (false, "Já existe um produto com este nome");
            }
            _context.Produtos.Add(produtos);
            _context.SaveChanges();
            return (true, "");
        }

        public string Delete(long id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return "Product deleted";
            }

            return "Product not found";
        }

        public IEnumerable<Produtos> GetAll(string term, int page, int pageSize)
        {
            IEnumerable<Produtos> products = _context.Produtos
              .Where(p => p.Nome.Contains(term) || p.Categoria.Nome.Contains(term))
              .Skip((page - 1) * pageSize)
              .Take(pageSize);

            return products;
        }     

        public Produtos Update(Produtos product)
        {
            var result = _context.Produtos.Where(x => x.Id == product.Id).SingleOrDefault();

            if (result != null)
            {
                result.CategoriaId = product.CategoriaId;
                result.Nome = product.Nome;
                result.PrecoUnitario = product.PrecoUnitario;
                result.QuantidadeEstoque = product.QuantidadeEstoque;
                result.Status = product.Status;

                _context.Update(result);
                _context.SaveChanges();

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
