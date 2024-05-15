using MicoHospital.Core.Models;
using MicoHospital.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MicoHospital.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


      

        //public async Task<IActionResult> CreateAdmin(AdminLoginVm adminLoginVm)
        //{
        //    AppUser appUser = new AppUser()
        //    {
        //        UserName = "SuperAdmin",
        //        FullName = "Sirac Huseynov"
        //    };

        //    await _userManager.CreateAsync(appUser, "Admin123@");
        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

        //    return Ok("SuperAdmin yarandi!");
        //}

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole identityRole = new IdentityRole("SuperAdmin");
        //    IdentityRole identityRole1 = new IdentityRole("Admin");
        //    IdentityRole identityRole2 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(identityRole);
        //    await _roleManager.CreateAsync(identityRole1);
        //    await _roleManager.CreateAsync(identityRole2);

        //    return Ok("Rollar yarandi!");
        //}

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if(!ModelState.IsValid)
                return View();

            AppUser appUser = await _userManager.FindByNameAsync(adminLoginVm.Username);  

            if(appUser == null)
            {
                ModelState.AddModelError("", "Username or password is invalid");
                return View();  
            }

            var result = await _signInManager.PasswordSignInAsync(appUser, adminLoginVm.Password, adminLoginVm.IsPersistent, false);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is invalid");
                return View();
            }

            return RedirectToAction("Index", "Dashboard");

        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
    }
}
