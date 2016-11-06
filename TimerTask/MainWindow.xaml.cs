using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimerTask.Unit;

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
            Loaded += InitMenuItems;
        }

        private void InitMenuItems(object sender, RoutedEventArgs routedEventArgs)
        {
            AddNewItem.Items.Clear();
            foreach (var info in DefaultInfos)
            {
                var menuItem = new MenuItem() { Header = info.Caption, CommandParameter = info.Path };
                menuItem.Click += AddTask_BtnClick;
                menuItem.DataContext = info;
                AddNewItem.Items.Add(menuItem);
            }

            //DefaultInfos.ForEach(p =>AddNewItem.Items.Add());
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
        }

        private void AddNewTask_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        public List<UnitInfo> DefaultInfos = new List<UnitInfo>
        {
            new UnitInfo(){Caption = "关机", Path = "cmd.exe"},
            new UnitInfo(){Caption = "重新启动", Path = ""},
            new UnitInfo(){Caption = "休眠", Path = ""},
            new UnitInfo(){Caption = "打开浏览器", Path = ""},
        };

        private void AddNewTask_OnMouseLeave(object sender, MouseEventArgs e)
        {
           
        }

    }
}
