using AppRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRpgEtec.Services.Usuarios
{
    public class UsuarioService : Request // <-- Isso indica que a classe (UsuarioService) está herdando da classe Request
    {
        private readonly Request _request;

        private const string apiUrlBase = "http://luizsilva12.somee.com/RpgApi/Usuarios";

        public UsuarioService()
        {
            _request = new Request();
        }

        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Registrar";
            u.Id = await _request.PostReturnIntAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Autenticar";
            u = await _request.PostAsync(apiUrlBase + urlComplementar, u, string.Empty);
            return u;
        }
    }
}
