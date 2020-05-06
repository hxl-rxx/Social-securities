using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_security.MVC.Models
{
    public class UserLogin
    {
        public int ID { get; set; }
        public string Uname { get; set; }
        public string PassWord { get; set; }
        public int Userlevel { get; set; }
    }
}
