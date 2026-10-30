using Common.Application;

using Common.AspNetCore;
using CoreModule.Domain.PostAgg;
using CoreModule.Domain.PostAgg.Repository;
using Microsoft.AspNetCore.Authorization;


namespace Application.Posts.Create;

public class CreatePostCommandHandler : IBaseCommandHandler<CreatePostCommand>
{
    private readonly IPostRepository _repository;

    public CreatePostCommandHandler(IPostRepository repository)
    {
        _repository = repository;
    }

   
    public async Task<OperationResult> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post(request.UserId,request.Title, request.Text);
        _repository.Add(post);
        await _repository.Save();

        return OperationResult.Success();
    }
}