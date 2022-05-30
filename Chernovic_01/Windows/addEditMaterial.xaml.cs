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

namespace Chernovic_01.Windows
{
    /// <summary>
    /// Логика взаимодействия для addEditMaterial.xaml
    /// </summary>
    public partial class addEditMaterial : Window
    {
        Materials _materials = new Materials();

        public addEditMaterial(Materials materials)
        {
            InitializeComponent();
            _materials = materials;
            if(_materials.Id !=0) //или _materials != null
            {
                listPostavok.ItemsSource = ChernovicEntities.GetContext().MaretialPostvshik.Where(p => p.idMaterial == _materials.Id).ToList();
                cbType.ItemsSource = ChernovicEntities.GetContext().TypeMaterial.Where(p => p.Id == _materials.IdType).ToList();
                cbType.SelectedIndex = 0;
            }
            cbType.ItemsSource = ChernovicEntities.GetContext().TypeMaterial.ToList();
            cbPost.ItemsSource = ChernovicEntities.GetContext().Postavshiki.ToList();
            DataContext = _materials;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(tbMin.Text) <= 0)
                {
                    MessageBox.Show("Минимальное количество не может быть отрицательным");
                }
                else
                {
                    if (_materials.Id == 0) //или _materials == null
                    {


                        _materials.IdType = cbType.SelectedIndex + 1;
                        ChernovicEntities.GetContext().Materials.Add(_materials);


                    }
                    ChernovicEntities.GetContext().SaveChanges();
                    MessageBox.Show("Сохранено");
                    this.Close();
                }
          
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MessageBox.Show("Вы действительно хотите удалить запись?","Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                   List<MaretialPostvshik> maretialPostvshiks = ChernovicEntities.GetContext().MaretialPostvshik.Where(p => p.idMaterial == _materials.Id).ToList();
                   if(maretialPostvshiks.Count>0)
                    {
                        foreach (MaretialPostvshik mP in maretialPostvshiks)
                        {
                            ChernovicEntities.GetContext().MaretialPostvshik.Remove(mP);
                         
                        }
                        ChernovicEntities.GetContext().Materials.Remove(_materials);
                        ChernovicEntities.GetContext().SaveChanges();
                        MessageBox.Show("Запись удалена");
                        this.Close();
                    }
                    else
                    {
                        ChernovicEntities.GetContext().Materials.Remove(_materials);
                        ChernovicEntities.GetContext().SaveChanges();
                        MessageBox.Show("Запись удалена");
                        this.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnAddP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool b = false;
                List<MaretialPostvshik> spisoMatPost = ChernovicEntities.GetContext().MaretialPostvshik.Where(p => p.idMaterial == _materials.Id).ToList();
                foreach(MaretialPostvshik mP in spisoMatPost)
                {
                    if(mP.idPostvshik == (cbPost.SelectedItem as Postavshiki).Id)
                    {
                        b = true;
                    }
                }
                if(b == false)
                {
                    MaretialPostvshik newMaretialPostvshik = new MaretialPostvshik()
                    {
                        idMaterial = _materials.Id,
                        idPostvshik = cbPost.SelectedIndex + 1
                    };
                    ChernovicEntities.GetContext().MaretialPostvshik.Add(newMaretialPostvshik);
                    ChernovicEntities.GetContext().SaveChanges();
                    listPostavok.ItemsSource = null;
                    listPostavok.ItemsSource = ChernovicEntities.GetContext().MaretialPostvshik.Where(p => p.idMaterial == _materials.Id).ToList();
                }
                else
                {
                    MessageBox.Show("Такой постащик уже есть");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void btnOtmena_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tbPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            cbPost.ItemsSource = ChernovicEntities.GetContext().Postavshiki.Where(p => p.NamePostavshik.ToLower().Contains(tbPoisk.Text.ToLower())).ToList();
            if(String.IsNullOrWhiteSpace(tbPoisk.Text))
            {
                cbPost.ItemsSource = ChernovicEntities.GetContext().Postavshiki.ToList();
            }
        }

        private void btnDeletePos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                        ChernovicEntities.GetContext().MaretialPostvshik.Remove((sender as Button).DataContext as MaretialPostvshik);
                        ChernovicEntities.GetContext().SaveChanges();
                        MessageBox.Show("Запись удалена");
                       
                  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
