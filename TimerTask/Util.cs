using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimerTask.Unit;

namespace TimerTask
{
    static class Util
    {
        public static Process Exe(string path, string param)
        {
            return Process.Start(path, param);
        }

        public static Process Exe(ProcessStartInfo processStartInfo)
        {
            return Process.Start(processStartInfo);
        }

        public static List<TaskItem> DefaultInfos = new List<TaskItem>
        {
            new TaskItem{Path = "cmd.exe",    Params = "/C shutdown -s -t 0",  IsReadOnly = true, Caption = "关机"},
            new TaskItem{Path = "cmd.exe",    Params = "/C shutdown -r -t 0",  IsReadOnly = true, Caption = "重新启动",  },
            new TaskItem{Path = "cmd.exe",    Params = "/C shutdown -h -t 0",  IsReadOnly = true, Caption = "休眠"},
            
            null,
            new TaskItem{Path = @"D:\Program Files (x86)\Thunder Network\Thunder\Program\Thunder.exe", IsReadOnly = true, Params = "http://youtube.com",  Caption = "打开迅雷下载生活大爆炸"},
            null,

            new TaskItem{Path = "chrome.exe", Params = "", IsReadOnly = true,   Caption = "打开浏览器"},
            new TaskItem{Path = "chrome.exe", Params = "https://youtu.be/-c9c2JW5um4?t=431",IsReadOnly = true,   Caption = "打开youtube"},
            new TaskItem{Path = "chrome.exe", Params = "https://youtu.be/-c9c2JW5um4",   Caption = "打开youtube"}
        };

        public static Window GetParentWindow(this FrameworkElement elm)
        {
            while (elm.Parent != null && elm is Window == false)
                elm = (FrameworkElement)elm.Parent;
            return elm as Window;
        }
    }
}
