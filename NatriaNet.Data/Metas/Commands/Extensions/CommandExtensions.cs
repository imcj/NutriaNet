using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.Data.Metas.Commands.Extensions;

public static class CommandExtensions
{
    public static string ToCommandText(this ICommand command, DatabaseProduct database)
    {
        ICommandText commandText;
        commandText = database switch
        {
            DatabaseProduct.MySql57 => new MySqlCommandText(),
            DatabaseProduct.Sqlite => new SqliteCommandText(),
            _ => throw new NotImplementedException()
        };

        return command switch
        {
            CreateTableCommand cmd => commandText.CreateTable(cmd),
            _ => throw new NotImplementedException()
        };
        
    }
}
