using AutoMapper;
using Microsoft.Extensions.Configuration;


namespace CoreWebApiBoilerPlate.Tests
{


    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IConfiguration> mockConfig;
        private Mock<IMapper> mockMapper;
        private AuthController authController;
        

        [OneTimeSetUp]
        public void Setup()
        {
            var testSetup = new TestSetup();
            mockConfig = new Mock<IConfiguration>();
            mockMapper = new Mock<IMapper>();
            authController = new AuthController(
                new RepositoryWrapper(testSetup.Context),
                mockConfig.Object,
                mockMapper.Object);
        }

        [Test]
        public async Task Login_With_Valid_Credentials_Returns_Token_And_UserData()
        {
            // Arrange
            var loginModel = new LoginRequestModel { UserName = "johnDoe", Password = "password123" };

            mockConfig.Setup(config => config["JWT:Key"]).Returns("VOR4mh4t3GmrZL0lJSfb");

            // Act
            var result = await authController.Login(loginModel) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task Login_With_Invalid_Credentials_Returns_Unauthorized()
        {
            // Arrange
            var loginModel = new LoginRequestModel { UserName = "janeDoe", Password = "wrongPassword" };
            //mockRepo.Setup(repo => repo.UserRepository.GetQueryable().SingleOrDefaultAsync(default)).ReturnsAsync(default(User));

            // Act
            var result = await authController.Login(loginModel) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status401Unauthorized));
            Assert.That(result.Value, Is.EqualTo("Invalid Username or Password!"));
        }
    }

}