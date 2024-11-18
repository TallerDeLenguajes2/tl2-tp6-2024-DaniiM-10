using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP6.Models;

namespace TP6.Controllers;

public class PresupuestosController : Controller
{
    private readonly ILogger<PresupuestosController> _logger;
    private PresupuestosRepository presupuestosRepository;
    private ProductosRepository productosRepository;

    public PresupuestosController(ILogger<PresupuestosController> logger)
    {
        _logger = logger;
        presupuestosRepository = new PresupuestosRepository();
        productosRepository = new ProductosRepository();
    }
    public IActionResult Index()
    {
        return View(presupuestosRepository.GetPresupuestos());
    }

    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        return View(new Presupuestos());
    }
    [HttpPost]
    public IActionResult CrearPresupuesto(Presupuestos presupuesto)
    {
        if (!ModelState.IsValid) { return View(presupuesto); }

        var success = presupuestosRepository.PostPresupuesto(presupuesto);

        if (success)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Hubo un problema al crear el presupuesto. Por favor, intente nuevamente.");
            return View(presupuesto);
        }
    }

    [HttpGet]
    public IActionResult EditarPresupuesto(int idPresupuesto)
    {
        Presupuestos presupuesto = presupuestosRepository.GetPresupuesto(idPresupuesto);
        if (presupuesto == null) { return NotFound(); }
        return View(presupuesto);
    }
    [HttpPost]
    public IActionResult EditarPresupuesto(Presupuestos presupuesto)
    {
        if (!ModelState.IsValid) { return View(presupuesto); }

        var success = presupuestosRepository.PutPresupuesto(presupuesto);
        if (success)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Hubo un problema al actualizar el presupuesto. Por favor, intente nuevamente.");
            return View(presupuesto);
        }
    }

    public IActionResult EliminarPresupuesto(int idPresupuesto)
    {
        var success = presupuestosRepository.DeletePresupuesto(idPresupuesto);

        if (!success) { ModelState.AddModelError("", "Hubo un problema al eliminar el presupuesto. Por favor, intente nuevamente."); }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarPresupuestoDetalle(int idPresupuesto, int idProducto)
    {
        Presupuestos presupuestos = new Presupuestos();
        presupuestos.idPresupuesto = idPresupuesto;
        PresupuestosDetalles presupuestosDetalles = presupuestosRepository.GetPresupuestoDetalle(idPresupuesto, idProducto);

        if (presupuestosDetalles == null) { return NotFound(); }

        presupuestos.Detalles.Add(presupuestosDetalles);
        return View(presupuestos);
    }
    [HttpPost]
    public IActionResult EditarPresupuestoDetalle(Presupuestos presupuesto)
    {
        if (!ModelState.IsValid) { return View(presupuesto); }

        var detalle = presupuesto.Detalles.FirstOrDefault();
        if (detalle == null)
        {
            ModelState.AddModelError("", "Detalle de presupuesto no encontrado.");
            return View(presupuesto);
        }

        var success = presupuestosRepository.PutPresupuestoDetalle(presupuesto.idPresupuesto, detalle.producto.idProducto, detalle.Cantidad > 0 ? detalle.Cantidad : 1);

        if (success)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Hubo un problema al actualizar el detalle del presupuesto. Por favor, intente nuevamente.");
            return View(presupuesto);
        }
    }

    public IActionResult EliminarPresupuestoDetalle(int idPresupuesto, int idProducto)
    {
        var success = presupuestosRepository.DeletePresupuestoDetalle(idPresupuesto, idProducto);

        if (!success) { ModelState.AddModelError("", "Hubo un problema al eliminar el detalle del presupuesto. Por favor, intente nuevamente."); }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AgregarProducto()
    {
        var productos = productosRepository.GetProductos();
        return View(productos);
    }
    [HttpPost]
    public IActionResult AgregarProducto(int idPresupuesto, int idProducto, int Cantidad)
    {
        if (Cantidad <= 0) { Cantidad = 1; }

        var producto = productosRepository.GetProducto(idProducto);
        if (producto == null)
        {
            ModelState.AddModelError("", "Producto no encontrado.");
            return RedirectToAction("Index");
        }

        PresupuestosDetalles presupuestoDetalle = new PresupuestosDetalles
        {
            Cantidad = Cantidad,
            producto = producto
        };

        var success = presupuestosRepository.PostPresupuestoDetalle(idPresupuesto, presupuestoDetalle);

        if (!success) { ModelState.AddModelError("", "Hubo un problema al agregar el producto. Por favor, intente nuevamente."); }
        return RedirectToAction("Index");
    }
}
