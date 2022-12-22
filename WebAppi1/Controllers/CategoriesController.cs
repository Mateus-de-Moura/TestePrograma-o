using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using System.Runtime.InteropServices.WindowsRuntime;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categories;

        public CategoriesController(ICategoriesService categories)
        {
            _categories = categories;
        }

        [HttpPost("AddCategorie")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AddCategorie(Categorias categorie) 
        {
            if (string.IsNullOrEmpty(categorie.Nome)) 
            {
                return BadRequest("O Nome da Categoria é Obrigatorio");
            }
            var result = _categories.Add(categorie);
            if (result.Item1)
            {
                return Ok(categorie);
            }
            return BadRequest(result.Item2);
        }

        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAll() 
        { 
           var GetCategories = _categories.GetAll();

            if (GetCategories == null)
            {
                return BadRequest("Nenhuma categoria cadastrada");
            }
            return Ok(GetCategories);
        }
    }
}
