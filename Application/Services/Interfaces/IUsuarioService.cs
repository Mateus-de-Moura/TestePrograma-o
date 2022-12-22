using Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Usuarios DoLogin(Usuarios user);
        public void CreateUser(Usuarios user);
    }
}
