using Common.Application;
using Domain.RoleAgg.Enums;


namespace Application.Roles.Edit;

public record EditRoleCommand(Guid Id, string Title, List<Permission> Permissions) : IBaseCommand;