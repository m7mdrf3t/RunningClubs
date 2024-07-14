using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IuserReposatory _userReposatory;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;

        public UserController(IuserReposatory userReposatory ,UserManager<AppUser> userManager , IPhotoService photoService)
        {
            _userReposatory = userReposatory;
            _userManager = userManager;
            _photoService = photoService;
        }
        
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        { 
            var users = await _userReposatory.GetAllUsers();
            List<UserVeiwModel> userVm = new List<UserVeiwModel>();
            foreach (var user in users)
            {
                var userviewmodel = new UserVeiwModel()
                {
                    id = user.Id,
                    username = user.UserName,
                    pace = user.pace,
                    millage = user.Millage,
                    city = user.city,
                    state = user.state,
                    ProfileImageUrl = user.ProfileImageUrl
                };

                userVm.Add(userviewmodel);
            }

            return View(userVm);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var CurUser = await _userReposatory.GetUserById(id);
            if (CurUser == null) return RedirectToAction("Index", "User");

            var userDetailViewModel = new UserDetailViewModel()
            {
                Username = CurUser.UserName,
                pace = CurUser.pace,
                Millage = CurUser.Millage
            };

            return View(userDetailViewModel);

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var CurUser = await _userManager.GetUserAsync(User);
            if (CurUser == null) return RedirectToAction("Index", "user");

            var editMV = new EditProfileViewModel()
            {
                city = CurUser.city,
                state = CurUser.state,
                pace = CurUser.pace,
                Millage = CurUser.Millage,
                ProfileImage = CurUser.ProfileImageUrl,
            };

            return View(editMV);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editProfileVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to save Edit model");
                return View("EditProfile", editProfileVM);
            }

            var curUser = await _userManager.GetUserAsync(User);

            if(curUser == null)
            {
                return View("Error");
            }

            if(editProfileVM.Image != null)
            {
                var imageAdditionState = await _photoService.AddPhotoAsync(editProfileVM.Image);

                if(imageAdditionState.Error != null)
                {
                    ModelState.AddModelError("", "Failed to Upload Image");
                    return View("EditProfile", editProfileVM);
                }

                if (!string.IsNullOrEmpty(curUser.ProfileImageUrl))
                {
                   _ = _photoService.DeletePhotoAsync(curUser.ProfileImageUrl);
                }

                curUser.ProfileImageUrl = imageAdditionState.Url.ToString();
                editProfileVM.ProfileImage = curUser.ProfileImageUrl;

                await _userManager.UpdateAsync(curUser);

                return View(editProfileVM);

            }
                curUser.pace = editProfileVM.pace;
                curUser.Millage = editProfileVM.Millage;
                curUser.city = editProfileVM.state;
                curUser.state = editProfileVM.state;

            await _userManager.UpdateAsync(curUser);
            return RedirectToAction("Detail", "User", new { curUser.id });
        }

    }
}

