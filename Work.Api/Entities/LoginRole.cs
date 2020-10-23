using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work.Api.Models;

namespace Work.Api.Entities
{
    public class LoginRole
    {
        public int Id { get; set; }
        public Role RoleName { get; set; }
        public string Discription { get; set; }
        public List<Url> Urls { get; set; }
    }
}
