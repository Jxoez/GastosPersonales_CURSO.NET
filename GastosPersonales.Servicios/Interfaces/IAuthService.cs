using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastosPersonales.Servicios.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Login(string email, string password);

        Task<bool> Register(
            string nombre, 
            string apellido, 
            string email, 
            string password);
    }
}
