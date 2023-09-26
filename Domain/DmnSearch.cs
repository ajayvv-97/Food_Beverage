using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class DmnSearch
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ColumnHeaders { get; set; }
        public string Select { get; set; }
        public string Table { get; set; }
        public string WhereNdGroupBy { get; set; }
        public string OrderBy { get; set; }

    }
}
