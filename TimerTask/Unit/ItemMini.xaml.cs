using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TimerTask.Unit
{
    /// <summary>
    /// ItemMini.xaml 的交互逻辑
    /// </summary>
    public partial class ItemMini
    {
        public ItemMini()
        {
            InitializeComponent();
        }

        private UnitInfo _unit;
        public UnitInfo Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                TitleTLabel.Content = _unit.Caption;
                TimeTextBlock.Text = _unit.Time.ToString("yyyy/MM/dd HH:mm");
            }
        }

        public ItemMini(UnitInfo info):this()
        {
            Unit = info;
        }

        private static readonly Brush ActivedColor = new SolidColorBrush(Colors.Black);
        private static readonly Brush NormalColor = new SolidColorBrush(Colors.AliceBlue);
        private static readonly Thickness ActivedBorder = new Thickness(0.5);
        private static readonly Thickness NormalBorder = new Thickness(0.1);

        public void SetOrder(int index)
        {
            NumberLabel.Content = index;
        }

        public bool Actived
        {
            get { return false; }
            set { SetActived(value); }
        }

        public void SetActived(bool value)
        {
            BorderBrush = value ? ActivedColor : NormalColor;
            BorderThickness = value ? ActivedBorder : NormalBorder;
        }

        private void ItemMini_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var detail = new Detail(_unit) { Height = 200, Width = 600 };            

            if (detail.ShowDialog() == true)
            {
                Unit = detail.Unit;
            }
        }

        private void RunNowBtn_Click(object sender, RoutedEventArgs e)
        {
            var proc = _unit.RunProcess();            
            if (proc != null) Console.WriteLine(@"当前线程Id: " + proc.Id);
        }
    }
}
