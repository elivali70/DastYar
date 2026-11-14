using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.User.Entities;
using App.EndPoint.Mvc.ShopUI.Models;
using App.EndPoint.UI.Areas.Admin.Models.AppFile;
using App.EndPoint.UI.Areas.Customer.Models.Order;
using App.EndPoint.UI.Areas.Customer.Models.UpdateProfile;
using App.EndPoint.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace App.EndPoint.Mvc.ShopUI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "CustomerRole")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerAppService _customerAppService;
        private readonly IAppFileStorageService _appFileStorageService;
        private readonly IAppFileAppService _appFileAppService;
        private readonly IFileTypeAppService _fileTypeAppService;

        public HomeController(UserManager<AppUser> userManager,
            ICustomerAppService customerAppService,
            IAppFileStorageService appFileStorageService,
            IAppFileAppService appFileAppService,
            IFileTypeAppService fileTypeAppService)
        {
            _userManager = userManager;
            _customerAppService = customerAppService;
            _appFileStorageService = appFileStorageService;
            _appFileAppService = appFileAppService;
            _fileTypeAppService = fileTypeAppService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int userId, CancellationToken cancellationToken)
        {
            var id= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var model = new UpdateProfileViewModel()
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
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var pathFile = _appFileStorageService.CreatFolder(model.UserId, "Customer");
                var filePath = _appFileStorageService.SaveFile(model.File, pathFile);
                var fileTypeDto = new FileTypeDto()
                {
                    Extention = Path.GetExtension(pathFile),
                    Name = model.File.Name,
                };
                var fileTypeId = await _fileTypeAppService.Add(fileTypeDto, cancellationToken);

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
                await _customerAppService.Update(model.UserId, model.Name, model.PhoneNumber, model.Email, model.UserName, model.Address, fileId, cancellationToken);
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
