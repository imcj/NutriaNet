
namespace NutrialNet.Expand.Domain.Models.Columns;

internal class LinkedExpandColumn : ExpandColumn
{
    public ExpandTable ExpandTable { get; set; }

    public LinkedExpandColumn Bidirectionality { get; set; }
    
    public bool IsAllowLinkingToMultipleRecords { get; set; }
}
