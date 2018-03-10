using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LakeLabRemote.DataSource;
using LakeLabRemote.DataSourceAPI;
using LakeLabRemote.Models.ViewModels;
using LakeLabRemote.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class HomeController : Controller
    {
        private LakeLabDbContext _dbContext;
        private ValueStorage _valueStorage;
        private DeviceStorage _deviceStorage;
        private UserManager<AppUser> _userManager;

        public HomeController(LakeLabDbContext context, ValueStorage vs, DeviceStorage ds, UserManager<AppUser> um)
        {
            _dbContext = context;
            _valueStorage = vs;
            _deviceStorage = ds;
            _userManager = um;
        }

        public async Task<IActionResult> Index()
        {
            return View(new HomeIndexViewModel(await _deviceStorage.GetAllDeviceEntitiesForUserAsDictionaryAsync(await _userManager.GetUserAsync(HttpContext.User))));
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> DeleteAllValues()
        {
            await _valueStorage.DeleteAllValuesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}