using System;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Reposatory
{
	public class DashboardReposatory : IDashboardReposatory
	{
        private ApplicationDbContext _dbContext { get; }
        private readonly IHttpContextAccessor _httpContext;

        public DashboardReposatory(ApplicationDbContext DbContext , IHttpContextAccessor httpContext)
		{
            _dbContext = DbContext;
            _httpContext = httpContext;
        }

        public async Task<List<Club>> GetAllUserClubs()
        {

            var curUser = _httpContext.HttpContext?.User.GetUserID();
            var Userclubs = _dbContext.Clubs.Where(c => c.AppuserID == curUser);
            return Userclubs.ToList();

        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var curUser = _httpContext.HttpContext?.User.GetUserID();
            var UserRaces = _dbContext.Races.Where(c => c.AppuserID == curUser);
            return UserRaces.ToList();
        }
    }
}

