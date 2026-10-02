
using GastosPersonales.Consumer;
using GastosPersonales.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize]
public class MovimientosController : Controller
{

    // GET: MOVIMIENTOS
    public ActionResult Index()    
    {
        int idUsuario = GetUsuarioActual();

        var movimientos = CRUD<Movimiento>.GetAll();
        movimientos = movimientos
        .Where(m => m.idUsuario == idUsuario)
        .ToList();

        return View(movimientos);
    }

    // GET: MOVIMIENTOS/Details/5
    public ActionResult Details(int idmovimiento)
    {
        int idUsuario = GetUsuarioActual();

        var movimiento = CRUD<Movimiento>.GetById(idmovimiento);
        if (movimiento == null)
        {
            return NotFound();
        }

        if (movimiento.idUsuario != idUsuario)
        {
            return Forbid();
        }
        return View(movimiento);
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

    // Metodo para obtener al usuario actual
    private int GetUsuarioActual()
    {
        return int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );
    }

    // GET: MOVIMIENTOS/Create
    public ActionResult Create()
    {
        ViewBag.Categorias = GetCategorias();
        return View();
    }

    // POST: MOVIMIENTOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Movimiento movimiento)
    {
        try
        {
            movimiento.idUsuario = GetUsuarioActual();

            CRUD<Movimiento>.Create(movimiento);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(movimiento);
        }
    }

    // GET: MOVIMIENTOS/Edit/5
    public ActionResult Edit(int idmovimiento)
    {
        int idUsuario = GetUsuarioActual();

        var movimiento = CRUD<Movimiento>.GetById(idmovimiento);
        
        if (movimiento == null)
        {
            return NotFound();
        }

        if (movimiento.idUsuario != idUsuario)
        {
            return Forbid();
        }

        ViewBag.Categorias = GetCategorias();
        return View(movimiento);
    }

    // POST: MOVIMIENTOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int idmovimiento, Movimiento movimiento)
    {
        try
        {
            int idUsuario = GetUsuarioActual();

            var movimientoExistente =
                CRUD<Movimiento>.GetById(idmovimiento);

            if (movimientoExistente == null)
            {
                return NotFound();
            }

            if (movimientoExistente.idUsuario != idUsuario)
            {
                return Forbid();
            }

            movimiento.idMovimiento = idmovimiento;
            movimiento.idUsuario = idUsuario;

            CRUD<Movimiento>.Update(idmovimiento, movimiento);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(movimiento);
        }
    }

    // GET: MOVIMIENTOS/Delete/5
    public ActionResult Delete(int idmovimiento)
    {
        int idUsuario = GetUsuarioActual();
        var movimiento = CRUD<Movimiento>.GetById(idmovimiento);
        if (movimiento == null)
        {
            return NotFound();
        }

        if (movimiento.idUsuario != idUsuario)
        {
            return Forbid();
        }
        return View(movimiento);
    }

    // POST: MOVIMIENTOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int idmovimiento, Movimiento movimiento)
    {
        try
        {
            int idUsuario = GetUsuarioActual();

            var movimientoExistente =
                CRUD<Movimiento>.GetById(idmovimiento);

            if (movimientoExistente == null)
            {
                return NotFound();
            }

            if (movimientoExistente.idUsuario != idUsuario)
            {
                return Forbid();
            }

            CRUD<Movimiento>.Delete(idmovimiento);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(movimiento);
        }
    }

}
