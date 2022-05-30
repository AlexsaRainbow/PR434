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

namespace Learn.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageServise.xaml
    /// </summary>
    public partial class PageServise : Page
    {
        public List<Service> x;
        User08Entities context = new User08Entities();
        Service service = new Service();
        string _code = "";
        public PageServise(string code)
        {
            InitializeComponent();
            ListServise.ItemsSource = context.Service.ToList();
            cbServiseSkidka.SelectedIndex = 0;
            _code = code;
            x = context.Service.ToList();
        }

        private void TbPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            int allServise = context.Service.ToList().Count;
          
            string seachText = TbPoisk.Text;
            if (!string.IsNullOrWhiteSpace(seachText))
            {
                x = x.Where(p => p.Title.ToLower().StartsWith(seachText.ToLower())).ToList();
                ListServise.ItemsSource = x;
                TbKolZapIz.Text = x.Count.ToString() +" из " + allServise.ToString();
            }
            else
            {
                ListServise.ItemsSource = context.Service.ToList();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbSort.ItemsSource = new List<string> { "возрастание", "убывание" };
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    x = x.OrderBy(p => p.Cost).ToList();
                    break;
                case 1:
                    x = x.OrderByDescending(p => p.Cost).ToList();
                    break;
            }
            ListServise.ItemsSource = x;
        }

        private void filter()
        {
            int allServise = context.Service.ToList().Count;
            if (!String.IsNullOrWhiteSpace(TbPoisk.Text))
            {
                if(cbServiseSkidka.SelectedIndex>0)
                {
                   switch(cbServiseSkidka.SelectedIndex)
                   {
                        case 0:
                            ListServise.ItemsSource = context.Service.ToList();
                            break;
                        case 1:
                            x = x.Where(p => p.Discount >= 0 && p.Discount < 0.05 && p.Title.ToLower().StartsWith(TbPoisk.Text.ToLower())).ToList();
                            ListServise.ItemsSource = x;
                            TbKolZapIz.Text = x.Count.ToString() + " из " + allServise.ToString();

                            break;
                        case 2:
                            x = x.Where(p => p.Discount >= 0.05 && p.Discount < 0.15 && p.Title.ToLower().StartsWith(TbPoisk.Text.ToLower())).ToList();
                            ListServise.ItemsSource = x;
                            TbKolZapIz.Text = x.Count.ToString() + " из " + allServise.ToString();
                            break;
                        case 3:
                            x = x.Where(p => p.Discount >= 0.15 && p.Discount < 0.3 && p.Title.ToLower().StartsWith(TbPoisk.Text.ToLower())).ToList();
                            ListServise.ItemsSource = x;
                            TbKolZapIz.Text = x.Count.ToString() + " из " + allServise.ToString();
                            break;
                        case 4:
                            x = x.Where(p => p.Discount >= 0.3 && p.Discount < 0.7 && p.Title.ToLower().StartsWith(TbPoisk.Text.ToLower())).ToList();
                            ListServise.ItemsSource = x;
                            TbKolZapIz.Text = x.Count.ToString() + " из " + allServise.ToString();
                            break;
                        case 5:
                            x = x.Where(p => p.Discount >= 0.7 && p.Discount < 1 && p.Title.ToLower().StartsWith(TbPoisk.Text.ToLower())).ToList();
                            ListServise.ItemsSource = x;
                            TbKolZapIz.Text = x.Count.ToString() + " из " + allServise.ToString();
                            break;
                    }
                }
            }
        }

        private void cbServiseSkidka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            filter();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Windows.AddEditServies addEditServies = new Windows.AddEditServies(false,null);
            addEditServies.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var serviceForRemoving = ListServise.SelectedItems.Cast<Service>().ToList();
            List<ClientService> clientServices = context.ClientService.ToList();
            bool f = false;
            for (int i = 0; i < clientServices.Count; i++)
            {
                if (clientServices[i].ID == (ListServise.SelectedItem as Service).ID)
                {
                    f = true;
                }
            }

            if (MessageBox.Show($"Вы точно хотите удалить следующие элементов?", "Внимание",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (f)
                {
                    MessageBox.Show("Нельзя удалять");
                }
                else
                {
                    try
                    {
                        context.Service.RemoveRange(serviceForRemoving);
                        context.SaveChanges();
                        MessageBox.Show("Данные удалены");

                        ListServise.ItemsSource = context.Service.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void BtnEddit_Click(object sender, RoutedEventArgs e)
        {
            Windows.AddEditServies addEditServies = new Windows.AddEditServies(true,ListServise.SelectedItem as Service);
            addEditServies.Show();
        }

        private void BtnZapis_Click(object sender, RoutedEventArgs e)
        {
            Windows.ZapisClienta zapisClienta = new Windows.ZapisClienta(ListServise.SelectedItem as Service);
            zapisClienta.Show();
        }

        private void ButtonVisibli(object sender, RoutedEventArgs e)
        {
            Classes.CodeAdmin cA = new Classes.CodeAdmin();
            if (_code != "0000")
            {
                (sender as StackPanel).Visibility = Visibility.Hidden;
                BtnAdd.Visibility = Visibility.Hidden;
            }
        }

        
        private void BtnNazad_Click(object sender, RoutedEventArgs e)
        {
            Classes.MangerFrame.MainFrame.GoBack();
        }
    }
}
