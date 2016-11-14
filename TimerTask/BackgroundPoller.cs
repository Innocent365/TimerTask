using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using TimerTask.Unit;
using Timer = System.Timers.Timer;

namespace TimerTask
{
    public class BackgroundPoller
    {
        private static readonly BackgroundWorker BackgroundWorker = new BackgroundWorker();
        private readonly Timer _timer = new Timer { AutoReset = false };

        public IEnumerator<TaskItem> TaskList;
        public Action<object> NoticeAction;

        public void Start()
        {
            BackgroundWorker.RunWorkerCompleted += (o, eventArgs) => { TimerLoop(); };
            BackgroundWorker.DoWork += StartProcess;
        }

        private void RemoveTask()
        {
//            Dispatcher.Invoke(() =>
//            {
//                var item = ItemBox.Children.OfType<ItemMini>().First(p => p.Unit == TaskList.Current);
//                ItemBox.Children.Remove(item);
//            });
            TaskList.MoveNext();
        }

        public void StartProcess(object sender, DoWorkEventArgs e)
        {
            RemoveTask();

            var thread = new Thread(() => StartProcess(TaskList.Current));
            //_undoDic[TaskList.Current] = thread;
            thread.Start();
        }

        public void TimerLoop(
            [CallerMemberName] string memberName = "",
                               [CallerFilePath] string sourceFilePath = "",
                               [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            Console.WriteLine(@"调用TimerLoop方法:" + memberName);
            Console.WriteLine(@"调用TimerLoop方法:" + sourceLineNumber);

            var task = TaskList.Current;
            if (task == null)
            {
                Console.WriteLine(@"current任务无效");
                return;
            };

            var z = task.Time - DateTime.Now;
            if (Math.Abs((task.Time - DateTime.Now).Seconds) > 30)
            {
                Console.WriteLine(@"执行时间小于当前时间");
                return;
            }

            _timer.Interval = z.TotalMilliseconds > 0 ? z.TotalMilliseconds:3000;
            _timer.Elapsed += (sender, args) =>
            {
                BackgroundWorker.RunWorkerAsync();
            };
            _timer.Start();
        }

        public void StartProcess(TaskItem task)
        {
            if (task == null || string.IsNullOrEmpty(task.Path)) return;

            while (task.Status != TaskStatus.Completed)
            {
                var now = DateTime.Now;
                if (task.Status != TaskStatus.Ready && task.Time - now < new TimeSpan(30))
                {
                    var notice = string.Format("{0} 即将执行！", task.Caption);
                    NoticeAction(notice);
                    Thread.Sleep(2900);
                }
                if (now >= task.Time)
                {
                    task.RunProcess();
                }
            }
        }

        public void Dispose()
        {
            _timer.Elapsed -= TimerLoop;
            _timer.Stop();
        }

        private void TimerLoop(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
