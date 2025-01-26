using Microsoft.AspNetCore.Mvc;
using TP6.Models;
using TP7.ViewModels;
namespace TP6.Controllers;

public class LoginController : Controller
{
    private readonly IUserRepository _userRepository;

    private readonly ILogger<LoginController> _logger;


    public LoginController(IUserRepository userRepository, ILogger<LoginController> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public IActionResult Index()
    {
        try
        {
            var model = new LoginViewModel
            {
                IsAuthenticated = HttpContext.Session.GetString("IsAuthenticated") == "true"
            };
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "Problema cargar la vista del login.";
            return View("Index");
        }
    }
    
    public IActionResult Login(LoginViewModel model)
    {
        try
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                model.ErrorMessage = "El usuario y la clave son requeridos.";
                return View("Index", model);
            }

            User usuario = _userRepository.GetUser(model.Username, model.Password);
            if(usuario != null)
            {
                HttpContext.Session.SetString("IsAuthenticated", "true");
                HttpContext.Session.SetString("User", usuario.Username);
                HttpContext.Session.SetString("AccessLevel", usuario.AccessLevel.ToString());
                _logger.LogInformation("El usuario: "+ usuario.Username+" se logueo correctamente.");
                return RedirectToAction("Index", "Productos");
            }

            _logger.LogWarning("Credenciales invalidas | Usuario: "+ usuario.Username + "Clave: "+ usuario.Password);
            model.ErrorMessage = "Credenciales Inválidas.";
            model.IsAuthenticated = false;
            return View("Index", model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo autenticar el usuario, intente nuevamente.";
            return View("Index", model);
        }

    }

    public IActionResult Logout()
    {
        try
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No pudimos cerrar la sesion.";
            return View("Index");
        }
    }
    [HttpGet]

    public IActionResult CrearUsuario()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo cargar la vista para la creacion de usuario.";
            return View("Index");
        }
    }

    [HttpPost]

    public IActionResult AltaUsuario(CrearUsuarioViewModel usuarioVM)
    {
        try
        {            
            if(!ModelState.IsValid) return RedirectToAction ("CrearUsuario");
            User usuario = new User(usuarioVM);
            _userRepository.AltaUsuario(usuario);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.ErrorMessage = "No se pudo crear el usuario.";
            return View("Index");
        }
    }
}
