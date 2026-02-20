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

        public static List<Shirt> GetShirts()
        {
            return shirts;
        }
        public static bool ShirtExists(int id)
        {
            return shirts.Any(x => x.ShirtId ==id);
        }

        public static Shirt? GetShirtById(int id)
        {
            return shirts.FirstOrDefault(x => x.ShirtId ==id);
        }

// This method checks if a shirt with the same properties already exists in the repository. It compares the brand, color, gender, and size of the shirt being created with the existing shirts in the repository. If a shirt with the same properties is found, it returns that shirt; otherwise, it returns null.
        public static Shirt? GetShirtByProperties(string? brand, string? color, string? gender, int? size)
        {
            return shirts.FirstOrDefault(x => !string.IsNullOrEmpty(brand) && !string.IsNullOrWhiteSpace(x.Brand) && x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)
            || !string.IsNullOrEmpty(color) && !string.IsNullOrWhiteSpace(x.Color) && x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) && size.HasValue && x.Size.HasValue && x.Size.Value == size.Value);
          
           
        }
                public static void Addshirt(Shirt shirt)
        {
            int maxId = shirts.Max(x => x.ShirtId);
            shirt.ShirtId = maxId + 1;
            shirts.Add(shirt);
        }

        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate = shirts.First(x => x.ShirtId == shirt.ShirtId);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Color = shirt.Color;      
            shirtToUpdate.Size  = shirt.Size;
            shirtToUpdate.Gender = shirt.Gender;
        }

        public static void DeleteShirt(int shirtId)
        {
         var shirt = GetShirtById(shirtId);
         if (shirt != null) 
        {
            shirts.Remove(shirt);
        }
    }
  } 
}