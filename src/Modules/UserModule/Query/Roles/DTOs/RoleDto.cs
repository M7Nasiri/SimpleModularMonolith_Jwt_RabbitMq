using Common.Query;
using Domain.RoleAgg.Enums;


namespace Query.Roles.DTOs;

public class RoleDto : BaseDto
{
    public string Title { get; set; }
    public List<Permission> Permissions { get; set; }
}