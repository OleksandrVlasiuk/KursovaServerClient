using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API3.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string MyComment { get; set; }
        public string File { get; set; }
        public int Likes { get; set; }
        public string UserAccount_id { get; set; }
    }
}
