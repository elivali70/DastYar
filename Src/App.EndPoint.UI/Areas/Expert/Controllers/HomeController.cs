using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Entities;
using App.EndPoint.UI.Areas.Admin.Models.AppFile;
using App.EndPoint.UI.Areas.Customer.Models.UpdateProfile;
using App.EndPoint.UI.Areas.Expert.Models.Home;
using App.EndPoint.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace App.EndPoint.Mvc.ShopUI.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "ExpertRole")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IExpertAppService _expertAppService;
        private readonly IAppFileStorageService _appFileStorageService;
        private readonly IAppFileAppService _appFileAppService;
        private readonly IFileTypeAppService _fileTypeAppService;
        private readonly IServiceAppService _serviceAppService;

        public HomeController(UserManager<AppUser> userManager,
            IExpertAppService expertAppService,
            IAppFileStorageService appFileStorageService,
            IAppFileAppService appFileAppService,
            IFileTypeAppService fileTypeAppService,
            IServiceAppService serviceAppService)
        {
            _userManager = userManager;
            _expertAppService = expertAppService;
            _appFileStorageService = appFileStorageService;
            _appFileAppService = appFileAppService;
            _fileTypeAppService = fileTypeAppService;
            _serviceAppService = serviceAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteService(int userId, CancellationToken cancellationToken)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();
            if (id == userId)
            {
            var expert = await _expertAppService.Get(id, cancellationToken);
                if (expert == null)
                    return NotFound();
                var services = await _serviceAppService.GetAll(cancellationToken);
            var serviceList= services.Select(x=>new SelectListItem
            {
               Value= x.Id.ToString(),
               Text= x.Name,
            }).ToList();
            if (user == null)
            {
                return NotFound();
            }
            var model = new ExpertFavoriteServiceViewModel()
            {
                UserId = userId,
                ExpertId = expert.Id,
               Services=serviceList,

            };
            return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> FavoriteService(ExpertFavoriteServiceViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound();
            if (id == model.UserId)
            {
                var expert = await _expertAppService.Get(id, cancellationToken);
                if (expert == null)
                    return NotFound();
               
                await _expertAppService.AddFavoriteServices(expert.Id,model.SelectedServiceIds, cancellationToken);
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
            }
             ModelState.AddModelError(string.Empty, errorMessage: "خطا در وارد کردن اطلاعات");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int userId, CancellationToken cancellationToken)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var expert = await _expertAppService.Get(userId, cancellationToken);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UpdateExpertProfileViewModel()
            {
                UserId = userId,
                Address = user.Address,
                Email = user.Email,
                LastName = user.LastName,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateExpertProfileViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var pathFile = _appFileStorageService.CreatFolder(model.UserId, "Customer");
                var filePath = _appFileStorageService.SaveFile(model.File, pathFile);
                var fileTypeDto = new FileTypeDto()
                {
                    Extention = Path.GetExtension(pathFile),
                    Name = model.File.ContentType,
                };
                var fileTypeId = await _fileTypeAppService.GetOrAdd(fileTypeDto, cancellationToken);

                var appFileDto = new AppFileDto()
                {
                    Name = model.FileName,
                    Path = pathFile,
                    CreatedAt = DateTime.Now,
                    UserId = model.UserId,
                    Description = model.DiscriptionFile,
                    FileTypeId = fileTypeId,
                };
                var fileId = await _appFileAppService.Add(appFileDto, cancellationToken);
                await _expertAppService.Update(model.UserId, model.Name, model.PhoneNumber, model.Email, model.UserName, model.Address, fileId, cancellationToken);
                return View("Index");
            }
            else
                ModelState.AddModelError(string.Empty, errorMessage: "خطا در فرایند به روزرسانی مجددا تلاش فرمایید.");
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
