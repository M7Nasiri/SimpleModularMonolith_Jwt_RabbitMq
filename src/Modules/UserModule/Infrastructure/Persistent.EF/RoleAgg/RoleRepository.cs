using Domain.RoleAgg;
using Domain.RoleAgg.Repository;
using Domain.UserAgg;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Persistent.EF.RoleAgg
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {

        private readonly DbCtx _context;

        public RoleRepository(DbCtx context) : base(context)
        {
            _context = context;
        }
      
    }
}
