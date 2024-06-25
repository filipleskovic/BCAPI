using Racing.Models;
using Racing.Repository;
using Service.Common;
namespace Racing.Service

{
    public class DriverService : IDriverService
    {
        private IRepository<Driver> _repository;
        public DriverService(IRepository<Driver> repository)
        {
            _repository = repository;
        }
        public async Task<Driver> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }
        public async Task<Driver> PostAsync(Driver driver)
        {
            return await _repository.PostAsync(driver);
        }
        public async Task<int> PutAsync(Driver driver, Guid id)
        {
            return await _repository.PutAsync(driver, id);
        }
        public async Task<int> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<IList<Driver>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
