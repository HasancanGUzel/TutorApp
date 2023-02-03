using OzelDers.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzelDers.Data.Abstract
{
    public interface IRepository<T>
    {
         Task<T> GetByIdAsync(int id); //Id'ye göre entity getirecek
        Task<List<T>> GetAllAsync(); //İlgili tüm kayıtları getirecek
        Task CreateAsync(T entity); //yeni kayıt yaratacak
        void Update(T entity); //kayıt güncelleyecek
        void Delete(T entity); //kayıt silecek
        Task UpdateAsyncc(T entity);
    }
}