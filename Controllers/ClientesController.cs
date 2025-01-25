using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP6.Models;
using TP7.ViewModels;

namespace TP6.Controllers;

public class ClientesController : Controller
{
    private readonly ILogger<ClientesController> _logger;
    private ClientesRepository clientesRepository;

    public ClientesController(ILogger<ClientesController> logger)
    {
        _logger = logger;
        clientesRepository = new ClientesRepository();
    }

    public IActionResult Index()
    {
        return View(clientesRepository.GetClientes());
    }

    [HttpGet]
    public IActionResult AltaCliente()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AltaCliente(AltaClienteViewModel altaClienteViewModel)
    {
        if(!ModelState.IsValid) return RedirectToAction ("Index");
        var cliente = new Clientes()
        {
            Nombre = altaClienteViewModel.Nombre,
            Email = altaClienteViewModel.Email,
            Telefono = altaClienteViewModel.Telefono
        };
        clientesRepository.PostCliente(cliente);
        return RedirectToAction ("Index");
    }

    [HttpGet]
    public IActionResult ModificarCliente(int id)
    {
        var Cliente  = clientesRepository.GetCliente(id);
        var clienteVM = new ModificarClienteViewModel()
        {
            ClienteId = Cliente.ClienteId,
            Nombre = Cliente.Nombre,
            Email = Cliente.Email,
            Telefono = Cliente.Telefono
        };
        return View(clienteVM);
    }

    [HttpPost]
    public IActionResult ModificarCliente(ModificarClienteViewModel clienteVM)
    {
        if(!ModelState.IsValid) return RedirectToAction ("Index");
        var cliente = new Clientes(){
            ClienteId = clienteVM.ClienteId,
            Nombre = clienteVM.Nombre,
            Email = clienteVM.Email,
            Telefono = clienteVM.Telefono
        };
        clientesRepository.PutCliente(cliente);
        return RedirectToAction ("Index"); 
    }

    public IActionResult EliminarCliente(int idCliente)
    {
        var success = clientesRepository.DeleteCliente(idCliente);

        if (!success) { ModelState.AddModelError("", "Hubo un problema al eliminar el cliente. Por favor, intente nuevamente."); }
        return RedirectToAction("Index");
    }
}
