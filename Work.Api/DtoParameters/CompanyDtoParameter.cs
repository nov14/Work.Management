using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work.Api.DtoParameters
{
    public class CompanyDtoParameter
    {
        private const int MaxPageSize = 5;
        public string CompanyName { get; set; }
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 3;
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
