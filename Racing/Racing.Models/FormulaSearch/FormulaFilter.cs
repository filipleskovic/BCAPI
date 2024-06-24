using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Models.FormulaSearch
{
    public class FormulaFilter
    {
        public int? MaxTopSpeed { get; set; }
        public int? MinTopSpeed { get; set; }
        public string? Name { get; set; }
        public FormulaFilter(string? name, int? minTopSpeed, int? maxTopSpeed) 
        {
            Name = name;
            MinTopSpeed = minTopSpeed;
            MaxTopSpeed = maxTopSpeed;
        }
    }
}
