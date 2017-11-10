﻿using System;
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

        public List<Equipment> GetEquipment()
        {
            //eager loading, db is disposed after use
            using (wiki_nolek_dk_dbEntities db = new wiki_nolek_dk_dbEntities())
            {
                db.Configuration.LazyLoadingEnabled = true;

                var query = from o in db.Equipments
                    select new EquipmentViewModel
                    {
                        EquipmentTypeName = o.EquipmentType.EquipmentTypeName,
                        EquipmentConfigurationDescription = o.EquipmentConfiguration.EquipmentConfigurationDescription
                    };

                return db.Equipments.ToList();
            }
        }
    }
}
