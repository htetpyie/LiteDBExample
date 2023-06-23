using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using LiteDB;

namespace LiteDBExample.Console.Services;

public class LiteDBService
{
    private LiteDatabase _db;

    public LiteDBService()
    {
        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDBExample");
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        _db = new LiteDatabase("LiteDBExample/BlogExample.db");
    }

    public T GetOne<T>(Expression<Func<T, bool>> expression)
    {
        return _db.GetCollection<T>().FindOne(expression);
    }

    public List<T> GetList<T>()
    {
        return _db.GetCollection<T>()
            .FindAll()
            .ToList();
    }

    public void Insert<T>(T model)
    {
        _db.GetCollection<T>()
           .Insert(model);
    }

    public bool Update<T>(T model)
    {
        return _db.GetCollection<T>()
            .Update(model);
    }

    public bool Delete<T>(BsonValue id)
    {
        return _db.GetCollection<T>()
            .Delete(id);
    }
}