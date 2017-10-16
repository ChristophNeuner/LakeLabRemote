using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LakeLabRemote.Models;
using LakeLabRemote.DataSource;
using System.Threading.Tasks;
using LakeLabRemote.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AdminController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IUserValidator<AppUser> _userValidator;
        private IPasswordValidator<AppUser> _passwordValidator;
        private IPasswordHasher<AppUser> _passwordHasher;
        private LakeLabDbContext _dbContext;
        public AdminController(UserManager<AppUser> usrMgr, IUserValidator<AppUser> userValid, IPasswordValidator<AppUser> passValid, IPasswordHasher<AppUser> passwordHash, LakeLabDbContext dbContext)
        {
            _userManager = usrMgr;
            _userValidator = userValid;
            _passwordValidator = passValid;
            _passwordHasher = passwordHash;
            _dbContext = dbContext;
        }

        public ViewResult Index() => View(_userManager.Users);

        public IActionResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }
            return View(nameof(Index), _userManager.Users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await _userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPassword = null;

                if (!string.IsNullOrEmpty(password))
                {
                    validPassword = await _passwordValidator.ValidateAsync(_userManager, user, password);
                    if (validPassword.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPassword);
                    }
                }

                if ((validEmail.Succeeded && validPassword == null) || (validEmail.Succeeded && validPassword.Succeeded && password != string.Empty))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditDevices([Required] string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "The user no longer exists.");
                return View(nameof(Index));
            }
            List<Device> members = new List<Device>();
            List<Device> nonMembers = new List<Device>();
            foreach (Device device in _dbContext.Devices)
            {
                List<Device> list = user.HasDevice(device) ? members : nonMembers;
                list.Add(device);
            }

            return View(new AppUserDevicesEditViewModel(user, members, nonMembers));
        }

        //[HttpPost]
        //public async Task<IActionResult> EditDevices(AppUserDevicesModificationViewModel model)
        //{

        //}

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
