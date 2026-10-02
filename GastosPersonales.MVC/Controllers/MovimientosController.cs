
using Microsoft.AspNetCore.Mvc;
using GastosPersonales.Modelos;
using GastosPersonales.Consumer;

public class MovimientosController : Controller
{

    // GET: MOVIMIENTOS
    public ActionResult Index()    
    {
        var movimientos = CRUD<Movimiento>.GetAll();
        return View(movimientos);
    }

    // GET: MOVIMIENTOS/Details/5
    public ActionResult Details(int idmovimiento)
    {
        var movimiento = CRUD<Movimiento>.GetById(idmovimiento);
        if (movimiento == null)
        {
            return NotFound();
        }
        return View(movimiento);
    }

    // GET: MOVIMIENTOS/Create
    public ActionResult Create()
    {
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
            CRUD<Movimiento>.Create(movimiento);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("",ex.Message);
            return View(movimiento);
        }
    }

    // GET: MOVIMIENTOS/Edit/5
    public ActionResult Edit(int idmovimiento)
    {
        var movimiento = CRUD<Movimiento>.GetById(idmovimiento);
        if (movimiento == null)
        {
            return NotFound();
        }
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
        var movimiento = CRUD<Movimiento>.GetById(idmovimiento);
        if (movimiento == null)
        {
            return NotFound();
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
