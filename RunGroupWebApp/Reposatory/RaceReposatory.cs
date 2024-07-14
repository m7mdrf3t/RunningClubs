using System;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Reposatory
{
	public class RaceReposatory : IRaceReposatory
	{
        public ApplicationDbContext _DbContext { get; }

        public RaceReposatory(ApplicationDbContext _Dbcontext)
		{
            _DbContext = _Dbcontext;
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
             return await _DbContext.Races.ToListAsync();
        }

        public async Task<Race> GetByIDAsync(int Id)
        {
            return await _DbContext.Races.Include(a => a.adress).FirstOrDefaultAsync(c => c.id == Id);
        }

        public async Task<Race> GetByIDNoTrackingAsync(int Id)
        {
            return await _DbContext.Races.Include(a => a.adress).AsNoTracking().FirstOrDefaultAsync(c => c.id == Id);
        }


        
        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await _DbContext.Races.Where(c => c.adress.City.Contains(city)).ToListAsync();
        }

        public bool add(Race race)
        {
            _DbContext.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _DbContext.Remove(race);
            return Save();

        }

        public bool Save()
        {
            var saved = _DbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _DbContext.Update(race);
            return Save();
        }
    }
}

