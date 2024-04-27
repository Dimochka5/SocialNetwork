using Microsoft.Data.SqlClient;

namespace SocialNetwork.Models.DatabaseModels
{
    public class AccountInChat:ICRUD<AccountInChat>
    {
            #region Atrributes
            public int Id { get; set; }

            public int IdAccount { get; set; }

            public int IdChat {  get; set; }

            public bool IsDeleted { get; set; }

            #endregion

            #region Constructors
            public AccountInChat() { }

            public AccountInChat(int id, int idAccount,int idChat,bool isDeleted)
            {
                Id = id;
                IdAccount=idAccount;
                IdChat=idChat;
                IsDeleted =isDeleted;
            }

            #endregion
            #region Methods
            public static List<AccountInChat> GetAll()
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
                List<AccountInChat> aicList = new List<AccountInChat>();
                string sqlQuery = "SELECT * FROM UserInChat";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AccountInChat aic = new AccountInChat();
                    aic.Id = (int)reader["Id"];
                    aic.IdAccount = (int)reader["IdAccount"];
                    aic.IdChat = (int)reader["IdChat"];
                    aic.IsDeleted = (bool)reader["IsDeleted"];
                    aicList.Add(aic);
                }
                reader.Close();
                connection.Close();
                return aicList;
            }
            public static bool Create(AccountInChat accountInChat)
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
                using (connection)
                {
                    string sqlQuery = "INSERT INTO UserInChat(Id,IdAccount,IdChat,IsDeleted) VALUES (@Id,@IdAccount,@IdChat,@IsDeleted)";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Id", AccountInChat.GetAll().Count());
                    command.Parameters.AddWithValue("@IdAccount", accountInChat.IdAccount);
                    command.Parameters.AddWithValue("@IdChat", accountInChat.IdChat);
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
            public static AccountInChat Read(int id)
            {
                string connectionString = "Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                string sqlQuery = "SELECT * FROM UserInChat WHERE Id = @Id AND IsDeleted=0";
                AccountInChat aic = null;

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
                            aic = new AccountInChat();
                            aic.Id = (int)reader["Id"];
                            aic.IdChat = (int)reader["IdChat"];
                            aic.IdAccount =(int)reader["IdAccount"];
                            aic.IsDeleted =(bool)reader["IsDeleted"];
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        return aic;
                    }

                }
                return aic;
            }
            public static bool Update(AccountInChat aic)
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
                string sqlQuery = "UPDATE UserInChat SET IdAccount=@IdAccount, IdChat=@IdChat WHERE Id = @Id";
                using (connection)
                {
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Id", aic.Id);
                    command.Parameters.AddWithValue("@IdAccount", aic.IdAccount);
                    command.Parameters.AddWithValue("@IdChat", aic.IdChat);
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
                string sqlQuery = "UPDATE UserInChat SET IsDeleted=@IsDeleted WHERE Id = @Id";
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
     }
}


