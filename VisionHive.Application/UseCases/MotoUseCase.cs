using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;
using VisionHive.Domain.Repositories;

namespace VisionHive.Application.UseCases;

public class MotoUseCase(IMotoRepository motoRepository) : IMotoUseCase
{
    public async Task<MotoResponse> Post(MotoRequest motorequest, CancellationToken ct = default)
    {
        var entity = motorequest.ToDomain();
        var created = await motoRepository.AddMotoAsync(entity, ct);
    }

    public Task<PageResult<Moto>> GetPagination(PaginatedRequest paginatedRequest)
    {
        return motoRepository.GetPaginationAsync(
            paginatedRequest.PageNumber,
            paginatedRequest.PageSize,
            paginatedRequest.Subject);
    }
}


