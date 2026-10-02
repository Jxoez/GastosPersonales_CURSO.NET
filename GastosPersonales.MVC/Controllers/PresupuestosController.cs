
using GastosPersonales.Consumer;
using GastosPersonales.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class PresupuestosController : Controller
{

    // GET: PRESUPUESTOS
    public ActionResult Index()    
    {
        var presupuestos = CRUD<Presupuesto>.GetAll();
        return View(presupuestos);
    }

    // GET: PRESUPUESTOS/Details/5
    public ActionResult Details(int idpresupuesto)
    {

        var presupuesto = CRUD<Presupuesto>.GetById(idpresupuesto);
        if (presupuesto == null)
        {
            return NotFound();
        }

        return View(presupuesto);
    }

    // Metodo interno para obtener usuarios
    private List<SelectListItem> GetUsuarios()
    {
        var usuarios = CRUD<Usuario>.GetAll();
        return usuarios.Select(u => new SelectListItem
        {
            Value = u.idUsuario.ToString(),
            Text = u.nombre + " " + u.apellido
        }).ToList();
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
        ViewBag.Usuarios = GetUsuarios();
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
            CRUD<Presupuesto>.Create(presupuesto);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(presupuesto);
        }
    }

    // GET: PRESUPUESTOS/Edit/5
    public ActionResult Edit(int idpresupuesto)
    {
        var presupuesto = CRUD<Presupuesto>.GetById(idpresupuesto);
        ViewBag.Usuarios = GetUsuarios();
        ViewBag.Categorias = GetCategorias();
        if (presupuesto == null)
        {
            return NotFound();
        }
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
            CRUD<Presupuesto>.Update(idpresupuesto, presupuesto);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(presupuesto);
        }
    }

    // GET: PRESUPUESTOS/Delete/5
    public ActionResult Delete(int idpresupuesto)
    {
        var presupuesto = CRUD<Presupuesto>.GetById(idpresupuesto);
        if (presupuesto == null)
        {
            return NotFound();
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
