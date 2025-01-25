using System.ComponentModel.DataAnnotations;

namespace TP6.Models;

public class PresupuestosDetalles 
{
    [Required]
    public Productos producto { get; set; } = new Productos();

    [Required]
    public int Cantidad { get; set; }

    public PresupuestosDetalles(){}
}