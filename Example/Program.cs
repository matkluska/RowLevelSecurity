 using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using RowLevelSecurity.Context;

namespace RowLevelSecurity.Example
{
    internal class Program
    {
        /*private static void Main(string[] args)
        {
            using (var context = new ExampleContext())
            {
                Database.SetInitializer(new InitializeBlogData());

                context.SetUsername("Mati");
                Console.WriteLine("Mati blogs: ");
                foreach (var contextBlog in context.Blogs)
                    Console.WriteLine(contextBlog.Name);
                context.SetUsername("Miki");
                Console.WriteLine();
                Console.WriteLine("Miki blogs: ");
                foreach (var contextBlog in context.Blogs)
                    Console.WriteLine(contextBlog.Name);
                Console.ReadLine();
            }
        }*/
    }

    [Table("BlogSecuredContext")]
    public class ExampleContext : RowSecurityContext
    {
        public DbSet<Blog> Blogs { get; set; }
    }

//    public class QueryManager
//    {
//        [Authorize(EntityType = typeof(Blog))]
//        public IEnumerable<Blog> GetBlogs(string userName, string connectionString)
//        {
//            var context = new ExampleContext();
//            return context.Blogs.AsEnumerable();
//        }
//    }
}