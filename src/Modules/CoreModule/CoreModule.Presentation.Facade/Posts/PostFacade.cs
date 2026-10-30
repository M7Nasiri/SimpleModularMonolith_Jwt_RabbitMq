using Application.Posts.Create;
using Application.Posts.Edit;
using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;


using System;
using System.Collections.Generic;
using System.Text;
using CoreModule.Query.Posts.DTOs;
using CoreModule.Query.Posts.GetByFilter;
using CoreModule.Query.Posts.GetById;

namespace Presentation.Facade.Posts
{
    public class PostFacade : IPostFacade
    {
        private readonly IMediator _mediator;
        public PostFacade(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<OperationResult> CreatePost(CreatePostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditPost(EditPostCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<PostDto?> GetPostById(Guid postId)
        {
            return await _mediator.Send(new GetPostrByIdQuery(postId));
        }

        public async Task<PostFilterResult> GetPostByFilter(PostFilterParams filterParams)
        {
            return await _mediator.Send(new GetPostByFilterQuery(filterParams));
        }
    }
}
