
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NutrialNet.Expand.Interfaces.Web;

var appBuilder = WebApplication.CreateBuilder(args);

appBuilder.Services.AddControllers()
    .AddApplicationPart(typeof(ExpandController).Assembly);

var app = appBuilder.Build();

//app.UseRouting();
//app.UseEndpoints(endpoints => endpoints.MapControllers());
//app.MapControllers();

app.Run();

public partial class TestProgram
{
}
