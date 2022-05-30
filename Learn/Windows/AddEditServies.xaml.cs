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
    /// Логика взаимодействия для AddEditServies.xaml
    /// </summary>
    public partial class AddEditServies : Window
    {
        User08Entities context = new User08Entities();
        public Service service;
        bool isEdit;
        public AddEditServies(bool IsEdit, Service se)
        {
            InitializeComponent();
            isEdit = IsEdit;
            if (se is null)   service = new Service();
            else service = se;
            DataContext = service;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isEdit)
                {
                    try
                    {
                        if (context.Service.Where(p => p.Title == TbTitle.Text).Any())
                        {
                            MessageBox.Show("В базе данных уже существует услуга с таким названием");
                        }
                        else if(int.Parse(TbVrema.Text) > 14400 || int.Parse(TbVrema.Text) <= 0)
                        {
                            MessageBox.Show("Длительность услуги не может составлять более четырех часов или менее 1 минуте");
                        }
                        else
                        {
                            User08Entities.GetContext().Service.Add(service);
                        }
                    }
                    catch (Exception _e)
                    {
                        MessageBox.Show(_e.Message);
                    }
                }
              
                User08Entities.GetContext().SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOtmena_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите отменить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) this.Close();
        }
    }
}
