using Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.UserAgg.Repository;
public interface IUserRepository : IBaseRepository<User>
{
    //Task<List<User>> GetAll();
    //Task<User> GetById(long id);
    //Task<User> GetTracking(long id);
    //void Create(User user);
    //Task Delete(long id);
    //void Update(long id, User user);
    //bool Exists(Expression<Func<User, bool>> expression);
    //Task<int> Save();
}