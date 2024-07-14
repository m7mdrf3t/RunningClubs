using System;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
	public interface IClubReposatory
	{
		Task<IEnumerable<Club>> GetAll();

		Task<Club> GetByIDAsync(int Id);

        Task<Club> GetByIDNoTrackingAsync(int Id);

        Task<IEnumerable<Club>> GetClubByCity(string city);

		bool add(Club club);

		bool Update(Club club);

		bool Delete(Club clib);

		bool Save();
	}
}

