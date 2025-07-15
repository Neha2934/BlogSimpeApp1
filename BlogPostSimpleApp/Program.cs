using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        using var context = new AppDbContext();

        //        if (!context.BlogTypes.Any())
        //        {
        //            var type1 = new BlogType { Name = "Tech", Description = "Tech Blogs", Status = 1 };
        //            var type2 = new BlogType { Name = "Food", Description = "Food Blogs", Status = 2 };
        //            context.BlogTypes.AddRange(type1, type2);
        //            context.SaveChanges();
        //        }
        //        if (!context.PostTypes.Any())
        //        {
        //            var type1 = new PostType { Name = "Animation", Description = "Animation Blogs", Status = 1 };
        //            var type2 = new PostType { Name = "Games", Description = "Games Blogs", Status = 2 };
        //            context.PostTypes.AddRange(type1, type2);
        //            context.SaveChanges();
        //        }
        //        if (!context.Blogs.Any())
        //        {
        //            var type1 = new Blog { Url = "https://en.wikipedia.org/wiki/Tech_blogging", isPublic = true, BlogTypeId = 1 };
        //            var type2 = new Blog { Url = "https://en.wikipedia.org/wiki/Food_blogging", isPublic = true, BlogTypeId = 2 };
        //            context.Blogs.AddRange(type1, type2);
        //            context.SaveChanges();
        //        }

        //        if (!context.Posts.Any())
        //        {
        //            var type1 = new Post { Title = "Cars", Content = "CarsPost", BlogId = 2, PostTypeId = 1, UserId = 1 };
        //            var type2 = new Post { Title = "Bike", Content = "BikesPost", BlogId = 3, PostTypeId = 2, UserId = 2 };
        //            context.Posts.AddRange(type1, type2);
        //            context.SaveChanges();

        //   }


        //var users = new List<User>
        //     {


        // new User { Name = "Arsh", Email = "alice@example.com", PhoneNumber = "2468631236" },
        //  new User { Name = "Kiran", Email = "bob@example.com", PhoneNumber = "9875434598" },
        // new User { Name = "Akash", Email = "charlie@example.com", PhoneNumber = "3321733453" }
        //};

        //context.users.AddRange(users);
        //context.SaveChanges();


        //Console.Write("Enter blog URL: ");
        //var url = Console.ReadLine();
        //var blog = new Blog { Url = url };
        //context.Blogs.Add(blog);
        //context.SaveChanges();


        //var user = context.users.First();
        //var post = new Post
        //{
        //    Title = "Hello Entity Framework",
        //    Content = "This is my first Assignment!",
        //    BlogId = blog.BlogId,
        //    UserId = user.UserId,
        //    PostTypeId = 1
        //};

        //context.Posts.Add(post);
        //context.SaveChanges();


        //var blogs = context.Blogs
        //                 .Include(b => b.Posts)
        //                 .ThenInclude(p => p.User)
        //                 .ToList();

        //foreach (var b in blogs)
        //{
        //    Console.WriteLine($"Blog: {b.Url}");
        //    foreach (var p in b.Posts)
        //    {
        //        Console.WriteLine($"  Post: {p.Title} - {p.Content} (by {p.User?.Name ?? "Unknown"})");
        //    }
        //}
        var blogTypes = new List<BlogType>

        {
            new BlogType {Name = "Corporate", Status = 1, Description = "Corporate blog"},
            new BlogType {Name = "Personal", Status= 2, Description = "Personal blog"},
            new BlogType {Name = "Private", Status= 3, Description = "Private blog"},
        };

        var blogs = new List<Blog>
        {
            new Blog {Url = "www.corporateblog.com", BlogType = blogTypes[0]},
            new Blog {Url = "www.personalblog.com", BlogType = blogTypes[1]},
            new Blog {Url = "www.privateblog.com", BlogType=blogTypes[2]},
        };

        context.BlogTypes.AddRange(blogTypes);
        context.Blogs.AddRange(blogs);

        context.SaveChanges();
        //// Clear existing data
        //context.Posts.RemoveRange(context.Posts);
        //context.Blogs.RemoveRange(context.Blogs);
        //context.BlogTypes.RemoveRange(context.BlogTypes);
        //context.PostTypes.RemoveRange(context.PostTypes);
        //context.users.RemoveRange(context.users);
        //context.SaveChanges();

        //// Reset identity counters
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('BlogType', RESEED, 0)"); // because table name is BlogType
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Blogs', RESEED, 0)");
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Posts', RESEED, 0)");
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PostTypes', RESEED, 0)"); // because table name is PostType
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");


    }
}