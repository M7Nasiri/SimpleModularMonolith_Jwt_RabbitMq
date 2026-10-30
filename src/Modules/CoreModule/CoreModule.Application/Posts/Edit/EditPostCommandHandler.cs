using Common.Application;

using Common.AspNetCore;
using CoreModule.Domain.PostAgg.Repository;
using Microsoft.AspNetCore.Authorization;


namespace Application.Posts.Edit;

public class EditPostCommandHandler : IBaseCommandHandler<EditPostCommand>
{
    private readonly IPostRepository _repository;

    public EditPostCommandHandler(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetTracking(request.Id);
        post.Edit(request.UserId,request.Title,request.Text);
        await _repository.Save();

        return OperationResult.Success();
    }
}