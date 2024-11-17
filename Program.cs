using BlogPost;
namespace EFgetStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new BloggingContext();

            // Ensure the database is created and migrations are applied
            db.Database.EnsureCreated();  // Creates the database if it doesn't exist

            Console.WriteLine($"Database path: {db.DbPath}.");

            // Create
            Console.WriteLine("Inserting a new blog");
            db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            db.SaveChanges();

            // Read
            Console.WriteLine("Querying for a blog");
            var blog = db.Blogs
                .OrderBy(b => b.BlogId)
                .First();

            // Update
            Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(
                new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
            db.SaveChanges();

            // Delete
            Console.WriteLine("Delete the blog");
            db.Remove(blog);
            db.SaveChanges();

            var ans = db.Blogs.ToList();
            foreach(var x in ans)
            {
                Console.WriteLine("New Data"+x.BlogId+" "+x.Url);
            }
        }
    }
}
