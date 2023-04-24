using NUnit.Framework;

namespace CoreWebApiBoilerPlate.Tests
{

    public class TestContext : DefaultDBContext
    {
        public TestContext(DbContextOptions<DefaultDBContext> options) : base(options)
        {
        }
    }



    [SetUpFixture]
    public class TestSetup
    {
        public static TestContext Context { get; private set; }


        [OneTimeSetUp]
        public void TestSetupInit()
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



        [OneTimeTearDown]
        public void ClearDB()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }

}
