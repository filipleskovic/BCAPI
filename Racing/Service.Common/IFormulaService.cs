using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Racing.Models;
using Racing.Models.FormulaSearch;
using Racing.Service;
namespace Service.Common
{
    public interface IFormulaService: IService<Formula>
    {
        Task<IList<Formula>> GetAllAsync(FormulaFilter filter, FormulaSort sort);
    }
}
