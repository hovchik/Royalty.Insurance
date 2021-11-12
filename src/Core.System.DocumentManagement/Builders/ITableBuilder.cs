using DocumentFormat.OpenXml;

namespace Core.System.DocumentManagement.Builders
{
    public interface ITableBuilder
    {
        ITableBuilder BasicBorder(UInt32Value borderSize);

        ICellBuilder NewRow();
    }
}
