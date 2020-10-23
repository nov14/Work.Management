using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Entities;

namespace Work.Api.Models
{
    public class EmployeeUpdateDto:IValidatableObject
    {

        [Display(Name = "员工号")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "{0}的长度等于4")]
        public string EmployeeNo { get; set; }

        [Display(Name = "姓")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        [StringLength(10, ErrorMessage = "{0}的最大长度不超过{1}")]
        public string FirstName { get; set; }

        [Display(Name = "名")]
        [Required(ErrorMessage = "{0}这个字段是必填的")]
        [StringLength(10, ErrorMessage = "{0}的最大长度不超过{1}")]
        public string LastName { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [Display(Name = "出生日期")]
        public DateTime BirthOfDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult("姓和名不能一样", new[] { nameof(FirstName), nameof(LastName) });
            }
        }
    }
}
