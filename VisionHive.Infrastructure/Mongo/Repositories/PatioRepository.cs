using VisionHive.Domain.Entities;
using VisionHive.Domain.Repositories;
using MongoDB.Driver;

namespace VisionHive.Infrastructure.Mongo.Repositories
{
    public class PatioRepository : MongoRepository<Patio>, IPatioRepository
    {
        private static readonly System.Linq.Expressions.Expression<System.Func<Patio, System.Guid>> IdSelector = x => x.Id;
        public PatioRepository(IMongoDatabase database) : base(database, "patios") { }

        public Task<Patio?> GetByIdAsync(Guid id, CancellationToken ct=default) => FindById(id, IdSelector, ct);
        public new Task<IReadOnlyList<Patio>> GetAllAsync(CancellationToken ct=default) => base.GetAllAsync(ct);
        public new Task AddAsync(Patio e, CancellationToken ct=default) => base.AddAsync(e, ct);
        public Task UpdateAsync(Patio e, CancellationToken ct=default) => ReplaceAsync(e.Id, e, IdSelector, ct);
        public Task DeleteAsync(Guid id, CancellationToken ct=default) => base.DeleteAsync(id, IdSelector, ct);
    }
}
