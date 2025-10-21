using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;
using VisionHive.Domain.Repositories;

namespace VisionHive.Application.UseCases;

public class FilialUseCase(IFilialRepository repo) : IFilialUseCase
{
    public async Task<FilialResponse> PostAsync(FilialRequest request, CancellationToken ct = default)
    {
        var entity = new Filial(request.Nome, request.Bairro, request.Cnpj);
        var created = await repo.AddAsync(entity, ct);

        return new FilialResponse
        {
            Id = created.Id,
            Nome = created.Nome,
            Bairro = created.Bairro,
            Cnpj = created.Cnpj
        };
    }

    public Task<PageResult<Filial>> GetPaginationAsync(PaginatedRequest request, CancellationToken ct = default)
    {
        var page = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var size = request.PageSize   <= 0 ? 10 : request.PageSize;
        return repo.GetPaginationAsync(page, size, request.Subject, ct);
    }
        

    public async Task<FilialResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await repo.GetByIdAsync(id, ct);
        if (entity is null) return null;

        return new FilialResponse
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Bairro = entity.Bairro,
            Cnpj = entity.Cnpj
        };
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => repo.DeleteAsync(id, ct);

    public async Task<bool> UpdateAsync(Guid id, FilialRequest request, CancellationToken ct = default)
    {
        var entity = await repo.GetByIdAsync(id, ct);
        if (entity is null) return false;

        entity.AtualizarDados(request.Nome, request.Bairro, request.Cnpj);
        return await repo.UpdateAsync(entity, ct);
    }
}