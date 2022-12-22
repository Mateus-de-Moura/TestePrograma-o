using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface ICategoriesService
    {
        public List<Categorias> GetAll();
        public (bool,string) Add(Categorias categorias);

    }
}
