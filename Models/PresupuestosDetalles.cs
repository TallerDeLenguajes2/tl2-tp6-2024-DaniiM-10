namespace TP6.Models;

public class PresupuestosDetalles {
    public Productos producto { get; set; } = new Productos();
    public int Cantidad { get; set; }
}