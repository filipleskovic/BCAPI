using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Models.FormulaSearch
{
    public class FormulaGet
    {
        public FormulaFilter filter { get; set; }
        public FormulaSort sort { get; set; }

        public FormulaGet(FormulaFilter? filter, FormulaSort? sort)
        {
            this.filter = filter;
            this.sort = sort;
        }
    }
}
