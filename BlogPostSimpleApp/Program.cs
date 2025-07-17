using BlogPostSimpleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

class Program
{
    static void Main()
    {
        TestDatabase();
    }


    static void ListAllUsers()
    {
        using var context = new AppDbContext();
        var users = context.users.ToList();

        Console.WriteLine("Current Users ");
        if (!users.Any())
        {
            Console.WriteLine("No users found.");
            return;
        }

        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.UserId}, Name: {user.Name}, Email: {user.Email}, Phone: {user.PhoneNumber}");
        }
    }


    static void AddUser(string name, string email, string phone)
    {
        using var context = new AppDbContext();
        var user = new User
        {
            Name = name,
            Email = email,
            PhoneNumber = phone
        };

        context.users.Add(user);
        context.SaveChanges();
        Console.WriteLine($"Added user: {name}");
    }


    static void UpdateUser(int userId, string newName)
    {
        using var context = new AppDbContext();
        var user = context.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            Console.WriteLine($" No user found with ID {userId}");
            return;
        }

        user.Name = newName;
        context.SaveChanges();
        Console.WriteLine($" Updated user ID {userId} to new name: {newName}");
    }


    static void DeleteUser(int userId)
    {
        using var context = new AppDbContext();
        var user = context.users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            Console.WriteLine($" No user found with ID {userId} to delete.");
            return;
        }

        context.users.Remove(user);
        context.SaveChanges();
        Console.WriteLine($" Deleted user ID {userId}");
    }


    static void TestDatabase()
    {
        Console.WriteLine(" CRUD Tests...");


        using (var context = new AppDbContext())
        {
            context.users.RemoveRange(context.users);
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");


            var users = new List<User>
            {
                new User { Name = "Neha", Email = "Neha@example.com", PhoneNumber = "1234567890" },
                new User { Name = "Dhiman", Email = "Dhiman@example.com", PhoneNumber = "2345678901" }
            };
            context.users.AddRange(users);
            context.SaveChanges();
        }

        ListAllUsers();


        AddUser("Sumit", "Sumit@example.com", "3456789012");
        ListAllUsers();


        UpdateUser(2, "Kartik");
        ListAllUsers();


        DeleteUser(1);
        ListAllUsers();
    }
}