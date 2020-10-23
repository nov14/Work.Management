using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Entities;

namespace Work.Api.Models
{
    public class CompanyAddDto
    {
        [Display(Name = "公司名称")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        [MaxLength(100, ErrorMessage = "{0}最大长度不超过{1}")]
        public string Name { get; set; }

        [Display(Name = "公司简介")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "{0}的长度在{2}到{1}之间")]
        public string Introduction { get; set; }
        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();
    }
}
