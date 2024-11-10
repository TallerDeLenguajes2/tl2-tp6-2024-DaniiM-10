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
        var success = presupuestosRepository.PostPresupuesto(presupuesto);
        return (success) ? RedirectToAction("Index") : RedirectToAction("CrearPresupuesto");
    }

    [HttpGet]
    public IActionResult EditarPresupuesto(int idPresupuesto)
    {
        Presupuestos presupuesto = presupuestosRepository.GetPresupuesto(idPresupuesto);
        return View(presupuesto);
    }
    [HttpPost]
    public IActionResult EditarPresupuesto(Presupuestos presupuesto)
    {
        var success = presupuestosRepository.PutPresupuesto(presupuesto);
        return (success) ? RedirectToAction("Index") : RedirectToAction("EditarPresupuesto");
    }

    public IActionResult EliminarPresupuesto(int idPresupuesto)
    {
        var success = presupuestosRepository.DeletePresupuesto(idPresupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarPresupuestoDetalle(int idPresupuesto, int idProducto)
    {
        Presupuestos presupuestos = new Presupuestos();
        presupuestos.idPresupuesto = idPresupuesto;
        PresupuestosDetalles presupuestosDetalles = presupuestosRepository.GetPresupuestoDetalle(idPresupuesto, idProducto);
        presupuestos.Detalles.Add(presupuestosDetalles);
        return View(presupuestos);
    }
    [HttpPost]
    public IActionResult EditarPresupuestoDetalle(Presupuestos presupuesto)
    {
        var success = presupuestosRepository.PutPresupuestoDetalle(presupuesto.idPresupuesto, presupuesto.Detalles[0].producto.idProducto, presupuesto.Detalles[0].Cantidad > 0 ? presupuesto.Detalles[0].Cantidad : 1);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarPresupuestoDetalle(int idPresupuesto, int idProducto)
    {
        var success = presupuestosRepository.DeletePresupuestoDetalle(idPresupuesto, idProducto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AgregarProducto()
    {
        return View(productosRepository.GetProductos());
    }
    [HttpPost]
    public IActionResult AgregarProducto(int idPresupuesto, int idProducto, int Cantidad)
    {
        PresupuestosDetalles presupuestoDetalle = new PresupuestosDetalles();
        presupuestoDetalle.Cantidad = Cantidad;
        presupuestoDetalle.producto = productosRepository.GetProducto(idProducto);
        var success = presupuestosRepository.PostPresupuestoDetalle(idPresupuesto, presupuestoDetalle);
        return RedirectToAction("Index");
    }
}