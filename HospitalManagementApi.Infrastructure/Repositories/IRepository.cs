using System.Linq.Expressions;
using HospitalManagementApi.Domain.Common;

namespace HospitalManagementApi.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity: BaseEntity
{
    // Read operations
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    // Create operations
    Task<TEntity> AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    // Update operations
    Task<TEntity> UpdateAsync(TEntity entity);

    // Delete operations
    Task DeleteAsync(int id);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);

    // Save operations
    Task<int> SaveChangesAsync();
}
