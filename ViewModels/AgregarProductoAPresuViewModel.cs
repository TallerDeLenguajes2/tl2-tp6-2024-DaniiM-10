using System.ComponentModel.DataAnnotations;
namespace TP7.ViewModels;


public class AgregarProductoAPresuViewModel
{
    public int IdPresupuesto { get; set; }

    [Required(ErrorMessage = "El Id del producto es obligario")]
    [Range(1, int.MaxValue, ErrorMessage = "El número debe ser un entero positivo.")]
    public int IdProducto { get; set; }

    [Required(ErrorMessage = "La cantidad del producto es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "El número debe ser un entero positivo.")]
    public int Cantidad { get; set; }
    public AgregarProductoAPresuViewModel()
    {
    }
}