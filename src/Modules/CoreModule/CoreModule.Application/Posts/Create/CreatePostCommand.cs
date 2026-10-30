using Common.Application;


namespace Application.Posts.Create;

public record CreatePostCommand(Guid UserId,string Title,string Text) : IBaseCommand;