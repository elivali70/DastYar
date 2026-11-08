namespace App.EndPoint.UI.Areas.Admin.Models.AppFile
{
    public class AppFileAdminViewModel
    {
        
            public int Id { get; set; }
            public string Name { get; set; }

            public string Path { get; set; }

            public DateTime? CreatedAt { get; set; }

            public int UserId { get; set; }
            public string? UploaderUserName { get; set; }
            public string FileTypeName { get; set; }
            public string Extention { get; set; }
            public string? Description { get; set; }

    }
}
