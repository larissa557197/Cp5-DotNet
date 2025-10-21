namespace VisionHive.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using VisionHive.Domain.Entities;

    public interface IFilialRepository
    {
        Task<Filial?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Filial>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Filial entity, CancellationToken ct = default);
        Task UpdateAsync(Filial entity, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
