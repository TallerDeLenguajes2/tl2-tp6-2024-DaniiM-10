using System.ComponentModel.DataAnnotations;

namespace TP6.Models;

public class Presupuestos
{
    [Key]
    public int IdPresupuesto { get; set; }

    [Required]
    public Clientes Cliente { get; set; } = new Clientes();

    [Required]
    public DateTime FechaCreacion { get; set; }
    
    public List<PresupuestosDetalles> Detalles { get; set; } = new List<PresupuestosDetalles>();

    public Presupuestos() {}
}
