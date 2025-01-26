namespace TP7.ViewModels;

public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ErrorMessage { get; set; }
    public bool IsAuthenticated { get; set; } 

    public LoginViewModel(){}
}
