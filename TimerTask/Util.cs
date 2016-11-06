using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static List<UnitInfo> DefaultInfos = new List<UnitInfo>
        {
            new UnitInfo{Path = "cmd.exe",    Params = "/C shutdown -s -t 0", Time = DateTime.Now,   IsReadOnly = true, Caption = "关机"},
            new UnitInfo{Path = "cmd.exe",    Params = "/C shutdown -r -t 0", Time = DateTime.Now,   IsReadOnly = true, Caption = "重新启动",  },
            new UnitInfo{Path = "cmd.exe",    Params = "/C shutdown -h -t 0", Time = DateTime.Now,   Caption = "休眠"},
            null,
            new UnitInfo{Path = @"D:\Program Files (x86)\Thunder Network\Thunder\Program\Thunder.exe", Params = "http://youtube.com",  Time = DateTime.Now,   Caption = "打开迅雷"},
            null,
            new UnitInfo{Path = "chrome.exe", Params = "",  Time = DateTime.Now,   Caption = "打开浏览器"},
            new UnitInfo{Path = "chrome.exe", Params = "https://youtu.be/-c9c2JW5um4?t=431",  Time = DateTime.Now,   Caption = "打开youtube"},
            new UnitInfo{Path = "chrome.exe", Params = "https://youtu.be/-c9c2JW5um4",  Time = DateTime.Now,   Caption = "打开youtube"},
        };

    }
}
