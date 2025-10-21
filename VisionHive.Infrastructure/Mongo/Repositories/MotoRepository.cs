using VisionHive.Domain.Entities;
using VisionHive.Domain.Repositories;
using MongoDB.Driver;

namespace VisionHive.Infrastructure.Mongo.Repositories
{
    public class MotoRepository : MongoRepository<Moto>, IMotoRepository
    {
        private static readonly System.Linq.Expressions.Expression<System.Func<Moto, System.Guid>> IdSelector = x => x.Id;
        public MotoRepository(IMongoDatabase database) : base(database, "motos") { }

        public Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct=default) => FindById(id, IdSelector, ct);
        public new Task<IReadOnlyList<Moto>> GetAllAsync(CancellationToken ct=default) => base.GetAllAsync(ct);
        public new Task AddAsync(Moto e, CancellationToken ct=default) => base.AddAsync(e, ct);
        public Task UpdateAsync(Moto e, CancellationToken ct=default) => ReplaceAsync(e.Id, e, IdSelector, ct);
        public Task DeleteAsync(Guid id, CancellationToken ct=default) => base.DeleteAsync(id, IdSelector, ct);
    }
}
