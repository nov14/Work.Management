using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work.Api.Entities
{
    public class Url
    {
        public int Id { get; set; }
        public string LinkUrl { get; set; }
        public int RoleId { get; set; }
        public LoginRole LoginRole { get; set; }
    }
}
