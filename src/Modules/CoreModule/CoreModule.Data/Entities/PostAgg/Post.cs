using System;
using System.Collections.Generic;
using System.Text;
using Common.Domain;

namespace Domain.PostAgg;

public class Post : AggregateRoot
{
    public string Title { get; private set; }
    public string Text { get; private set; }
    public long WrittenBy { get; private set; }
    public int ViewCount { get; private set; }

    public Post(long writtenBy,string title, string text)
    {
        Title = title;
        Text = text;
        WrittenBy = writtenBy;
    }

    public void Edit(long writtenBy,string title, string text)
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