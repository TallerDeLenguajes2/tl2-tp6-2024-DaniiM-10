using System.ComponentModel.DataAnnotations;

namespace TP6.Models;

public class Presupuestos
{
    [Key]
    public int IdPresupuesto { get; set; }

    [Required]
    [StringLength(100)]
    public string NombreDestinatario { get; set; }

    [Required]
    public DateTime FechaCreacion { get; set; }
    
    public List<PresupuestosDetalles> Detalles { get; set; } = new List<PresupuestosDetalles>();

    public Presupuestos() {}
}
