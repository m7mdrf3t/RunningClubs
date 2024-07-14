using RunGroupWebApp.Models;

namespace RunGroupWebApp.Interfaces
{
	public interface IDashboardReposatory
	{
		Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();

    }
}

