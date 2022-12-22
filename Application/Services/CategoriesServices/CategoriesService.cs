using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Application.Services.CategoriesServices
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ProvaContext _context;

        public CategoriesService(ProvaContext context)
        {
            _context = context;
        }       
        public (bool,string) Add(Categorias categorias)
        {
            var exist = _context.Categorias.Any(x => x.Nome == categorias.Nome);
            if (exist)
            {
                return (false,"Já existe uma categoria com o nome informado.");
            }

            foreach (var item in categorias.Produtos)
            {
                item.CategoriaId = categorias.Id;
                _context.Produtos.Add(item);
                _context.SaveChanges();
            }

            _context.Categorias.Add(categorias);
            _context.SaveChanges();

            return (true, null);
        }

        public List<Categorias> GetAll()
        {
           return  _context.Categorias
                .Include(x => x.Produtos)
                .ToList();
        }
    }
}
