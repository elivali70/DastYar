using System;
using System.Collections.Generic;
using System.Text;

namespace App.EndPoint.UI.Areas.Admin.Models.AppFile

{
    public interface IAppFileStorageService
    {
        string CreatFolder(int userId, string name);
        Task<string> SaveFile(IFormFile file, string folderPath);
    }
}
