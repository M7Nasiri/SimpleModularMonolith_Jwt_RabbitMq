using Application.Roles.Create;
using Application.Roles.Edit;
using Common.Application;
using MediatR;
using Query.Roles.DTOs;
using Query.Roles.GetById;
using Query.Roles.GetList;


namespace Presentation.Facade.Roles;

public class RoleFacade : IRoleFacade
{
    private readonly IMediator _mediator;

    public RoleFacade(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<OperationResult> CreateRole(CreateRoleCommand command)
    {
        return await _mediator.Send(command);
    }
    public async Task<OperationResult> EditRole(EditRoleCommand command)
    {
        return await _mediator.Send(command);
    }
    public async Task<RoleDto?> GetRoleById(Guid roleId)
    {
        return await _mediator.Send(new GetRoleByIdQuery(roleId));
    }
    public async Task<List<RoleDto>> GetRoles()
    {
        return await _mediator.Send(new GetRoleListQuery());
    }
}