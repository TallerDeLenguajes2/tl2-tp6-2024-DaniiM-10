using System.ComponentModel.DataAnnotations;
using TP6.Models; 
namespace TP7.ViewModels;

public class ModificarProductoViewModel
{
    public int IdProducto { get; set; }
    
    [StringLength(250, ErrorMessage = "La descripción no puede exceder los 250 caracteres.")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
    public int Precio { get; set; }
    public ModificarProductoViewModel()
    {
    }

    public ModificarProductoViewModel(Producto producto)
    {
        this.IdProducto = producto.IdProducto;
        this.Descripcion = producto.Descripcion;
        this.Precio = producto.Precio;     
    }
}