using Common.Query;
using Common.Query.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreModule.Query.Posts.DTOs;

public class PostDto : BaseDto
{
    public string Title { get;  set; }
    public string Text { get;  set; }
    public Guid WrittenBy { get;  set; }
    public string? AuthorName { get; set; }
    public int ViewCount { get;  set; }
}


public class PostFilterData : BaseDto
{
    public string Title { get; set; }
    public string Text { get;  set; }
    public Guid WrittenBy { get;  set; }
    public string? AuthorName { get; set; }
    public int ViewCount { get; set; }

}

public class PostFilterParams : BaseFilterParam
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? AuthorName { get; set; }

}
public class PostFilterResult : BaseFilter<PostFilterData, PostFilterParams>
{

}