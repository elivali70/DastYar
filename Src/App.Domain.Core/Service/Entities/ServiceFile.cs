using App.Domain.Core.AppFile.Entities;
using System;
using System.Collections.Generic;

namespace App.Domain.Core.Service.Entities;

public partial class ServiceFile
{
    public int Id { get; set; }

    public int FileId { get; set; }

    public int ServiceId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual AppFile.Entities.AppFile File { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
