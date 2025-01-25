
using System.ComponentModel.DataAnnotations;

namespace TP7.ViewModels;

public class CrearPresupuestoViewModel
{
    [Required(ErrorMessage = "El campo nombre es requerido")]
    public string NombreDestinatario { get; set; }
    public DateTime FechaCreacion { get; set; }
}