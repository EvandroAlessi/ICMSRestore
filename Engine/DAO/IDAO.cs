using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAO
{
    interface IDAO<T>
    {
        public Task<List<T>> GetAll();

        public Task<T> Get(int id);

        public Task<bool> Exists(int id);

        public T Insert(T entity);

        public bool Edit(T entity);

        public bool Delete(int id);
    }
}
