using GastosPersonales.Consumer;
using GastosPersonales.Modelos;
using GastosPersonales.Servicios.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GastosPersonales.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Login(string email, string password)
        {
            var usuarios = CRUD<Usuario>.GetAll();

            var usuario = usuarios.FirstOrDefault(u =>
                u.email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
            {
                return false;
            }

            bool passwordCorrecta =
                BCrypt.Net.BCrypt.Verify(password, usuario.password);

            if (!passwordCorrecta)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.nombre),
                new Claim(ClaimTypes.Email, usuario.email),
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.idUsuario.ToString()
                )
            };

            var identity = new ClaimsIdentity(
                claims,
                "Cookies"
            );

            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(
                "Cookies",
                principal
            );

            return true;
        }

        public async Task<bool> Register(
            string nombre,
            string apellido,
            string email,
            string password)
        {
            var usuarios = CRUD<Usuario>.GetAll();

            var existe = usuarios.Any(u =>
                u.email.Equals(
                    email,
                    StringComparison.OrdinalIgnoreCase
                ));

            if (existe)
            {
                return false;
            }

            var usuario = new Usuario
            {
                nombre = nombre,
                apellido = apellido,
                email = email,
                password = password
            };

            CRUD<Usuario>.Create(usuario);

            CRUD<Usuario>.Create(usuario);

            return true;
        }
    }
}