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

namespace VosmerkaUser08
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _currenPage = 1; //начальная страница
        private int _countAgents = 20;//сколько на странице записей
        private int _maxPages;//максимальное количество страниц(расчитывается)
        VosmerkaEntities paper = new VosmerkaEntities();
        List<Product> listProduct = VosmerkaEntities.GetContext().Product.ToList();
        List<Product> x = VosmerkaEntities.GetContext().Product.ToList();
        public MainWindow()
        {
            InitializeComponent();
            ListProduct.ItemsSource = VosmerkaEntities.GetContext().Product.ToList();
            List<ProductType> comboSpisok = paper.ProductType.ToList();
            comboSpisok.Insert(0, new ProductType { Title = "Все типы" });
            cbType.ItemsSource = comboSpisok;
            cbType.SelectedIndex = 0;
            Refresh(listP);
        }

        private void TbPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbPoisk.Text))
            {
                x = x.Where(p => p.Title.ToLower().StartsWith(TbPoisk.Text.ToLower())).ToList();
                ListProduct.ItemsSource = x;
            }
            else
            {
                x=  VosmerkaEntities.GetContext().Product.ToList();
                if (cbType.SelectedIndex > 0)
                {
                    x = x.Where(p => p.ProductTypeID == (cbType.SelectedItem as ProductType).ID).ToList();
                    if (rbV.IsChecked == true)
                    {

                        switch (cbSort.SelectedIndex)
                        {
                            case 0:
                                x = x.OrderBy(p => p.Title).ToList();
                                break;
                            case 1:
                                x = x.OrderBy(p => p.ProductionWorkshopNumber).ToList();
                                break;
                            case 2:
                                x = x.OrderBy(p => p.MinCostForAgent).ToList();
                                break;
                        }
                    }
                    else if (rbYb.IsChecked == true)
                    {

                        switch (cbSort.SelectedIndex)
                        {
                            case 0:
                                x = x.OrderByDescending(p => p.Title).ToList();
                                break;
                            case 1:
                                x = x.OrderByDescending(p => p.ProductionWorkshopNumber).ToList();
                                break;
                            case 2:
                                x = x.OrderByDescending(p => p.MinCostForAgent).ToList();
                                break;
                        }
                    }
                    ListProduct.ItemsSource = x;
                }
                ListProduct.ItemsSource = x;
            }
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
        }

        List<Product> listP;

        private void Refresh(List<Product> listP)
        {
            listProduct = paper.Product.ToList();
            _maxPages = (int)Math.Ceiling(listProduct.Count * 1.0 / _countAgents); //расчет количество страниц. 

            var listAgentPage = listProduct.Skip((_currenPage - 1) * _countAgents).Take(_countAgents).ToList();

            TxtCurrentPage.Text = _currenPage.ToString();
            LblTotalPages.Content = "из " + _maxPages;

            //  LblInfo.Content = $"Всего {listAgent.Count} записи, по {_countAgents} записей на одной странице";

            ListProduct.ItemsSource = listAgentPage;
        }

        private void GoToFirstPage(object sender, RoutedEventArgs e)
        {
            _currenPage = 1;
            Refresh(listP);
        }

        private void GoToPerviousPage(object sender, RoutedEventArgs e)
        {
            if (_currenPage <= 1) _currenPage = 1;
            else _currenPage--;
            Refresh(listP);
        }

        private void GoToNextPage(object sender, RoutedEventArgs e)
        {
            if (_currenPage >= _maxPages) _currenPage = _maxPages;
            else _currenPage++;
            Refresh(listP);
        }

        private void GoToLastPage(object sender, RoutedEventArgs e)
        {
            _currenPage = _maxPages;
            Refresh(listP);
        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            cbSort.ItemsSource = new List<string> { "Наименование", "Номер цеха", "Стоимость" };
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            WinAddProduct winAddProduct = new WinAddProduct(false, null);
            winAddProduct.ShowDialog();
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var context = VosmerkaEntities.GetContext().Product.ToList();

            if (cbType.SelectedIndex > 0)
            {
                if (!string.IsNullOrWhiteSpace(TbPoisk.Text))
                {
                    x = x.Where(p => p.Title.ToLower().StartsWith(TbPoisk.Text.ToLower())).ToList();
                }
                    x = x.Where(p => p.ProductTypeID == (cbType.SelectedItem as ProductType).ID).ToList();

                if (rbV.IsChecked == true)
                {
                   
                    switch (cbSort.SelectedIndex)
                    {
                        case 0:
                            x = x.OrderBy(p => p.Title).ToList();
                            break;
                        case 1:
                            x = x.OrderBy(p => p.ProductionWorkshopNumber).ToList();
                            break;
                        case 2:
                            x = x.OrderBy(p => p.MinCostForAgent).ToList();
                            break;
                    }
                }
                else if (rbYb.IsChecked == true)
                {
                   
                    switch (cbSort.SelectedIndex)
                    {
                        case 0:
                            x = x.OrderByDescending(p => p.Title).ToList();
                            break;
                        case 1:
                            x = x.OrderByDescending(p => p.ProductionWorkshopNumber).ToList();
                            break;
                        case 2:
                            x = x.OrderByDescending(p => p.MinCostForAgent).ToList();
                            break;
                    }
                }
                ListProduct.ItemsSource = x;
            }
            else
            {
                x = context;
                if (!string.IsNullOrWhiteSpace(TbPoisk.Text))
                {
                    x = x.Where(p => p.Title.ToLower().StartsWith(TbPoisk.Text.ToLower())).ToList();
                }
                if (rbV.IsChecked == true)
                {
                    switch (cbSort.SelectedIndex)
                    {
                        case 0:
                            x = x.OrderBy(p => p.Title).ToList();
                            break;
                        case 1:
                            x = x.OrderBy(p => p.ProductionWorkshopNumber).ToList();
                            break;
                        case 2:
                            x = x.OrderBy(p => p.MinCostForAgent).ToList();
                            break;
                    }
                }
                else if (rbYb.IsChecked == true)
                {
                    
                    switch (cbSort.SelectedIndex)
                    {
                        case 0:
                            x = x.OrderByDescending(p => p.Title).ToList();
                            break;
                        case 1:
                            x = x.OrderByDescending(p => p.ProductionWorkshopNumber).ToList();
                            break;
                        case 2:
                            x = x.OrderByDescending(p => p.MinCostForAgent).ToList();
                            break;
                    }
                }
                ListProduct.ItemsSource = x;
            }
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            WinAddProduct winAddProduct = new WinAddProduct(true, ListProduct.SelectedItem as Product);
            winAddProduct.ShowDialog();
            // this.Close();
        }

        private void rbV_Checked(object sender, RoutedEventArgs e)
        {
            switch(cbSort.SelectedIndex)
            {
                case 0 :
                    x = x.OrderBy(p => p.Title).ToList();
                    break;
                case 1:
                    x = x.OrderBy(p => p.ProductionWorkshopNumber).ToList();
                    break;
                case 2:
                    x = x.OrderBy(p => p.MinCostForAgent).ToList();
                    break;
            }
            ListProduct.ItemsSource = x;
        }

        private void rbYb_Checked(object sender, RoutedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    x = x.OrderByDescending(p => p.Title).ToList();
                    break;
                case 1:
                    x = x.OrderByDescending(p => p.ProductionWorkshopNumber).ToList();
                    break;
                case 2:
                    x = x.OrderByDescending(p => p.MinCostForAgent).ToList();
                    break;
            }
            ListProduct.ItemsSource = x;
        }


        private void btSbros_Click(object sender, RoutedEventArgs e)
        {
            ListProduct.ItemsSource = VosmerkaEntities.GetContext().Product.ToList();
            x = VosmerkaEntities.GetContext().Product.ToList();
            rbYb.IsChecked = false;
            rbV.IsChecked = false;
            cbType.SelectedIndex = 0;
        }
    }
}
