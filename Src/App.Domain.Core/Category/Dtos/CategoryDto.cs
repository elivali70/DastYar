using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Category.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? ParentId { get; set; }

        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
