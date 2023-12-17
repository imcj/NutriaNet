namespace NutriaNet.Data.Metas.Parser.MySql;

public enum TokenKind
{
    EOF,

    PAREN_LEFT,

    PAREN_RIGHT,

    Word,

    COMMA,

    INT,

    UNKNOWN
}
