using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Core.System.DocumentManagement.Builders
{
    public interface ICellBuilder : IRowBuilder
    {
        ICellBuilder AppendCellToRow(Text cellValue, StringValue width);
        Table BuildTable();
    }
}
