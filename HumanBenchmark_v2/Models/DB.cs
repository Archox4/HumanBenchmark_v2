using System.Data.SqlClient;

namespace HumanBenchmark_v2.Models
{
	public class DB
	{

		public void check_user()
		{
			SqlConnection conn;
			string constr = @"Data Source=DESKTOP-FBDMIFK;Initial Catalog=WebAppDB1;Integrated Security=True;";
			
			
			using(conn = new SqlConnection(constr))
			{
				conn.Open();
				Console.WriteLine("Connection Open!");

				string sql = "SELECT COUNT(*) FROM [dbo].[user_data]";

				SqlCommand sqlCommand = new SqlCommand(sql, conn);
				SqlDataReader reader = sqlCommand.ExecuteReader();
				reader.Read();
				Console.WriteLine("////////////////////////////////" + reader.GetValue(0));
			}
			


			conn.Close();
		}
        public string get_username(string UID)
        {
            SqlConnection conn;
            string constr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAppDB1;Integrated Security=True;Connect Timeout=30;";
			string username = "";

            using (conn = new SqlConnection(constr))
            {
                conn.Open();
                Console.WriteLine("Connection Open!");

                string sql = "SELECT [Username] FROM [dbo].[user_data] WHERE [UID]='" + UID +"'";

                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                reader.Read();
                Console.WriteLine(reader.GetValue(0));
				username = reader.GetString(0);
            }



            conn.Close();
			return username;
        }
		//public void get_username() { }

		public bool username_state(string username) 
		{
			SqlConnection conn;
			string constr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAppDB1;Integrated Security=True;Connect Timeout=30;";
			int x;
			bool avaible = false;
			using (conn = new SqlConnection(constr))
			{
				conn.Open();
				Console.WriteLine("Connection Open!");

				string sql = "SELECT COUNT(*) FROM [dbo].[user_data] WHERE [Username]='" + username + "'";

				SqlCommand sqlCommand = new SqlCommand(sql, conn);
				SqlDataReader reader = sqlCommand.ExecuteReader();
				reader.Read();
				x = reader.GetInt32(0);
			}

			if (x == 0)
			{
				avaible = true;
			}

			conn.Close();
			return avaible;
		}

		public void set_username(string UID, string username)
		{
			SqlConnection conn;
			string constr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAppDB1;Integrated Security=True;Connect Timeout=30;";


			using (conn = new SqlConnection(constr))
			{
				conn.Open();


				string sql = "UPDATE [dbo].[user_data] SET [Username]='" + username + "' WHERE [UID]='" + UID + "'";

				SqlCommand sqlCommand = new SqlCommand(sql, conn);
				sqlCommand.ExecuteNonQuery();

			}



			conn.Close();

		}
    }
}
