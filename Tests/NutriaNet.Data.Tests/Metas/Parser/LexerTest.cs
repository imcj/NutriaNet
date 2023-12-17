using NutriaNet.Data.Metas.Parser.MySql;

namespace NutriaNet.Data.Tests.Metas.Parser;

public class LexerTest
{
    [Fact]
    public async Task ReadToken_ShouldBeWorkd()
    {
        var stream = "Hello World".ToStream();
        
        var lexer = Lexer.Parser(stream);
         
        var cursor = lexer.GetAsyncEnumerator();

        await cursor.MoveNextAsync();
        var token = cursor.Current;
        Assert.Equal("Hello".ToCharArray(), token.Value);

        await cursor.MoveNextAsync();
        var space = cursor.Current;
        Assert.Equal(" ".ToCharArray(), space.Value);

        await cursor.MoveNextAsync();
        token = cursor.Current;
        Assert.Equal("World".ToCharArray(), token.Value);
        
    }

    [Fact]
    public async Task ReadToken_ShouldBePunctuation()
    {
        var stream = "INT(,)".ToStream();
        var tokens = await Lexer.Parser(stream).ToListAsync();
        var c = tokens.Select(t => t.Value);


        Assert.Equal(
            new string[] { "INT", "(", ",", ")" }.Select(strings => strings.ToCharArray()),
            c
        );

        Assert.Equal(new char[][] { new char[] { ' ' } }, new char[][] { new char[] { ' ' } });
    }

    [Fact]
    public async Task ReadToken_ShouldBeNumber()
    {
        var stream = "10".ToStream();
        var tokens = await Lexer.Parser(stream).ToListAsync();
        var c = tokens.Select(t => t.Value);


        Assert.Equal(
            new string[] { "10" }.Select(strings => strings.ToCharArray()),
            c
        );
  
    }

    [Fact]
    public async Task ReadToken_ShouldBeIntType()
    {
        var stream = "INT(8)".ToStream();
        var tokens = await Lexer.Parser(stream).ToListAsync();
        var c = tokens.Select(t => t.Value);


        Assert.Equal(
            new string[] { "INT", "(", "8", ")" }.Select(strings => strings.ToCharArray()),
            c
        );

    }


    [Theory]
    [InlineData(new string[] { "DECIMAL", }, "DECIMAL")]
    [InlineData(new string[] { "DECIMAL", "(", "1", ")" }, "DECIMAL(1)")]
    [InlineData(new string[] { "DECIMAL", "(", ")" }, "DECIMAL()")]
    public async Task ReadToken_ShouldBeDecimalType(string[] excepted, string strings)
    {
        var stream = strings.ToStream();
        var tokens = await Lexer.Parser(stream).ToListAsync();
        var c = tokens.Select(t => t.Value);


        Assert.Equal(
            excepted.Select(strings => strings.ToCharArray()),
            c
        );

    }
}
