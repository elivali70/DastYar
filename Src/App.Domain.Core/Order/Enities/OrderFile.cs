using App.Domain.Core.AppFile.Entities;
using System;
using System.Collections.Generic;

namespace App.Domain.Core.Order.Enities;

public partial class OrderFile
{
    public int Id { get; set; }

    public int FileId { get; set; }

    public int OrderId { get; set; }
    public bool IsDeleted { get; set; }


    public virtual AppFile.Entities.AppFile File { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
