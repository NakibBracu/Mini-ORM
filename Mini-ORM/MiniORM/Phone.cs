using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniORM
{
    public class Phone : Base<int>
    {
        public string Number { get; set; }
        public string Extension { get; set; }
        public string CountryCode { get; set; }
    }

}
