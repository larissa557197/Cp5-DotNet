using VisionHive.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;

namespace VisionHive.Domain.Repositories
{


    public interface IMotoRepository
    {
        Task<MotoResponse> AddMotoAsync(MotoRequest request, CancellationToken ct = default);
        Task<MotoResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<PageResult<MotoResponse>> GetPaginationAsync(PaginatedRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, MotoRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
