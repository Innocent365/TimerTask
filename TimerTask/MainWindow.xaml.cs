using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        }        

        private void AddTask_BtnClick(object sender, RoutedEventArgs e)
        {
            var add = new Detail { Height = 200, Width = 600 };
            
            if (add.ShowDialog() == true)
            {
                var item = new ItemMini(add.Unit);
                ItemBox.Children.Add(item);
                item.SetOrder(ItemBox.Children.Count);
            }
        }
    }
}
