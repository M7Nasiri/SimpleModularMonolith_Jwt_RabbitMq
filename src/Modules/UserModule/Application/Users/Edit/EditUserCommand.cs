using Common.Application;
using Common.Application.Validation;
using Domain.UserAgg.Enums;
using Microsoft.AspNetCore.Http;


namespace Application.Users.Edit;

public class EditUserCommand : IBaseCommand
{
    public EditUserCommand(Guid userId, IFormFile? avatar, string name, string family, string phoneNumber, string email, Gender gender)
    {
        UserId = userId;
        Avatar = avatar;
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }
    public Guid UserId { get;  set; }
    public IFormFile? Avatar { get; private set; }
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public Gender Gender { get; private set; }
}