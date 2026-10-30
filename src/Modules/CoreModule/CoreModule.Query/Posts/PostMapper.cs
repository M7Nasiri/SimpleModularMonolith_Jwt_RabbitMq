using CoreModule.Infrastructure.Persistent.EF;
using CoreModule.Query.Posts.DTOs;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using CoreModule.Domain.PostAgg;

namespace CoreModule.Query.Posts;

public static class PostMapper
{
    public static async Task<PostDto> Map(this Post post, CoreModuleCtx ctx)
    {
        return new PostDto()
        {
            Id = post.Id,
            CreationDate = post.CreationDate,
            Text = post.Text,
            Title = post.Title,
            WrittenBy = post.WrittenBy,
            ViewCount = post.ViewCount,
            AuthorName = await ctx.Users.Where(u=>u.Id == post.WrittenBy).Select(u=>u.Name).FirstOrDefaultAsync()
        };
    }

    //This query cannot be exceute on sql
    //public static async Task<PostFilterData> MapFilterData(this Post post,DbCtx ctx)
    //{
    //    return new PostFilterData()
    //    {
    //        Id = post.Id,
    //        CreationDate = post.CreationDate,
    //        Text = post.Text,
    //        Title = post.Title,
    //        WrittenBy = post.WrittenBy,
    //        ViewCount = post.ViewCount,
    //        AuthorName = await ctx.Users.Where(u => u.Id == post.WrittenBy).Select(u => u.Name).FirstOrDefaultAsync()
    //    };
    //}
}