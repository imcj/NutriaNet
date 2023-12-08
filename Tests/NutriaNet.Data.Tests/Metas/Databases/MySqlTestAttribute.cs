using System.Reflection;
using Xunit.Sdk;

namespace NutriaNet.Data.Tests.Metas.Databases;

internal class MySqlTestAttribute : BeforeAfterTestAttribute
{
    protected MySQLFixture Fixture = new();

    public override void Before(MethodInfo methodUnderTest)
    {
        using var connection = Fixture.GetDbConnection(null);
        connection.Open();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = @"DROP DATABASE IF EXISTS `test`;
CREATE DATABASE test;";

        var a = cmd.ExecuteNonQuery();
        connection.Close();
    }
}
