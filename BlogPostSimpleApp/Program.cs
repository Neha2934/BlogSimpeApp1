
using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        using var context = new AppDbContext();

        var blogType1 = new BlogType { Status = 1, Name = "Neha", Description = "Myself" };
        var blogType2 = new BlogType { Status = 2, Name = "Sumit", Description = "Sumit" };
        context.BlogTypes.AddRange(blogType1, blogType2);
        context.SaveChanges();


        var status1 = new Status { StatusCode = 1, Name = "Active", Description = "Active status" };
        var status2 = new Status { StatusCode = 2, Name = "Inactive", Description = "Inactive status" };
        context.Statuses.AddRange(status1, status2);
        context.SaveChanges();


        var blog1 = new Blog { Url = "www.Neha.com", isPublic = true, BlogTypeId = blogType1.BlogTypeId, StatusId = status1.StatusId };
        var blog2 = new Blog { Url = "www.Sumit.com", isPublic = true, BlogTypeId = blogType2.BlogTypeId, StatusId = status2.StatusId };
        context.Blogs.AddRange(blog1, blog2);
        context.SaveChanges();


        var postType1 = new PostType { Status = 1, Name = "Delhi", Description = "City Name" };
        var postType2 = new PostType { Status = 2, Name = "Canada", Description = "The Best Country" };
        context.PostTypes.AddRange(postType1, postType2);
        context.SaveChanges();


        var user1 = new User { Name = "Neha", Email = "neha@gmail.com", PhoneNumber = "6758465733" };
        var user2 = new User { Name = "Sumit", Email = "sumit@gmail.com", PhoneNumber = "567876543" };
        context.users.AddRange(user1, user2);
        context.SaveChanges();


        var post1 = new Post { Title = "Mother", Content = "Details Included in the Bio", BlogId = blog1.BlogId, PostTypeId = postType1.PostTypeId, UserId = user1.UserId };
        var post2 = new Post { Title = "Cook", Content = "By Lots of Practicing", BlogId = blog2.BlogId, PostTypeId = postType2.PostTypeId, UserId = user2.UserId };
        context.Posts.AddRange(post1, post2);
        context.SaveChanges();



        var results = context.Posts
       .Include(p => p.Blog)
       .ThenInclude(b => b.BlogType)
       .Include(p => p.Blog.Status)
       .Include(p => p.PostType)
       .Include(p => p.User)
       .Where(p =>
              p.PostType.Status == 1 &&
              p.Blog.BlogType.Status == 1 &&
              p.Blog.Status.Name == "Active")
       .Select(p => new
       {
           BlogUrl = p.Blog.Url,
           BlogIsPublic = p.Blog.isPublic,
           BlogTypeName = p.Blog.BlogType.Name,
           UserName = p.User.Name,
           UserEmail = p.User.Email,
           TotalUserPosts = context.Posts.Count(x => x.UserId == p.UserId),
           PostTypeName = p.PostType.Name
       })
       .OrderBy(p => p.UserName)
       .ToList();


        foreach (var item in results)
        {
            Console.WriteLine("==================================");
            Console.WriteLine($"Blog URL: {item.BlogUrl}");
            Console.WriteLine($"Is Public: {item.BlogIsPublic}");
            Console.WriteLine($"Blog Type: {item.BlogTypeName}");
            Console.WriteLine($"User Name: {item.UserName}");
            Console.WriteLine($"User Email: {item.UserEmail}");
            Console.WriteLine($"Total Posts by User: {item.TotalUserPosts}");
            Console.WriteLine($"Post Type: {item.PostTypeName}");
        }
    }
}

////using BlogPostSimpleApp.Models;
////using Microsoft.EntityFrameworkCore;
////using System;
////using System.Linq;

////class Program
////{
////    static void Main(string[] args)
////    {
////        using var context = new AppDbContext();

////        // Step 1: Return all Blog properties + BlogType Name
////        var blogsWithType = context.Blogs
////            .Select(b => new
////            {
////                b.BlogId,
////                b.Url,
////                b.isPublic,
////                b.BlogTypeId,
////                BlogTypeName = b.BlogType.Name
////            })
////            .ToList();

////        Console.WriteLine("Step 1: Blogs with BlogType Name");
////        foreach (var b in blogsWithType)
////        {
////            Console.WriteLine($"BlogId: {b.BlogId}, Url: {b.Url}, Public: {b.isPublic}, BlogType: {b.BlogTypeName}");
////        }

////        // Step 2: Add total number of Posts per Blog
////        var blogsWithPostCount = context.Blogs
////            .Select(b => new
////            {
////                b.BlogId,
////                b.Url,
////                b.isPublic,
////                b.BlogTypeId,
////                BlogTypeName = b.BlogType.Name,
////                PostCount = b.Posts.Count()
////            })
////            .ToList();

////        Console.WriteLine("\nStep 2: Blogs with BlogType and Post Count");
////        foreach (var b in blogsWithPostCount)
////        {
////            Console.WriteLine($"BlogId: {b.BlogId}, Url: {b.Url}, BlogType: {b.BlogTypeName}, Posts: {b.PostCount}");
////        }

