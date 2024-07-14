using System;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
	public interface IRaceReposatory
	{
        Task<IEnumerable<Race>> GetAll();

        Task<Race> GetByIDAsync(int Id);

        Task<Race> GetByIDNoTrackingAsync(int Id);

        Task<IEnumerable<Race>> GetRaceByCity(string city);

        bool add(Race club);

        bool Update(Race club);

        bool Delete(Race clib);

        bool Save();
    }
}

