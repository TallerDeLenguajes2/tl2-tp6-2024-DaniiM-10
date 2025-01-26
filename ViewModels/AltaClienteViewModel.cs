using System.ComponentModel.DataAnnotations; 
namespace TP7.ViewModels;
public class AltaClienteViewModel
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; }
    
    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no es válido.")]
    public string Email { get; set; }
        
    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono no es válido.")]
    public string Telefono { get; set; }
    public AltaClienteViewModel()
    {
    }
}