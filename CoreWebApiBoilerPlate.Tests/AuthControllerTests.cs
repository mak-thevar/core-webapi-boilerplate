using AutoMapper;
using CoreWebApiBoilerPlate.Application.DTO.Request;
using CoreWebApiBoilerPlate.Application.Services.Interfaces;
using CoreWebApiBoilerPlate.WebApi.Controllers;
using Microsoft.Extensions.Configuration;


namespace CoreWebApiBoilerPlate.Tests
{


    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IConfiguration> mockConfig;
        private Mock<IMapper> mockMapper;
        private Mock<IAuthService> mockAuthService;
        private AuthController authController;
        

        [OneTimeSetUp]
        public void Setup()
        {
            //var testSetup = new TestSetup();
            mockConfig = new Mock<IConfiguration>();
            mockMapper = new Mock<IMapper>();
            mockAuthService = new Mock<IAuthService>();
            authController = new AuthController(mockAuthService.Object);
        }

        [Test]
        public async Task Login_With_Valid_Credentials_Returns_Token_And_UserData()
        {
            // Arrange
            var loginModel = new LoginRequestDTO { UserName = "johnDoe", Password = "password123" };

            mockConfig.Setup(config => config["JWT:Key"]).Returns(TestSetup.JWT_KEY);

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
            var loginModel = new LoginRequestDTO { UserName = "janeDoe", Password = "wrongPassword" };
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