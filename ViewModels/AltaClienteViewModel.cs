using System.ComponentModel.DataAnnotations;

namespace TP7.ViewModels;

public class AltaClienteViewModel {
    [Required (ErrorMessage = "El campo Nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required (ErrorMessage = "El campo Email es obligatorio")]
    public string Email { get; set; }
    
    public string Telefono { get; set; }

    public AltaClienteViewModel() {}
}