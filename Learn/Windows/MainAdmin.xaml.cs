using Learn.Classes;
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
using System.Windows.Shapes;

namespace Learn.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : Window
    {
        public MainAdmin(string code)
        {
            InitializeComponent();
            MangerFrame.MainFrame = MainFrame;
            Classes.CodeAdmin cA = new Classes.CodeAdmin();
            if (code != "0000")
            {
                MainFrame.Navigate(new Pages.PageServise(code));
            }
            else
            {
                MainFrame.Navigate(new Pages.MainPage());
            }
        }
    }
}
