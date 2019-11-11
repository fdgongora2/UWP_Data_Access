using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWP_Data_Access_SQLSERVER.Models;


namespace UWP_Data_Access_SQLSERVER.Models
{

    public class Pedido : INotifyPropertyChanged
    {

        private int _OrderID;
        public int OrderID { get { return _OrderID; }
            
            set
            {
                if (value != _OrderID)
                {
                    _OrderID = value;
                    NotifyPropertyChanged();
                }
            } }

        private string _CustomerID;
        public string CustomerID {
            get { return this._CustomerID; }

            set
            {
                if (value != this._CustomerID)
                {
                    this._CustomerID = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _EmployeeID;
        public int EmployeeID
        {
            get { return this._EmployeeID; }

            set
            {
                if (value != this._EmployeeID)
                {
                    this._EmployeeID = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DateTime _OrderDate;
        public DateTime OrderDate
        {
            get { return this._OrderDate; }

            set
            {
                if (value != this._OrderDate)
                {
                    this._OrderDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DateTime _RequiredDate;
        public DateTime RequiredDate
        {
            get { return this._RequiredDate; }

            set
            {
                if (value != this._RequiredDate)
                {
                    this._RequiredDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime _ShippedDate;
        public DateTime ShippedDate
        {
            get { return this._ShippedDate; }

            set
            {
                if (value != this._ShippedDate)
                {
                    this._ShippedDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _ShipVia;
        public int ShipVia
        {
            get { return this._ShipVia; }

            set
            {
                if (value != this._ShipVia)
                {
                    this._ShipVia = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private decimal _Freight;
        public decimal Freight
        {
            get { return this._Freight; }

            set
            {
                if (value != this._Freight)
                {
                    this._Freight = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _ShipName;
        public string ShipName
        {
            get { return this._ShipName; }

            set
            {
                if (value != this._ShipName)
                {
                    this._ShipName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _ShipAddress;
        public string ShipAddress
        {
            get { return this._ShipAddress; }

            set
            {
                if (value != this._ShipAddress)
                {
                    this._ShipAddress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _ShipCity;
        public string ShipCity
        {
            get { return this._ShipCity; }

            set
            {
                if (value != this._ShipCity)
                {
                    this._ShipCity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _ShipRegion;
        public string ShipRegion
        {
            get { return this._ShipRegion; }

            set
            {
                if (value != this._ShipRegion)
                {
                    this._ShipRegion = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _ShipPostalCode;
        public string ShipPostalCode
        {
            get { return this._ShipPostalCode; }

            set
            {
                if (value != this._ShipPostalCode)
                {
                    this._ShipPostalCode = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _ShipCountry;
        public string ShipCountry
        {
            get { return this._ShipCountry; }

            set
            {
                if (value != this._ShipCountry)
                {
                    this._ShipCountry = value;
                    NotifyPropertyChanged();
                }
            }
        }


        // Valores calculados obtenidos en la consulta para visualizarlos en el Datagrid 
        // No se consideran valores válidos
        private string _Cliente;
        public string Cliente
        {
            get { return this._Cliente; }

            set
            {
                if (value != this._Cliente)
                {
                    this._Cliente = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _Empleado;
        public string Empleado
        {
            get { return this._Empleado; }

            set
            {
                if (value != this._Empleado)
                {
                    this._Empleado = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _CompaniaTransporte;
        public string CompaniaTransporte
        {
            get { return this._CompaniaTransporte; }

            set
            {
                if (value != this._CompaniaTransporte)
                {
                    _CompaniaTransporte = value;
                    NotifyPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public static ObservableCollection<Pedido> GetPedidos()
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
                                    var pedido = new Pedido();

                                    // Para evitar el problema de que los valores sean Nulos en la BBDD 
                                    // y eso genere un error al asignarlos, se han creado unas funciones en que
                                    // comprueban si el valor es Nulo. Todas las funciones están en la clase "Utiles".

                                    pedido.OrderID = Utiles.SafeGetInt32(reader, 0);
                                    pedido.CustomerID = Utiles.SafeGetString(reader, 1);
                                    pedido.EmployeeID = Utiles.SafeGetInt32(reader, 2);
                                    pedido.OrderDate = Utiles.SafeGetDateTime(reader, 3);
                                    pedido.RequiredDate = Utiles.SafeGetDateTime(reader, 4);
                                    pedido.ShippedDate = Utiles.SafeGetDateTime(reader, 5);
                                    pedido.ShipVia = Utiles.SafeGetInt32(reader, 6);
                                    pedido.Freight = Utiles.SafeGetDecimal(reader, 7);
                                    pedido.ShipName = Utiles.SafeGetString(reader, 8);
                                    pedido.ShipAddress = Utiles.SafeGetString(reader, 9);
                                    pedido.ShipCity = Utiles.SafeGetString(reader, 10);
                                    pedido.ShipRegion = Utiles.SafeGetString(reader, 11);
                                    pedido.ShipPostalCode = Utiles.SafeGetString(reader, 12);
                                    pedido.ShipCountry = Utiles.SafeGetString(reader, 13);
                                    pedido.Empleado = Utiles.SafeGetString(reader, 14);
                                    pedido.Cliente = Utiles.SafeGetString(reader, 15);
                                    pedido.CompaniaTransporte = Utiles.SafeGetString(reader, 16);

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


        public bool CargarDatosPedido(int numero_pedido)
        {
            string GetProductsQuery =
               "SELECT *     " +
               " FROM Orders                                             " +
               " WHERE OrderID =  " + numero_pedido.ToString();


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
                                // Para evitar el problema de que los valores sean Nulos en la BBDD 
                                // y eso genere un error al asignarlos, se han creado unas funciones en que
                                // comprueban si el valor es Nulo. Todas las funciones están en la clase "Utiles".

                                if (reader.Read())

                                {

                                    CustomerID = Utiles.SafeGetString(reader, 1);
                                    EmployeeID = Utiles.SafeGetInt32(reader, 2);
                                    OrderDate = Utiles.SafeGetDateTime(reader, 3);
                                    RequiredDate = Utiles.SafeGetDateTime(reader, 4);
                                    ShippedDate = Utiles.SafeGetDateTime(reader, 5);
                                    ShipVia = Utiles.SafeGetInt32(reader, 6);
                                    Freight = Utiles.SafeGetDecimal(reader, 7);
                                    ShipName = Utiles.SafeGetString(reader, 8);
                                    ShipAddress = Utiles.SafeGetString(reader, 9);
                                    ShipCity = Utiles.SafeGetString(reader, 10);
                                    ShipRegion = Utiles.SafeGetString(reader, 11);
                                    ShipPostalCode = Utiles.SafeGetString(reader, 12);
                                    ShipCountry = Utiles.SafeGetString(reader, 13);
                                    Empleado = Utiles.SafeGetString(reader, 14);
                                    Cliente = Utiles.SafeGetString(reader, 15);
                                    CompaniaTransporte = Utiles.SafeGetString(reader, 16);

                                    return true;
                                }
                            }
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

        public int Alta_Pedido()
        {
            // <param> Parámetro de salida .... Número del nuevo pedido o -1 si ha habido un error</param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.

            int returnvalue = -1;

            string Consulta = " INSERT  INTO Orders    " +
               " VALUES  ( @customerid, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate , " +
                " @ShipVia , @Freight , @ShipName , @ShipAddress , @ShipCity , @ShipRegion, @ShipPostalCode , @ShipCountry ) ;" +

                // Sentencia para que devuelva el nuevo ID de pedido que es autoincremental
                " SELECT SCOPE_IDENTITY();";



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

                            // Un problema con los parámetros es que si tienen un valor no establecido válido
                            // nos dará un error. Para solucionarlo ponemos al valor ((object)XXXXX) ?? DBNull.Value
                            // de esta forma si el valor el nulo se le pasa un valor Nulo reconocido por el servidor de 
                            // bases de datos.
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

                            object returnObj = cmd.ExecuteScalar();

                            if (returnObj != null)
                            {
                                int.TryParse(returnObj.ToString(), out returnvalue);
                            }



                        }
                    }
                }

            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }

            return returnvalue;
        }


        public bool Actualizar_Pedido_SinConcurrencia()
        {
            // <param> Parámetro de salida .... </param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.

            if (OrderID > 0)

            {

                string Consulta = " UPDATE Orders    " +
                   " SET customerID = @customerid, EmployeeID = @EmployeeID, OrderDate = @OrderDate, RequiredDate = @RequiredDate , ShippedDate = @ShippedDate , " +
                   " ShipVia = @ShipVia ,Freight = @Freight , ShipName = @ShipName , ShipAddress = @ShipAddress , ShipCity =@ShipCity , ShipRegion = @ShipRegion, ShipPostalCode = @ShipPostalCode , ShipCountry =@ShipCountry  " +
                   " WHERE OrderID = @OrderID";
                
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

                                // Un problema con los parámetros es que si tienen un valor no establecido válido
                                // nos dará un error. Para solucionarlo ponemos al valor ((object)XXXXX) ?? DBNull.Value
                                // de esta forma si el valor el nulo se le pasa un valor Nulo reconocido por el servidor de 
                                // bases de datos.
                                cmd.Parameters.AddWithValue("@OrderID", ((object)OrderID) ?? DBNull.Value);

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
            }
            else
            {
                Debug.WriteLine("Error : Intento de guardar un pedido sin número de pedido válido" );
            }

            return false;
        }

        public bool Borrar_Pedido()
        {
            // <param> Parámetro de salida .... </param>
            // A la hora de crear la sentencia a ejecutar podemos crearla concatenando cadenas. Esto tiene dos problemas:            
            //
            //    1. Un ataque SQLInjection en los valores que pasamos.
            //    2. La dificultad de crear la cadena de la senytencia ... poner comillas, etc.
            //
            // Si utilizamos parámetros solucionamos los problemas anteriores.

            if (OrderID > 0 && Pedido_Borrable())

            {

                string Consulta = " DELETE                  " +
                                  " FROM Orders             " +
                                  " WHERE OrderID = @OrderID";

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

                                // Un problema con los parámetros es que si tienen un valor no establecido válido
                                // nos dará un error. Para solucionarlo ponemos al valor ((object)XXXXX) ?? DBNull.Value
                                // de esta forma si el valor el nulo se le pasa un valor Nulo reconocido por el servidor de 
                                // bases de datos.
                                cmd.Parameters.AddWithValue("@OrderID", ((object)OrderID) ?? DBNull.Value);

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
            }
            else
            {
                Debug.WriteLine("Error : Intento de borrar un pedido sin número de pedido válido" );
            }

            return false;
        }


        public bool Pedido_Borrable()
        {
            // Faltarían poner las validaciones 

            return OrderID > 0;
        }
    }


    }
