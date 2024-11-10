namespace TP6.Models;

public class Presupuestos {
    private List<PresupuestosDetalles> DetallesPrivate;

    public Presupuestos() {
        this.DetallesPrivate = new List<PresupuestosDetalles>();
    }

    public int idPresupuesto { get; set; }
    public string? NombreDestinatario { get; set; }
    public List<PresupuestosDetalles> Detalles { get => DetallesPrivate; }
    public DateTime FechaCreacion { get; set; }

    public void setDetallesPresupuesto(List<PresupuestosDetalles> pdList) {
        this.DetallesPrivate = pdList;
    }
}