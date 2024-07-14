using System;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Reposatory
{
	public class ClubReposatory : IClubReposatory
	{

        public ApplicationDbContext _DbContext { get; }

        public ClubReposatory(ApplicationDbContext DbContext)
		{
            _DbContext = DbContext;
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _DbContext.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIDAsync(int Id)
        {
            return await _DbContext.Clubs.Include(a => a.adress).FirstOrDefaultAsync(c => c.id == Id);
        }

        public async Task<Club> GetByIDNoTrackingAsync(int Id)
        {
            return await _DbContext.Clubs.Include(a => a.adress).AsNoTracking().FirstOrDefaultAsync(c => c.id == Id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentException("City cannot be null or empty.", nameof(city));
            }

            var clubs = await _DbContext.Clubs
                                        .Where(c => c.adress.City.Contains(city))
                                        .ToListAsync();

            // Logging for debugging
            if (!clubs.Any())
            {
                // Log that no clubs were found
                Console.WriteLine($"No clubs found for city: {city}");
            }
            else
            {
                // Log the number of clubs found
                Console.WriteLine($"{clubs.Count} clubs found for city: {city}");
            }

            return clubs;
        }

        public bool add(Club club)
        {
            _DbContext.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            _DbContext.Remove(club);
            return Save();

        }

        public bool Save()
        {
            var saved = _DbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Club club)
        {
            _DbContext.Update(club);
            return Save();
        }

        
    }
}

