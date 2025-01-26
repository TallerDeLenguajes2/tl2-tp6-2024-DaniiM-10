using System.ComponentModel.DataAnnotations; 
using TP6.Models;
namespace TP7.ViewModels;

public class ModificarClienteViewModel
{
    public int ClienteId { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no es válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono no es válido.")]

    public string Telefono { get; set; }

    public ModificarClienteViewModel(){}

    public ModificarClienteViewModel(Cliente cliente)
    {
        this.ClienteId = cliente.ClienteId;
        this.Nombre = cliente.Nombre;
        this.Email = cliente.Email;
        this.Telefono = cliente.Telefono;

    }
}