using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;

namespace VisionHive.Application.UseCases;

public interface IPatioUseCase
{
    Task<PatioResponse> PostAsync(PatioRequest request, CancellationToken ct = default);
    Task<PageResult<Patio>> GetPaginationAsync(PaginatedRequest request, CancellationToken ct = default);
    Task<PatioResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, PatioRequest request, CancellationToken ct = default);
}