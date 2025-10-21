using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;


namespace VisionHive.Application.UseCases;

public interface IMotoUseCase
{
    Task<MotoResponse> Post(MotoRequest request, CancellationToken ct = default);
    Task<MotoResponse?> GetById(Guid id, CancellationToken ct = default);
    Task<PageResult<MotoResponse>> GetPagination(PaginatedRequest request, CancellationToken ct = default);
    Task<bool> Put(Guid id, MotoRequest request, CancellationToken ct = default);
    Task<bool> Delete(Guid id, CancellationToken ct = default);
}