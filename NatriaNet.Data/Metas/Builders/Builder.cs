namespace NutriaNet.Data.Metas.Builders;

public class Builder
{


    public static Builder CreateDatabase(string name)
    {
        var builder = new Builder();
        return builder;
    }

    public static Builder CreateTable(string tableName)
    {
        var builder = new Builder();
        return builder;
    }
}
