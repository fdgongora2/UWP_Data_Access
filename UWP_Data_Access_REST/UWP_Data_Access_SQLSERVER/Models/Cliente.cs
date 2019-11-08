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
    public class Cliente : INotifyPropertyChanged
    {

        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public static ObservableCollection<Cliente> GetClientes(string cadena_busqueda)
        {
            string GetProductsQuery =
               " SELECT * " +
               " FROM Customers                                            " +
               " WHERE UPPER(Companyname) LIKE '%" + cadena_busqueda.ToUpper() +"%'" +
               " ORDER BY CompanyName ASC                                   ";

            var clientes = new ObservableCollection<Cliente>();
            try
            {
                using (SqlConnection conn = new SqlConnection((App.Current as App).ConnectionString))
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
                                    var cliente_aux = new Cliente();

                                    // Para evitar el problema de que los valores sean Nulos en la BBDD 
                                    // y eso genere un error al asignarlos, se han creado unas funciones en que
                                    // comprueban si el valor es Nulo. Todas las funciones están en la clase "Utiles".

                                    
                                    cliente_aux.CustomerID = Utiles.SafeGetString(reader, 0);
                                    cliente_aux.CompanyName = Utiles.SafeGetString(reader, 1);
                                    cliente_aux.Address = Utiles.SafeGetString(reader, 2);
                                    cliente_aux.City = Utiles.SafeGetString(reader, 3);
                                    cliente_aux.Region = Utiles.SafeGetString(reader, 4);
                                    cliente_aux.PostalCode = Utiles.SafeGetString(reader, 5);
                                    cliente_aux.Country = Utiles.SafeGetString(reader, 6);

                                    clientes.Add(cliente_aux);
                                }
                            }
                        }
                    }
                }
                return clientes;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        }
    }
