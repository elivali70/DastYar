using App.Domain.AppServices.User;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Enums;
using App.EndPoint.UI.Areas.Admin.Models;
using App.EndPoint.UI.Areas.Admin.Models.AppFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.IO;
using System.Threading.Tasks;

namespace App.EndPoint.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "AdminRole")]
    public class UserManagerController : Controller
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly RoleManager<IdentityUser<int>> _roleManager;
        private readonly IAdminAppService _adminAppService;
        private readonly IAppFileStorageService _appFileStorageService;
        private readonly IAppFileAppService _appFileAppService;
        private readonly IFileTypeAppService _fileTypeAppService;
        private readonly ILogger<UserManagerController> _logger;

        public UserManagerController(SignInManager<IdentityUser<int>> signInManager,
            UserManager<IdentityUser<int>> userManager,
            RoleManager<IdentityUser<int>> roleManager,
           IAdminAppService adminAppService,
           IAppFileStorageService appFileStorageService,
           IAppFileAppService appFileAppService,
           IFileTypeAppService fileTypeAppService,
            ILogger<UserManagerController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _adminAppService = adminAppService;
           _appFileStorageService = appFileStorageService;
            _appFileAppService = appFileAppService;
           _fileTypeAppService = fileTypeAppService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            _logger.LogTrace("start method {methodName}", nameof(Index));
            var users = await _adminAppService.GetAll(cancellationToken);
            _logger.LogInformation("List of users has read from database");


            var model = new List<UserViewModel>();
            foreach (var user in users)
            {
                model.Add(new UserViewModel
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Address = user.Address,
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber,
                   Role=user.Role.ToString(),

                });
            }
            _logger.LogTrace("finish method {methodName}", nameof(Index));

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(int id, CancellationToken cancellationToken)
        {
            _logger.LogTrace("start method {methodName}", nameof(ChangePassword));
            var user = await _adminAppService.Get(id, cancellationToken);
            var model = new ChangePasswordViewModel()
            {
                Id = id,
                UserName = user.UserName,
            };
            _logger.LogTrace("finish method {methodName}", nameof(ChangePassword));

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {

                _logger.LogTrace("start method {methodName}", nameof(ChangePassword));
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                _logger.LogDebug("find user in {methodName}", nameof(ChangePassword));

                if (user == null)
                {
                    return NotFound();
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                _logger.LogDebug("current pass find in {methodName}", nameof(ChangePassword));

                var changePass = await _userManager.ChangePasswordAsync(user, token, model.Password);
                _logger.LogDebug("passWord is changed {methodName}", nameof(ChangePassword));

                if (!changePass.Succeeded)
                {
                    foreach (var error in changePass.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View(error);
                    }
                }
                _logger.LogTrace("finish method {methodName}", nameof(ChangePassword));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var role = await _userManager.GetRolesAsync(user);
            var model = new UpdateUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = role.ToList(),
            };
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel model, int id,CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                _logger.LogTrace("start methode {action name}", nameof(Update));
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user == null)
                {
                    return NotFound();
                }
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                var update = await _userManager.UpdateAsync(user);
                if (!update.Succeeded)
                {
                    foreach (var error in update.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                    return View(model);
                }
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var changePass = await _userManager.ChangePasswordAsync(user, token, model.Password);
                    if (!changePass.Succeeded)
                    {
                        foreach (var error in changePass.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                            return View(model);
                        }
                        ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                        return View(model);
                    }
                    if (model.File != null && model.DiscriptionFile!=null)
                    {
                        await SaveFile(model.Id, model.Role.ToString(), model.File, model.DiscriptionFile, cancellationToken);
                    }
                }
                var role = await _userManager.GetRolesAsync(user);
                if (update.Succeeded)
                {
                    if (model.Role == null || !model.Role.Contains(role.ToString())) ;
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.ToString());
                    }
                    if (model.Role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.ToString());
                        _logger.LogInformation("The user Update By Admin with id={id}", id);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in update.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                    ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                    return View(model);
                }

            }
            _logger.LogTrace("finish methode {action name}", nameof(Update));
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                _logger.LogTrace("start methode {action name}", nameof(Update));
                var appUserDto = new AppUserDto()
                {
                   
                    Name = model.Name,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    UserName = model.UserName,
                };
                if (model.Role.ToString() == RoleEnum.CustomerRole.ToString())
                {
                    appUserDto.Role = RoleEnum.CustomerRole;
                }
                if (model.Role.ToString() == RoleEnum.ExpertRole.ToString())
                {
                    appUserDto.Role = RoleEnum.ExpertRole;
                }
                _logger.LogInformation("The {user} Add By Admin with", nameof(model.UserName));
                var result = await _adminAppService.Add(appUserDto, cancellationToken);
                var user= await _userManager.FindByEmailAsync(model.Email);
                var userId = user.Id;

                _logger.LogTrace("finish methode {action name}", nameof(Update));
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    _logger.LogWarning("the {action name} of User is not succeed", nameof(Create));
                    ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                    return View(model);
                }
             await  SaveFile(userId,model.Role.ToString(),model.File,model.DiscriptionFile,cancellationToken);
                //var path = _appFileStorageService.CreatFolder(userId, model.Role.ToString());
                //var userPath = _appFileStorageService.SaveFile(model.File, path);
                //var fileTypeDto = new FileTypeDto()
                //{
                //    Extention=model.File.Extention,
                //    Name=model.FileType.Name,
                //};
                //var fileTypeId = await _fileTypeAppService.Add(fileTypeDto, cancellationToken);
                //var appFileDto = new AppFileDto()
                //{
                //    Name = model.File.Name,
                //    Description = model.FileType.Description,
                //    Path=path,
                //    UserId=userId,
                //    FileTypeId=fileTypeId,
                //};
                //var appFile= await _appFileAppService.Add(appFileDto, cancellationToken);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task SaveFile(int userId, string role, IFormFile file, string discriptionFile,CancellationToken cancellationToken )
        {
            var path = _appFileStorageService.CreatFolder(userId, role);
            var userPath = _appFileStorageService.SaveFile(file, path);
            var fileTypeDto = new FileTypeDto()
            {
                Extention = Path.GetExtension(path),
                Name = file.Name,
            };
            var fileTypeId = await _fileTypeAppService.Add(fileTypeDto, cancellationToken);
            var appFileDto = new AppFileDto()
            {
                Name =file.Name,
                Description = discriptionFile,
                Path = path,
                UserId = userId,
                FileTypeId = fileTypeId,
            };
            var appFile = await _appFileAppService.Add(appFileDto, cancellationToken);
        }
    }
}
