using Microsoft.Data.SqlClient;

namespace SocialNetwork.Models.DatabaseModels
{
    public class Reaction:ICRUD<Reaction>
    {
        #region Atrributes 
        public int Id { get; set; }

        public int ReactionValue { get; set; }

        public int IdAccount { get; set; }

        public int IdPost { get; set; }

        public bool IsDeleted { get; set; }

        #endregion

        #region Contructors
        //default constructor
        public Reaction() { }

        //constructor with parameters 
        public Reaction(int id, int reaction, int idAccount, int idPost)
        {
            this.Id = id;
            this.ReactionValue = reaction;
            this.IdPost = idPost;
            this.IdAccount = idAccount;
            this.IsDeleted = false;
        }
        #endregion

        #region CRUD METHODS
        //return all reactions from database
        public static IEnumerable<Reaction> GetAll()
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            List<Reaction> reactions = new List<Reaction>();
            string sqlQuery = "SELECT * FROM Reaction";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Reaction reaction = new Reaction();
                reaction.Id = (int)reader["Id"];
                reaction.ReactionValue = (int)reader["ReactionValue"];
                reaction.IdAccount = (int)reader["IdAccount"];
                reaction.IdPost = (int)reader["IdPost"];
                reaction.IsDeleted = (bool)reader["IsDeleted"];
                reactions.Add(reaction);
            }
            reader.Close();
            connection.Close();
            return reactions.Where(account => account.IsDeleted == false);
        }

        private static List<Reaction> GetAll(int i)
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            List<Reaction> reactions = new List<Reaction>();
            string sqlQuery = "SELECT * FROM Reaction";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Reaction reaction = new Reaction();
                reaction.Id = (int)reader["Id"];
                reaction.ReactionValue = (int)reader["ReactionValue"];
                reaction.IdAccount = (int)reader["IdAccount"];
                reaction.IdPost = (int)reader["IdPost"];
                reaction.IsDeleted = (bool)reader["IsDeleted"];
                reactions.Add(reaction);
            }
            reader.Close();
            connection.Close();
            return reactions;
        }

        //create reaction in database with attributes
        public static bool Create(int id, int reaction, int idAccount, int idPost)
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            using (connection)
            {
                string sqlQuery = "INSERT INTO Reaction (Id,ReactionValue, IdAccount, IdPost, IsDeleted) VALUES (@Id,@Reaction, @IdAccount, @IdPost, @isDeleted)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Reaction", reaction);
                command.Parameters.AddWithValue("@IdAccount", idAccount);
                command.Parameters.AddWithValue("@IdPost", idPost);
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

        //create reaction in database with object
        public static bool Create(Reaction reaction)
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            using (connection)
            {
                string sqlQuery = "INSERT INTO Reaction (Id,ReactionValue, IdAccount, IdPost, IsDeleted) VALUES (@Id,@Reaction, @IdAccount, @IdPost, @isDeleted)";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", reaction.Id);
                command.Parameters.AddWithValue("@Reaction", reaction.ReactionValue);
                command.Parameters.AddWithValue("@IdAccount", reaction.IdAccount);
                command.Parameters.AddWithValue("@IdPost", reaction.IdPost);
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

        //get reaction from database with id
        public static Reaction Read(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            string sqlQuery = "SELECT * FROM Reaction WHERE Id = @Id";
            Reaction reaction = null;
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
                        reaction=new Reaction();
                        reaction.Id = (int)reader["Id"];
                        reaction.ReactionValue = (int)reader["ReactionValue"];
                        reaction.IdAccount = (int)reader["IdAccount"];
                        reaction.IdPost = (int)reader["IdPost"];
                        reaction.IsDeleted = (bool)reader["IsDeleted"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return reaction;
        }

        //update reaction in database with object
        public static bool Update(Reaction reaction)
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            string sqlQuery = "UPDATE Reaction SET ReactionValue=@Reaction,IdAccount=@IdAccount,IdPost=@IdPost, IsDeleted=@IsDeleted WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", reaction.Id);
                command.Parameters.AddWithValue("@Reaction", reaction.ReactionValue);
                command.Parameters.AddWithValue("@IdAccount", reaction.IdAccount);
                command.Parameters.AddWithValue("@IdPost", reaction.IdPost);
                command.Parameters.AddWithValue("@IsDeleted", reaction.IsDeleted);
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

        //update reaction in database with attributes
        public static bool Update(int id, int reaction, int idPost, int idAccount, bool isDeleted)
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            string sqlQuery = "UPDATE Reaction SET ReactionValue=@Reaction,IdPost=@IdPost, IdAccount=@IdAccount,IsDeleted=@IsDeleted WHERE Id = @Id";
            using (connection)
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Reaction", reaction);
                command.Parameters.AddWithValue("@IdPost", idPost);
                command.Parameters.AddWithValue("@IdAccount", idAccount);
                command.Parameters.AddWithValue("@IsDeleted", isDeleted);
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

        //delete reaction from database
        public static bool Delete(int id)
        {
            SqlConnection connection = new SqlConnection("Data Source = DESKTOP - CPI51I3\\MSSQLSERVER01; Initial Catalog = SocialNetworkDB; Integrated Security = True; Trust Server Certificate = True");
            string sqlQuery = "UPDATE Reaction SET IsDeleted=@IsDeleted WHERE Id = @Id";
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

        public bool Delete()
        {
            if (this.Id != null)
            {
                if (Reaction.Delete(this.Id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Update()
        {
            if (this.Id != null || this.ReactionValue!=null || this.IdAccount!= null||this.IdPost!=null||this.IsDeleted!=null)
            {
                if (Reaction.Update(this.Id, this.ReactionValue, this.IdPost,this.IdAccount, this.IsDeleted))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Create()
        {
            if (this.Id != null || this.ReactionValue != null || this.IdAccount != null || this.IdPost != null)
            {
                this.Id = Reaction.GetAll(1).Count();
                this.IsDeleted = false;
                if (Reaction.Create(this.Id, this.ReactionValue,this.IdAccount,this.IdPost))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Reaction Read()
        {
            if (this.Id != null)
            {
                return Reaction.Read(this.Id);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
