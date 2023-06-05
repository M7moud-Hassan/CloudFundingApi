using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ImagesEntity : BaseEntity
    {
        public string Images { get; set; }
        public ProjectEntity Project { get; set; }
        public int ProjectId { get; set; }

        public ImagesEntity()
        {
            Images = string.Empty;
        }
    }
}
