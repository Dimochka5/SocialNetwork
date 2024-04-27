using Microsoft.Data.SqlClient;

namespace SocialNetwork.Models.DatabaseModels
{
    public class PostImage : ICRUD<PostImage>
    {
        #region Atrributes
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public int IdPost { get; set; }

        public bool IsDeleted { get; set; }

        #endregion

        #region Contructors

        public PostImage() { }

        public PostImage(int id, byte[] image, int idPost)
        {
            Id = id;
            Image = image;
            IdPost = idPost;
            IsDeleted = false;
        }

        #endregion

        #region CRUD METHODS
        //return all post images from database
        public static IEnumerable<PostImage> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            List<PostImage> images = new List<PostImage>();
            string sqlQuery = "SELECT * FROM PostImage";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PostImage image = new PostImage();
                image.Id = (int)reader["Id"];
                image.Image = (byte[])reader["Image"];
                image.IdPost = (int)reader["IdPost"];
                image.IsDeleted = (bool)reader["IsDeleted"];
                images.Add(image);
            }
            reader.Close();
            connection.Close();
            return images;
        }


        //create post image in database with object
        public static bool Create(PostImage image)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            using (connection)
            {
                string sqlQuery = "INSERT INTO PostImage (Id,Image, IdPost, IsDeleted) VALUES (@Id,@Image, @IdPost, @isDeleted)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", GetAll().Count());
                command.Parameters.AddWithValue("@Image", image.Image);
                command.Parameters.AddWithValue("@IdPost", image.IdPost);
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

        //get post image from database with id
        public static PostImage Read(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "SELECT * FROM PostImage WHERE Id = @Id";
            PostImage image = null;
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
                        image = new PostImage();
                        image.Id = (int)reader["Id"];
                        image.Image = (byte[])reader["Image"];
                        image.IdPost = (int)reader["IdAccount"];
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

        //update post image in database with object
        public static bool Update(PostImage image)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE PostImage SET Image=@Image,IdAccount=@IdAccount, IsDeleted=@IsDeleted WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", image.Id);
                command.Parameters.AddWithValue("@Image", image.Image);
                command.Parameters.AddWithValue("@IdAccount", image.IdPost);
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

        //delete post image from database
        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE PostImage SET IsDeleted=@IsDeleted WHERE Id = @Id";
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
