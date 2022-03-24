using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGOApp
{
    public class DbModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }   
        public string Email_Pass { get; set; }
        public string Details { get; set; }
        public bool Sold { get; set; }
        public bool Listed { get; set; }

        public string FullInfo
        {
            get
            {
                return $"{Username}";
            }
        }
    }

}
