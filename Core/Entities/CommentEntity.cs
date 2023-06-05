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
        public RegisterEntity User { get; set; }
        public int UserId { get;set; }
        public DateTime CreatedAt { get; set; }

        public CommentEntity()
        {
            CreatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"comment by {User.FirstName} on {Project.Title} project.";
        }
    }
}
