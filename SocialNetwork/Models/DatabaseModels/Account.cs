using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics.Metrics;
using System.Linq;

namespace SocialNetwork.Models.DatabaseModels
{
    public class Account : ICRUD<Account>
    {
        #region Attributes
        public int Id { get; set; }
        public string Name {get;set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime DateRegistration { get; set; }

        public string? Description{ get; set; }
        private bool IsDeleted { get; set; }

        public List<AccountImage> Images { get; set; }

        public List<Chat> Chats { get; set; }

        #endregion

        #region Contructors
        //default constructor
        public Account() { }

        public Account(int id)
        {
            Account searchAccount=Read(id);
            if (searchAccount != null && searchAccount.IsDeleted == false)
            {
                this.Id = searchAccount.Id;
                this.Name = searchAccount.Name;
                this.Email = searchAccount.Email;
                this.Password = searchAccount.Password;
                this.DateRegistration = searchAccount.DateRegistration;
            }
        }

        //constructor with parameters 
        public Account(int id,string name,string email,string password,DateTime datetime,string? description) {
            Email = email;
            Password = password;
            DateRegistration = datetime;
            Description = description;
            Name = name;
            Id = id;
        }
        #endregion


        #region CRUD METHODS
        //Get all accounts from database for using in code
        public static List<Account> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            List<Account> accounts = new List<Account>();
            string sqlQuery = "SELECT * FROM Account";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Account account = new Account();
                account.Id = (int)reader["Id"];
                account.Name = (string)reader["Name"];
                account.Email = (string)reader["Email"];
                account.Password = (string)reader["Password"];
                account.DateRegistration = (DateTime)reader["DateRegistration"];
                if (reader["Description"] != DBNull.Value)
                {
                    account.Description= (string)reader["Description"];
                }
                account.IsDeleted = (bool)reader["IsDeleted"];
                accounts.Add(account);
            }
            reader.Close();
            connection.Close();
            return accounts;
        }

        //create account in database with object
        public static bool Create(Account account,out int id) {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            using (connection)
            {
                string sqlQuery = "INSERT INTO Account (Id,Name, Email, Password, DateRegistration,IsDeleted) VALUES (@Id,@Name, @Email, @Password, @DateTime,@isDeleted)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                id = Account.GetAll().Count();
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", account.Name);
                command.Parameters.AddWithValue("@Email", account.Email);
                command.Parameters.AddWithValue("@Password", account.Password);
                command.Parameters.AddWithValue("@DateTime", account.DateRegistration);
                command.Parameters.AddWithValue("@isDeleted", false);
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

        //get account from database with id
        public static Account Read(int id)
        {
            string connectionString = "Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            string sqlQuery = "SELECT * FROM Account WHERE Id = @Id AND IsDeleted=0";
            Account account = null;

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
                        account = new Account();
                        account.Id = (int)reader["Id"];
                        account.Name = (string)reader["Name"];
                        account.Email = (string)reader["Email"];
                        account.Password = (string)reader["Password"];
                        account.DateRegistration = (DateTime)reader["DateRegistration"];
                        if (reader["Description"] != DBNull.Value)
                        {
                            account.Description = (string)reader["Description"];
                        }
                        account.IsDeleted = (bool)reader["IsDeleted"];
                        SetLists(account);
                    }
                    reader.Close();
                }
                catch(Exception ex)
                {
                    return account;
                }

            }
            return account;
            
        }

        //update account in database with object
        public static bool Update(Account account)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Account SET Name = @Name, Email = @Email, Password = @Password, DateRegistration = @DateTime, Description = @Description WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand( sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", account.Id);
                command.Parameters.AddWithValue("@Name", account.Name);
                command.Parameters.AddWithValue("@Email", account.Email);
                command.Parameters.AddWithValue("@Password", account.Password);
                command.Parameters.AddWithValue("@DateTime", account.DateRegistration);
                command.Parameters.AddWithValue("@Description", (object)account.Description ?? DBNull.Value);
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

        //delete account from database
        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Account SET IsDeleted=@IsDeleted WHERE Id = @Id";
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
        public static bool UserExist(string email,string password,out int id)
        {
            Account account = Account.GetAll().Where(ac => ac.Email == email && ac.Password == password&&ac.IsDeleted==false).FirstOrDefault();
            if (account!=null)
            {
                id = account.Id;
                return true;
            }
            else
            {
                id = -1;
                return false;
            }
        }

        private static void SetLists(Account account)
        {
            account.Images = AccountImage.GetAll().Where(image => image.IsDeleted == false && image.IdAccount == account.Id).ToList();
            account.Chats = Chat.GetAll().Join(
                AccountInChat.GetAll().Where(aic => aic.IdAccount == account.Id),
                chat => chat.Id, 
                aic => aic.IdChat,  
                (chat, aic) => chat).Where(chat=>chat.IsDeleted==false).ToList();
        }
    }
}
