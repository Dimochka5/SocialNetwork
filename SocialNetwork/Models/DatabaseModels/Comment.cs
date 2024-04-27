using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace SocialNetwork.Models.DatabaseModels
{
    public class Comment:ICRUD<Comment>
    {
        #region Atrributes 
        public int Id { get; set; }

        public string Text { get; set; }    

        public DateTime DateTime { get; set; }

        public int IdAccount {  get; set; } 

        public int IdPost { get; set; }

        public bool IsDeleted { get; set; }

        #endregion

        #region Contructors
        //default constructor
        public Comment() { }

        //constructor with parameters 
        public Comment(int id, string text,DateTime datetime,int idAccount,int idPost)
        {
            this.Id = id;
            this.Text = text;
            this.DateTime = datetime;
            this.IdPost = idPost;
            this.IdAccount = idAccount;
            this.IsDeleted = false;
        }
        #endregion

        #region CRUD METHODS
        //return all comments from database
        public static IEnumerable<Comment> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            List<Comment> comments = new List<Comment>();
            string sqlQuery = "SELECT * FROM Comment";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Comment comment=new Comment();
                comment.Id = (int)reader["Id"];
                comment.Text = (string)reader["Text"];
                comment.DateTime = (DateTime)reader["DateTime"];
                comment.IdAccount = (int)reader["IdAccount"];
                comment.IdPost = (int)reader["IdPost"];
                comment.IsDeleted = (bool)reader["IsDeleted"];
                comments.Add(comment);
            }
            reader.Close();
            connection.Close();
            return comments;
        }

        //create comment in database with object
        public static bool Create(Comment comment)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            using (connection)
            {
                string sqlQuery = "INSERT INTO Comment (Id,Text,DateTime, IdAccount, IdPost, IsDeleted) VALUES (@Id,@Text,@DateTime, @IdAccount, @IdPost, @isDeleted)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", Comment.GetAll().Count()+1);
                command.Parameters.AddWithValue("@Text", comment.Text);
                command.Parameters.AddWithValue("@DateTime", comment.DateTime);
                command.Parameters.AddWithValue("@IdAccount", comment.IdAccount);
                command.Parameters.AddWithValue("@IdPost", comment.IdPost);
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

        //get comment from database with id
        public static Comment Read(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "SELECT * FROM Comment WHERE Id = @Id";
            Comment comment = null;
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
                        comment = new Comment();
                        comment.Id = (int)reader["Id"];
                        comment.Text = (string)reader["Text"];
                        comment.DateTime = (DateTime)reader["DateTime"];
                        comment.IdAccount = (int)reader["IdAccount"];
                        comment.IdPost = (int)reader["IdPost"];
                        comment.IsDeleted = (bool)reader["IsDeleted"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return comment;
        }

        //update comment in database with object
        public static bool Update(Comment comment)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Comment SET Text = @Text, DateTime=@DateTime,IdAccount=@IdAccount,IdPost=@IdPost, IsDeleted=@IsDeleted WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", comment.Id);
                command.Parameters.AddWithValue("@Text", comment.Text);
                command.Parameters.AddWithValue("@DateTime", comment.DateTime);
                command.Parameters.AddWithValue("@IdAccount", comment.IdAccount);
                command.Parameters.AddWithValue("@IdPost", comment.IdPost);
                command.Parameters.AddWithValue("@IsDeleted", comment.IsDeleted);
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

        //delete comment from database
        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Comment SET IsDeleted=@IsDeleted WHERE Id = @Id";
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
