using Common.Domain;
using Domain.RoleAgg.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.RoleAgg;

public class RolePermission : BaseEntity
{
    public RolePermission(Permission permission)
    {
        Permission = permission;
    }

    public Guid RoleId { get; internal set; }
    public Permission Permission { get; private set; }
}