using System.Linq.Expressions;
using MongoDB.Driver;

namespace infrastructure;

public interface IMongoDBHelper{
    Task UpdateAsync<T>(string collectionName, Expression<Func<T, bool>> condition, T item);
    
    Task<List<T>> Select<T>(string collectionName, Expression<Func<T, bool>> condition);
}

public class MongoDBHelper: IMongoDBHelper
{
    private readonly IConfig config;

    public MongoDBHelper(IConfig config)
    {
        this.config = config;
    }

    public async Task UpdateAsync<T>(string collectionName, Expression<Func<T, bool>> condition, T item){
        IMongoCollection<T> collection = GetCollection<T>(collectionName);
        FilterDefinition<T> filter = BuildFilter<T>(condition);

        await collection.ReplaceOneAsync(filter, item, new ReplaceOptions { IsUpsert = true });
    }

    public async Task<List<T>> Select<T>(string collectionName, Expression<Func<T, bool>> condition){
        IMongoCollection<T> collection = GetCollection<T>(collectionName);
        FilterDefinition<T> filter = BuildFilter<T>(condition);

        var documents = await collection.FindAsync(filter);

        return documents.ToList();
    }
        
    private IMongoCollection<T> GetCollection<T>(string collectionName){
        IMongoClient client = new MongoClient(config.ConnectionString);
        IMongoDatabase database = client.GetDatabase(config.DatabaseName);

        return database.GetCollection<T>(collectionName);
    }

    private FilterDefinition<T> BuildFilter<T>(Expression<Func<T, bool>> expression){
        return Builders<T>.Filter.Where(expression);
    }
}