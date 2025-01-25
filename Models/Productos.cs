using System.ComponentModel.DataAnnotations;

namespace TP6.Models;

public class Productos
{
    [Key]
    public int IdProducto { get; set; }

    public string? Descripcion { get; set; }

    [Required]
    public int Precio { get; set; }

    public Productos() {}
}