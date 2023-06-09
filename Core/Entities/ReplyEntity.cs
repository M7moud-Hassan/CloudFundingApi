﻿using Core.Identity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Entities
{
    public class ReplyEntity : BaseEntity
    {
        public string ReplyText { get; set; }
        public CommentEntity Comment { get; set; }
        public int CommentId { get; set; }
        public AppUser User { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ReplyEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
