using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work.Api.Models
{
    public class CompanyDto
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
}
