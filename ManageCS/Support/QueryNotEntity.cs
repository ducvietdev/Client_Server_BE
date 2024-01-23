using Microsoft.Data.SqlClient;

namespace ManageCS.Support
{
    public class QueryNotEntity
    {
        public void Query(string query)
        {
            string connectionString = "Data Source=Admin;Initial Catalog=ManageCS;Persist Security Info=True;User ID=sa;Password=123456"; // Thay YourConnectionString bằng chuỗi kết nối thực tế

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}