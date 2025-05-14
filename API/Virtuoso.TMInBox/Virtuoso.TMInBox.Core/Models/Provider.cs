using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuoso.TMInBox.Core.Models
{
    public class Provider:Auditable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NPINo { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int IsActive { get; set; }
    }
}
