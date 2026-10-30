using Common.Application;
using Domain.RoleAgg.Enums;


namespace Application.Roles.Create;

public record CreateRoleCommand(string Title, List<Permission> Permissions) : IBaseCommand;