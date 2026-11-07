using App.Domain.AppServices.Order;
using App.Domain.Core.Order.Contracts.AppServices;
using App.Domain.Core.Order.Dtos;
using App.Domain.Core.Order.Enums;
using App.Domain.Core.Service.Contracts.AppServices;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Entities;
using App.EndPoint.UI.Areas.Admin.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "AdminRole")]
    public class BidController : Controller
    {
        private readonly IBidAppService _bidAppService;
        private readonly IExpertAppService _expertAppService;
       

        public BidController(IBidAppService bidAppService,
            IExpertAppService expertAppService
            )
        {
            _bidAppService = bidAppService;
            _expertAppService = expertAppService;
           
        }
        public async Task< IActionResult> Index(CancellationToken cancellationToken)
        {
          
            var model = await _bidAppService.GetAll(cancellationToken);
            var bids= new List<BidOutPutViewModel>();
            foreach(var item in model)
            {
                var expert = await _expertAppService.Get(item.ExpertId, cancellationToken);
               var bid= new BidOutPutViewModel() {
                   Id=item.Id,
                   CreatedAt=item.CreatedAt,
                   ExpertUserName=expert.UserName,
                   OrderId=item.Id,
                   ProposalStatus=item.ProposalStatus,
                   SuggestedPrice=item.SuggestedPrice,
               };
                bids.Add(bid);
            }
           
            return View(bids);
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

                var model = await _bidAppService.Search(keyWord, cancellationToken);
                var bids = new List<BidOutPutViewModel>();
                foreach (var item in model)
                {
                    var expert = await _expertAppService.Get(item.ExpertId, cancellationToken);
                    var bid = new BidOutPutViewModel()
                    {
                        Id = item.Id,
                        CreatedAt = item.CreatedAt,
                        ExpertUserName = expert.UserName,
                        OrderId = item.Id,
                        ProposalStatus = item.ProposalStatus,
                        SuggestedPrice = item.SuggestedPrice,
                    };
                    bids.Add(bid);
                }

                return View(bids);
            }
            return LocalRedirect("~/");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _bidAppService.Delete(id, cancellationToken);
            return LocalRedirect("~/");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {
            var order = await _bidAppService.Get(id, cancellationToken);
            var expert = await _expertAppService.Get(order.ExpertId, cancellationToken);
            
            var bidViewModel = new BidOutPutViewModel()
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                ExpertUserName = expert.UserName,
                OrderId = order.Id,
                ProposalStatus = order.ProposalStatus,
                SuggestedPrice = order.SuggestedPrice,
            };
            ViewBag.ProposalStatus = Enum.GetNames<ProposalStatusEnum>();
            return View(bidViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BidOutPutViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var expertId=await _expertAppService.Get(model.ExpertUserName, cancellationToken);
                var bidDto = new BidDto()
                {
                    Id = model.Id,
                   CreatedAt=model.CreatedAt,
                   ExpertId = expertId,
                   OrderId=model.OrderId,
                   ProposalStatus=model.ProposalStatus,
                   SuggestedPrice=model.SuggestedPrice,
                   
                };
                await _bidAppService.Update(bidDto, cancellationToken);
                return LocalRedirect("~/");
            }
            else
            {
                ViewBag.ProposalStatus = Enum.GetNames<ProposalStatusEnum>();
                return View(model);
            }
        }


}
}
