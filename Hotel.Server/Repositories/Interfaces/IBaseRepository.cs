using System.Threading.Tasks;

namespace Hotel.Server.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task AddAsync<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        Task<bool> Complete();
    }
}
