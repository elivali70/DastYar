using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.AppFile.Dtos
{
    public class AppFileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Path { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public int UserId { get; set; }
        public int FileTypeId { get; set; }
        public string? Description { get; set; }
    }
}
