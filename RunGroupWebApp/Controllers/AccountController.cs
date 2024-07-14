using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RunGroupWebApp.Interfaces
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _usermanager;
        private readonly SignInManager<AppUser> _SignInManagar;
        private readonly ApplicationDbContext _context;


        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , ApplicationDbContext applicationDbContext)
        {
            _context = _context;
            _SignInManagar = signInManager;
            _usermanager = userManager;

        }

        // GET: /<controller>/
        public IActionResult Login()
        {
            var responce = new LoginViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) 
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _usermanager.FindByEmailAsync(loginViewModel.EmailAddress);

            if(user != null)
            {
                var PasswordCheck = await _usermanager.CheckPasswordAsync(user , loginViewModel.Password);
                if (PasswordCheck)
                {
                    var result = await _SignInManagar.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                    
                }

                TempData["Error"] = "Wrong Creandiatials , Please Try again";

                return View(loginViewModel);
            }
            TempData["Error"] = "Wrong Email , Please try to remeber your name";
            return View(loginViewModel);
        }


        public IActionResult Register()
        {
            var RegResponce = new RegisterViewModel();
            return View(RegResponce);
        }


        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (!ModelState.IsValid) return View(registerViewModel);

            var responce = await _usermanager.FindByEmailAsync(registerViewModel.EmailAdress);
            if(responce != null)
            {
                TempData["Error"] = "This Email is Already Register , Login instead";
                return View(registerViewModel);
            }

            var user = new AppUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.EmailAdress
            };

            var NewUser = await _usermanager.CreateAsync(user, registerViewModel.Password);
            if (NewUser.Succeeded)
            {
                await _usermanager.AddToRoleAsync(user , UserRoles.User);
                
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _SignInManagar.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }


     
}

