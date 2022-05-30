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

namespace Learn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string _code = ""; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnVxodAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(TbCode.Text == "0000")
                {
                    Classes.CodeAdmin cA = new Classes.CodeAdmin();
                    cA.CodeAdminS("0000");
                    cA.code = "0000";
                    Windows.MainAdmin mainAdmin = new Windows.MainAdmin(TbCode.Text);
                    mainAdmin.Show();
                }
                else
                {
                    Windows.MainAdmin mainAdmin = new Windows.MainAdmin(null);
                    mainAdmin.Show();
                }
            }
            catch
            {

            }
        }
    }
}
