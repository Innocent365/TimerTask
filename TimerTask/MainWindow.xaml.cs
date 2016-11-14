using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;
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
            InitMenuItems();

            Taskbar.MenuActivation = PopupActivationMode.RightClick;
            

            _backgroundPoller = new BackgroundPoller();
            _backgroundPoller.NoticeAction += o =>
            {
                Dispatcher.Invoke(() =>
                {
                    Taskbar.ShowBalloonTip(o.ToString(),"是的", BalloonIcon.Info);
                });
                  
            };
            _backgroundPoller.Start();

            Hide();
        }

        private readonly BackgroundPoller _backgroundPoller;

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
                var menuItem = new MenuItem { Header = info.Caption, CommandParameter = info.Path };
                menuItem.Click += AddTask_BtnClick;
                menuItem.DataContext = info;
                AddNewItem.Items.Add(menuItem);
            }
        }

        private IEnumerator<TaskItem> FluskTask()
        {
            var reback =  ItemBox.Children.OfType<ItemMini>().Select(p=>p.Unit).OrderBy(p=>p.Time).GetEnumerator();
            reback.MoveNext();
            return reback;
        }
        private void AddTask_BtnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var info = menuItem.DataContext as TaskItem;

            var add = new Detail(info) { Height = 200, Width = 600 };

            if (add.ShowDialog() == true)
            {
                var item = new ItemMini(add.Unit);

                item.RunMeNow += mini => _backgroundPoller.StartProcess(mini.Unit);
                //item.RequirePause += PauseTask;
                item.RmoveNow += RemoveItem;
                
                ItemBox.Children.Add(item);
                item.SetOrder(ItemBox.Children.Count);
            }

            _backgroundPoller.TaskList = FluskTask();
            _backgroundPoller.TimerLoop();
        }

        private void PauseTask(ItemMini obj)
        {
            obj.Actived = false;
            var task = obj.Unit;
            //if (_undoDic.ContainsKey(task)) _undoDic[task].Join();
        }

        private void RemoveItem(ItemMini item)
        {
            ItemBox.Children.Remove(item);
            _backgroundPoller.TaskList = FluskTask();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Hide();
            e.Cancel = true;
            base.OnClosing(e);
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            _backgroundPoller.Dispose();
//            Taskbar.NotifyIcon.Dispose();
        }

        private void Taskbar_OnTrayLeftMouseUp(object sender, RoutedEventArgs e)
        {
            Show();
        }

        private void Taskbar_OnTrayRightMouseUp(object sender, RoutedEventArgs e)
        {
        }

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("确认退出么?");
            App.Current.Shutdown();
        }

        private void PauseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
