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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Data_Access_SQLSERVER
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class EditarPedido : Page
    {

        private Pedido pedido_editandose;
        
        public EditarPedido()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            { 
                // Edición
                pedido_editandose = e.Parameter as Pedido;
            }
            else
            { 
                // Alta de pedido --- Utilizamos el campo OrderID como bandera que indica el alta
                pedido_editandose = new Pedido();
                pedido_editandose.OrderID = 0;
                pedido_editandose.OrderDate = DateTime.Now;
            }

            base.OnNavigatedTo(e);
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (pedido_editandose.OrderID == 0)
            {
                // ALTA
                pedido_editandose.CustomerID = "VINET";
                pedido_editandose.EmployeeID = 6;
                pedido_editandose.ShipVia = 3;
                pedido_editandose.Freight = 5000;
                pedido_editandose.OrderDate = DateTime.Now;
                pedido_editandose.RequiredDate = DateTime.Now;
                pedido_editandose.ShippedDate = DateTime.Now;

                if (pedido_editandose.Alta_Pedido() > 0)
                {
                    int duration = 2000;
                   // ExampleInAppNotification.Show("Pedido dado de alta correctamente.", duration);
                    this.Frame.Navigate(typeof(Pedidos));
                   
                }

            }
            else
            {
                // EDICIÓN
                pedido_editandose.Actualizar_Pedido_SinConcurrencia();
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
