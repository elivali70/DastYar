using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Enities;
using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Entities;
using App.EndPoint.UI.Areas.Customer.Models.Order;
using App.EndPoint.UI.Areas.Expert.Models.Home;
using App.EndPoint.UI.Areas.Expert.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.EndPoint.UI.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "ExpertRole")]
    public class OrderController : Controller
    {
        private readonly IServiceAppService _serviceAppService;
        private readonly IOrderAppService _orderAppService;
        private readonly IOrderProposalAppService _orderProposalAppService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IExpertAppService _expertAppService;
        private readonly IBidAppService _bidAppService;
        private readonly ICommentAppService _commentAppService;

        public OrderController(IServiceAppService serviceAppService,
            IOrderAppService orderAppService,
            IOrderProposalAppService orderProposalAppService,
            UserManager<AppUser> userManager,
            IExpertAppService expertAppService,
            IBidAppService bidAppService,
            ICommentAppService commentAppService)
        {
            _serviceAppService = serviceAppService;
            _orderAppService = orderAppService;
            _orderProposalAppService = orderProposalAppService;
            _userManager = userManager;
            _expertAppService = expertAppService;
            _bidAppService = bidAppService;
            _commentAppService = commentAppService;

        }

        [HttpGet]
        public async Task<IActionResult> OpenOrderList(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expert = await _expertAppService.Get(userId, cancellationToken);
            var expertOpenOrder = await _orderAppService.GetOpenOrders(expert.Id, cancellationToken);
           var model= expertOpenOrder.Select(m=>new OpenOrderOutPutViewModel()
            {
                OrderStatus=m.OrderStatus,
                Discription=m.Discription,
                PrefferedDateTime=m.PrefferedDateTime,
                BasePrice=m.BasePrice,
                CreatedAt=m.CreatedAt,
                CustomerAddress=m.CustomerAddress,
                CustomerLastName=m.CustomerLastName,
                CustomerPhoneNumber=m.CustomerPhoneNumber,
                CustomerUserName=m.CustomerUserName,
                ExpertUserName=m.ExpertUserName,
                ServiceName=m.ServiceName,
                OrderId=m.OrderId,
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ChooseOrder(int orderId,CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expert = await _expertAppService.Get(userId, cancellationToken);
            var model = new SuggestedPriceViewModel()
            {
                OrderId = orderId,
                ExpertId = expert.Id,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChooseOrder(SuggestedPriceViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expert = await _expertAppService.Get(userId, cancellationToken);
            var bidId=  await _orderAppService.SelectOrder(expert.Id,model.OrderId,model.SuggestedPrice,cancellationToken);
                return View("BidId", bidId);
            }
            return View(model);
        }

       

    }

}
