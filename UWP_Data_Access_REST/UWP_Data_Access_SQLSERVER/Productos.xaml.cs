using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWP_Data_Access_SQLSERVER.Models;
using System.Collections.ObjectModel;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Data_Access_SQLSERVER
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Productos : Page
    {
        ObservableCollection<Product> pedidos;
        public Productos()
        {
            this.InitializeComponent();
            pedidos  = Product.GetProducts((App.Current as App).ConnectionString);
            dataGridPedidos.ItemsSource = pedidos;
        }

        private void DataGridPedidos_Sorting(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridColumnEventArgs e)
        {
            if (e.Column.SortDirection == null || e.Column.SortDirection == Microsoft.Toolkit.Uwp.UI.Controls.DataGridSortDirection.Ascending)
            {
                //Use the Tag property to pass the bound column name for the sorting implementation 
                if (e.Column.Tag.ToString() == "Range")
                {
                    //Implement ascending sort on the column "Range" using LINQ
                    dataGridPedidos.ItemsSource = new ObservableCollection<Product>(from item in pedidos
                                                                                     orderby item.ProductID ascending
                                                                                     select item);
                }
            }
        }
    }
}
