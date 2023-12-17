namespace NutriaNet.Data.Metas.Parser.MySql;

public struct Token
{
    public TokenKind Kind { get; }

    public char[] Value { get; }

    public int Start { get; }

    public int End { get; }


    public Token(TokenKind kind, char[] value, int start, int end)
    {
        Kind = kind;
        Value = value;
        Start = start;
        End = end;
    }
}
