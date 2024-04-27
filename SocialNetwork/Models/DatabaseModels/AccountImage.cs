using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace SocialNetwork.Models.DatabaseModels
{
    public class AccountImage : ICRUD<AccountImage>
    {
        #region Atrributes
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public int IdAccount { get; set; }

        public bool IsDeleted { get; set; }

        #endregion

        #region Contructors

        public AccountImage() { }

        public AccountImage(int id, byte[] image, int idAccount)
        {
            Id = id;
            Image = image;
            IdAccount = idAccount;
            IsDeleted = false;
        }

        #endregion

        #region CRUD METHODS
        //return all account images from database
        public static IEnumerable<AccountImage> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            List<AccountImage> images = new List<AccountImage>();
            string sqlQuery = "SELECT * FROM AccountImage";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                AccountImage image = new AccountImage();
                image.Id = (int)reader["Id"];
                image.Image = (byte[])reader["Image"];
                image.IdAccount = (int)reader["IdAccount"];
                image.IsDeleted = (bool)reader["IsDeleted"];
                images.Add(image);
            }
            reader.Close();
            connection.Close();
            return images;
        }

        //create account image in database with object
        public static bool Create(AccountImage image)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            using (connection)
            {
                string sqlQuery = "INSERT INTO AccountImage (Id,Image, IdAccount, IsDeleted) VALUES (@Id,@AccountImage, @IdAccount, @isDeleted)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", AccountImage.GetAll().Count()+1);
                command.Parameters.AddWithValue("@AccountImage", image.Image);
                command.Parameters.AddWithValue("@IdAccount", image.IdAccount);
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
        }

        //get account image from database with id
        public static AccountImage Read(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "SELECT * FROM AccountImage WHERE Id = @Id";
            AccountImage image = null;
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
                        image = new AccountImage();
                        image.Id = (int)reader["Id"];
                        image.Image = (byte[])reader["Image"];
                        image.IdAccount = (int)reader["IdAccount"];
                        image.IsDeleted = (bool)reader["IsDeleted"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return image;
        }

        //update account image in database with object
        public static bool Update(AccountImage image)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE AccountImage SET Image=@Image,IdAccount=@IdAccount, IsDeleted=@IsDeleted WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", image.Id);
                command.Parameters.AddWithValue("@Image", image.Image);
                command.Parameters.AddWithValue("@IdAccount", image.IdAccount);
                command.Parameters.AddWithValue("@IsDeleted", image.IsDeleted);
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


        //delete account image from database
        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            string sqlQuery = "UPDATE AccountImage SET IsDeleted=@IsDeleted WHERE Id = @Id";
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
