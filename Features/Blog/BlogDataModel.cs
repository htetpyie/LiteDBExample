using LiteDB;

namespace LiteDBExample.Console.Features.Blog;

public class BlogDataModel
{
    [BsonId]
    public int Blog_Id { get; set; }

    public string Blog_Title { get; set; }

    public string Blog_Author { get; set; }

    public string Blog_Content { get; set; }
}