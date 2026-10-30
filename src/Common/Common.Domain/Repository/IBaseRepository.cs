using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Common.Domain.Repository;

public interface IBaseRepository<T> where T : AggregateRoot
{
    Task<T?> GetById(Guid id);

    Task<T?> GetTracking(Guid id);

    void Add(T entity);

    Task AddRange(ICollection<T> entities);

    void Update(T entity);

    Task<int> Save();

    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

    bool Exists(Expression<Func<T, bool>> expression);

    T? Get(Guid id);
}