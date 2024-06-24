using Racing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Racing.Models;
using Racing.Models.FormulaSearch;
namespace Repository.Common
{
    public interface IFormulaRepository:IRepository<Formula>
    {
        Task<IList<Formula>> GetAllAsync(FormulaFilter filter, FormulaSort sort);

    }
}
