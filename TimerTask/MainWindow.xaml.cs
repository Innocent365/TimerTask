using System;
using System.Collections.Generic;
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
            AssistThreadStartup();
        }

        private void AssistThreadStartup()
        {
            var task = ResetTaskLoop();
            if(task==null) return;

            _assistThread = new Thread(() => Start(task)) { IsBackground = true };
            _backGroundThread.Start();
        }

        private int _count;
        private void Start(UnitInfo task)
        {
            //if(task.Time > )
            //Timer

            return;
            //_count = ItemBox.Children.OfType<ItemMini>().Count();
            //while (_count > 0)
            //{
                
            //}
        }

        private Thread _backGroundThread;//后台线程，专门负责执行任务
        private Thread _assistThread;//辅助线程: 一直负责讲即将到来的任务交给后台线程，始终在执行

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

        private UnitInfo ResetTaskLoop()
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

//            InitThread();
        }

        private void StartProcess(UnitInfo task)
        {
            while (task != null && task.Status != TaskStatus.Completed)
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
//            InitThread();
        }
    }
}
