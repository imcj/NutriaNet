namespace NutriaNet.Data.Tests.Metas.Parser;

public class CharTest
{
    [Fact]
    public void ShouldCompareWithCharAndNumberAndByte()
    {
        char c = 'a';
        int i = (int)c;
        byte b = (byte)i;

        Assert.Equal(97, c);
        Assert.Equal(97, i);
        Assert.Equal(97, b);

        Assert.True(98 > c);
        Assert.True(98 > i);
        Assert.True(98 > b);

        Assert.True('0' <= 'a');
    }

    [Fact]
    public async Task ReadChar_ShouldBeZero_WhenOutOfRange()
    {
        var stream = new MemoryStream();
        stream.WriteByte((byte)'a');
        stream.WriteByte((byte)'1');
        stream.Seek(0, SeekOrigin.Begin);

        byte[] aByte = new byte[1];

        var _ = await stream.ReadAsync(aByte, 0, 1);
        Assert.Equal('a', (char)aByte[0]);

        await stream.ReadAsync(aByte);
        Assert.Equal('1', (char)aByte[0]);

        var readed = await stream.ReadAsync(aByte, 0, 1);

        Assert.Equal(0, readed);
    }

    //[Fact]
    //public async Task ReadAsync_ShouldBeMemory()
    //{
    //    var stream = new MemoryStream();
    //    stream.WriteByte((byte)'a');
    //    stream.WriteByte((byte)'1');
    //    stream.Seek(0, SeekOrigin.Begin);

    //    var mem = new Memory<byte>();

    //    var length = await stream.ReadAsync(mem);

    //    Assert.Equal(1, length);
    //    Assert.Equal('a', (char)mem.Span[0]);
    //}
}
