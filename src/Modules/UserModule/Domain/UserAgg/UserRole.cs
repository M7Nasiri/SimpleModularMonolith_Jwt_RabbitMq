using Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.UserAgg
{
    public class UserRole : BaseEntity
    {
        public UserRole(Guid roleId)
        {
            RoleId = roleId;
        }

        public Guid UserId { get; internal set; }
        public Guid RoleId { get; private set; }
    }
}
