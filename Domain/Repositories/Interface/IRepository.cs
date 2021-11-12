using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Interface
{
    interface IRepository<T> where T:BaseEntity
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> FindById(int id);
        public Task Create(T item);
        public Task Update(T item);
        public Task Delete(T item);
    }
}
