using System.Net.Http.Headers;
using System;
using LiteDB;
using LiteDBExample.Console.Services;

namespace LiteDBExample.Console.Features.Blog;

public class BlogExample
{
    private LiteDBService _liteDbService;

    public BlogExample()
    {
        SetLiteDBService(); //Need DI
    }

    public LiteDBService SetLiteDBService()
    {
        if (_liteDbService == null)
        {
            this._liteDbService = new LiteDBService();
        }

        return _liteDbService;
    }

    public void Run()
    {
        CreateBlog();

        PrintList();

        UpdateBlog();

        PrintList();

        DeleteBlog();

        PrintList();
    }

    public bool CreateBlog()
    {
        try
        {
            System.Console.WriteLine("Blog create start ...");
            for (int i = 1; i < 10; i++)
            {
                BlogDataModel data = new BlogDataModel
                {
                    Blog_Id = i,
                    Blog_Title = "Title " + i,
                    Blog_Author = "Author " + i,
                    Blog_Content = "Content " + i
                };
                _liteDbService.Insert(data);
            }
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            return false;
        }

        System.Console.WriteLine("Blogs are created successfully!");
        return true;
    }

    public bool UpdateBlog()
    {
        try
        {
            System.Console.WriteLine("Update title and content with id 1 to 100");
            BlogDataModel data = _liteDbService
                .GetOne<BlogDataModel>(x => x.Blog_Id == 1);

            System.Console.WriteLine(data.Blog_Title);

            data.Blog_Title = "Title 100";
            data.Blog_Content = "Content 100";

            bool isUpdate = _liteDbService.Update(data);
            if (!isUpdate)
                System.Console.WriteLine("Object not found");
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            return false;
        }

        System.Console.WriteLine("Blog update success!");
        return true;
    }

    public bool DeleteBlog()
    {
        int blog_id = 3;
        System.Console.WriteLine("Delete blog with id 3");

        bool isDeleted = _liteDbService
            .Delete<BlogDataModel>(blog_id);

        if (isDeleted)
            System.Console.WriteLine("Blog is deleted successfully.");
        else
            System.Console.WriteLine("Delete error");

        return isDeleted;
    }

    public List<BlogDataModel> GetList()
    {
        List<BlogDataModel> list = new List<BlogDataModel>();

        try
        {
            list = _liteDbService.GetList<BlogDataModel>();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine("Exception in list => " + ex.Message);
        }

        return list;
    }

    public void PrintList()
    {
        System.Console.WriteLine("Blog lists are  =>  ");
        foreach (var item in GetList())
        {
            System.Console.WriteLine(item.Blog_Id);
            System.Console.WriteLine(item.Blog_Title);
            System.Console.WriteLine(item.Blog_Author);
            System.Console.WriteLine(item.Blog_Content);
            System.Console.WriteLine("");
        }

        System.Console.WriteLine("--------------------------------");
    }
}