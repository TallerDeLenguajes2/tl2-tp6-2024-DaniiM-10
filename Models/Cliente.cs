using TP7.ViewModels;
namespace TP6.Models;

public class Cliente
{
    public int ClienteId { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }

    public Cliente()
    {}
    public Cliente(AltaClienteViewModel clienteVM)
    {
        this.Nombre = clienteVM.Nombre;
        this.Email = clienteVM.Email;
        this.Telefono = clienteVM.Telefono;
    }

    public Cliente(ModificarClienteViewModel clienteVM)
    {
        this.ClienteId = clienteVM.ClienteId;
        this.Nombre = clienteVM.Nombre;
        this.Email = clienteVM.Email;
        this.Telefono = clienteVM.Telefono;
    }
}