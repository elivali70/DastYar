using App.Domain.Core.Order.Enities;
using App.Domain.Core.Service.Entities;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace App.Domain.Core.AppFile.Entities;

public partial class AppFile
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
    public int UserId { get; set; }
    public int FileTypeId { get; set; }
    public FileType FileType { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<OrderFile> OrderFiles { get; set; } = new List<OrderFile>();

    public virtual ICollection<ServiceFile> ServiceFiles { get; set; } = new List<ServiceFile>();
}
