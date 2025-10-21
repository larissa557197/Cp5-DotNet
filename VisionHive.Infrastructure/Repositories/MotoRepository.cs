using Microsoft.EntityFrameworkCore;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;
using VisionHive.Domain.Repositories;
using VisionHive.Infrastructure.Contexts;

namespace VisionHive.Infrastructure.Repositories;

public class MotoRepository : IMotoRepository
{
    private readonly VisionHiveContext _context;
    public MotoRepository(VisionHiveContext context) =>  _context = context;

    public async Task<Moto> AddAsync(Moto moto, CancellationToken ct = default)
    {
        _context.Motos.Add(moto);
        await _context.SaveChangesAsync(ct);
        return moto;
    }

    public async Task<PageResult<Moto>> GetPaginationAsync(int page, int pageSize, string? search, CancellationToken ct = default)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = _context.Motos
            .Include(m => m.Patio)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(m =>
                (m.Placa != null && m.Placa.Contains(search)) ||
                (m.Chassi != null && m.Chassi.Contains(search)) ||
                (m.NumeroMotor != null && m.NumeroMotor.Contains(search)));
        }

        query = query.OrderByDescending(m => m.Id);

        var totalInt = await query.CountAsync(ct);
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PageResult<Moto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            Total = totalInt
        };
    }

    public Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _context.Motos
            .Include(m => m.Patio)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, ct);

    public async Task<bool> UpdateAsync(Moto moto, CancellationToken ct = default)
    {
        _context.Motos.Update(moto);
        return await _context.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.Motos.FindAsync(new object?[] { id }, ct);
        if (entity is null) return false;

        _context.Motos.Remove(entity);
        return await _context.SaveChangesAsync(ct) > 0;
    }
}
