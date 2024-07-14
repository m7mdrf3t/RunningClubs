using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        public readonly IClubReposatory _ClubReposatory;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubReposatory ClubReposatory , IPhotoService photoService , IHttpContextAccessor httpContextAccessor)
        {
            _ClubReposatory = ClubReposatory;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> club = await _ClubReposatory.GetAll();
            return View(club);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Club clubdetail = await _ClubReposatory.GetByIDAsync(id);
            return View(clubdetail);
        }

        public IActionResult Create()
        {
            var userID = _httpContextAccessor.HttpContext.User.GetUserID();
            var CreateClubModelView = new CreateClubViewModel { AppUserId = userID};
            return View(CreateClubModelView);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel VMclub)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(VMclub.Image);

                var club = new Club
                {
                    Title = VMclub.Title,
                    Description = VMclub.Description,
                    Image = result.Url.ToString(),
                    AppuserID = VMclub.AppUserId,
                    adress = new Adress
                    {
                        City = VMclub.address.City,
                        state = VMclub.address.state,
                        Street = VMclub.address.Street
                    }
                };

                _ClubReposatory.add(club);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo is not uploaded");
            }

            return View(VMclub);

            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _ClubReposatory.GetByIDAsync(id);
            if (club == null) return View("Error");

            var clubVm = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                addressID = club.AdressId,
                address = club.adress,
                URL = club.Image,
                clubCategory = club.clubcategory
            };

            return View(clubVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel ClubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit Club");
                return View("Edit", ClubVM);
            }

            var userClub = await _ClubReposatory.GetByIDNoTrackingAsync(id);

            if (ClubVM != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Couldn't delete Image");
                    return View(ClubVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(ClubVM.Image);

                var club = new Club
                {
                    id = id,
                    Title = ClubVM.Title,
                    Description = ClubVM.Description,
                    Image = photoResult.Url.ToString(),
                    adress = ClubVM.address,
                    AdressId = ClubVM.addressID
                };

                _ClubReposatory.Update(club);

                return RedirectToAction("Index");

            }
            else
            {
                return View(ClubVM);

            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _ClubReposatory.GetByIDAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }


        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _ClubReposatory.GetByIDAsync(id);
            if (clubDetails == null) return View("Error");

            if (string.IsNullOrEmpty(clubDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(clubDetails.Image);
            }

            _ClubReposatory.Delete(clubDetails);
            return RedirectToAction("Index");

        }


    }
}

