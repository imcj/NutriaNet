using NutrialNet.Expand.Domain.Models.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutrialNet.Expand.Domain.Models;

public class AlterTableStatement : IDefinitionStatement
{
    public string Key { get; set; }

    public string? Name { get; set; }

    public IEnumerable<ExpandColumn> Add { get; set; }

    public IEnumerable<ExpandColumn> Drop { get; set; }
}
