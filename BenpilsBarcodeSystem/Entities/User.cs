using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem
{
    public class User
    { 
        public int iD { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Designation { get; set; }
        public string GetName()
        {
            return $"{LastName}, {FirstName}";
        }
    }
}
