using Common.Query;
using System;
using System.Collections.Generic;
using System.Text;
using CoreModule.Query.Posts.DTOs;

namespace CoreModule.Query.Posts.GetByFilter;

public class GetPostByFilterQuery : QueryFilter<PostFilterResult, PostFilterParams>
{
    public GetPostByFilterQuery(PostFilterParams filterParams) : base(filterParams)
    {
    }
}
