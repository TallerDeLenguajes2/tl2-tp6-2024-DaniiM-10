using TP6.Models;
using TP7.ViewModels;


public interface IUserRepository
{
    public User GetUser(string username, string password);
    public void AltaUsuario(User usuario);
}