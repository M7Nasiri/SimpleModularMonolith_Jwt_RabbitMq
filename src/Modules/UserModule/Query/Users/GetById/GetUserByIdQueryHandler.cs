using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Users;
using Query.Users.DTOs;


namespace Query.Users.GetById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly DbCtx _context;

    public GetUserByIdQueryHandler(DbCtx context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(f => f.Id == request.UserId, cancellationToken);
        if (user == null)
            return null;


        return await user.Map().SetUserRoleTitles(_context);
    }
}