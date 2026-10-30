using Common.Query;
using System;
using System.Collections.Generic;
using System.Text;
using CoreModule.Query.Posts.DTOs;

namespace CoreModule.Query.Posts.GetById;
public class GetPostrByIdQuery : IQuery<PostDto?>
{
    public GetPostrByIdQuery(Guid postId)
    {
        PostId = postId;
    }

    public Guid PostId { get; private set; }
}