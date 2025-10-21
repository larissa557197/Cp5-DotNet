using Microsoft.EntityFrameworkCore;
using VisionHive.Domain.Entities;
using VisionHive.Domain.Pagination;
using VisionHive.Domain.Repositories;
using VisionHive.Infrastructure.Contexts;

namespace VisionHive.Application.Repositories;

public class FilialRepository : IFilialRepository
{
    private readonly VisionHiveContext _context;
    public FilialRepository(VisionHiveContext context) =>  _context = context;

    public async Task<Filial> AddAsync(Filial filial, CancellationToken ct = default)
    {
        _context.Filiais.Add(filial);
        await _context.SaveChangesAsync(ct);
        return filial;
    }
    
     public async Task<PageResult<Filial>> GetPaginationAsync(
            int page,
            int pageSize,
            string? search,
            CancellationToken ct = default)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = _context.Filiais
                // .Include(f => f.Patios) // descomente se quiser carregar os pátios na listagem
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                // dígitos do termo (feito fora da expressão)
                var sDigits = new string(s.Where(char.IsDigit).ToArray());

                if (!string.IsNullOrEmpty(sDigits))
                {
                    query = query.Where(f =>
                        (f.Nome   != null && EF.Functions.Like(f.Nome,   $"%{s}%")) ||
                        (f.Bairro != null && EF.Functions.Like(f.Bairro, $"%{s}%")) ||
                        (f.Cnpj   != null && (
                            EF.Functions.Like(f.Cnpj, $"%{s}%") ||
                            // normalização do CNPJ feita na expressão com Replace (traduzível)
                            EF.Functions.Like(
                                f.Cnpj.Replace(".", "").Replace("-", "").Replace("/", ""),
                                $"%{sDigits}%"
                            )
                        )));
                }
                else
                {
                    query = query.Where(f =>
                        (f.Nome   != null && EF.Functions.Like(f.Nome,   $"%{s}%")) ||
                        (f.Bairro != null && EF.Functions.Like(f.Bairro, $"%{s}%")) ||
                        (f.Cnpj   != null && EF.Functions.Like(f.Cnpj,   $"%{s}%")));
                }
            }

            // ordena por Nome (ajuste se preferir por Id)
            query = query.OrderBy(f => f.Nome);

            var totalInt = await query.CountAsync(ct);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return new PageResult<Filial>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                Total = totalInt
            };
        }

        public Task<Filial?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => _context.Filiais
                .Include(f => f.Patios) // útil para detalhar a filial com seus pátios
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id, ct);

        public async Task<bool> UpdateAsync(Filial filial, CancellationToken ct = default)
        {
            _context.Filiais.Update(filial);
            return await _context.SaveChangesAsync(ct) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _context.Filiais.FindAsync(new object?[] { id }, ct);
            if (entity is null) return false;

            _context.Filiais.Remove(entity);
            return await _context.SaveChangesAsync(ct) > 0;
        }
    }
