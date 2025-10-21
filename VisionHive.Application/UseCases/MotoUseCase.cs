using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;
using VisionHive.Domain.Repositories;

namespace VisionHive.Application.UseCases;

public class MotoUseCase(IMotoRepository repo) : IMotoUseCase
{
    public async Task<MotoResponse> PostAsync(MotoRequest request, CancellationToken ct = default)
    {
        var entity = new Moto(request.Placa, request.Chassi, request.NumeroMotor, request.Prioridade, request.PatioId);
        var created = await repo.AddAsync(entity, ct);

        return new MotoResponse
        {
            Id = created.Id,
            Placa = created.Placa,
            Chassi = created.Chassi,
            NumeroMotor = created.NumeroMotor,
            Prioridade = created.Prioridade.ToString()
        };
    }

    public Task<PageResult<Moto>> GetPaginationAsync(int page, int pageSize, string? search, CancellationToken ct = default)
        => repo.GetPaginationAsync(page, pageSize, search, ct);

    public async Task<MotoResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await repo.GetByIdAsync(id, ct);
        if (entity is null) return null;

        return new MotoResponse
        {
            Id = entity.Id,
            Placa = entity.Placa,
            Chassi = entity.Chassi,
            NumeroMotor = entity.NumeroMotor,
            Prioridade = entity.Prioridade.ToString()
        };
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => repo.DeleteAsync(id, ct);

    public async Task<bool> UpdateAsync(Guid id, MotoRequest request, CancellationToken ct = default)
    {
        var entity = await repo.GetByIdAsync(id, ct);
        if (entity is null) return false;

        entity.AtualizarDados(request.Placa, request.Chassi, request.NumeroMotor, request.Prioridade, request.PatioId);
        return await repo.UpdateAsync(entity, ct);
    }
}