using Application.Entities;
using Application.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using WebApi.Controllers;
using Xunit;


namespace ProductServiceTests
{
    public class UnitTest1
    {
        private readonly IProductsService product;
        private readonly ProductController Controller;
        public UnitTest1()
        {
            product = Substitute.For<IProductsService>();
            Controller = new ProductController(product);
        }
        [Fact]
        public void Teste_Add_Product_unnamed()
        {
            var product = new Produtos { Id = 1, CategoriaId = 0, Nome = null, PrecoUnitario = 150, QuantidadeEstoque = 2, Status = true };

            var result = (ObjectResult)Controller.Add(product);

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);         
        }

        [Fact]
        public void Teste_get_Product()
        {
            string nome = "teste 2";
            int page = 1;
            int pageSize = 1;

            var result = Controller.Get(nome, page, pageSize);

            result.AsEnumerable();

        }
        [Fact]
        public void Teste_Put_Product()
        {
            var product = new Produtos { Id = 1, CategoriaId = 0, Nome = "testeUpdate", PrecoUnitario = 150, QuantidadeEstoque = 2, Status = true };

            var result = (ObjectResult)Controller.Update(product);

            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);

        }
        [Fact]
        public void Teste_Delete_Product()
        {
           int id = 1;  

            var result = (ObjectResult)Controller.Delete(id);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);

        }
    }
}
