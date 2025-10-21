using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;
using VisionHive.Domain.Repositories;

namespace VisionHive.Application.UseCases;

public class PatioUseCase(IPatioRepository repo) : IPatioUseCase
{
    public async Task<PatioResponse> PostAsync(PatioRequest request, CancellationToken ct = default)
    {
        var entity = new Patio(request.Nome, request.LimiteMotos, request.FilialId);
        var created = await repo.AddAsync(entity, ct);

        return new PatioResponse
        {
            Id = created.Id,
            Nome = created.Nome,
            LimiteMotos = created.LimiteMotos
        };
    }

    public Task<PageResult<Patio>> GetPaginationAsync(int page, int pageSize, string? search, CancellationToken ct = default)
        => repo.GetPaginationAsync(page, pageSize, search, ct);

    public async Task<PatioResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await repo.GetByIdAsync(id, ct);
        if (entity is null) return null;

        return new PatioResponse
        {
            Id = entity.Id,
            Nome = entity.Nome,
            LimiteMotos = entity.LimiteMotos
        };
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => repo.DeleteAsync(id, ct);

    public async Task<bool> UpdateAsync(Guid id, PatioRequest request, CancellationToken ct = default)
    {
        var entity = await repo.GetByIdAsync(id, ct);
        if (entity is null) return false;

        entity.AtualizarDados(request.Nome, request.LimiteMotos);
        return await repo.UpdateAsync(entity, ct);
    }
}
