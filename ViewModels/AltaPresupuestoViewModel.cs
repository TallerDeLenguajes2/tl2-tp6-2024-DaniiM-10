using System.ComponentModel.DataAnnotations; 
namespace TP7.ViewModels;
public class AltaPresupuestoViewModel
{

    [Required(ErrorMessage = "El cliente es obligatorio.")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime FechaCreacion { get; set; }
    public AltaPresupuestoViewModel()
    {
    }
}