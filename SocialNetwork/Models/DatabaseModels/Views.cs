using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;

namespace SocialNetwork.Models.DatabaseModels
{
    public class Views:ICRUD<Views>
    {
        #region Atrributes 
        public int Id { get; set; }

        public int IdPost {  get; set; }

        public int IdAccount {  get; set; }

        public bool IsDeleted {  get; set; }

        #endregion

        #region Contructors
        //default constructor
        public Views() { }

        //constructor with parameters 
        public Views(int id, int idPost,int idAccount)
        {
            this.Id = id;
            this.IdPost = idPost;
            this.IdAccount = idAccount;
            this.IsDeleted = false;
        }
        #endregion

        #region CRUD METHODS
        //return all views from database
        public static IEnumerable<Views> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            List<Views> views = new List<Views>();
            string sqlQuery = "SELECT * FROM Views";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Views view = new Views();
                view.Id = (int)reader["Id"];
                view.IdAccount = (int)reader["IdAccount"];
                view.IdPost = (int)reader["IdPost"];
                view.IsDeleted = (bool)reader["IsDeleted"];
                views.Add(view);
            }
            reader.Close();
            connection.Close();
            return views;
        }

        //create views in database with object
        public static bool Create(Views view)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            using (connection)
            {
                IEnumerable<Views> views = Views.GetAll();
                if (views.Where(v=>v.IdAccount==view.IdAccount&&v.IdPost==view.IdPost).IsNullOrEmpty()) {
                    string sqlQuery = "INSERT INTO Views (Id, IdAccount, IdPost, IsDeleted) VALUES (@Id, @IdAccount, @IdPost, @isDeleted)";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@Id", views.Count());
                    command.Parameters.AddWithValue("@IdAccount", view.IdAccount);
                    command.Parameters.AddWithValue("@IdPost", view.IdPost);
                    command.Parameters.AddWithValue("@isDeleted", false);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        //get views from database with id
        public static Views Read(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "SELECT * FROM Views WHERE Id = @Id";
            Views view = null;
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        view = new Views();
                        view.Id = (int)reader["Id"];
                        view.IdAccount = (int)reader["IdAccount"];
                        view.IdPost = (int)reader["IdPost"];
                        view.IsDeleted = (bool)reader["IsDeleted"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return view;
        }

        //update views in database with object
        public static bool Update(Views view)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Views SET IdAccount = @IdAccount, IdPost=@IdPost, IsDeleted=@IsDeleted WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", view.Id);
                command.Parameters.AddWithValue("@IdAccount", view.IdAccount);
                command.Parameters.AddWithValue("@IdPost", view.IdPost);
                command.Parameters.AddWithValue("@IsDeleted", view.IsDeleted);
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

        //delete views from database
        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Views SET IsDeleted=@IsDeleted WHERE Id = @Id";
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
