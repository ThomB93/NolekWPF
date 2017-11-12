using NolekWPF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace NolekWPF.UserControls.Equipment
{
    /// <summary>
    /// Interaction logic for EquipmentUpdateView.xaml
    /// </summary>
    public partial class EquipmentUpdateView : UserControl
    {
        public EquipmentUpdateView()
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

        private void Selection_Changed(object sender, SelectionChangedEventArgs e)
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
                            var equipmentRelation = db.Equipments.Where(c => c.EquipmentId == selectedEquipment.EquipmentId);
                            //var componentsForEquipment = new List<Component>();
                            //foreach (var row in equipmentRelation)
                            //{
                            //    var component = db.Components.FirstOrDefault(c => c.ComponentId == row.ComponentID);
                            //    componentsForEquipment.Add(component);
                            //}

                            string cs = "Data Source=mssql4.unoeuro.com;Initial Catalog=wiki_nolek_dk_db;Persist Security Info=True;User ID=wiki_nolek_dk;Password=3bcztmdy";
                            SqlConnection con = new SqlConnection(cs);
                            con.Open();
                            using (SqlCommand cmd = new SqlCommand("ReadImagePath", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                SqlParameter param1 = new SqlParameter("@EquipmentID", SqlDbType.Int);
                                var a = equipmentRelation.FirstOrDefault(c => c.EquipmentId == c.EquipmentId);
                                param1.Value = a.EquipmentId;
                                param1.Direction = ParameterDirection.Input;
                                cmd.Parameters.Add(param1);

                                SqlDataReader rdr = cmd.ExecuteReader();

                                int returnvalue = (int)cmd.Parameters["@EquipmentID"].Value;

                                if (rdr.HasRows)
                                {
                                    while (rdr.Read())
                                    {
                                        tbImagePath.Text = rdr.GetString(0);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Mistakes were made.");
                                }
                                
                            }
                            con.Close();                           
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("The chosen equipment does note exist.");
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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
                        var equipmentRelation = db.Equipments.Where(c => c.EquipmentId == selectedEquipment.EquipmentId);


                        string cs = "Data Source=mssql4.unoeuro.com;Initial Catalog=wiki_nolek_dk_db;Persist Security Info=True;User ID=wiki_nolek_dk;Password=3bcztmdy";
                        SqlConnection con = new SqlConnection(cs);

                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("UpdateImagePath", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter param1 = new SqlParameter("@EquipmentID", SqlDbType.Int);
                            var a = equipmentRelation.FirstOrDefault(c => c.EquipmentId == c.EquipmentId);
                            param1.Value = a.EquipmentId;
                            param1.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param1);

                            SqlParameter param2 = new SqlParameter("@EquipmentImagePath", SqlDbType.NVarChar);
                            param2.Value = tbImagePath.Text;
                            param2.Direction = ParameterDirection.Input;
                            cmd.Parameters.Add(param2);

                            SqlDataReader rdr = cmd.ExecuteReader();
                            EquipDataGrid.ItemsSource = GetEquipment();
                        }
                        con.Close();
                    }
                }
            }
        }
    }

}
