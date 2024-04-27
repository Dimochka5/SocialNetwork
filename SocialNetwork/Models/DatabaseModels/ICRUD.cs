using System.Runtime.InteropServices;

namespace SocialNetwork.Models.DatabaseModels
{
    public interface ICRUD<T> where T : class
    {
        public static List<T> GetAll;
        public static bool Create;
        public static T Read;
        public static bool Update;
    }
}
