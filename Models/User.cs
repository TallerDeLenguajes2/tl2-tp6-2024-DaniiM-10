using System.Security.Principal;
using TP7.ViewModels;
namespace TP6.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public AccessLevel AccessLevel { get; set; }
    public string Nombre { get; set; }
    public User()
    {
    }
    public User(CrearUsuarioViewModel usuVM)
    {
        this.Username = usuVM.Username;
        this.Nombre = usuVM.Nombre;
        this.Password = usuVM.Password;
        this.AccessLevel = usuVM.AccessLevel;
    }
}

public enum AccessLevel
{
    Admin, 

    Cliente
}
