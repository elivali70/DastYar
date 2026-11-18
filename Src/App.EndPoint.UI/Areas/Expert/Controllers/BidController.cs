using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Enities;
using App.Domain.Core.Order.Enums;
using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Entities;
using App.EndPoint.UI.Areas.Admin.Models.Order;
using App.EndPoint.UI.Areas.Expert.Models.Comment;
using App.EndPoint.UI.Areas.Expert.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.EndPoint.UI.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Roles = "ExpertRole")]
    public class BidController : Controller
    {
        private readonly IServiceAppService _serviceAppService;
        private readonly IOrderAppService _orderAppService;
        private readonly IOrderProposalAppService _orderProposalAppService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IExpertAppService _expertAppService;
        private readonly IBidAppService _bidAppService;
        private readonly ICommentAppService _commentAppService;

        public BidController(IServiceAppService serviceAppService,
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
        public async Task<IActionResult> MyBids(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expert = await _expertAppService.Get(userId, cancellationToken);
            var bids = await _bidAppService.GetMyBids(expert.Id, cancellationToken);
            var model = bids.Select(x => new BidsOutPutViewModel()
            {
                CreatedAt = x.CreatedAt,
                SuggestedPrice = x.SuggestedPrice,
                ExpertId = expert.Id,
                Id = x.Id,
                OrderId = x.OrderId,
                ProposalStatus = x.ProposalStatus,
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBid(BidsOutPutViewModel model, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expert = await _expertAppService.Get(userId, cancellationToken);
            if (expert == null)
                return Forbid();
            var bid = await _bidAppService.Get(model.Id, cancellationToken);
            if (bid == null)
                return NotFound();
            if (bid.ExpertId != expert.Id)
                return Forbid();
            if (bid.ProposalStatus == ProposalStatusEnum.Pending)
            {
                TempData["ErrorMessage"] = "فقط پیشنهادهای در انتظار را می‌توانید حذف کنید.";

                return RedirectToAction(nameof(MyBids));
            }
            else
            {
                await _bidAppService.DeleteSuggestedBid( expert.Id, bid.Id,bid.OrderId, cancellationToken);

                TempData["SuccessMessage"] ="پیشنهاد شما با موفقیت پس گرفته شد.";

                return RedirectToAction(nameof(MyBids));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Comment(BidsOutPutViewModel model, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expert = await _expertAppService.Get(userId, cancellationToken);
            if (model.ProposalStatus == ProposalStatusEnum.Accepted)
            {
                var order = await _orderAppService.Get(model.OrderId, cancellationToken);
                if (order.OrderStatus == OrderStatusEnum.Completed)
                {
                    var comment = new ExpertCommentViewModel()
                    {
                        OrderId = model.OrderId,
                        Discription = null,

                    };
                    return View(comment);
                }

                var errorMesage = "You Cant Add Comment to this action";
                return View("MyBids", errorMesage);
            }
            else
            {

                var errorMesage = "You Cant Add Comment to this action";
                return View("MyBids",errorMesage);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Comment(ExpertCommentViewModel model, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var expert = await _expertAppService.Get(userId, cancellationToken);
            if (ModelState.IsValid)
            {
                await _commentAppService.Add(userId,model.OrderId,model.Discription,cancellationToken);
                return RedirectToAction("MyBids");
            }
            ModelState.AddModelError(string.Empty, "Please Try Again to write your Comment");
            return View(model);
        }
    }
}
