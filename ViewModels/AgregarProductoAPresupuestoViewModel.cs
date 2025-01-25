using System.ComponentModel.DataAnnotations;

namespace TP7.ViewModels;

public class AgregarProductoAPresupuestoViewModel
{
    public int IdPresupuesto { get; set; }
    public int IdProducto { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Subtotal { get; set; }
}