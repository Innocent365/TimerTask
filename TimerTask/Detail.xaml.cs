using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using TimerTask.Unit;

namespace TimerTask
{
    /// <summary>
    /// Detail.xaml 的交互逻辑
    /// </summary>
    public partial class Detail
    {
        public Detail(TaskItem unit=null)
        {
            InitializeComponent();
            Unit = unit;
        }

        public TaskItem Unit
        {
            get { return new TaskItem
            {
                Caption = Caption.Text,
                Path = PathTextBox.Text,
                Params = ParamsTextBox.Text,
                Time = Time,
                Note = NoteTextBox.Text
            }; }
            set
            {
                if(value==null) return;
                Time = value.IsReadOnly ? DateTime.Now.Add(TimeSpan.FromMinutes(0.8)) : value.Time;
                Caption.Text = value.Caption;
                PathTextBox.Text = value.Path;
                ParamsTextBox.Text = value.Params;
                NoteTextBox.Text = value.Note;
                if (value.IsReadOnly) PathTextBox.IsEnabled = false;
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
                MinuteBlock.Text = value.Minute.ToString();
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
            return;
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
