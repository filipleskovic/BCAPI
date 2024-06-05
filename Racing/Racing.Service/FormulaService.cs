
using Racing.Models;
using Racing.Models.FormulaSearch;
using Racing.Repository;
namespace Racing.Service
{
    public class FormulaService 
    {
        private readonly FormulaRepository _repository;

        public FormulaService(string connectionString)
        {
            _repository = new FormulaRepository(connectionString);
        }

        public async Task<Formula> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<int> PostAsync(Formula formula)
        {
            return await _repository.PostAsync(formula);
        }
        public async Task<int> PutAsync(Formula formula, Guid id)
        {
            return await _repository.PutAsync(formula, id);
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<IList<Formula>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<IList<Formula>> GetAllAsync(FormulaGet formulaGet)
        {
            return await _repository.GetAllAsync(formulaGet);
        }
    }
}
