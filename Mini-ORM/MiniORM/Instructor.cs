using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MiniORM
{
    public class Instructor: Base<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Address PresentAddress { get; set; }
        public Address PermanentAddress { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
    }

}
