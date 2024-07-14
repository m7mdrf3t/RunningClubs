using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IClubReposatory _clubReposatory;

    public HomeController(ILogger<HomeController> logger , IClubReposatory clubReposatory)
    {
        _logger = logger;
        _clubReposatory = clubReposatory;
    }

    public async Task<IActionResult> Index()
    {

        var ipInfo = new IpInfos();
        var homevm = new HomeViewModel();

        try
        {
            var Url = "http://ipinfo.io/45.240.155.19?token=c3e211a05c871b";
            var info = new WebClient().DownloadString(Url);
            ipInfo = JsonConvert.DeserializeObject<IpInfos>(info);

            RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
            ipInfo.Country = myRI1.EnglishName;
            homevm.City = ipInfo.City;
            homevm.State = ipInfo.Region;

            if(homevm.City != null)
            {
                homevm.clubs = await _clubReposatory.GetClubByCity(homevm.City);

            }
            else
            {
                homevm.clubs = null;
            }

            return View(homevm);

        }
        catch (Exception ex)
        {
            homevm.clubs = null;
        }

        return View(homevm);

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

