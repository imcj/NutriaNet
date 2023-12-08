using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NutrialNet.Expand.Tests;

public class ExpandDatabaseControllerTest : IClassFixture<WebApplicationFactory<TestProgram>>
{

    private readonly WebApplicationFactory<TestProgram> _factory;

    public ExpandDatabaseControllerTest(WebApplicationFactory<TestProgram> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TestGetTable()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/Expand");
    }

    [InlineData("/api/Expand/Default")]
    [Theory]
    public async Task ShouldCreateExpandTable(string url)
    {
        var statement = @"
[
    {
        ""discriminator"": ""CreateTableStatement"",
        ""key"": ""
    }
]

";
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync(
            "/api/Expand/Default",
            new
            {

            }
        );
    }
}
