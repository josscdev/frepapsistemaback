using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Abstraction
{
    public interface IRepository<T> where T : class
    {
        //IList<T> GetProducto();
        //Task<List<T>> ObtenerTodo();
        Task<T> Add(T entity);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task<T> Update(T entity);
        Task<bool> Remove(T entity);


    }
}
