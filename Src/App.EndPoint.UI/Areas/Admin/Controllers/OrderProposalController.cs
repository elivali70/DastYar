using App.Domain.AppServices.Order;
using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.User.Contracts.AppServices;
using App.EndPoint.UI.Areas.Admin.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "AdminRole")]
    public class OrderProposalController : Controller
    {
        private readonly IOrderProposalAppService _orderProposalAppService;
        private readonly IExpertAppService _expertAppService;
        private readonly IOrderAppService _orderAppService;

        public OrderProposalController(IOrderProposalAppService orderProposalAppService,
            IExpertAppService expertAppService,
            IOrderAppService orderAppService
            )
        {
            _orderProposalAppService = orderProposalAppService;
            _expertAppService = expertAppService;
            _orderAppService = orderAppService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            var model = await _orderProposalAppService.GetAll(cancellationToken);
            var orderProposals = new List<OrderProposalOutPutViewModel>();
            foreach (var item in model)
            {
                var expert = await _expertAppService.Get(item.ExpertId, cancellationToken);
                var orderProposal = new OrderProposalOutPutViewModel()
                {
                    Id = item.Id,
                    CreatedAt = item.CreatedAt,
                    ExpertUserName = expert.UserName,
                    OrderId = item.Id,
                    ProposalStatus = item.ProposalStatus,
                    SuggestedPrice = item.SuggestedPrice,
                    Discription=item.Discription,
                    DurationTime = item.DurationTime,
                    StatrtedDateTime = item.StatrtedDateTime,
                };
                orderProposals.Add(orderProposal);
            }

            return View(orderProposals);
        }
        public async Task<IActionResult> Search(int orderId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(orderId.ToString()))
            {
                ModelState.AddModelError(
                    nameof(orderId),
                    "عبارت جستجو الزامی است."
                );

                return View("Index");
            }
            if (ModelState.IsValid)
            {

                var model = await _orderProposalAppService.GetByOrderId(orderId, cancellationToken);
              
                    var expert = await _expertAppService.Get(model.ExpertId, cancellationToken);
                    var orderProposal = new OrderProposalOutPutViewModel()
                    {
                        Id = model.Id,
                        CreatedAt = model.CreatedAt,
                        ExpertUserName = expert.UserName,
                        OrderId = orderId,
                        ProposalStatus = model.ProposalStatus,
                        SuggestedPrice = model.SuggestedPrice,
                        Discription = model.Discription,
                        DurationTime = model.DurationTime,
                        StatrtedDateTime = model.StatrtedDateTime,
                    };

                return View(orderProposal);
            }
            return LocalRedirect("~/");
        }
    }
}