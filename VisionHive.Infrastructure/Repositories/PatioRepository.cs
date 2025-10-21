using Microsoft.EntityFrameworkCore;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;
using VisionHive.Domain.Repositories;
using VisionHive.Infrastructure.Contexts;

namespace VisionHive.Infrastructure.Repositories;

public class PatioRepository : IPatioRepository
{
    private readonly VisionHiveContext _context;
    public PatioRepository(VisionHiveContext context) => _context = context;
    
    public async Task<Patio> AddAsync(Patio patio, CancellationToken ct = default)
        {
            _context.Patios.Add(patio);
            await _context.SaveChangesAsync(ct);
            return patio;
        }

    public async Task<PageResult<Patio>> GetPaginationAsync(
            int page,
            int pageSize,
            string? search,
            CancellationToken ct = default)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            // Inclui Filial para permitir filtro por nome da filial e exibição
            var query = _context.Patios
                .Include(p => p.Filial)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                // Filtro por Nome do Pátio e Nome da Filial (ajuste se o nome no seu modelo for outro)
                query = query.Where(p =>
                    (p.Nome != null && p.Nome.Contains(search)) ||
                    (p.Filial != null && p.Filial.Nome != null && p.Filial.Nome.Contains(search))
                );
            }

            // Ordenação simples – ajuste se preferir por DataCadastro, etc.
            query = query.OrderByDescending(p => p.Id);

            var totalInt = await query.CountAsync(ct);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PageResult<Patio>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                Total = totalInt
            };
        }

        public Task<Patio?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => _context.Patios
                .Include(p => p.Filial)
                // Se você quiser também carregar as motos: descomente a linha abaixo
                // .Include(p => p.Motos)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<bool> UpdateAsync(Patio patio, CancellationToken ct = default)
        {
            // A entidade deve chegar já atualizada via método de domínio (AtualizarDados)
            _context.Patios.Update(patio);
            return await _context.SaveChangesAsync(ct) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _context.Patios.FindAsync(new object?[] { id }, ct);
            if (entity is null) return false;

            _context.Patios.Remove(entity);
            return await _context.SaveChangesAsync(ct) > 0;
        }
    }
