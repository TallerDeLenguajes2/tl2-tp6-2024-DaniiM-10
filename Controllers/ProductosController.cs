using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP6.Models;

namespace TP6.Controllers;

public class ProductosController : Controller
{
    private readonly ILogger<ProductosController> _logger;
    private ProductosRepository productosRepository;

    public ProductosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        productosRepository = new ProductosRepository();
    }
    public IActionResult Index()
    {
        return View(productosRepository.GetProductos());
    }

    [HttpGet]
    public IActionResult CrearProducto()
    {
        return View(new Productos());
    }
    [HttpPost]
    public IActionResult CrearProducto(Productos producto)
    {
        if (!ModelState.IsValid) { return View(producto); }

        var success = productosRepository.PostProducto(producto);
        if (success)
        { 
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Hubo un problema al crear el producto. Por favor, intente nuevamente.");
            return View(producto);
        }
    }

    [HttpGet]
    public IActionResult EditarProducto(int IdProducto)
    {
        Productos producto = productosRepository.GetProducto(IdProducto);
        if (producto == null) { return NotFound(); }
        return View(producto);
    }
    [HttpPost]
    public IActionResult EditarProducto(Productos producto)
    {
        if (!ModelState.IsValid) { return View(producto); }

        var success = productosRepository.PutProducto(producto.IdProducto, producto);
        if (success)
        {
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Hubo un problema al actualizar el producto. Por favor, intente nuevamente.");
            return View(producto);
        }
    }

    public IActionResult EliminarProducto(int IdProducto)
    {
        var success = productosRepository.DeleteProducto(IdProducto);

        if (!success) { ModelState.AddModelError("", "Hubo un problema al eliminar el producto. Por favor, intente nuevamente."); }
        return RedirectToAction("Index");
    }
}