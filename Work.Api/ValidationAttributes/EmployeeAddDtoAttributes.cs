using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Models;

namespace Work.Api.ValidationAttributes
{
    public class EmployeeAddDtoAttributes:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addDto = (EmployeeAddDto)validationContext.ObjectInstance;
            if(addDto.EmployeeNo == addDto.FirstName)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(EmployeeAddDto) });
            }

            return ValidationResult.Success;
        }
    }
}
