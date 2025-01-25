using System.ComponentModel.DataAnnotations;

namespace TP7.ViewModels;

public class ModificarClienteViewModel {
    public int ClienteId { get; set; }
    
    [Required (ErrorMessage = "El campo Nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required (ErrorMessage = "El campo Email es obligatorio")]
    public string Email { get; set; }
    
    public string Telefono { get; set; }

    public ModificarClienteViewModel() {}
}