using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Domain.UserAgg;
using Domain.UserAgg.Repository;
using Infrastructure._Utilities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistent.EF.UserAgg
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        private readonly DbCtx _context;

        public UserRepository(DbCtx context) : base(context)
        {
            _context = context;
        }
        //public async Task<List<User>> GetAll()
        //{
        //    return await _context.Users.AsNoTracking().ToListAsync();
        //}

        //public async Task<User> GetById(long id)
        //{
        //    return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u=>u.Id == id);
        //}

        //public async Task<User> GetTracking(long id)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        //}

        //public void Create(User user)
        //{
        //    _context.Add(user);
        //}

        //public async Task Delete(long id)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        //    if (user != null)
        //    {
        //        _context.Remove(user);
        //    }
        //}

        //public void Update(long id, User user)
        //{
        //    var oldUser = _context.Users.FirstOrDefault(u => u.Id == id);
        //    if (oldUser != null)
        //    {
        //        _context.Update(user);
        //    }
        //}

        //public bool Exists(Expression<Func<User, bool>> expression)
        //{
        //    return _context.Users.Any(expression);
        //}

        //public async Task<int> Save()
        //{
        //    return await _context.SaveChangesAsync();
        //}
    }
}
