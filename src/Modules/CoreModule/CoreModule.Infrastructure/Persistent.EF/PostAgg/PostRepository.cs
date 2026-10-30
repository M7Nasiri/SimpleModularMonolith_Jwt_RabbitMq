using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CoreModule.Domain.PostAgg;
using CoreModule.Domain.PostAgg.Repository;
using CoreModule.Infrastructure._Utilities;
using CoreModule.Infrastructure.Persistent.EF;

namespace CoreModule.Infrastructure.Persistent.EF.PostAgg
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {

        private readonly CoreModuleCtx _context;

        public PostRepository(CoreModuleCtx context) : base(context)
        {
            _context = context;
        }
       
    }
}
