using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP6.Models;

namespace TP6.Controllers;

public class PresupuestosController : Controller
{
    private readonly ILogger<PresupuestosController> _logger;
    private PresupuestosRepository presupuestosRepository;

    public PresupuestosController(ILogger<PresupuestosController> logger)
    {
        _logger = logger;
        presupuestosRepository = new PresupuestosRepository();
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
        return RedirectToAction("Index");
    }

    /*[HttpGet]
    public IActionResult EditarPresupuesto(int idPresupuesto)
    {
        Presupuestos presupuesto = presupuestosRepository.GetPresupuesto(idPresupuesto);
        return View(presupuesto);
    }
    [HttpPost]
    public IActionResult EditarPresupuesto(Productos producto)
    {
        var success = presupuestosRepository.(producto.idPresupuesto, producto);
        return RedirectToAction("Index");
    }*/

    public IActionResult EliminarPresupuesto(int idPresupuesto)
    {
        var success = presupuestosRepository.DeletePresupuesto(idPresupuesto);
        return RedirectToAction("Index");
    }
}