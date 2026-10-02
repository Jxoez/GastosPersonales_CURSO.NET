using GastosPersonales.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace GastosPersonales.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: /Account/Index
        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Account/Index
        [HttpPost]
        public async Task<IActionResult> Index(
      string email,
      string password)
        {
            if (string.IsNullOrWhiteSpace(email) ||
              string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Ingrese el email y la contraseña.";
                return View();
            }

            email = email.Trim().ToLower();

            var resultado = await _authService.Login(
              email,
              password
            );

            if (!resultado)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }

            return RedirectToAction(
              "Index",
              "Home"
            );
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(
      string nombre,
      string apellido,
      string email,
      string password,
      string confirmarPassword)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
              string.IsNullOrWhiteSpace(apellido) ||
              string.IsNullOrWhiteSpace(email) ||
              string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View();
            }

            if (password != confirmarPassword)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View();
            }

            email = email.Trim().ToLower();

            var resultado = await _authService.Register(
              nombre,
              apellido,
              email,
              password
            );

            if (!resultado)
            {
                ViewBag.Error = "Ya existe un usuario con ese email.";
                return View();
            }

            TempData["Mensaje"] =
              "Registro exitoso. Ahora puede iniciar sesión.";

            return RedirectToAction("Index");
        }

        // POST: /Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");

            return RedirectToAction("Index", "Account");
        }
        }
    }
