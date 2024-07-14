using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Reposatory;
using RunGroupWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceReposatory _RaceReposatory;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceReposatory RaceReposatory , IPhotoService photoService , IHttpContextAccessor httpContextAccessor)
        {
            _RaceReposatory = RaceReposatory;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> Races = await _RaceReposatory.GetAll();
            return View(Races);
        }

        public async Task<IActionResult> Details(int id)
        {
            Race race = await _RaceReposatory.GetByIDAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            var CurUserID = _httpContextAccessor.HttpContext?.User.GetUserID();
            var CreateRaceModelView = new CreateRaceViewModel { AppUserId = CurUserID };
            return View(CreateRaceModelView);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel VMrace)
        {

            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(VMrace.Image);

                var race = new Race
                {
                    Title = VMrace.Title,
                    Description = VMrace.Description,
                    Image = result.Url.ToString(),
                    AppuserID = VMrace.AppUserId,
                    adress = new Adress
                    {
                        City = VMrace.address.City,
                        state = VMrace.address.state,
                        Street = VMrace.address.Street
                    }
                };

                _RaceReposatory.add(race);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo is not uploaded");
            }

            return View(VMrace);

        }


        public async Task<IActionResult> Edit(int id)
        {
            var race = await _RaceReposatory.GetByIDAsync(id);

            if (race == null) return View("Erorr");
            var VMRace = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                address = race.adress,
                addressID = race.AdressId,
                RaceCategory = race.clubcategory,
                URL = race.Image
            };

            return View(VMRace);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditRaceViewModel vmrace)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Can't Edit Race");
                return View("Edit", vmrace);
            }

            var UserRace = await _RaceReposatory.GetByIDNoTrackingAsync(id);

            if(vmrace != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(UserRace.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Can't delete Image");
                    return View(vmrace);
                }

                var photoReslut = await _photoService.AddPhotoAsync(vmrace.Image);

                var Race = new Race
                {
                    id = id,
                    Title = vmrace.Title,
                    Description = vmrace.Description,
                    adress = vmrace.address,
                    AdressId = vmrace.addressID,
                    Image = photoReslut.Url.ToString()
                };

                _RaceReposatory.Update(Race);

                return RedirectToAction("Index");

            }
            else
            {
                return View(vmrace);
            }
            
        }

    }
}

