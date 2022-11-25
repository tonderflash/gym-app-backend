using Microsoft.EntityFrameworkCore;
using MyGymApp.Aplication.Common.Interfaces;
using MyGymApp.Domain.Common.BaseEntity;
using MyGymApp.Infrastructure.Context;

namespace MyGymApp.Infrastructure.Services;

public class BaseCrudService<TEntity> : IBaseCrudService<TEntity>
    where TEntity : class, IBase
{
    private readonly IDbContext _context;
    private readonly DbSet<TEntity> _db;

    public BaseCrudService(IDbContext context)
    {
        _context = context;
        _db = context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query()
    {
        return _db.AsQueryable();
    }
    public async Task<TEntity> Add(TEntity entity)
    {
        var result = await _db.AddAsync(entity);
        await _context.SaveChangesAsync();

        return result.Entity;
    }
    public async Task<TEntity> Delete(int id)
    {
        var entity = await Get(id);

        var result = _db.Remove(entity);
        await _context.SaveChangesAsync();

        return result.Entity;
    }
    public async Task<TEntity> Get(int id)
    {
        var entity = await _db.Where(x => x.Id == id).FirstOrDefaultAsync();
        return entity;
    }
    public async Task<TEntity> Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}