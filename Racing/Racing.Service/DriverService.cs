using Racing.Models;
using Racing.Repository;
namespace Racing.Service

{
    public class DriverService : IService<Driver>
    {
        DriverRepository _repository;
        public DriverService(string connectionString)
        {
            _repository = new DriverRepository(connectionString);
        }
        public async Task<Driver> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }
        public async Task<int> PostAsync(Driver driver)
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
