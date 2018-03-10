using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LakeLabRemote.Models;
using LakeLabRemote.DataSource;
using System.Threading.Tasks;
using LakeLabRemote.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using LakeLabRemote.DataSourceAPI;

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
        private DeviceStorage _deviceStorage;
        public AdminController(UserManager<AppUser> usrMgr, IUserValidator<AppUser> userValid, IPasswordValidator<AppUser> passValid, IPasswordHasher<AppUser> passwordHash, LakeLabDbContext dbContext, DeviceStorage deviceStorage)
        {
            _userManager = usrMgr;
            _userValidator = userValid;
            _passwordValidator = passValid;
            _passwordHasher = passwordHash;
            _dbContext = dbContext;
            _deviceStorage = deviceStorage;
        }

        public async Task<ViewResult> Index()
        {
            List<AppUserViewModel> appUserViewModels = new List<AppUserViewModel>();
            foreach(AppUser user in _userManager.Users)
            {
                appUserViewModels.Add(new AppUserViewModel(user, await _deviceStorage.GetAllDeviceEntitiesForUserAsUnsortedListAsync(user)));
            }

            return View(appUserViewModels);
        }

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
            List<Device> accessibleDevices = new List<Device>();
            List<Device> notAccessibleDevices = new List<Device>();
            foreach (Device device in _dbContext.Devices)
            {
                List<Device> list = await _deviceStorage.IsDeviceAccessibleForUserAsync(user, device) ? accessibleDevices : notAccessibleDevices;
                list.Add(device);
            }

            return View(new AppUserDevicesEditViewModel(user, accessibleDevices, notAccessibleDevices));
        }

        [HttpPost]
        public async Task<IActionResult> EditDevices(AppUserDevicesModificationViewModel model)
        {
            List<AppUserDevice> appUserDeviceAssociationsToAdd = new List<AppUserDevice>();
            if(model.DeviceIdsToAdd != null && model.DeviceIdsToAdd.Length != 0)
            {
                foreach (Guid id in model.DeviceIdsToAdd)
                {
                    appUserDeviceAssociationsToAdd.Add(new AppUserDevice(model.UserId, id));
                }
                await _dbContext.AppUserDeviceAssociation.AddRangeAsync(appUserDeviceAssociationsToAdd);
            }

            List<AppUserDevice> toDelete = new List<AppUserDevice>();
            if (model.DeviceIdsToRemove != null && model.DeviceIdsToRemove.Length != 0)
            {
                foreach (Guid id in model.DeviceIdsToRemove)
                {
                    toDelete.AddRange(_dbContext.AppUserDeviceAssociation.Where(p => p.AppUserId == model.UserId).Where(d => d.DeviceId == id));
                }

                _dbContext.AppUserDeviceAssociation.RemoveRange(toDelete);
                await _dbContext.SaveChangesAsync();
            }
                
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
