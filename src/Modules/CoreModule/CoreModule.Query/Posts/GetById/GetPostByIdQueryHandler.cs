using Common.Query;
using CoreModule.Infrastructure.Persistent.EF;
using CoreModule.Query.Posts.DTOs;

using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Posts.GetById;
public class GetPostByIdQueryHandler : IQueryHandler<GetPostrByIdQuery, PostDto?>
{
    private readonly CoreModuleCtx _context;

    public GetPostByIdQueryHandler(CoreModuleCtx context)
    {
        _context = context;
    }


    public async Task<PostDto?> Handle(GetPostrByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .FirstOrDefaultAsync(f => f.Id == request.PostId, cancellationToken);
        if (post == null)
            return null;
        return await post.Map(_context);
    }
}