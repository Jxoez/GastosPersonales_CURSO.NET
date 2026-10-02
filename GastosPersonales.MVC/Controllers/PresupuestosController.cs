
using Microsoft.AspNetCore.Mvc;
using GastosPersonales.Modelos;
using GastosPersonales.Consumer;

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

    // GET: PRESUPUESTOS/Create
    public ActionResult Create()
    {
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
