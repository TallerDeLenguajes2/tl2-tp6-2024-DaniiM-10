using System.ComponentModel.DataAnnotations;

namespace TP6.Models;

public class Productos
{
    [Key]
    public int IdProducto { get; set; }

    [StringLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
    public string? Descripcion { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
    public int Precio { get; set; }

    public Productos() {}
}