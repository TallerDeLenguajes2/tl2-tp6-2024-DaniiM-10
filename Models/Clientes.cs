using System.ComponentModel.DataAnnotations;

namespace TP6.Models;

public class Clientes {
    public int ClienteId { get; set; }
    public string Nombre { get; set; }

    public string Email { get; set; }
    
    public string Telefono { get; set; }

    public Clientes() {}
}