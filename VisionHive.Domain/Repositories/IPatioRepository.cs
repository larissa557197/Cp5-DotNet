using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;

namespace VisionHive.Domain.Repositories
{
   

    public interface IPatioRepository
    {
        Task<Patio> AddAsync(Patio patio, CancellationToken ct = default);
        Task<PageResult<Patio>> GetPaginationAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<Patio?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> UpdateAsync(Patio patio, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
