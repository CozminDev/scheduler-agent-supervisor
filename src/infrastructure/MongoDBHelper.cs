using System.Linq.Expressions;
using MongoDB.Driver;

namespace infrastructure;

public interface IMongoDBHelper{
    IMongoClient GetClient();

    Task UpdateAsync<T>(string collectionName, Expression<Func<T, bool>> condition, T item);

    Task UpdateFieldAsync<T, TField>(string collectionName, Expression<Func<T, bool>> condition, Expression<Func<T, TField>> field, TField value);
    
    Task<List<T>> Select<T>(string collectionName, Expression<Func<T, bool>> condition);
}

public class MongoDBHelper: IMongoDBHelper
{
    private readonly IMongoClient client;

    private readonly IConfig config;

    public MongoDBHelper(IConfig config)
    {
        this.config = config;
        client = new MongoClient(config.ConnectionString);
    }

    public async Task UpdateAsync<T>(string collectionName, Expression<Func<T, bool>> condition, T item){
        IMongoCollection<T> collection = GetCollection<T>(collectionName);
        FilterDefinition<T> filter = BuildFilter<T>(condition);

        await collection.ReplaceOneAsync(filter, item, new ReplaceOptions { IsUpsert = true });
    }

    public async Task UpdateFieldAsync<T, TField>(string collectionName, Expression<Func<T, bool>> condition, Expression<Func<T, TField>> field, TField value){
        IMongoCollection<T> collection = GetCollection<T>(collectionName);
        FilterDefinition<T> filter = BuildFilter<T>(condition);
        UpdateDefinition<T> update = BuildUpdate<T, TField>(field, value);

        await collection.UpdateManyAsync(filter, update);
    }

    public async Task<List<T>> Select<T>(string collectionName, Expression<Func<T, bool>> condition){
        IMongoCollection<T> collection = GetCollection<T>(collectionName);
        FilterDefinition<T> filter = BuildFilter<T>(condition);

        var documents = await collection.FindAsync(filter);

        return documents.ToList();
    }
        
    private IMongoCollection<T> GetCollection<T>(string collectionName){
        IMongoDatabase database = client.GetDatabase(config.DatabaseName);

        return database.GetCollection<T>(collectionName);
    }

    private FilterDefinition<T> BuildFilter<T>(Expression<Func<T, bool>> expression){
        return Builders<T>.Filter.Where(expression);
    }

    private UpdateDefinition<T> BuildUpdate<T, TField>(Expression<Func<T, TField>> expression, TField value){
        return Builders<T>.Update.Set<TField>(expression, value);
    }

    public IMongoClient GetClient()
    {
        return client;
    }
}