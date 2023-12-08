using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriaNet.DataImporter.Domain.Services;

public class ValueCopyService
{

    readonly static public Type TypeInt16 = typeof(ushort);

    readonly static public Type TypeInt32 = typeof(uint);

    readonly static public Type TypeInt64 = typeof(ulong);

    readonly static public Type TypeInt8 = typeof(byte);

    readonly static public Type TypeDecimal = typeof(decimal);

    

    public object Copy(object value, Type target)
    {
        var type = value.GetType();

        if (value is string)
        {
        }

        return null;
    }
}
