using System.Diagnostics;

namespace GHC_Practice2017_Pizza.Entities
{
    [DebuggerDisplay("row {Row} col {Column}")]
    public class Point
    {
        public int Row;
        public int Column;

        public Point(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Point()
        { }
    }
}