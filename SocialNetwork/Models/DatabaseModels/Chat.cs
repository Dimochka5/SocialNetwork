using Microsoft.Data.SqlClient;

namespace SocialNetwork.Models.DatabaseModels
{
    public class Chat : ICRUD<Chat>
    {
        #region Atrributes
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; } 

        public List<Message> Messages { get; set; }

        #endregion

        #region Constructors
        public Chat(){}

        public Chat(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion
        #region Methods
        public static List<Chat> GetAll() {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            List<Chat> chats = new List<Chat>();
            string sqlQuery = "SELECT * FROM Chat";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Chat chat = new Chat();
                chat.Id = (int)reader["Id"];
                chat.Name = (string)reader["Name"];
                chat.IsDeleted = (bool)reader["IsDeleted"];
                chats.Add(chat);
            }
            reader.Close();
            connection.Close();
            return chats;
        }
        public static bool Create(Chat chat,out int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            using (connection)
            {
                id = Chat.GetAll().Count();
                string sqlQuery = "INSERT INTO Chat (Id,Name,IsDeleted) VALUES (@Id,@Name,@IsDeleted)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id",id);
                command.Parameters.AddWithValue("@Name", chat.Name);
                command.Parameters.AddWithValue("@IsDeleted", false);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public static Chat Read(int id)
        {
            string connectionString = "Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            string sqlQuery = "SELECT * FROM Chat WHERE Id = @Id AND IsDeleted=0";
            Chat chat = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        chat = new Chat();
                        chat.Id = (int)reader["Id"];
                        chat.Name = (string)reader["Name"];
                        chat.IsDeleted = (bool)reader["IsDeleted"];
                        SetLists(ref chat);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    return chat;
                }

            }
            return chat;
        }
        public static bool Update(Chat chat)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Chat SET Name = @Name WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", chat.Id);
                command.Parameters.AddWithValue("@Name", chat.Name);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Chat SET IsDeleted=@IsDeleted WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@IsDeleted", true);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
        #endregion

        private static void SetLists(ref Chat chat)
        {
            int id = chat.Id;
            chat.Messages = Message.GetAll().Where(m => m.IdChat == id&&m.IsDeleted==false).ToList();
        }
    }
}
