using GastosPersonales.Consumer;
using GastosPersonales.Modelos;
using GastosPersonales.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


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

        public IActionResult Index()
        {
            var movimientos = CRUD<Movimiento>.GetAll();

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
