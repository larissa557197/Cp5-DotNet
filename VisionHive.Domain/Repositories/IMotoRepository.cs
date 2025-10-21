using VisionHive.Domain.Pagination;
using VisionHive.Domain.Entities;

namespace VisionHive.Domain.Repositories
{


    public interface IMotoRepository
    {
        Task<Moto> AddAsync(Moto moto, CancellationToken ct = default);
        Task<PageResult<Moto>> GetPaginationAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> UpdateAsync(Moto moto, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
