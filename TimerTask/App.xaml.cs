using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TimerTask
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        public App()
        {
            //OpenSingleWindow();
        }
        private void OpenSingleWindow()
        {
            if (CheckProcessExist())
            {
                MaxWindow();
            }
            else
            {
                new MainWindow().Show();
            }
        }

        private void MaxWindow()
        {
            throw new NotImplementedException();
        }

        private bool CheckProcessExist()
        {
            return false;
        }
    }
}