////        // Step 3: Return all Post properties + Blog Url + BlogType Name
////        var postsWithBlog = context.Posts
////            .Select(p => new
////            {
////                p.PostId,
////                p.Title,
////                p.Content,
////                p.BlogId,
////                BlogUrl = p.Blog.Url,
////                BlogTypeName = p.Blog.BlogType.Name
////            })
////            .ToList();

////        Console.WriteLine("\nStep 3: Posts with Blog Info");
////        foreach (var p in postsWithBlog)
////        {
////            Console.WriteLine($"PostId: {p.PostId}, Title: {p.Title}, BlogUrl: {p.BlogUrl}, BlogType: {p.BlogTypeName}");
////        }
////    }
////}

////using BlogPostSimpleApp.Models;
////using Microsoft.EntityFrameworkCore;
////using System;
////using System.Collections.Generic;
////using System.Linq;

////class Program
////{
////    static void Main()
////    {
////        TestDatabase();
////    }


////    static void ListAllUsers()
////    {
////        using var context = new AppDbContext();
////        var users = context.users.ToList();

////        Console.WriteLine("Current Users ");
////        if (!users.Any())
////        {
////            Console.WriteLine("No users found.");
////            return;
////        }

////        foreach (var user in users)
////        {
////            Console.WriteLine($"ID: {user.UserId}, Name: {user.Name}, Email: {user.Email}, Phone: {user.PhoneNumber}");
////        }
////    }


////    static void AddUser(string name, string email, string phone)
////    {
////        using var context = new AppDbContext();
////        var user = new User
////        {
////            Name = name,
////            Email = email,
////            PhoneNumber = phone
////        };

////        context.users.Add(user);
////        context.SaveChanges();
////        Console.WriteLine($"Added user: {name}");
////    }


////    static void UpdateUser(int userId, string newName)
////    {
////        using var context = new AppDbContext();
////        var user = context.users.FirstOrDefault(u => u.UserId == userId);

////        if (user == null)
////        {
////            Console.WriteLine($" No user found with ID {userId}");
////            return;
////        }

////        user.Name = newName;
////        context.SaveChanges();
////        Console.WriteLine($" Updated user ID {userId} to new name: {newName}");
////    }


////    static void DeleteUser(int userId)
////    {
////        using var context = new AppDbContext();
////        var user = context.users.FirstOrDefault(u => u.UserId == userId);

////        if (user == null)
////        {
////            Console.WriteLine($" No user found with ID {userId} to delete.");
////            return;
////        }

////        context.users.Remove(user);
////        context.SaveChanges();
////        Console.WriteLine($" Deleted user ID {userId}");
////    }


////    static void TestDatabase()
////    {
////        Console.WriteLine(" CRUD Tests...");


////        using (var context = new AppDbContext())
////        {
////            context.users.RemoveRange(context.users);
////            context.SaveChanges();
////            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");


////            var users = new List<User>
////            {
////                new User { Name = "Arsh", Email = "arsh@example.com", PhoneNumber = "1234567890" },
////                new User { Name = "kiran", Email = "kiran@example.com", PhoneNumber = "2345678901" }
////            };
////            context.users.AddRange(users);
////            context.SaveChanges();
////        }

////        ListAllUsers();


////        AddUser("Parmar", "parmar@example.com", "3456789012");
////        ListAllUsers();


////        UpdateUser(2, "Harman");
////        ListAllUsers();


////        DeleteUser(1);
////        ListAllUsers();
////    }
////}
////if (!context.BlogTypes.Any())
////{
////    var type1 = new BlogType { Name = "Tech", Description = "Tech Blog", Status = 1 };
////    var type2 = new BlogType { Name = "food", Description = "food Blog", Status = 2 };

////    context.BlogTypes.AddRange(type1, type2);
////    context.SaveChanges();
////}
////if (!context.PostTypes.Any())
////{
////    var type1 = new PostType { Name = "Animal", Description = "Animal Blog", Status = 1 };
////    var type2 = new PostType { Name = "Cars", Description = "Cars Blog", Status = 2 };

////    context.PostTypes.AddRange(type1, type2);
////    context.SaveChanges();
////}
////if (!context.Blogs.Any())
////{
////    var type1 = new Blog { Url = "https://myblog.com", isPublic = true, BlogTypeId = 1 };
////    var type2 = new Blog { Url = "https://myfoodblog.com", isPublic = true, BlogTypeId = 2 };

////    context.Blogs.AddRange(type1, type2);
////    context.SaveChanges();
////}
////if (!context.Posts.Any())
////{
////    var type1 = new Post { Title = "Maths", Content = "Math Posts", BlogId = 1, PostTypeId = 1, UserId = 1 };
////    var type2 = new Post { Title = "Games", Content = "Game Posts", BlogId = 2, PostTypeId = 2, UserId = 2 };

