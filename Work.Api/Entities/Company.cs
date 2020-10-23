using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work.Api.Entities
{
    /// <summary>
    /// 公司实体
    /// </summary>
    public class Company
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 公司简介
        /// </summary>
        public string Introduction { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
