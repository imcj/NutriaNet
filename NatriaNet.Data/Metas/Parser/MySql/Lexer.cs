using Microsoft.Win32.SafeHandles;

namespace NutriaNet.Data.Metas.Parser.MySql;

public class Lexer
{
    protected readonly Stream body;

    protected int position = 0;

    protected byte[] wordBuffers = new byte[512];

    protected byte[] code = new byte[1];

    protected byte previousCode = new();

    public Lexer(Stream stream)
    {
        this.body = stream;
    }

    public async Task<Token> ReadToken()
    {

        var code = await ReadByte();

        if (code == null)
            return EOF();

        var token = await ReadPunctuation(code);
        if (token.Kind != TokenKind.UNKNOWN)
        {
            position++;
            return token;
        }

        if (IsAlphabet(code))
        {
            body.Seek(position, SeekOrigin.Begin);

            return await ReadWord();   
        }

        if (IsNumber(code))
        {
            return await ReadNumber(code);
        }

        position++;


        return new Token(TokenKind.UNKNOWN, new char[] { (char)code }, position - 1, position);
    }

    protected Token EOF()
    {
        return new Token(TokenKind.EOF, new char[] { }, position, position + 1);
    }

    protected Token Unknown()
    {
        return new Token(TokenKind.UNKNOWN, new char[] { }, position, position + 1);
    }

    protected Task<byte?> ReadByteAgain()
    {
        body.Seek((int)position, SeekOrigin.Begin);
        return ReadByte();
    }

    protected async Task<byte?> ReadByte()
    {
        previousCode = code[0];
        var readed = await body.ReadAsync(code.AsMemory(0, 1));

        if (readed == 0)
            return null;

        return code[0];
    }

    public async Task<Token> ReadWord()
    {
        byte? code;

        var start = position;
        int i = 0;
        do
        {
            code = await ReadByte();

            i = position - start;
            position++;

            if (null == code)
                break;

            wordBuffers[i] = (byte)code;
            

        } while (IsAlphabet(code));

        position--;

        body.Seek(position, SeekOrigin.Begin);


        var cloned = new char[i];

        for (var j = 0; j < i; j++)
            cloned[j] = (char)wordBuffers[j];

        return new Token(TokenKind.Word, cloned, start, position);
    }

    public async Task<Token> ReadNumber(byte? aByte)
    {
        wordBuffers[0] = (byte)aByte;
        var start = position;
        position++;

        byte? code;

        
        int i;
        do
        {
            code = await ReadByte();

            i = position - start;
            position++;

            if (null == code)
                break;

            wordBuffers[i] = (byte)code;


        } while (IsNumber(code));

        position--;

        body.Seek(position, SeekOrigin.Begin);


        var cloned = new char[i];

        for (var j = 0; j < i; j++)
            cloned[j] = (char)wordBuffers[j];

        return new Token(TokenKind.Word, cloned, start, position);
    }

    public async Task<Token> ReadPunctuation(byte? code)
    {
        if (null == code)
            return EOF();

        var value = new char[] { (char)code };

        var end = position + 1;

        return (char)code switch
        {
            '(' => new Token(TokenKind.PAREN_LEFT, value, position, end),
            ')' => new Token(TokenKind.PAREN_RIGHT, value, position, end),
            ',' => new Token(TokenKind.COMMA, value, position, end),
            _ => Unknown()
        };
    }

    static bool IsAlphabet(byte? code)
    {
        return code != null && (code >= 'a' && code <= 'z' || code >= 'A' && code <= 'Z');
    }

    static bool IsNumber(byte? code) => code != null && (code >= '0' && code <= '9');

    public static async IAsyncEnumerable<Token> Parser(Stream stream)
    {
        var lexer = new Lexer(stream);

        Token token;

        while (true)
        {
            token = await lexer.ReadToken();

            if (TokenKind.EOF == token.Kind)
                break;

            yield return token;
        }
    }
}
