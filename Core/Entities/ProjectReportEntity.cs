using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProjectReportEntity : BaseEntity
    {
        public string Report { get; set; }
        public ProjectEntity Project { get; set; }
        public int ProjectId { get; set; }
        public RegisterEntity User { get; set; }
        public int UserId { get; set; }
    }
}
