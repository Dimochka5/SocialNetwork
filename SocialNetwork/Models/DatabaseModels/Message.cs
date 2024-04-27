using Microsoft.Data.SqlClient;

namespace SocialNetwork.Models.DatabaseModels
{
    public class Message:ICRUD<Message>
    {
            #region Atrributes
            public int Id { get; set; }

            public string Text {  get; set; }   
            public DateTime Time { get; set; }
            public int IdAccount { get; set; }

            public int IdChat { get; set; }

            public bool IsDeleted { get; set; }

            public Account Account { get; set; }

            #endregion

            #region Constructors
            public Message() { }

            public Message(int id, int idAccount, int idChat, bool isDeleted,string text,DateTime time)
            {
                Id = id;
                IdAccount = idAccount;
                IdChat = idChat;
                IsDeleted = isDeleted;
                Text = text;
                Time = time;
            }

            #endregion
            #region Methods
            public static List<Message> GetAll()
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
                List<Message> messages = new List<Message>();
                string sqlQuery = "SELECT * FROM Message";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Message message = new Message();
                    message.Id = (int)reader["Id"];
                    message.IdAccount = (int)reader["IdAccount"];
                    message.IdChat = (int)reader["IdChat"];
                    message.IsDeleted = (bool)reader["IsDeleted"];
                    message.Text = (string)reader["Text"];
                    message.Time = (DateTime)reader["Time"];
                    SetLists(ref message);
                    messages.Add(message);
                }
                reader.Close();
                connection.Close();
                return messages;
            }
            public static bool Create(Message message,out int id)
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
                using (connection)
                {
                    string sqlQuery = "INSERT INTO Message(Id,IdAccount,IdChat,Text,Time,IsDeleted) VALUES (@Id,@IdAccount,@IdChat,@Text,@Time,@IsDeleted)";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    id = Message.GetAll().Count();
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@IdAccount", message.IdAccount);
                    command.Parameters.AddWithValue("@IdChat", message.IdChat);
                    command.Parameters.AddWithValue("@Text",message.Text);
                    command.Parameters.AddWithValue("@Time",(DateTime)message.Time);
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
            public static Message Read(int id)
            {
                string connectionString = "Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                string sqlQuery = "SELECT * FROM Message WHERE Id = @Id AND IsDeleted=0";
                Message message = null;

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
                            message = new Message();
                            message.Id = (int)reader["Id"];
                            message.IdChat = (int)reader["IdChat"];
                            message.IdAccount = (int)reader["IdAccount"];
                            message.IsDeleted = (bool)reader["IsDeleted"];
                            message.Text = (string)reader["Text"];
                            message.Time = (DateTime)reader["Time"];
                        }
                        reader.Close();
                        SetLists(ref message);
                        return message;
                    }
                    catch (Exception ex)
                    {
                        return message;
                    }

                }
                return message;
            }
            public static bool Update(Message message)
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
                string sqlQuery = "UPDATE Message SET IdAccount=@IdAccount, IdChat=@IdChat,Text=@Text WHERE Id = @Id";
                using (connection)
                {
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Id", message.Id);
                    command.Parameters.AddWithValue("@IdAccount", message.IdAccount);
                    command.Parameters.AddWithValue("@IdChat", message.IdChat);
                    command.Parameters.AddWithValue("Text",message.Text);
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
                string sqlQuery = "UPDATE Message SET IsDeleted=@IsDeleted WHERE Id = @Id";
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

        private static void SetLists(ref Message message)
        {
            if (message != null)
            {
                message.Account = Account.Read(message.IdAccount);
            }
        }

        #endregion
    }
}



