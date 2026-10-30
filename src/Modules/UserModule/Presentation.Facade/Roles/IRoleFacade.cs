

using Application.Roles.Create;
using Application.Roles.Edit;
using Common.Application;
using Query.Roles.DTOs;

namespace Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult> CreateRole(CreateRoleCommand command);
    Task<OperationResult> EditRole(EditRoleCommand command);

    Task<RoleDto?> GetRoleById(Guid roleId);
    Task<List<RoleDto>> GetRoles();
}