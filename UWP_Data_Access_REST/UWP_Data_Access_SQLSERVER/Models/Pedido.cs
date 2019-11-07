using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_Data_Access_SQLSERVER.Models;


namespace UWP_Data_Access_SQLSERVER.Models
{
   
    public class Pedido : INotifyPropertyChanged
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }


        // Valores calculados obtenidos en la consulta para visualizarlos en el Datagrid 
        // No se consideran valores válidos
        public string Cliente { get; set; }
        public string Empleado { get; set; }
        public string CompaniaTransporte { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public static ObservableCollection<Pedido> GetPedidos(string connectionString)
        {
            const string GetProductsQuery =
               "SELECT o.*, c.CompanyName , CONCAT(CONCAT(e.LastName,' ,') , e.FirstName) as Empleado, s.CompanyName as ShipCompany                                              " +               
               " FROM Orders o                                            " +
               "    LEFT JOIN Customers c on o.CustomerID = c.CustomerID " +
               "    LEFT JOIN Employees e on o.EmployeeID = e.EmployeeID " +
               "    LEFT JOIN Shippers s on o.Shipvia = s.ShipperID " +
               " ORDER BY OrderID DESC                                   ";

            var pedidos = new ObservableCollection<Pedido>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetProductsQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var pedido = new Pedido();
                                    pedido.OrderID = Utiles.SafeGetInt32( reader , 0);
                                    pedido.CustomerID = Utiles.SafeGetString (reader, 1);
                                    pedido.EmployeeID = Utiles.SafeGetInt32(reader , 2);
                                    pedido.OrderDate = Utiles.SafeGetDateTime(reader , 3);
                                    pedido.RequiredDate = Utiles.SafeGetDateTime( reader, 4);
                                    pedido.ShippedDate = Utiles.SafeGetDateTime(reader , 5);
                                    pedido.ShipVia = Utiles.SafeGetInt32(reader , 6);
                                    pedido.Freight = Utiles.SafeGetDecimal(reader, 7);
                                    pedido.ShipName = Utiles.SafeGetString(reader,8);
                                    pedido.ShipAddress = Utiles.SafeGetString(reader, 9);
                                    pedido.ShipCity = Utiles.SafeGetString(reader, 10);
                                    pedido.ShipRegion = Utiles.SafeGetString(reader ,11);
                                    pedido.ShipPostalCode = Utiles.SafeGetString(reader,12);
                                    pedido.ShipCountry = Utiles.SafeGetString(reader,13);
                                    pedido.Empleado = Utiles.SafeGetString(reader,14);
                                    pedido.Cliente = Utiles.SafeGetString(reader, 15);
                                    pedido.CompaniaTransporte = Utiles.SafeGetString(reader,16);

                                    pedidos.Add(pedido);
                                }
                            }
                        }
                    }
                }
                return pedidos;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public bool Alta_Pedido()
        {
            string Consulta = " INSERT  INTO Orders    " +
               " VALUES  ( @customerid, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate , " +
                " @ShipVia , @Freight , @ShipName , @ShipAddress , @ShipCity , @ShipRegion, @ShipPostalCode , @ShipCountry ) ";
               
            
            try
            {
                using (SqlConnection conn = new SqlConnection((App.Current as App).ConnectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = Consulta;
                            cmd.Parameters.AddWithValue("@customerid", ((object)CustomerID) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@EmployeeID", ((object)EmployeeID) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@OrderDate", ((object)OrderDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@RequiredDate", ((object)RequiredDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShippedDate", ((object)ShippedDate) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipVia", ((object)ShipVia) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Freight", ((object)Freight) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipName", ((object)ShipName) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipAddress", ((object)ShipAddress) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipCity", ((object)ShipCity) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipRegion", ((object)ShipRegion) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipPostalCode", ((object)ShipPostalCode) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@ShipCountry", ((object)ShipCountry) ?? DBNull.Value);

                            // EL valor devuelto corresponde con las filas afectadas por la sentencia
                            return (cmd.ExecuteNonQuery() == 1);
                            
                        }
                    }
                }
                
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return false;
        }

    }

}
