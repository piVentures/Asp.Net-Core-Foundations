using WebApiDemo.Controllers;

namespace WebApiDemo.Models.Repositories
{
    public static class ShirtRepository
    {
        
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt{ShirtId = 1, Brand = "w's Brand", Color = "Blue", Gender = "Men", Price = 30, Size = 10},
            new Shirt{ShirtId = 2, Brand = "x's Brand", Color = "Red", Gender = "Women", Price = 33, Size = 10},
            new Shirt{ShirtId = 3, Brand = "y's Brand", Color = "Green", Gender = "Men", Price = 34, Size = 10},
            new Shirt{ShirtId = 4, Brand = "z's Brand", Color = "White", Gender = "Women", Price = 32, Size = 10}
        };

        public static bool ShirtExists(int id)
        {
            return shirts.Any(x => x.ShirtId ==id);
        }

        public static Shirt? GetShirtById(int id)
        {
            return shirts.FirstOrDefault(x => x.ShirtId ==id);
        }
    }
} 