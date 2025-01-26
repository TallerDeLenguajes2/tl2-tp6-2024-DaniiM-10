using TP7.ViewModels;
namespace TP6.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string Descripcion { get; set; }
    public int Precio { get; set; }
    public Producto()
    {}
    public Producto(int idProducto, string descripcion, int precio)
    {
        this.IdProducto = idProducto;
        this.Descripcion = descripcion;
        this.Precio = precio;
    }
    public Producto(AltaProductoViewModel produVM)
    {
        this.Descripcion = produVM.Descripcion;
        this.Precio = produVM.Precio;

    }

    public Producto(ModificarProductoViewModel produVM)
    {
        this.IdProducto = produVM.IdProducto;
        this.Descripcion = produVM.Descripcion;
        this.Precio = produVM.Precio;

    }
}