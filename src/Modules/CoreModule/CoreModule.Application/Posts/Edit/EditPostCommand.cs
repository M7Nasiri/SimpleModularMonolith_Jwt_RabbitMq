using Common.Application;


namespace Application.Posts.Edit;

public record EditPostCommand(Guid Id, Guid UserId,string Title,string Text) : IBaseCommand;