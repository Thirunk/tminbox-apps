using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuoso.TMInBox.Core.Models
{
    public class ProviderPricing:Auditable
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public string? DiscountCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public int IsActive { get; set; }
    }
}
