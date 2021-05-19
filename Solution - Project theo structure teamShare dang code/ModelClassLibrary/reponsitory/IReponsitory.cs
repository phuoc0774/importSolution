using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelClassLibrary.reponsitory
{
    public interface IReponsitory<T> where T : class, IModel
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        bool Insert(T obj);

        Tuple<bool, T[]> Insert(T[] wrap, int? check);
        bool Update(T obj);
        bool Delete(object id);

        bool DeleteAll(List<T> id);
        void Save();

        //IEnumerable<T> getAll();
        //T getById(object id);
        //void insert(T obj);
        //void update(T obj);
        //void delete(object id);
        //void save();
    }
}
