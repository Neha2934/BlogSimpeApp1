using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

class Program
{
    static void Main()
    {
        using var context = new AppDbContext();

        var statuses = new List<Status>
    {
        new Status { StatusCode = 1, Name = "Active", Description = "The blog is live and publicly accessible." },
        new Status { StatusCode = 2, Name = "Draft", Description = "The blog is being drafted and not published." },
        new Status { StatusCode = 3, Name = "Archived", Description = "The blog has been archived and is no longer active." },
        new Status { StatusCode = 4, Name = "Pending Review", Description = "The blog is waiting for review before publishing." },
        new Status { StatusCode = 5, Name = "Rejected", Description = "The blog was rejected during review." },
        new Status { StatusCode = 6, Name = "Deleted", Description = "The blog has been marked as deleted." },
        new Status { StatusCode = 7, Name = "Private", Description = "The blog is private and not publicly accessible." },
        new Status { StatusCode = 8, Name = "Scheduled", Description = "The blog is scheduled to be published at a later time." },
        new Status { StatusCode = 9, Name = "Under Construction", Description = "The blog is under development." },
        new Status { StatusCode = 10, Name = "Hidden", Description = "The blog is hidden from public view but not deleted." }
    };

        context.Statuses.AddRange(statuses);

        // Save once for all data
        context.SaveChanges();
        var blogTypes = new List<BlogType>
    {
        new BlogType { Status = 1, Name = "Corporate", Description = "Official company blogs" },
        new BlogType { Status = 1, Name = "Personal", Description = "Personal life experiences and thoughts" },
        new BlogType { Status = 1, Name = "Private", Description = "Restricted or confidential blogs" },
        new BlogType { Status = 1, Name = "Tech", Description = "Blogs about technology and development" },
        new BlogType { Status = 1, Name = "Travel", Description = "Travel diaries and guides" },
        new BlogType { Status = 1, Name = "Food", Description = "Recipes, reviews, and culinary experiences" },
        new BlogType { Status = 1, Name = "Education", Description = "Educational content and tutorials" },
        new BlogType { Status = 1, Name = "Health", Description = "Health tips and wellness guides" },
        new BlogType { Status = 1, Name = "Finance", Description = "Money management, investing, and budgeting" },
        new BlogType { Status = 1, Name = "News", Description = "Current events and updates" }
    };

        context.BlogTypes.AddRange(blogTypes);
        context.SaveChanges();
        var blogs = new List<Blog>
    {
        new Blog { Url = "https://insightcorp.com", isPublic = true, BlogTypeId = 1, StatusId = 1 },
        new Blog { Url = "https://lifejournal.net", isPublic = false, BlogTypeId = 2, StatusId = 2 },
        new Blog { Url = "https://hiddenvault.org", isPublic = false, BlogTypeId = 3, StatusId = 3 },
        new Blog { Url = "https://codechronicles.dev", isPublic = true, BlogTypeId = 4, StatusId = 1 },
        new Blog { Url = "https://roamwithme.travel", isPublic = true, BlogTypeId = 5, StatusId = 1 },
        new Blog { Url = "https://flavorexplorer.com", isPublic = true, BlogTypeId = 6, StatusId = 1 },
        new Blog { Url = "https://eduvisionacademy.org", isPublic = true, BlogTypeId = 7, StatusId = 1 },
        new Blog { Url = "https://wellnessguide.io", isPublic = true, BlogTypeId = 8, StatusId = 2 },
        new Blog { Url = "https://financeinsighthub.com", isPublic = false, BlogTypeId = 9, StatusId = 4 },
        new Blog { Url = "https://nowdailynews.com", isPublic = true, BlogTypeId = 10, StatusId = 1 }
    };

        context.Blogs.AddRange(blogs);
        context.SaveChanges();



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
        //var blogTypes = new List<BlogType>

        //{
        //    new BlogType {Name = "Corporate", Status = 1, Description = "Corporate blog"},
        //    new BlogType {Name = "Personal", Status= 2, Description = "Personal blog"},
        //    new BlogType {Name = "Private", Status= 3, Description = "Private blog"},
        //};

        //var blogs = new List<Blog>
        //{
        //    new Blog {Url = "www.corporateblog.com", BlogType = blogTypes[0]},
        //    new Blog {Url = "www.personalblog.com", BlogType = blogTypes[1]},
        //    new Blog {Url = "www.privateblog.com", BlogType=blogTypes[2]},
        //};

        //context.BlogTypes.AddRange(blogTypes);
        //context.Blogs.AddRange(blogs);


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