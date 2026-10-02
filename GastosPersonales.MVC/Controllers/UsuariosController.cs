using Microsoft.AspNetCore.Mvc;
using GastosPersonales.Modelos;
using GastosPersonales.Consumer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize]
public class UsuariosController : Controller
{
    // Metodo interno para obtener el usuario actual
    private int GetUsuarioActual()
    {
        return int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );
    }

    // GET: USUARIOS
    public ActionResult Index()
    {
        int idUsuario = GetUsuarioActual();

        var usuario = CRUD<Usuario>.GetById(idUsuario);

        if (usuario == null)
        {
            return NotFound();
        }

        // Mostrar solamente el usuario actual
        var usuarios = new List<Usuario>
        {
            usuario
        };

        return View(usuarios);
    }

    // GET: USUARIOS/Details/5
    public ActionResult Details(int idusuario)
    {
        int idUsuario = GetUsuarioActual();

        var usuario = CRUD<Usuario>.GetById(idusuario);

        if (usuario == null)
        {
            return NotFound();
        }

        // Verificar que sea el usuario actual
        if (usuario.idUsuario != idUsuario)
        {
            return Forbid();
        }

        return View(usuario);
    }



    // GET: USUARIOS/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: USUARIOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Usuario usuario)
    {
        try
        {
            CRUD<Usuario>.Create(usuario);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(usuario);
        }
    }

    // GET: USUARIOS/Edit/5
    public ActionResult Edit(int idusuario)
    {
        int idUsuario = GetUsuarioActual();

        var usuario = CRUD<Usuario>.GetById(idusuario);

        if (usuario == null)
        {
            return NotFound();
        }

        // Verificar que sea el usuario actual
        if (usuario.idUsuario != idUsuario)
        {
            return Forbid();
        }

        return View(usuario);
    }

    // POST: USUARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int idusuario, Usuario usuario)
    {
        try
        {
            int idUsuario = GetUsuarioActual();

            // Verificar que el usuario exista
            var usuarioExistente = CRUD<Usuario>.GetById(idusuario);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            // Verificar que sea el usuario actual
            if (usuarioExistente.idUsuario != idUsuario)
            {
                return Forbid();
            }

            // Mantener el mismo usuario
            usuario.idUsuario = idUsuario;

            CRUD<Usuario>.Update(idusuario, usuario);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(usuario);
        }
    }

    // GET: USUARIOS/Delete/5
    public ActionResult Delete(int idusuario)
    {
        int idUsuario = GetUsuarioActual();

        var usuario = CRUD<Usuario>.GetById(idusuario);

        if (usuario == null)
        {
            return NotFound();
        }

        // Verificar que sea el usuario actual
        if (usuario.idUsuario != idUsuario)
        {
            return Forbid();
        }

        return View(usuario);
    }

    // POST: USUARIOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int idusuario, Usuario usuario)
    {
        try
        {
            int idUsuario = GetUsuarioActual();

            // Obtener el usuario existente
            var usuarioExistente = CRUD<Usuario>.GetById(idusuario);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            // Verificar que sea el usuario actual
            if (usuarioExistente.idUsuario != idUsuario)
            {
                return Forbid();
            }

            CRUD<Usuario>.Delete(idusuario);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(usuario);
        }
    }

}
