using GastosPersonales.Consumer;
using GastosPersonales.Modelos;
using GastosPersonales.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;


namespace GastosPersonales.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        // Metodo interno para obtener el usuario actual
        private int GetUsuarioActual()
        {
            return int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
        }

        public IActionResult Index() { 
        
            int idUsuario = GetUsuarioActual();

            var movimientos = CRUD<Movimiento>.GetAll();
            movimientos = movimientos
            .Where(m => m.idUsuario == idUsuario)
            .ToList();

            double ingresos = movimientos
                .Where(m => m.tipo == "Ingreso")
                .Sum(m => m.monto);

            double gastos = movimientos
                .Where(m => m.tipo == "Gasto")
                .Sum(m => m.monto);

            double balance = ingresos - gastos;

            ViewBag.Ingresos = ingresos;
            ViewBag.Gastos = gastos;
            ViewBag.Balance = balance;
            ViewBag.CantidadMovimientos = movimientos.Count;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
