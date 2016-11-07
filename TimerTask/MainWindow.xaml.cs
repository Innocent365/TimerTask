using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TimerTask.Unit;
using Timer = System.Timers.Timer;

namespace TimerTask
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            InitMenuItems();
            TimerStartup();
        }

        private void TimerStartup()
        {
            _task = GetTask();
            if (_task == null) return;

            var z = _task.Time - DateTime.Now;
            _timer = new Timer(z.TotalMilliseconds);
            _timer.Elapsed += (sender, args) =>
            {
                _backgroundWorker = new BackgroundWorker();
                _backgroundWorker.DoWork += StartProcess;
                _backgroundWorker.RunWorkerCompleted += (o, eventArgs) => { TimerLoop(); };
                _backgroundWorker.RunWorkerAsync();
            };
            _timer.Start();
            
            _timer.AutoReset = false;
        }

        private void StartProcess(object sender, DoWorkEventArgs e)
        {
            StartProcess(_task);
        }

        private delegate void BackgroundWorkerDo(UnitInfo unit);

        private void TimerLoop()
        {
            var task = GetTask();
            if (task == null) return;

            var z = task.Time - DateTime.Now;
            _timer.Interval = z.TotalMilliseconds;
            _timer.Elapsed += (sender, args) =>
            {
                _backgroundWorker.RunWorkerAsync();
            };
            _timer.Start();
        }

        private BackgroundWorker _backgroundWorker;
        private Timer _timer = new Timer{AutoReset = false};
        private UnitInfo _task;

        private void InitMenuItems()
        {
            AddNewItem.Items.Clear();
            foreach (var info in Util.DefaultInfos)
            {
                if (info == null)
                {
                    AddNewItem.Items.Add(new Separator());
                    continue;
                };
                var menuItem = new MenuItem() { Header = info.Caption, CommandParameter = info.Path };
                menuItem.Click += AddTask_BtnClick;
                menuItem.DataContext = info;
                AddNewItem.Items.Add(menuItem);
            }
        }

        private UnitInfo GetTask()
        {
            var array = ItemBox.Children.OfType<ItemMini>().ToArray();
            if (array.Any() == false) return null;

            var nearestTime = array.Min(p => p.Unit.Time);
            var task = array.Select(p=>p.Unit).FirstOrDefault(p => p.Time == nearestTime);

            return task;
        }
        private void AddTask_BtnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var info = menuItem.DataContext as UnitInfo;

            var add = new Detail(info) { Height = 200, Width = 600 };

            if (add.ShowDialog() == true)
            {
                var item = new ItemMini(add.Unit);
                ItemBox.Children.Add(item);
                item.SetOrder(ItemBox.Children.Count);
            }
            TimerStartup();
        }

        private void StartProcess(UnitInfo task)
        {
            if(task == null) return;
            while (task.Status != TaskStatus.Completed)
            {
                var now = DateTime.Now;
                if (task.Status != TaskStatus.Ready && task.Time - now < new TimeSpan(60))
                {
                    MessageBox.Show(string.Format("{0} 即将执行！", task.Caption));
                    Thread.Sleep(2900);
                }
                if (now >= task.Time)
                {
                    task.RunProcess();
                }
            }
        }
    }
}
