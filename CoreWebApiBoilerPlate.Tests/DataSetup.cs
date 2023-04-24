using AutoMapper;
using Castle.Core.Configuration;
using CoreWebApiBoilerPlate.Controllers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWebApiBoilerPlate.Tests
{

    public class TestContext : DefaultDBContext
    {
        public TestContext(DbContextOptions<DefaultDBContext> options) : base(options)
        {
        }
    }


 
    public class TestSetup
    {
        public TestContext Context { get; private set; }

        public TestSetup()
        {
            var options = new DbContextOptionsBuilder<DefaultDBContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            Context = new TestContext(options);

            var users = new List<User>
            {
                new User
                {
                    Name = "John Doe",
                    Role = new Role
                    {
                        Id = 1,
                        Description = "Admin"
                    },
                    EmailId = "johnDoe@gmail.com",
                    Password = EasyEncryption.MD5.ComputeMD5Hash("password123"),
                    Username = "johnDoe"
                },
                new User
                {
                    Name = "Jane Doe",
                    Role = new Role
                    {
                        Id = 2,
                        Description = "User"
                    },
                    EmailId = "janeDoe@gmail.com",
                    Password = EasyEncryption.MD5.ComputeMD5Hash("password456"),
                    Username = "janeDoe"
                },
                new User
                {
                    Name = "Bob Smith",
                    Role = new Role
                    {
                        Id = 3,
                        Description = "User"
                    },
                    EmailId = "bobSmith@gmail.com",
                    Password = EasyEncryption.MD5.ComputeMD5Hash("password789"),
                    Username = "bobSmith"
                }
            };
            Context.Users.AddRange(users);
            Context.SaveChanges();
        }


     
        ~TestSetup()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }

}
