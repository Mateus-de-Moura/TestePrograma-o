using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Application.Services.TokenService;
using Application.Services.UsuariosService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAppi1.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ProvaContext _context;
        private readonly IUsuarioService _IUserService;
        public AuthenticationController(ProvaContext context, IUsuarioService UserService)
        {
            _context = context;
            _IUserService = UserService;
        }

        [HttpPost("DoLogin")]
        [AllowAnonymous]
        public IActionResult CrateUser(Usuarios user)
        {
            var userTeste = new Usuarios { Id = 1, Email = "prova@doubleit.com.br", Senha = "Prova@DoubleIt21", Nome = "Candidato" };
            _IUserService.CreateUser(userTeste);

            Usuarios userAuth = null;

            if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Senha))
            {
                 userAuth = _IUserService.DoLogin(user);
            }
            else 
            {
                return BadRequest("Os campos de E-mail e Senha são obrigatórios.");
            }

            if (userAuth != null)
            {
                var token = JwtService.GenerateToken(userAuth);
                return Ok(new { token = token });
            }

            return BadRequest();
        }
    }
}
