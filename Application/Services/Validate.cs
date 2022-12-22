using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class Validate
    {
        public (bool, string) ValidateProduct(Produtos product)
        {
            if (string.IsNullOrEmpty(product.Nome))
            {
                return (false ,"Nome do produto não informado");
            }

            if (product.PrecoUnitario <= 0)
            {
                return (false ,"Preço unitário deve ser maior que 0");
            }

            if (product.QuantidadeEstoque <= 0)
            {
                return (false, "Quantidade deve ser maior que 0");
            }

            return (true, "");
        }
    }
}
