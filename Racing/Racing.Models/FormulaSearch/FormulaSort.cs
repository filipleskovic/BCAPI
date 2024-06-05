using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Models.FormulaSearch
{
    public class FormulaSort
    {
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; }
        public FormulaSort(string orderBy, string orderDirection) 
        {
            OrderBy=orderBy;
            OrderDirection=orderDirection;
        }
    }
}
