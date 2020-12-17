using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace NetShop
{
    class DataBaseController
    {
        public static DataTable LoadTable(string tableName, SqlConnection sqlConnection)
        {
            var sqlExpression = "SELECT * FROM " + tableName;
            sqlConnection.Open();
            var adapter = new SqlDataAdapter(sqlExpression, sqlConnection);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        public static void InsertOrder(PersonalInformation orderInfo, SqlConnection sqlConnection, List<Cart> cartList)
        {
            var command = sqlConnection.CreateCommand();
            command.CommandText = "INSERT INTO OrderInfoTable VALUES('" + orderInfo.Surname + "','" + orderInfo.Name + "','" + orderInfo.Patronymic + "','" +
                orderInfo.Address + "','" + orderInfo.PhoneNumber + "','" + orderInfo.MailAddress + "')" +
                " SELECT SCOPE_IDENTITY()";
            sqlConnection.Open();
            try
            {
                var id = (decimal)command.ExecuteScalar();
                InsertToLinkTable(cartList, command, Convert.ToInt32(id));
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public static void InsertToLinkTable(List<Cart> cartList, SqlCommand command, int id)
        {
            foreach (var product in cartList)
            {
                command.CommandText = "INSERT INTO OrderedProductsTable VALUES (" + id + "," + product.ProductID + ",'" + product.OrderName
                    + "'," + product.NumberOfProducts + ")";
                command.ExecuteNonQuery();
            }
        }
    }
}
