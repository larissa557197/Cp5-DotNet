using VisionHive.Domain.Entities;
using VisionHive.Domain.Repositories;
using MongoDB.Driver;

namespace VisionHive.Infrastructure.Mongo.Repositories
{
    public class FilialRepository : MongoRepository<Filial>, IFilialRepository
    {
        private static readonly System.Linq.Expressions.Expression<System.Func<Filial, System.Guid>> IdSelector = x => x.Id;
        public FilialRepository(IMongoDatabase database) : base(database, "filiais") { }

        public Task<Filial?> GetByIdAsync(Guid id, CancellationToken ct=default) => FindById(id, IdSelector, ct);
        public new Task<IReadOnlyList<Filial>> GetAllAsync(CancellationToken ct=default) => base.GetAllAsync(ct);
        public new Task AddAsync(Filial e, CancellationToken ct=default) => base.AddAsync(e, ct);
        public Task UpdateAsync(Filial e, CancellationToken ct=default) => ReplaceAsync(e.Id, e, IdSelector, ct);
        public Task DeleteAsync(Guid id, CancellationToken ct=default) => base.DeleteAsync(id, IdSelector, ct);
    }
}
