using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniORM
{
    public class AdmissionTest : Base<int>
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double TestFees { get; set; }
    }

}
