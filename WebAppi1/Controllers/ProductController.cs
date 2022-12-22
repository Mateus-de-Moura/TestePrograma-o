using Application.Context;
using Application.Entities;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _product;
        private readonly Validate _validate;

        public ProductController(IProductsService product)
        {
            _product = product;
            _validate = new Validate();
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<Produtos> Get(string term, int page, int pageSize)
        {
            return (IEnumerable<Produtos>)_product.GetAll(term, page, pageSize);
        }

        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Add(Produtos product)
        {
            var result = _validate.ValidateProduct(product);
            if (result.Item1)
            {
                if (!_product.Add(product).Item1)
                {
                    return BadRequest("Já existe um produto com este nome");
                }
                else
                {
                    return Ok();
                }
            }

            return BadRequest(result.Item2);
        }

        [HttpDelete("Delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(long id)
        {
            var result = _product.Delete(id);
            return Ok(result);
        }

        [HttpPut("Update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(Produtos product)
        {
            var result = _validate.ValidateProduct(product);
            if (result.Item1)
            {
                var resullt = _product.Update(product);
                if (resullt != null)
                {
                    return Ok();

                }
                else 
                {
                    return NotFound("Product Not Found");
                }
            }
            else
            {
                return BadRequest(result.Item2);
            }

        }
    }
}
