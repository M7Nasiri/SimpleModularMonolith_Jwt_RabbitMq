using Common.Application;

namespace Application.Users.RemoveToken;

public record RemoveUserTokenCommand(Guid UserId, Guid TokenId) : IBaseCommand<string>;