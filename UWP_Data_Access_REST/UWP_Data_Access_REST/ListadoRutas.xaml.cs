using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_Data_Access_REST.Models;
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

namespace UWP_Data_Access_REST
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ListadoRutas : Page
    {
        private RutasBus rutasBusesBarcelona;
        private List<Tmb> listadoRutasBus;
        public ListadoRutas()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            rutasBusesBarcelona = await GestorRutas.GetAllBusRoutesAsync();
            listadoRutasBus = rutasBusesBarcelona.data.tmbs;
            Lv_estaciones.ItemsSource = listadoRutasBus;


        }
    }
}
