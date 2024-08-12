using SharpPayStack.Interfaces;
using System.Linq.Expressions;
using SharpPayStack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace SharpPayStack.Repository;



public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected DatabaseContext _context;

    public RepositoryBase(DatabaseContext context) =>
        _context = context;


    /// <summary>
    /// Add an entity to the database
    /// </summary>
    /// <param name="entity"></param>
    public void Create(T entity) => _context.Set<T>().Add(entity);

    /// <summary>
    /// Remove an entity from the database
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public IQueryable<T> FindMany(bool trackChanges = false)
    {
        if (!trackChanges)
            return _context.Set<T>().AsNoTracking();

        return _context.Set<T>();
    }

    /// <summary>
    /// Finds an entity by a condition given by the expression. Also includes condition
    /// to load the entities related.
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="trackChanges">A boolean to check tracking entity in the storage</param>
    /// <returns></returns>
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges)
    {
        if (!trackChanges)
            return _context.Set<T>()
            .Where(expression)
            .AsNoTracking();

        return _context.Set<T>().Where(expression);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);

        var entityEntry = _context.Set<T>().Entry(entity);

        entityEntry.State = EntityState.Modified;

        _context.Set<T>().Entry(entity)
                .Property<DateTime>("UpdatedAt").CurrentValue = DateTime.UtcNow;
    }
}