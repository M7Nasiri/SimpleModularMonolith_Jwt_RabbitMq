using Common.Query;
using Query.Roles.DTOs;

namespace Query.Roles.GetById;

public record GetRoleByIdQuery(Guid RoleId) : IQuery<RoleDto?>;