using System.ComponentModel.DataAnnotations; 
namespace TP7.ViewModels;
public class AltaProductoViewModel
{
    [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
    public int Precio { get; set; }
    public AltaProductoViewModel()
    {
    }

}
