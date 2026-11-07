using App.Domain.Core.Order.Contracts.AppServices;
using App.EndPoint.UI.Areas.Admin.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoint.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "AdminRole")]
    public class CommentController : Controller
    {
        private readonly ICommentAppService _commentAppService;

        public CommentController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var comments= await _commentAppService.GetAll(cancellationToken);
           var commentsViewModel= comments.Select(s => new CommentViewModel()
            {
                Id = s.Id,
                CreatedAt= s.CreatedAt,
                Discription=s.Discription,
                OrderId= s.OrderId,
                UserId= s.UserId,

            }).ToList();
            return View(commentsViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
             await _commentAppService.Delete(id,cancellationToken);
           
            return View("Index");
        }

    }
}
