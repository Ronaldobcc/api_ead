using cadastro.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace cadastro.Helper
{
    

        public interface ISessao
        {
            void CriarSessaoDoUsuario(UsuarioModel usuario);
            void RemoverSessaoUsuario();
            UsuarioModel BuscarSessaoDoUsuario();
        }
    }
