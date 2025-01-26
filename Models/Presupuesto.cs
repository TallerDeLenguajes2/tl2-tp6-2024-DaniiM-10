using System.Text.Json.Serialization;
using TP7.ViewModels;
namespace TP6.Models;

public class Presupuesto
{
    public int IdPresupuesto { get; set; }
    public Cliente Cliente { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<PresupuestoDetalle> Detalle { get; set; }

    [JsonConstructor]

    public Presupuesto()
    {}
    public Presupuesto(int idPresupuesto, Cliente cliente, DateTime fechaCreacion)
    {
        this.IdPresupuesto = idPresupuesto;
        this.Cliente = cliente;
        this.FechaCreacion = fechaCreacion;
        this.Detalle = new List<PresupuestoDetalle>();

    }

    public Presupuesto(AltaPresupuestoViewModel presuVM)
    {
        this.Cliente = new Cliente();
        this.Cliente.ClienteId = presuVM.IdCliente;
        this.FechaCreacion = presuVM.FechaCreacion;
    }

    public Presupuesto(ModificarPresupuestoViewModel presuVM)
    {
        this.IdPresupuesto = presuVM.IdPresupuesto;
        this.Cliente = new Cliente();
        this.Cliente.ClienteId = presuVM.IdCliente;
        this.FechaCreacion = presuVM.FechaCreacion;
    }



    public double MontoPresupuesto()
    {
        int monto = this.Detalle.Sum(d => d.Cantidad*d.Producto.Precio);
        return monto;

    }
    public double MontoPresupuestoConIva()
    {
        return MontoPresupuesto()*1.21;
    }
    public int CantidadProductos ()
    {
        return this.Detalle.Sum(d => d.Cantidad);
    }


}
