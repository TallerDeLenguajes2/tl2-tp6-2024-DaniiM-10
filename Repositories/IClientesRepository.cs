using TP6.Models;
using TP7.ViewModels;


public interface IClientesRepository{
    void CrearCliente(Cliente cliente);

    List<Cliente> ObtenerClientes();

    void ModificarCliente(Cliente cliente);

    Cliente ObtenerCliente(int id);
    void EliminarCliente(int id);
}