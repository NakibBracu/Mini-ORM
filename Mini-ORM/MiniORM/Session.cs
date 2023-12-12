using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniORM
{
    public class Session : Base<int>
    {
        public int DurationInHour { get; set; }
        public string LearningObjective { get; set; }
    }

}
