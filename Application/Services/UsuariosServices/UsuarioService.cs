using Application.Context;
using Application.Entities;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UsuariosService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ProvaContext _context;

        public UsuarioService(ProvaContext context)
        {
            _context = context;
        }

        public void CreateUser(Usuarios user)
        {           
            _context.Usuarios.Add(user);
            _context.SaveChanges();
        }

        public Usuarios DoLogin(Usuarios user)
        {
            var usuario =  _context.Usuarios.FirstOrDefault(x => x.Email == user.Email && x.Senha == user.Senha);

            if (usuario != null)
            {
                return usuario;
            }
            return new Usuarios { };
        }       
    }
}
