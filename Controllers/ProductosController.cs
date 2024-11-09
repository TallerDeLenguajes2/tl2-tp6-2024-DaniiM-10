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
        var success = productosRepository.PostProducto(producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarProducto(int idProducto)
    {
        Productos producto = productosRepository.GetProducto(idProducto);
        return View(producto);
    }
    [HttpPost]
    public IActionResult EditarProducto(Productos producto)
    {
        var success = productosRepository.PutProducto(producto.idProducto, producto);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarProducto(int idProducto)
    {
        var success = productosRepository.DeleteProducto(idProducto);
        return RedirectToAction("Index");
    }
}