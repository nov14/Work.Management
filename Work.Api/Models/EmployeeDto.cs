﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Entities;

namespace Work.Api.Models
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string EmployeeNo { get; set; }
        public string Name { get; set; }
        public string GenderDisplay { get; set; }
        public int Age { get; set; }
    }
}
