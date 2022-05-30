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

namespace Chernovic_01.Pages
{
    /// <summary>
    /// Логика взаимодействия для materialPage.xaml
    /// </summary>
    public partial class materialPage : Page
    {
        List<Materials> listMaterialN;

        private int tekPage = 1;
        private int countMat = 15;
        private int maxPage;
        private int vsegoMat;

        public materialPage()
        {
            InitializeComponent();
            listMaterialN = ChernovicEntities.GetContext().Materials.ToList();
            List<TypeMaterial> spisokMaterial = ChernovicEntities.GetContext().TypeMaterial.ToList();
            spisokMaterial.Insert(0, new TypeMaterial { NameType = "Все типы" });
            cbFilter.ItemsSource = spisokMaterial;
            cbFilter.SelectedIndex = 0;
            vsegoMat = listMaterialN.Count;
            Refresh();
        }

        public void Refresh()
        {
            maxPage = (int)Math.Ceiling((listMaterialN.Count * 1.0) / countMat);
            var listMaterialPage = listMaterialN.Skip((tekPage - 1) * countMat).Take(countMat).ToList();
            tbTekPage.Text = tekPage.ToString();
            lbVsegoPage.Content = "из " + maxPage.ToString();
            //tbSkolcoDanih.Text = "Показано: " + listMaterialN.Count + " из " + listMaterialN.Count;
            listMaterial.ItemsSource = listMaterialPage;
        }
        public void UpdateList()
        {
            listMaterialN = null;
            listMaterialN = ChernovicEntities.GetContext().Materials.ToList();
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbSort.ItemsSource = new List<string> { "По возрастанию наименования", "По убыванию ниманования","По возрастанию остаток на складе", "По убыванию остаток на складе", "По возрастанию стоимость", "По убыванию стоимость"};
        }

        private void tbPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            listMaterialN = listMaterialN.Where(p => p.NameMaterial.ToLower().Contains(tbPoisk.Text.ToLower())).ToList();
            if(String.IsNullOrWhiteSpace(tbPoisk.Text))
            {
                listMaterialN = ChernovicEntities.GetContext().Materials.ToList();
            }
  
            Refresh();
            tbSkolcoDanih.Text = "Показано: " + listMaterialN.Count + " из " + vsegoMat.ToString();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    listMaterialN = listMaterialN.OrderBy(p => p.NameMaterial).ToList();
                    break;
                case 2:
                    listMaterialN = listMaterialN.OrderBy(p => p.KolNaSklade).ToList();
                    break;
                case 4:
                    listMaterialN = listMaterialN.OrderBy(p => p.Cena).ToList();
                    break;
                case 1:
                    listMaterialN = listMaterialN.OrderByDescending(p => p.NameMaterial).ToList();
                    break;
                case 3:
                    listMaterialN = listMaterialN.OrderByDescending(p => p.KolNaSklade).ToList();
                    break;
                case 5:
                    listMaterialN = listMaterialN.OrderByDescending(p => p.Cena).ToList();
                    break;
            }
            Refresh();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listMaterialN = ChernovicEntities.GetContext().Materials.ToList();
            listMaterialN = listMaterialN.Where(p => p.IdType == (cbFilter.SelectedItem as TypeMaterial).Id).ToList();
            if(cbFilter.SelectedIndex == 0)
            {
                listMaterialN = ChernovicEntities.GetContext().Materials.ToList();
            }
           
            Refresh();
            tbSkolcoDanih.Text = "Показано: " + listMaterialN.Count + " из " + vsegoMat.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Materials newMater = new Materials();
            Windows.addEditMaterial addEditMaterial = new Windows.addEditMaterial(newMater);
            addEditMaterial.ShowDialog();
            UpdateList();
            /* или
           Windows.addEditMaterial addEditMaterial = new Windows.addEditMaterial(null);
            addEditMaterial.ShowDialog();
            UpdateList();
             */
        }

        private void btnVNachalo_Click(object sender, RoutedEventArgs e)
        {
            tekPage = 1;
            Refresh();
        }

        private void btnNazadS_Click(object sender, RoutedEventArgs e)
        {
            if (tekPage <= 1)
                tekPage = 1;
            else
                tekPage--;
            Refresh();
        }

        private void btnVpered_Click(object sender, RoutedEventArgs e)
        {
            if (tekPage >= maxPage)
                tekPage = maxPage;
            else
                tekPage++;
            Refresh();
        }

        private void btnVKonez_Click(object sender, RoutedEventArgs e)
        {
            tekPage = maxPage;
            Refresh();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            
                Windows.addEditMaterial addEditMaterial = new Windows.addEditMaterial((sender as Button).DataContext as Materials);//или listMaterial.SelectedItem as Materials
                addEditMaterial.ShowDialog();
                UpdateList();
            
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
