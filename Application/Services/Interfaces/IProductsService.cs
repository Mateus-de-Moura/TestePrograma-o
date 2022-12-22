using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProductsService
    {
        public IEnumerable<Produtos> GetAll(string term, int page, int pageSize);
        public  (bool,string) Add(Produtos produtos);
        public Produtos Update(Produtos product);
        public string Delete(long id);

    }
}
