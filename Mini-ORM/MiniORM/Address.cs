using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniORM
{
    public class Address : Base<int> 
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

}
