using System.Text;

namespace NutriaNet.Data.Tests.Metas.Parser;

public static class Extensions
{
    public static Stream ToStream(this string strings)
    {
        var stream = new MemoryStream();
        var bytes = Encoding.UTF8.GetBytes(strings);
        stream.Write(bytes);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> enumerable)
    {
        var list = new List<T>();

        await foreach (var item in enumerable)
        {
            list.Add(item);
        }

        return list;
    }
}
