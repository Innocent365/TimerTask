using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerTask.Unit
{
    public class UnitInfo
    {
        public string Caption { get; set; }
        public string Path { get; set; }
        public string Note { get; set; }



        public DateTime Time { get; set; }
        public TimeSpan DelayTimeSpan { get; set; }
        public TimeSpan LastRunTimeSpan { get; set; }

    }
}
