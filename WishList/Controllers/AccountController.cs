using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WishList.Models;
using Microsoft.AspNetCore.Identity;
using WishList.Models.AccountViewModels;



namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel  registerViewModel)
        {
            if(ModelState.IsValid)
            {
                var result = _userManager.CreateAsync(new ApplicationUser()
                {
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.Email
                },
                registerViewModel.Password).Result;

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    return View(registerViewModel);
                }
                return RedirectToAction("Index", "Home");

            }
            return View(registerViewModel);
            
        }


    }
}
