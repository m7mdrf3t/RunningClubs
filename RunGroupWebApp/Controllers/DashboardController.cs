using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunGroupWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardReposatory _dashboardReposatory;

        public DashboardController(IDashboardReposatory DashboardReposatory)
        {
            _dashboardReposatory = DashboardReposatory;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardReposatory.GetAllUserRaces();
            var UserClubs = await _dashboardReposatory.GetAllUserClubs();

            var DashboardData = new DashboardViewModel
            {
                Clubs = UserClubs,
                Races = userRaces
            };

            return View(DashboardData);
        }


    }
}

