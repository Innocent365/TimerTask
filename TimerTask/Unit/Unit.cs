using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimerTask.Unit
{    
    public class TaskItem
    {
        public string Caption { get; set; }
        public string Path { get; set; }
        public string Params { get; set; }
        public string Note { get; set; }
        public bool IsReadOnly { get; set; }    //执行命令是否可以更改
        public DateTime Time { get; set; }
        public TimeSpan DelayTimeSpan { get; set; }
        public TimeSpan LastRunTimeSpan { get; set; }

        public TaskStatus Status { get; set; }
        public Process RunProcess()
        {
            Process proc;
            if (string.IsNullOrEmpty(Path))
            {
                MessageBox.Show("没有任务可以执行！");
                return null;
            }

            if (string.IsNullOrEmpty(Params) == false)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Path,
                    Arguments = Params,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                proc = Process.Start(startInfo);
            }
            else
            {
                proc = Process.Start(Path);
            }
            Status = TaskStatus.Completed;
            return proc;
        }

    }


}
