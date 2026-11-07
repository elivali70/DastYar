using App.Domain.AppServices.User;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Entities;
using App.Domain.Core.User.Enums;
using App.EndPoint.Mvc.ShopUI.Models;
using App.EndPoint.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace App.EndPoint.UI.Controllers.Account
{
    public class AccountController : Controller
    {

        private readonly IExpertAppService _expertAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IExpertAppService expertAppService, ICustomerAppService customerAppService, SignInManager<AppUser> signInManager)
        {
            _expertAppService = expertAppService;
           _customerAppService = customerAppService;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> LogIn()
        {
            await _signInManager.SignOutAsync();
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LogIn(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return LocalRedirect("~/");
                }
                ModelState.AddModelError(string.Empty, "خطا در فرایند لاگین");
            }

            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("~/");
        }
        [AllowAnonymous]
        public IActionResult ExpertRegister()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ExpertRegister(RegisterViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var expertDto = new ExpertRegisterDto()
                {
                    Name = model.Name,
                    Email = model.Email,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    UserName = model.UserName,
                    Password = model.Password,
                };
                var result = await _expertAppService.Register(expertDto, cancellationToken);
                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(expertDto.UserName, expertDto.Password, false, false);
                    return LocalRedirect("~/");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult CustomerRegister()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CustomerRegister(RegisterViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var customerRegisterDto = new CustomerRegisterDto()
                {
                    Name = model.UserName,
                    Email = model.Email,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    UserName = model.UserName,
                    Password = model.Password,
                };
                var result = await _customerAppService.Register(customerRegisterDto, cancellationToken);
                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(customerRegisterDto.UserName, customerRegisterDto.Password, false, false);
                    return LocalRedirect("~/");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return View(model);
        }
    }
    }
