using System;
using System.Data.SqlClient;

namespace ConsoleWithAzureSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
                //Azure SQL Server Name 
                conn.DataSource = "pksqldbserver.database.windows.net";
                //User to connect to Azure
                conn.UserID = "Atos";
                //Password used in Azure
                conn.Password = "Arena@2022";
                //Azure database name
                conn.InitialCatalog = "pksqldb";

                using (SqlConnection connection = new SqlConnection(conn.ConnectionString))
                {

                    //Query used in the code
                    String sql = "SELECT name,costrate,availability from dbo.location";
                    //Connect to Azure SQL using the connection
                    using (SqlCommand sqlcommand = new SqlCommand(sql, connection))
                    {
                        //Open the connection
                        connection.Open();
                        //Execute the reader function to read the information
                        using (SqlDataReader reader = sqlcommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Read information from column 0,1 and 2. Column 0 is string and column 1 and 2 are decimals
                                Console.WriteLine("\t{0}\t{1}\t{2}", reader.GetString(0), reader.GetDecimal(1), reader.GetDecimal(2));
                            }
                        }
                    }
                }
            }
            //If it fails write the error message exception
            catch (SqlException e)
            {
                //Write the error message
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
    }
}

