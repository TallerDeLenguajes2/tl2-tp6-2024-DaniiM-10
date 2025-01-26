using System.ComponentModel.DataAnnotations; 
namespace TP7.ViewModels;

public class ModificarPresupuestoViewModel
{
    public int IdPresupuesto { get; set; }

    [Required(ErrorMessage = "El id del cliente es obligatorio.")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime FechaCreacion { get; set; }
    public ModificarPresupuestoViewModel()
    {
    }
}