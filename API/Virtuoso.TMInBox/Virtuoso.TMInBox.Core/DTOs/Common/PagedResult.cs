using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Virtuoso.TMInBox.Core.DTOs
{
    public class PagedResult<T>
    {

        public int CurrentPage {  get; set; }
        public int TotalPages { get; set; } = 0;
        public int PageSize { get; set; }
        public int TotalCount {  get; set; }
        public IEnumerable<T> Data { get; set; }
        public PagedResult(IEnumerable<T> data,int totalCount,int pageNumber,int pageSize ) {
        CurrentPage= pageNumber;
            TotalPages=(int)Math.Ceiling((double)totalCount/(double)pageSize);
            PageSize= pageSize;
            TotalCount= totalCount;
            Data= data;
        
        }



    }
}
