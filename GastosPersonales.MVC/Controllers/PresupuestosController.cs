using GastosPersonales.Consumer;
using GastosPersonales.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize]
public class PresupuestosController : Controller
{

    // Metodo interno para obtener el usuario actual
    private int GetUsuarioActual()
    {
        return int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );
    }


    // GET: PRESUPUESTOS
    public IActionResult Index()
    {
        int idUsuario = GetUsuarioActual();

        var presupuestos = CRUD<Presupuesto>.GetAll();

        // Mostrar solamente los presupuestos del usuario actual
        presupuestos = presupuestos
            .Where(p => p.idUsuario == idUsuario)
            .ToList();

        return View(presupuestos);
    }


    // GET: PRESUPUESTOS/Details/5
    public ActionResult Details(int idpresupuesto)
    {
        int idUsuario = GetUsuarioActual();

        var presupuesto = CRUD<Presupuesto>.GetById(idpresupuesto);

        if (presupuesto == null)
        {
            return NotFound();
        }

        // Verificar que el presupuesto pertenezca al usuario actual
        if (presupuesto.idUsuario != idUsuario)
        {
            return Forbid();
        }

        return View(presupuesto);
    }


    // Metodo interno para obtener categorias
    private List<SelectListItem> GetCategorias()
    {
        var categorias = CRUD<Categoria>.GetAll();
        return categorias.Select(c => new SelectListItem
        {
            Value = c.idCategoria.ToString(),
            Text = c.nombre
        }).ToList();
    }


    // GET: PRESUPUESTOS/Create
    public ActionResult Create()
    {
        ViewBag.Categorias = GetCategorias();

        return View();
    }


    // POST: PRESUPUESTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Presupuesto presupuesto)
    {
        try
        {
            // Asignar automáticamente el presupuesto al usuario actual
            presupuesto.idUsuario = GetUsuarioActual();

            CRUD<Presupuesto>.Create(presupuesto);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Categorias = GetCategorias();

            ModelState.AddModelError("", ex.Message);

            return View(presupuesto);
        }
    }


    // GET: PRESUPUESTOS/Edit/5
    public ActionResult Edit(int idpresupuesto)
    {
        int idUsuario = GetUsuarioActual();

        var presupuesto = CRUD<Presupuesto>.GetById(idpresupuesto);

        if (presupuesto == null)
        {
            return NotFound();
        }

        // Verificar que el presupuesto pertenezca al usuario actual
        if (presupuesto.idUsuario != idUsuario)
        {
            return Forbid();
        }

        ViewBag.Categorias = GetCategorias();

        return View(presupuesto);
    }



    // POST: PRESUPUESTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int idpresupuesto, Presupuesto presupuesto)
    {
        try
        {
            int idUsuario = GetUsuarioActual();

            // Obtener el presupuesto existente
            var presupuestoExistente =
                CRUD<Presupuesto>.GetById(idpresupuesto);

            if (presupuestoExistente == null)
            {
                return NotFound();
            }

            // Verificar que el presupuesto pertenezca al usuario actual
            if (presupuestoExistente.idUsuario != idUsuario)
            {
                return Forbid();
            }

            // Mantener el presupuesto asignado al usuario actual
            presupuesto.idUsuario = idUsuario;

            CRUD<Presupuesto>.Update(idpresupuesto, presupuesto);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Categorias = GetCategorias();

            ModelState.AddModelError("", ex.Message);

            return View(presupuesto);
        }
    }


    // GET: PRESUPUESTOS/Delete/5
    public ActionResult Delete(int idpresupuesto)
    {
        int idUsuario = GetUsuarioActual();

        var presupuesto = CRUD<Presupuesto>.GetById(idpresupuesto);

        if (presupuesto == null)
        {
            return NotFound();
        }

        // Verificar que el presupuesto pertenezca al usuario actual
        if (presupuesto.idUsuario != idUsuario)
        {
            return Forbid();
        }

        return View(presupuesto);
    }


    // POST: PRESUPUESTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int idpresupuesto, Presupuesto presupuesto)
    {
        try
        {
            int idUsuario = GetUsuarioActual();

            // Obtener el presupuesto existente
            var presupuestoExistente =
                CRUD<Presupuesto>.GetById(idpresupuesto);

            if (presupuestoExistente == null)
            {
                return NotFound();
            }

            // Verificar que el presupuesto pertenezca al usuario actual
            if (presupuestoExistente.idUsuario != idUsuario)
            {
                return Forbid();
            }

            CRUD<Presupuesto>.Delete(idpresupuesto);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);

            return View(presupuesto);
        }
    }

}