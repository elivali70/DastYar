using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Enities;
using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Entities;
using App.EndPoint.UI.Areas.Customer.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.EndPoint.UI.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "CustomerRole")]
    public class OrderController : Controller
    {
        private readonly IServiceAppService _serviceAppService;
        private readonly IOrderAppService _orderAppService;
        private readonly IOrderProposalAppService _orderProposalAppService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IExpertAppService _expertAppService;
        private readonly IBidAppService _bidAppService;
        private readonly ICommentAppService _commentAppService;
        private readonly ICustomerAppService _customerAppService;

        public OrderController(IServiceAppService serviceAppService,
            IOrderAppService orderAppService,
            IOrderProposalAppService orderProposalAppService,
            UserManager<AppUser> userManager,
            IExpertAppService expertAppService,
            IBidAppService bidAppService,
            ICommentAppService commentAppService,
            ICustomerAppService customerAppService)
        {
            _serviceAppService = serviceAppService;
            _orderAppService = orderAppService;
            _orderProposalAppService = orderProposalAppService;
            _userManager = userManager;
            _expertAppService = expertAppService;
            _bidAppService = bidAppService;
            _commentAppService = commentAppService;
            _customerAppService = customerAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var customer= _customerAppService.Get(userId, cancellationToken);
            var customerId = customer.Id;
            var order = await _orderAppService.GetAllForCustomer(customerId, cancellationToken);
            var orderList = new List<OrderOutPutCustomeViewModel>();
            foreach (var item in order)
            {
                var orderProposal = await _orderProposalAppService.GetByOrderId(item.Id, cancellationToken);
                var expert = await _expertAppService.Get(item.ExpertId, cancellationToken);
                var orderViewModel = new OrderOutPutCustomeViewModel()
                {
                    CustomerId = customerId,
                    Id = item.Id,
                    OrderStatus = item.OrderStatus,
                    BasePrice = item.BasePrice,
                    Discription = item.Discription,
                    CreatedAt = item.CreatedAt,
                    DurationTime = orderProposal.DurationTime,
                    PrefferedDateTime = item.PrefferedDateTime,
                    StatrtedDateTime = orderProposal.StatrtedDateTime,
                    ServiceId = item.ServiceId,
                    SuggestedPrice = orderProposal.SuggestedPrice,
                    ExpertUserName = expert.UserName,
                    ProposalStatus = orderProposal.ProposalStatus,
                };
                orderList.Add(orderViewModel);
            }
            return View(orderList);
        }
        //public async Task<IActionResult> Detail(int orderId,CancellationToken cancellationToken)
        //{
        //    var customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    var order = await _orderAppService.GetForCustomer(customerId,orderId ,cancellationToken);
        //    var orderProposal = await _orderProposalAppService.GetByOrderId(orderId, cancellationToken);
        //    var expert = await _expertAppService.Get(order.ExpertId, cancellationToken);
        //    var orderViewModel = new OrderOutPutCustomeViewModel()
        //    {
        //        CustomerId = customerId,
        //        Id = orderId,
        //        OrderStatus = order.OrderStatus,
        //        BasePrice = order.BasePrice,
        //        Discription = order.Discription,
        //        CreatedAt = order.CreatedAt,
        //        DurationTime = orderProposal.DurationTime,
        //        PrefferedDateTime = order.PrefferedDateTime,
        //        StatrtedDateTime = orderProposal.StatrtedDateTime,
        //        ServiceId = order.ServiceId,
        //        SuggestedPrice = orderProposal.SuggestedPrice,
        //        ExpertUserName = expert.UserName,
        //ProposalStatus=orderProposal.ProposalStatus,
        //    };
        //    return View(orderViewModel);
        //}

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var model = new OrderCustomerViewModel();
            var userId= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var customer = _customerAppService.Get(userId, cancellationToken);
            model.CustomerId = customer.Id;
            var service = await _serviceAppService.GetAll(cancellationToken);
            ViewBag.Service = service;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderCustomerViewModel model, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "اطلاعات ورودی نامعتبر");
                var service = await _serviceAppService.GetAll(cancellationToken);
                ViewBag.Service = service;
                return View(model);
            }
            if (model.CustomerId != int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                ModelState.AddModelError(string.Empty, "خطا در فرایند احراز هویت");
                return LocalRedirect("~/");
            }
            if (model.PrefferedDateTime <= DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "زمان انجام کار باید در آینده باشد");
                var service = await _serviceAppService.GetAll(cancellationToken);
                ViewBag.Service = service;
                return View(model);
            }
            var services = await _serviceAppService.GetAll(cancellationToken);

            if (!services.Any(s => s.Id == model.ServiceId))
            {
                ModelState.AddModelError(
                    nameof(model.ServiceId),
                    "خدمت انتخاب‌شده معتبر نیست.");
                var service = await _serviceAppService.GetAll(cancellationToken);
                ViewBag.Service = service;
                return View(model);
            }
            else
            {
                model.CustomerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _orderAppService.Set(model.CustomerId, model.Discription, model.PrefferedDateTime, model.ServiceId, cancellationToken);
                TempData["SuccessMessage"] = $"سفارش شما با شناسه {result} با موفقیت ثبت شد.";

                return RedirectToAction(nameof(Index));
            }

        }

        [HttpGet]
        public async Task<IActionResult> Bids(int orderId, CancellationToken cancellationToken)
        {
            var bidViewModel = new List<BidViewModel>();
            var bids = await _bidAppService.GetAllByOrderId(orderId, cancellationToken);
            foreach (var bid in bids)
            {
                var expert = await _expertAppService.Get(bid.ExpertId, cancellationToken);
                var model = new BidViewModel()
                {
                    Id = bid.Id,
                    CreatedAt = bid.CreatedAt,
                    OrderId = orderId,
                    ProposalStatus = bid.ProposalStatus,
                    SuggestedPrice = bid.SuggestedPrice,
                };
                if (expert != null)
                {
                    model.ExpertId = expert.Id;
                    model.ExpertPhoneNumber = expert.PhoneNumber;
                    model.ExpertUserName = expert.UserName;
                    bidViewModel.Add(model);
                }
            }
            return View(bidViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SelectBid(int bidId, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var customer = _customerAppService.Get(userId, cancellationToken);
           var customerId = customer.Id;
            await _orderAppService.SelectBid(customerId,bidId, cancellationToken);
           return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AcceptedOrders(CancellationToken cancellationToken)
        {
            var customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = await _orderAppService.GetAccepted(customerId, cancellationToken);
            var orderList = new List<OrdersAcceptedViewModel>();
            foreach (var item in order)
            {
                var comment= await _commentAppService.GetByOrderId(item.Id, cancellationToken);
                var expert = await _expertAppService.Get(item.ExpertId, cancellationToken);
                var orderViewModel = new OrdersAcceptedViewModel()
                {
                    Id = item.Id,
                    Comment = comment.Discription,
                    CommentCreatedTime=comment.CreatedAt,
                    CreatedAt = item.CreatedAt,
                    ServiceId = item.ServiceId,
                    ExpertUserName = expert.UserName,
                    OrderId = item.Id,
                };
                orderList.Add(orderViewModel);
            }
            return View(orderList);
        }

        [HttpGet]
        public async Task<IActionResult> AddComment(int orderId,CancellationToken cancellationToken)
        {
            var commentViewModel = new CommentInPutViewModel()
            {
                OrderId = orderId,
                Discription = string.Empty,
            };
            
            return View(commentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentInPutViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var customer = _customerAppService.Get(userId, cancellationToken);
                var customerId = customer.Id;
                await _commentAppService.Add(customerId, model.OrderId, model.Discription, cancellationToken);
                return LocalRedirect("~/");
            }
            ModelState.AddModelError(string.Empty, "ثیت دیدگاه را مجددا انجام دهید");
            return View(model);
        }
     
    }

}
