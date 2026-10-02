
using Microsoft.AspNetCore.Mvc;
using GastosPersonales.Modelos;
using GastosPersonales.Consumer;

public class CategoriasController : Controller
{
   

    // GET: CATEGORIAS
    public ActionResult Index()    
    {
        var categorias = CRUD<Categoria>.GetAll();
        return View(categorias);
    }

    // GET: CATEGORIAS/Details/5
    public ActionResult Details(int idcategoria)
    {
        var categoria = CRUD<Categoria>.GetById(idcategoria);
        if (categoria == null)
        {
            return NotFound();
        }
        return View(categoria);
    }


    // GET: CATEGORIAS/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: CATEGORIAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Categoria categoria)
    {
        try
        {
            CRUD<Categoria>.Create(categoria);
            return RedirectToAction(nameof(Index));

        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(categoria);
        }
    }

    // GET: CATEGORIAS/Edit/5
    public ActionResult Edit(int idcategoria)
    {
        var categoria = CRUD<Categoria>.GetById(idcategoria);
        if (categoria == null)
        {
            return NotFound();
        }
        return View(categoria);
    }

    // POST: CATEGORIAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int idcategoria, Categoria categoria)
    {
        try
        {
            CRUD<Categoria>.Update(idcategoria, categoria);
            return RedirectToAction(nameof(Index));

        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(categoria);
        }
    }

    // GET: CATEGORIAS/Delete/5
    public ActionResult Delete(int idcategoria)
    {
        var categoria = CRUD<Categoria>.GetById(idcategoria);
        if (categoria == null)
        {
            return NotFound();
        }
        return View(categoria);
    }

    // POST: CATEGORIAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int idcategoria, Categoria categoria)
    {
        try
        {
            CRUD<Categoria>.Delete(idcategoria);
            return RedirectToAction(nameof(Index));

        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(categoria);
        }
    }

}
