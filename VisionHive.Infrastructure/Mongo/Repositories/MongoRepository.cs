using MongoDB.Driver;
using System.Linq.Expressions;

namespace VisionHive.Infrastructure.Mongo.Repositories
{
    public abstract class MongoRepository<T>
    {
        protected readonly IMongoCollection<T> Collection;
        protected MongoRepository(IMongoDatabase database, string name)
        {
            Collection = database.GetCollection<T>(name);
        }
        protected Task<T?> FindById(Guid id, Expression<Func<T, Guid>> idSelector, CancellationToken ct=default)
        {
            var filter = Builders<T>.Filter.Eq(idSelector, id);
            return Collection.Find(filter).FirstOrDefaultAsync(ct);
        }
        public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct=default) => Collection.Find(_=>true).ToListAsync(ct);
        public Task AddAsync(T entity, CancellationToken ct=default) => Collection.InsertOneAsync(entity, cancellationToken: ct);
        protected Task ReplaceAsync(Guid id, T entity, Expression<Func<T, Guid>> idSelector, CancellationToken ct=default)
        {
            var filter = Builders<T>.Filter.Eq(idSelector, id);
            return Collection.ReplaceOneAsync(filter, entity, cancellationToken: ct);
        }
        protected Task DeleteAsync(Guid id, Expression<Func<T, Guid>> idSelector, CancellationToken ct=default)
        {
            var filter = Builders<T>.Filter.Eq(idSelector, id);
            return Collection.DeleteOneAsync(filter, ct);
        }
    }
}
