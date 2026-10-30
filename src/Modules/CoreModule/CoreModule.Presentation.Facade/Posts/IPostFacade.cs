
using Common.Application;

using System;
using System.Collections.Generic;
using System.Text;
using Application.Posts.Create;
using Application.Posts.Edit;
using CoreModule.Query.Posts.DTOs;

namespace Presentation.Facade.Posts
{
    public interface IPostFacade
    {
        Task<OperationResult> CreatePost(CreatePostCommand command);
        Task<OperationResult> EditPost(EditPostCommand command);

        Task<PostDto?> GetPostById(Guid postId);
        Task<PostFilterResult> GetPostByFilter(PostFilterParams filterParams);
    }
}
