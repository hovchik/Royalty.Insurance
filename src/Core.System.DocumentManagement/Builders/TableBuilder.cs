using Core.System.DocumentManagement.Builders;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Core.System.DocumentManagement.Extensions
{
    public class TableBuilder :  ICellBuilder, ITableBuilder
    {

        private readonly Table _table;
        private TableRow _currenTableRow;
        public TableBuilder()
        {
            _table = new Table();
        }

        /// <summary>
        /// Add basic Thin lines border property
        /// </summary>
        /// <param name="borderSize">border Size</param>
        /// <returns></returns>
        public ITableBuilder BasicBorder(UInt32Value borderSize)
        {

            // Create a TableProperties object and specify its border information.
            TableProperties tableProperties = new TableProperties(
                new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = borderSize },
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = borderSize },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = borderSize },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = borderSize },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = borderSize },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines), Size = borderSize }
                )
            );

            _table.AppendChild(tableProperties);

            return this;
        }

        /// <summary>
        /// Create Row and append to table
        /// </summary>
        /// <returns></returns>
        public ICellBuilder NewRow()
        {
            _currenTableRow = new TableRow();
            _table.AppendChild(_currenTableRow);

            return this;
        }

        /// <summary>
        /// Create Row and append to table
        /// </summary>
        /// <returns></returns>
        public ICellBuilder AppendCellToRow(Text cellValue, StringValue width)
        {
            TableCell cell = new TableCell();

            // Specify the width property of the table cell.
            cell.AppendChild(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = width }));

            // Specify the table cell content.
            cell.AppendChild(new Paragraph(new Run(cellValue)));

            // Append the table cell to the table row.
            _currenTableRow.AppendChild(cell);

            return this;
        }


        /// <summary>
        /// Build and return row
        /// </summary>
        /// <returns></returns>
        public Table BuildTable()
        {
            return _table;
        }
    }
}
