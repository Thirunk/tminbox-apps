using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuoso.TMInBox.Core.Models
{
    public class PackageService:Auditable
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int ServiceId { get; set; }
        public int IsActive { get; set; }
    }
}
