using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using TimerTask.Unit;

namespace TimerTask
{
    /// <summary>
    /// Detail.xaml 的交互逻辑
    /// </summary>
    public partial class Detail
    {
        public Detail(UnitInfo unit=null)
        {
            InitializeComponent();
            Unit = unit;
        }

        public UnitInfo Unit
        {
            get { return new UnitInfo
            {
                Caption = Caption.Text,
                Path = PathTextBox.Text,
                Time = Time,
                Note = NoteTextBox.Text
            }; }
            set
            {
                if (value == null)
                {
                    Time = DateTime.Now;
                    return;
                };
                Caption.Text = value.Caption;
                PathTextBox.Text = value.Path;
                Time = value.Time;
                NoteTextBox.Text = value.Note;
            }
        }

        public new string Name
        {
            get { return Title; }
            set { Title = value; }
        }

        public DateTime Time
        {
            get
            {
                return System.DateTime.Parse(
                    string.Format(@"{0}/{1}/{2} {3}:{4}", DateTime.Today.Year, MonthBlock.Text, DayBlock.Text, HourBlock.Text,
                        MinuteBlock.Text));
            }
            set
            {
                MonthBlock.Text = value.Month.ToString();
                DayBlock.Text = value.Day.ToString();

                HourBlock.Text = value.Hour.ToString();
                MinuteBlock.Text = value.Hour.ToString();
            }
        }

        private void PathTextBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                PathTextBox.Text = fileDialog.FileName;
            }
        }



        private void PathTextBox_OnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            if (PathTextBox.Text.EndsWith(".exe") == false)
            {
                MessageBox.Show("执行路径无效");
            }
        }

        private void Caption_OnKeyUp(object sender, KeyEventArgs e)
        {
            var textBox = sender as CheckBox;
            if (e.Key == Key.Enter)
            {                
                ConfirmBtn.Focus();
                Activate();
            }
        }

        private void ConfirmBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void TimeBlock_OnKeyUp(object sender, KeyEventArgs e)
        {
            
        }
    }
}