////    context.Posts.AddRange(type1, type2);
////    context.SaveChanges();
////}
////if (!context.Statuses.Any())
////{
////    var type1 = new Status { StatusCode = 1, Name = "Food", Description = "Food Blog" };
////    var type2 = new Status { StatusCode = 2, Name = "Car", Description = "Car Blog" };
////    var type3 = new Status { StatusCode = 3, Name = "Animals", Description = "Animals Blog" };
////    var type4 = new Status { StatusCode = 4, Name = "Family", Description = "Family Blog" };
////    var type5 = new Status { StatusCode = 5, Name = "Computer", Description = "Computer Blog" };
////    var type6 = new Status { StatusCode = 6, Name = "Movies", Description = "Movies Blog" };
////    var type7 = new Status { StatusCode = 7, Name = "Phones", Description = "Phones Blog" };
////    var type8 = new Status { StatusCode = 8, Name = "Friend", Description = "Friend Blog" };
////    var type9 = new Status { StatusCode = 9, Name = "School", Description = "School Blog" };
////    var type10 = new Status { StatusCode = 10, Name = "College", Description = "College Blog" };

////    context.Statuses.AddRange(type1, type2, type3, type4, type5, type6, type7, type8, type9, type10);
////    context.SaveChanges();
////}
////if (!context.BlogTypes.Any())
////{
////    var type1 = new BlogType { Status = 1, Name = "Corporate", Description = "Official company blogs" };
////    var type2 = new BlogType { Status = 2, Name = "Personal", Description = "Personal life experiences and thoughts" };
////    var type3 = new BlogType { Status = 3, Name = "Private", Description = "Restricted or confidential blogs" };
////    var type4 = new BlogType { Status = 4, Name = "Tech", Description = "Blogs about technology and development" };
////    var type5 = new BlogType { Status = 5, Name = "Travel", Description = "Travel diaries and guides" };
////    var type6 = new BlogType { Status = 6, Name = "Food", Description = "Recipes, reviews, and culinary experiences" };
////    var type7 = new BlogType { Status = 7, Name = "Education", Description = "Educational content and tutorials" };
////    var type8 = new BlogType { Status = 8, Name = "Health", Description = "Health tips and wellness guides" };
////    var type9 = new BlogType { Status = 9, Name = "Finance", Description = "Money management, investing, and budgeting" };
////    var type10 = new BlogType { Status = 10, Name = "News", Description = "Current events and updates" };


////    context.BlogTypes.AddRange(type1, type2, type3, type4, type5, type6, type7, type8, type9, type10);
////    context.SaveChanges();
////}
////var blogTypes = new List<BlogType>

////{
////    new BlogType {Name = "Corporate", Status = 1, Description = "Corporate blog"},
////    new BlogType {Name = "Personal", Status= 2, Description = "Personal blog"},
////    new BlogType {Name = "Private", Status= 3, Description = "Private blog"},
////};

////var blogs = new List<Blog>
////{
////    new Blog {Url = "www.corporateblog.com", BlogType = blogTypes[0]},
////    new Blog {Url = "www.personalblog.com", BlogType = blogTypes[1]},
////    new Blog {Url = "www.privateblog.com", BlogType=blogTypes[2]},
////};

////context.BlogTypes.AddRange(blogTypes);
////context.Blogs.AddRange(blogs);

////context.SaveChanges();

////var users = new List<User>
////{
////    new User { Name = "Neha", Email = "Neha@gmail.com", PhoneNumber = "9845329852" },
////    new User { Name = "Dhiman", Email = "Dhiman@gmail.com", PhoneNumber = "9065489654" },
////    new User { Name = "Sumit", Email = "arsh@gmail.com", PhoneNumber = "9876086543" }
////};

////context.users.AddRange(users);
////context.SaveChanges();



////Console.Write("Enter blog URL: ");
////var url = Console.ReadLine();
////var blog = new Blog { Url = url };
////context.Blogs.Add(blog);
////context.SaveChanges();


////var user = context.users.First();
////var post = new Post
////{
////    Title = "Hello EF Core",
////    Content = "This is my first post!",
////    BlogId = blog.BlogId,
////    UserId = user.UserId,
////    PostTypeId = 1
////};

////context.Posts.Add(post);
////context.SaveChanges();


////var Blogs = context.Blogs
////                   .Include(b => b.Posts)
////                   .ThenInclude(p => p.User)
////                   .ToList();

////foreach (var b in blogs)
////{
////    Console.WriteLine($"Blog: {b.Url}");
////    foreach (var p in b.Posts)
////    {
////        Console.WriteLine($"  Post: {p.Title} - {p.Content} (by {p.User?.Name ?? "Unknown"})");
////    }
////}


////}}


//using BlogPostSimpleApp.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//class Program
//{
//    static void Main()
//    {


//        using var context = new AppDbContext();


//        // Clear existing data
//        context.Posts.RemoveRange(context.Posts);
//        context.Blogs.RemoveRange(context.Blogs);
//        context.BlogTypes.RemoveRange(context.BlogTypes);
//        context.PostTypes.RemoveRange(context.PostTypes);
//        context.users.RemoveRange(context.users);
//        context.SaveChanges();

//        // Reset identity counters
//        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('BlogType', RESEED, 0)"); // because table name is BlogType
//        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Blogs', RESEED, 0)");
//        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Posts', RESEED, 0)");
//        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PostTypes', RESEED, 0)"); // because table name is PostType
//        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");

//    }
//}