using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Transactions;

namespace SocialNetwork.Models.DatabaseModels
{
    public class Post : ICRUD<Post> {
        #region Atrributes
        public int Id { get; set; } 

        public string Text {  get; set; }

        public DateTime DateTime { get; set; }  

        public bool IsDeleted { get; set; }

        public int IdAccount { get; set; }

        public List<PostImage> Images { get; set; }

        public List<Reaction> Reactions { get; set; }

        public List<Views> Viewss { get; set; }

        public List<Comment> Comments { get; set; }

        public Account Account { get; set; }
        #endregion

        #region Constructors
        public Post() { }

        public Post(int id, string text, DateTime dateTime)
        {
            Id = id;
            Text = text;
            DateTime = dateTime;
            IsDeleted = false;
        }

        #endregion

        #region CRUDMethods

        public static IEnumerable<Post> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            List<Post> posts = new List<Post>();
            string sqlQuery = "SELECT * FROM Post";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Post post=new Post();
                post.Id = (int)reader["Id"];
                post.Text = (string)reader["Text"];
                post.DateTime = (DateTime)reader["DateTime"];
                post.IsDeleted = (bool)reader["IsDeleted"];
                post.IdAccount = (int)reader["IdAccount"];
                Post.SetLists(ref post);
                posts.Add(post);
            }
            reader.Close();
            connection.Close();
            return posts;
        }

        public static bool Create(Post ob)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            using (connection)
            {
                string sqlQuery = "INSERT INTO Post (Id,Text,DateTime, IsDeleted,IdAccount) VALUES (@Id,@Text, @DateTime, @isDeleted,@IdAccount)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", Post.GetAll().Count());
                command.Parameters.AddWithValue("@Text", ob.Text);
                command.Parameters.AddWithValue("@DateTime", ob.DateTime);
                command.Parameters.AddWithValue("@IdAccount", ob.IdAccount);
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

        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Post SET IsDeleted=@IsDeleted WHERE Id = @Id";
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

        public static Post Read(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "SELECT * FROM Post WHERE Id = @Id";
            Post post = null;
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
                        post=new Post();
                        post.Id = (int)reader["Id"];
                        post.Text = (string)reader["Text"];
                        post.DateTime= (DateTime)reader["DateTime"];
                        post.IsDeleted = (bool)reader["IsDeleted"];
                        post.IdAccount = (int)reader["IdAccount"];
                        SetLists(ref post);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return post;
        }

        public static bool Update(Post ob)
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CPI51I3\\MSSQLSERVER01;Initial Catalog=SocialNetworkDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            string sqlQuery = "UPDATE Post SET Text=@Text,DateTime=@DateTime, IsDeleted=@IsDeleted WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", ob.Id);
                command.Parameters.AddWithValue("@Text", ob.Text);
                command.Parameters.AddWithValue("@DateTime", ob.DateTime);
                command.Parameters.AddWithValue("@IsDeleted", ob.IsDeleted);
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

        private static void SetLists(ref Post post)
        {
            int id = post.Id;
            post.Comments = Comment.GetAll().Where(comment => comment.IsDeleted == false && comment.IdPost ==id).ToList();
            post.Images = PostImage.GetAll().Where(image => image.IsDeleted == false && image.IdPost ==id).ToList();
            //post.Viewss = Views.GetAll().Where(views => views.IdPost == post.Id && views.IsDeleted == false).ToList();
            post.Account = Account.Read(post.IdAccount);
            //post.Reactions = Reaction.GetAll().Where(reaction => reaction.IsDeleted == false && reaction.IdAccount == post.Id).ToList();
        }


        #endregion
    }
}
