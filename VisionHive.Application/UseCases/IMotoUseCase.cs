using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;


namespace VisionHive.Application.UseCases;

public interface IMotoUseCase
{
    Task<MotoResponse> PostAsync(MotoRequest request, CancellationToken ct = default);
    Task<PageResult<Moto>> GetPaginationAsync(PaginatedRequest request, CancellationToken ct = default);
    Task<MotoResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, MotoRequest request, CancellationToken ct = default);
}