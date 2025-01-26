using TP7.ViewModels;
namespace TP6.Models;

public class PresupuestoDetalle
{
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }
    public PresupuestoDetalle(Producto producto, int cantidad)
    {
        this.Producto = producto;
        this.Cantidad = cantidad;
    }
}
