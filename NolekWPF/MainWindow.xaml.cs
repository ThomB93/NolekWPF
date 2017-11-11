using System;
using System.Collections;
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
using NolekWPF.ViewModels;

namespace NolekWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EquipDataGrid.ItemsSource = GetEquipment();
        }

        public List<EquipmentView> GetEquipment()
        {
            //lazy loading, db is disposed after use
            using (wiki_nolek_dk_dbEntities db = new wiki_nolek_dk_dbEntities())
            {
                db.Configuration.LazyLoadingEnabled = true;
                return db.EquipmentViews.ToList();
            }
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }
        private void EquipDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(EquipDataGrid);
                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        EquipmentView selectedEquipment = (EquipmentView)EquipDataGrid.SelectedItem;
                        using (wiki_nolek_dk_dbEntities db = new wiki_nolek_dk_dbEntities())
                        {
                            db.Configuration.LazyLoadingEnabled = true;
                            var equipmentRelation = db.EquipmentComponents.Where(c => c.EquipmentID == selectedEquipment.EquipmentId);
                            var componentsForEquipment = new List<Component>();
                            foreach (var row in equipmentRelation)
                            {
                                var component = db.Components.FirstOrDefault(c => c.ComponentId == row.ComponentID);
                                componentsForEquipment.Add(component);
                            }
                            CompDataGrid.ItemsSource = componentsForEquipment;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Det valgte udstyr eksisterer ikke.");
            }
        }
    }
}
