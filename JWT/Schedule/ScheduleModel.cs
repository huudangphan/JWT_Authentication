using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    class ScheduleModel
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string day { get; set; }
        public string time { get; set; }
        public string job { get; set; }
    }
}
