using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuoso.TMInBox.Core.Models
{
    public class ProviderService:Auditable
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int ServiceId { get; set; }
        public int IsActive { get; set; }
    }
}
