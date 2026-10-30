using Common.Application;

namespace Application.Users.ChangePassword;

public class ChangeUserPasswordCommand : IBaseCommand
{
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
}