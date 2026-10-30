using Common.Query;
using Common.Query.Filter;
using Domain.UserAgg.Enums;

namespace Query.Users.DTOs;

public class UserDto:BaseDto
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string AvatarName { get; set; }
    public bool IsActive { get; set; }
    public Gender Gender { get; set; }
    public List<UserRoleDto> Roles { get; set; }
}
public class UserRoleDto
{
    public Guid RoleId { get; set; }
    public string RoleTitle { get; set; }
}


public class UserFilterData : BaseDto
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string AvatarName { get; set; }
    public Gender Gender { get; set; }
}

public class UserFilterParams:BaseFilterParam
{
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Guid? Id { get; set; }
}
public class UserFilterResult : BaseFilter<UserFilterData,UserFilterParams>
{

}