using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;

namespace VisionHive.Application.UseCases
{
    public interface IFilialUseCase
    {
        Task<FilialResponse> PostAsync(FilialRequest request, CancellationToken ct = default);
        Task<PageResult<Filial>> GetPaginationAsync(PaginatedRequest request, CancellationToken ct = default);
        Task<FilialResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, FilialRequest request, CancellationToken ct = default);
    }
}
