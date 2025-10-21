using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;


namespace VisionHive.Domain.Repositories
{
    

    public interface IFilialRepository
    {
        Task<Filial> AddAsync(Filial filial, CancellationToken ct = default);
        Task<PageResult<Filial>> GetPaginationAsync(int page, int pageSize, string? search, CancellationToken ct = default);
        Task<Filial?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> UpdateAsync(Filial filial, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
