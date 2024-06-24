
namespace Racing.Service
{
    public interface IService<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<int> PostAsync(T t);
        Task<int> PutAsync(T t, Guid id);
        Task<int> DeleteAsync(Guid id);
    }
}
