using Core.Identity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProjectEntity : BaseEntity
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public double TotalTarget { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsFeatured { get; set; }
        public CategoryEntity Category { get; set; }
        public int CategoryId { get; set; }
        public AppUser User { get; set; }
        public Guid UserId { get; set; }
        public ICollection<TagEntity> Tags { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProjectEntity()
        {
            Tags = new List<TagEntity>();
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
