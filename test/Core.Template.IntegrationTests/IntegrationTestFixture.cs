using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
namespace Core.Template.IntegrationTests
{
    public class IntegrationTestFixture : IDisposable
    {
        private readonly ITestOutputHelper _outputHelper;
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        public IntegrationTestFixture(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper ?? throw new ArgumentNullException(nameof(outputHelper));
            var applicationPath = Path.GetFullPath(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "../../../../../src/Core.Template"));
            var webHost = WebHost.CreateDefaultBuilder().UseStartup<IntegrationTestStartup>().UseContentRoot(applicationPath);
            Server = new TestServer(webHost);
            Client = Server.CreateClient();
        }
        public async Task<TResult> SendAsync<TResult>(HttpRequestMessage requestMessage)
        {
            var response = await Client.SendAsync(requestMessage);
            var readJsonString = await response.Content.ReadAsStringAsync();
            WriteLine(readJsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var resultObject = JsonConvert.DeserializeObject<TResult>(readJsonString);
            return resultObject;
        }
        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
        public void WriteLine(string message) => _outputHelper.WriteLine(message);
        public StringContent JsonContent<T>(T value) => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
    }
}
