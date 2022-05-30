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

namespace VosmerkaUser08
{
    /// <summary>
    /// Логика взаимодействия для WinAddProduct.xaml
    /// </summary>
    public partial class WinAddProduct : Window
    {
        public Product product;
        bool isEdit;
        public WinAddProduct(bool IsEdit, Product pr)
        {
            InitializeComponent();
            if (IsEdit) this.Title = "Редактировать продукт";
            else this.Title = "Добавить продукт";
            isEdit = IsEdit;
            if (pr is null) product = new Product();
            else product = pr;
            DataContext = product;

            tbType.ItemsSource = VosmerkaEntities.GetContext().ProductType.ToList();
            tbType.DisplayMemberPath = "Title";
        }

        private void BtnAddEdite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isEdit)
                {
                    try
                    {
                        VosmerkaEntities.GetContext().Product.Add(product);
                    }
                    catch (Exception _e)
                    {
                        MessageBox.Show(_e.Message);
                    }
                }
                VosmerkaEntities.GetContext().SaveChanges();
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

        private void BtnYdalit_Click(object sender, RoutedEventArgs e)
        {
            var context = VosmerkaEntities.GetContext().Product.ToList();

            context = context.Where(p => p.ID == product.ID).ToList();

            VosmerkaEntities.GetContext().Product.Remove(context.First() as Product);
            VosmerkaEntities.GetContext().SaveChanges();
            Close();
        }
    }
}
