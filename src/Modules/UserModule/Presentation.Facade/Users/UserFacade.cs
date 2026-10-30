using Application.Users.AddToken;
using Application.Users.ChangePassword;
using Application.Users.Create;
using Application.Users.Edit;
using Application.Users.Register;
using Application.Users.RemoveToken;
using Common.Application;
using Common.Application.SecurityUtil;

using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Query.Users.DTOs;
using Query.Users.GetByFilter;
using Query.Users.GetById;
using Query.Users.GetByPhoneNumber;
using Query.Users.UserTokens.GetByJwtToken;
using Query.Users.UserTokens.GetByRefreshToken;


namespace Presentation.Facade.Users;

public class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;
    private IDistributedCache _cache;
    public UserFacade(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task<OperationResult> CreateUser(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Status != OperationResultStatus.Success)
            return OperationResult.Error();

        return OperationResult.Success();
    }

    public async Task<OperationResult> ChangePassword(ChangeUserPasswordCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUser(EditUserCommand command)
    {
        var result = await _mediator.Send(command);

        return result;
    }

    public async Task<UserDto?> GetUserById(Guid userId)
    {
        return await _mediator.Send(new GetUserByIdQuery(userId));
    }

    public async Task<UserTokenDto?> GetUserTokenByRefreshToken(string refreshToken)
    {
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);
        return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(hashRefreshToken));
    }

    public async Task<UserTokenDto?> GetUserTokenByJwtToken(string jwtToken)
    {
        var hashJwtToken = Sha256Hasher.Hash(jwtToken);

        return await _mediator.Send(new GetUserTokenByJwtTokenQuery(hashJwtToken));

    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }
}