namespace VisionHive.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using VisionHive.Domain.Entities;

    public interface IPatioRepository
    {
        Task<Patio?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Patio>> GetAllAsync(CancellationToken ct = default);
        Task AddAsync(Patio entity, CancellationToken ct = default);
        Task UpdateAsync(Patio entity, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
