using App.EndPoint.UI.Areas.Admin.Models.AppFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.EndPoint.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "AdminRole")]
    public class AppFileManagerController : Controller
    {
        private readonly IAppFileAppService _appFileAppService;

        public AppFileManagerController(IAppFileAppService appFileAppService)
        {
            _appFileAppService = appFileAppService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var model= await _appFileAppService.GetAllFileForAdmin(cancellationToken);
            var AppFileAdminList = new List<AppFileAdminViewModel>();
            foreach (var item in model)
            {
            var AppFileAdmin = new AppFileAdminViewModel();
                AppFileAdmin.Id = item.Id;
                AppFileAdmin.Name = item.Name;
                AppFileAdmin.Description = item.Description;
                AppFileAdmin.Path = item.Path;
                AppFileAdmin.FileTypeName = item.FileTypeName;
                AppFileAdmin.UploaderUserName = item.UploaderUserName;
                AppFileAdmin.UserId= item.UserId;
                AppFileAdmin.Extention= item.Extention;
                AppFileAdmin.CreatedAt= item.CreatedAt;
                AppFileAdmin.IsUsed=item.IsUsed;
                AppFileAdminList.Add(AppFileAdmin);
            }
            return View(AppFileAdminList);
           
        }
        public async Task<IActionResult> Search(string keyWord,CancellationToken cancellationToken)
        {
            var model = await _appFileAppService.GetFileForAdmin(keyWord,cancellationToken);
            if (model != null)
            {
            var AppFileAdmin = new AppFileAdminViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Path = model.Path,
                FileTypeName = model.FileTypeName,
                UploaderUserName = model.UploaderUserName,
                UserId = model.UserId,
                Extention = model.Extention,
                CreatedAt = model.CreatedAt,
                IsUsed = model.IsUsed
            };
            return View(AppFileAdmin);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {

            var model = await _appFileAppService.GetFileForAdmin(id, cancellationToken);
            if (model.IsUsed)
            {
                return View();
            }
            else
            {
                await _appFileAppService.Delete(id, cancellationToken);
                return RedirectToAction(nameof(Index));
                
            }
        }
    }
}
