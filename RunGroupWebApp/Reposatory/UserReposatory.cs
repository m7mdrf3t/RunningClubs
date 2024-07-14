using System;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Reposatory
{
	public class UserReposatory : IuserReposatory
	{
        private readonly ApplicationDbContext _dbContext;

        public UserReposatory(ApplicationDbContext dbContext)
		{
            _dbContext = dbContext;
            
        }

        public bool Add(AppUser user)
        {
            _dbContext.Add(user);
            return Save();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _dbContext.Update(user);
            return Save();
        }
    }
}

