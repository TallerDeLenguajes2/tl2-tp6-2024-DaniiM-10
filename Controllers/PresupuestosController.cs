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
    public IActionResult PresupuestoId(int idPresupuesto)
    {
        return View(presupuestosRepository.GetPresupuesto(idPresupuesto));
    }

    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        return View(new Productos());
    }
    [HttpPost]
    public IActionResult CrearPresupuesto(Presupuestos presupuesto)
    {
        return View(presupuestosRepository.PostPresupuesto(presupuesto));
    }
}