using System;
using System.Data.Entity;
using System.Linq;
using RowLevelSecurity.Aspect;
using RowLevelSecurity.Context;

namespace RowLevelSecurity.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new ExampleContext())
            {
                Database.SetInitializer(new InitializeBlogData());

                var queryManager = new QueryManager();
                var blogs = queryManager.GetBlogs("Mati", context);
                var count = queryManager.GetBlogsCount("Mati", context);
                Console.WriteLine("Mati blogs {0}: ", count);
                foreach (var contextBlog in blogs)
                    Console.WriteLine(contextBlog.Name);

                context.AddRoleToUser("Miki", "Manager");
                blogs = queryManager.GetBlogs("Miki", context);
                count = queryManager.GetBlogsCount("Miki", context);
                Console.WriteLine();
                Console.WriteLine("Miki blogs {0}: ", count);
                foreach (var contextBlog in blogs)
                    Console.WriteLine(contextBlog.Name);

                blogs = queryManager.GetBlogs("Pieter", context);
                count = queryManager.GetBlogsCount("Pieter", context);
                Console.WriteLine();
                Console.WriteLine("Pieter blogs {0}: ", count);
                foreach (var contextBlog in blogs)
                    Console.WriteLine(contextBlog.Name);
                Console.ReadLine();
            }
        }
    }

    public class ExampleContext : RowSecurityContext
    {
        public DbSet<Blog> Blogs { get; set; }
    }

    public class QueryManager
    {
        [AuthorizeAspect]
        public IQueryable<Blog> GetBlogs(string userName, ExampleContext context)
        {
            return context.Blogs;
        }

        [AuthorizeAspect]
        public int GetBlogsCount(string userName, ExampleContext context)
        {
            return context.Blogs.Count();
        }
    }
}