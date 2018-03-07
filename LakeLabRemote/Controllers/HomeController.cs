using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LakeLabRemote.DataSource;
using LakeLabRemote.DataSourceAPI;
using LakeLabRemote.ViewModels;

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class HomeController : Controller
    {
        LakeLabDbContext _dbContext;
        ValueStorage _valueStorage;
        DeviceStorage _deviceStorage;

        public HomeController(LakeLabDbContext context, ValueStorage vs, DeviceStorage ds)
        {
            _dbContext = context;
            _valueStorage = vs;
            _deviceStorage = ds;
        }

        public async Task<IActionResult> Index()
        {
            return View(new HomeIndexViewModel(await _deviceStorage.GetCurrentUsersDevicesAsync()));
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