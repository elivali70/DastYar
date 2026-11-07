using System;
using System.Collections.Generic;
using App.Domain.Core.Service.Entities;
namespace App.Domain.Core.Category.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentId { get; set; }
    public DateTime CreatedAt {  get; set; }
    public int DisplayOrder { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<Service.Entities.Service> Services { get; set; } = new List<Service.Entities.Service>();
}
