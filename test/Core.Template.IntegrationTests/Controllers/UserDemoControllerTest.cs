using Core.Template.Application.Commands;
using Core.Template.Controllers.V1;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Core.Template.IntegrationTests.Controllers
{
    public class UserDemoControllerTest : IntegrationTestFixture
    {
        private const string API_BASE_URI = "/api/v1/Demo/";
        public UserDemoControllerTest(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }
        [Theory(DisplayName = "添加用户", Timeout = 30000)]
        [InlineData("用户1", 20, true)]
        [InlineData("用户2", 21, true)]
        [InlineData("用户3", 22, true)]
        [InlineData("用户4", 23, true)]
        [InlineData("用户5", 24, true)]
        [InlineData("用户6", 25, true)]
        [InlineData("用户7", 26, true)]
        [InlineData("用户8", 27, true)]
        [InlineData("用户9", 28, true)]
        [InlineData("用户10", 80, true)]
        public async Task Add_ShouldBe_OK(string name, int age, bool shouldBe)
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, API_BASE_URI + nameof(DemoController.Add));
            request.Headers.Clear();
            request.Headers.Add("CityID", "hrb");
            request.Content = JsonContent(new AddUserDemoCommand { Name = name, Age = age });
            // Act
            var content = await SendAsync<bool>(request);
            // Assert
            Assert.Equal(shouldBe, content);
        }
    }
}
