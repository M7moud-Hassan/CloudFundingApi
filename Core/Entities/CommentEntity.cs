using Core.Identity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CommentEntity : BaseEntity
    {
        public string CommentText { get; set; }
        public ProjectEntity Project { get; set; }
        public int ProjectId { get; set; }
        public AppUser User { get; set; }
        public Guid UserId { get;set; }
        public DateTime CreatedAt { get; set; }

        public CommentEntity()
        {
            CreatedAt = DateTime.Now;
        }

        
    }
}
