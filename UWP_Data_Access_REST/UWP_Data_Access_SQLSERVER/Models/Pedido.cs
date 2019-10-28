using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Data_Access_SQLSERVER.Models
{
   
    public class Pedido : INotifyPropertyChanged
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string Cliente { get; set; }
        public int EmployeeID { get; set; }
        public string Empleado { get; set; }


        public DateTime OrderDate { get; set; }

     
        
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
               "SELECT o.OrderID, o.CustomerID, o.OrderDate, c.CompanyName, CONCAT(CONCAT(e.LastName,' ,') , e.FirstName)                                              " +               
               " FROM Orders o                                            " +
               "    LEFT JOIN Customers c on o.CustomerID = c.CustomerID " +
               "    LEFT JOIN Employees e on o.EmployeeID = e.EmployeeID " +
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
                                    pedido.OrderID = reader.GetInt32(0);
                                    pedido.CustomerID = reader.GetString(1);
                                    pedido.OrderDate = reader.GetDateTime(2);
                                    pedido.Cliente = reader.GetString(3);
                                    pedido.Empleado = reader.GetString(4);

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


    }

}
