using Common.Application;

namespace Application.Users.Register;

public class RegisterUserCommand : IBaseCommand
{
    public RegisterUserCommand(string phoneNumber, string password)
    {
        PhoneNumber = phoneNumber;
        Password = password;
    }
    public string PhoneNumber { get; private set; }
    public string Password { get; private set; }
}