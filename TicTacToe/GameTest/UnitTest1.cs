using System;
using System.Net.Http;
using TicTacToe.Controllers;
using Xunit;

namespace GameTest
{

    public class UnitTest1
    {
        static HttpClient client = new HttpClient();
        [Fact]
        public async void Test1()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:44301/game/new");
            HttpResponseMessage response = await client.SendAsync(request);

            Assert.Equal("200", response.StatusCode.ToString());
        }
    }
}
