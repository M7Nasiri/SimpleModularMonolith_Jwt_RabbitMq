using System;
using System.Collections.Generic;
using System.Text;
using Common.Domain;

namespace CoreModule.Domain.PostAgg;

public class Post : AggregateRoot
{
    public string Title { get; private set; }
    public string Text { get; private set; }
    public Guid WrittenBy { get; private set; }
    public int ViewCount { get; private set; }

    public Post(Guid writtenBy,string title, string text)
    {
        Title = title;
        Text = text;
        WrittenBy = writtenBy;
    }

    public void Edit(Guid writtenBy,string title, string text)
    {
        Title = title;
        Text = text;
        WrittenBy = writtenBy;

    }

    public void IncreaseViewCount(int viewCount)
    {
        ViewCount = viewCount;
    }
}