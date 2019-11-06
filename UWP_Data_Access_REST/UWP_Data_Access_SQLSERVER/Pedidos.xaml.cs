using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_Data_Access_SQLSERVER.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Controls;


// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Data_Access_SQLSERVER
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Pedidos : Page
    {
        public Pedidos()
        {
            this.InitializeComponent();
            DG_Pedidos.ItemsSource = Pedido.GetPedidos((App.Current as App).ConnectionString);
        }

        private void DG_Pedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
        }

        private void DG_Pedidos_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

            DataGrid grid = sender as DataGrid;
            if (grid.SelectedItem != null)
            {

                this.Frame.Navigate(typeof(EditarPedido), grid.SelectedItem);
            }

        }
               
        private void Editar_pedido_Click(object sender, RoutedEventArgs e)
        {
            if (DG_Pedidos.SelectedItem != null)
            {
                this.Frame.Navigate(typeof(EditarPedido), DG_Pedidos.SelectedItem);
            }

        }

        private void Alta_pedido_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditarPedido));
        }

        private void Borrar_pedido_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
