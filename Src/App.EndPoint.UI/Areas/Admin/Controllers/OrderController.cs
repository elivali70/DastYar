using App.Domain.AppServices.AppFile;
using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enums;
using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.Service.Contracts.Repositories;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Contracts.Repositories;
using App.EndPoint.UI.Areas.Admin.Models.AppFile;
using App.EndPoint.UI.Areas.Admin.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;

namespace App.EndPoint.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "AdminRole")]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IServiceAppService _serviceAppService;
        private readonly IExpertAppService _expertAppService;
        private readonly ICustomerAppService _customerAppService;
       

        public OrderController(IOrderAppService orderAppService,
            IServiceAppService serviceAppService,
            IExpertAppService expertAppService,
            ICustomerAppService customerAppService
           
           )
        {
            _orderAppService = orderAppService;
            _serviceAppService = serviceAppService;
            _expertAppService = expertAppService;
            _customerAppService = customerAppService;
           
        }
        public async Task<IActionResult> Index(string? keyWord, CancellationToken cancellationToken)
        {
            var model = await _orderAppService.GetAll(keyWord, cancellationToken);
            var orderViewModel = model.Select(s => new OrderViewModel()
            {
                Id = s.Id,
                BasePrice = s.BasePrice,
                CustomerId = s.CustomerId,
                Discription = s.Discription,
                PrefferedDateTime = s.PrefferedDateTime,
                ExpertId = s.ExpertId,
                ServiceId = s.ServiceId,
                OrderStatus = s.OrderStatus,
                CreatedAt = s.CreatedAt,

            }).ToList();

            return View(orderViewModel);
        }
        public async Task<IActionResult> Search(string keyWord, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                ModelState.AddModelError(
                    nameof(keyWord),
                    "عبارت جستجو الزامی است."
                );

                return View("Index");
            }
            if (ModelState.IsValid)
            {

                var model = await _orderAppService.GetAll(keyWord, cancellationToken);
                var orderViewModel = model.Select(s => new OrderViewModel()
                {
                    Id = s.Id,
                    BasePrice = s.BasePrice,
                    CustomerId = s.CustomerId,
                    Discription = s.Discription,
                    PrefferedDateTime = s.PrefferedDateTime,
                    ExpertId = s.ExpertId,
                    ServiceId = s.ServiceId,
                    OrderStatus = s.OrderStatus,
                    CreatedAt = s.CreatedAt,

                }).ToList();

                return View(orderViewModel);
            }
            return LocalRedirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _orderAppService.Delete(id, cancellationToken);
            return LocalRedirect("~/");
        }
        [HttpPost]
        public async Task<IActionResult> Update(OrderViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {

                var orderDto = new OrderDto()
                {
                    Id = model.Id,
                    BasePrice = model.BasePrice,
                    CustomerId = model.CustomerId,
                    Discription = model.Discription,
                    PrefferedDateTime = model.PrefferedDateTime,
                    ExpertId = model.ExpertId,
                    ServiceId = model.ServiceId,
                    OrderStatus = model.OrderStatus,
                    CreatedAt = model.CreatedAt,
                };
                await _orderAppService.Update(orderDto, cancellationToken);
                return LocalRedirect("~/");
            }
            else
            {
                ViewBag.OrderStatus = Enum.GetNames<OrderStatusEnum>();
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.Get(id, cancellationToken);
            var orderViewModel = new OrderViewModel()
            {
                Id = order.Id,
                BasePrice = order.BasePrice,
                CustomerId = order.CustomerId,
                Discription = order.Discription,
                CreatedAt = order.CreatedAt,
                ExpertId = order.ExpertId,
                OrderStatus = order.OrderStatus,
                PrefferedDateTime = order.PrefferedDateTime,
                ServiceId = order.ServiceId,
            };
            ViewBag.OrderStatus = Enum.GetNames<OrderStatusEnum>();
            return View(orderViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.OrderStatus = Enum.GetNames<OrderStatusEnum>();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderInPutViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var serviceId = await _serviceAppService.GetId(model.ServiceName, cancellationToken);
                var customerId = await _customerAppService.GetId(model.CustomerPhoneNumber, cancellationToken);
                var expertId = await _expertAppService.Get(model.ExpertUserName, cancellationToken);
                var orderDto = new OrderDto()
                {
                    ServiceId = serviceId,
                    CustomerId = customerId,
                    ExpertId = expertId,
                    BasePrice = model.BasePrice,
                    Discription = model.Discription,
                    PrefferedDateTime = model.PrefferedDateTime,
                    OrderStatus = model.OrderStatus
                };
                var result = await _orderAppService.Add(orderDto, cancellationToken);
                return RedirectToAction("Index");
            }
            ViewBag.OrderStatus = Enum.GetNames<OrderStatusEnum>();
            return View(model);
        }
        public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken)
        {
            var order = await _orderAppService.Get(id, cancellationToken);
            var customer = await _customerAppService.Get(order.CustomerId, cancellationToken);
            var customerUserName = customer.UserName;
            var expert = await _expertAppService.Get(order.ExpertId, cancellationToken);
            var expertUserName = expert.UserName;
            var service = await _serviceAppService.Get(order.ServiceId, cancellationToken);
            var serviceName = service.Name;
            var orderOutPut = new OrderOutPutViewModel()
            {
                Id = id,
                CustomerUserName = customerUserName,
                BasePrice = order.BasePrice,
                OrderStatus = order.OrderStatus,
                CreatedAt = order.CreatedAt,
                Discription = order.Discription,
                ExpertUserName = expertUserName,
                ServiceName = serviceName,
                PrefferedDateTime = order.PrefferedDateTime,
            };
            return View(orderOutPut);
        }
    }
}