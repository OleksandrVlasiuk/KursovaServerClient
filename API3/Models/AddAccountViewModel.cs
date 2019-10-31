using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API3.Models
{
    public class AddAccountViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
