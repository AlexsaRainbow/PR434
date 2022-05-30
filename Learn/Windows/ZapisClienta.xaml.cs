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
    /// Логика взаимодействия для ZapisClienta.xaml
    /// </summary>
    public partial class ZapisClienta : Window
    {
        public ZapisClienta(Service ser)
        {
            InitializeComponent();
            LTi.Text = ser.Title;
            LVrem.Content = ser.VremaVMinutax.ToString() + " минут";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOtmena_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
