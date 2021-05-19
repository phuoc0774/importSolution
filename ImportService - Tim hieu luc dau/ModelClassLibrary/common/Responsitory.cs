using Microsoft.EntityFrameworkCore;
using ModelClassLibrary.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelClassLibrary.common
{
    public interface IResponsitory<T> where T : class, IModel
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        bool Insert(T obj);
        //List<T> Insert(Wrap<T> wrap, int? check);
        //List<T> Insert(T[] wrap, int? check);
        Tuple<bool, T[]> Insert(T[] wrap, int? check);
        bool Update(T obj);
        bool Delete(object id);
        void Save();
    }


    /*CLASS: RESPONSITORY<T>
        dbContext, dbSet
    */
    public class Responsitory<T> : IResponsitory<T> where T : class, IModel
    {
        protected DbContext dbContext = null;
        protected DbSet<T> dbSet = null;

        public Responsitory(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }



        public virtual bool Delete(object id)
        {
            T obj = dbSet.Find(id);
            if (obj != default(T) && obj != null)
            {
                dbSet.Remove(obj);
                Save();
                return true;
            }

            return false;
        }

        public virtual IEnumerable<T> GetAll()
        {
            var res = dbSet.ToList();
            return res;
        }

        public virtual T GetById(object id)
        {
            var res = dbSet.Find(id);
            return res;
        }

        public virtual bool Insert(T obj)
        {
            var istance = GetById(obj._id);
            if(istance == null)
            {
                dbSet.Add(obj);
                Save();
                return true;
            }
            return false;
        }

        public virtual Tuple<bool, T[]> Insert(T[] objs, int? check = null)
        {

            int _check = 0;
            if (check != null) _check = check.Value;
            //_check = 0;
            if (_check == 1)
            {
                var intsID = objs.Where(_ => _._id != null && (int)_._id != 0).ToList().Select(_ => _._id).ToList();
                var lstdbSet = dbSet.ToList();
                lstdbSet = lstdbSet.Where(_ => intsID.Contains((int)_._id)).ToList();
                if (lstdbSet.Count > 0)
                    return Tuple.Create(false, lstdbSet.ToArray());
            }

            dbSet.AddRange(objs);
            Save();
            return Tuple.Create(true, objs);
        }

        public virtual Tuple<bool, T[]> InsertOrUpdate(T[] objs)
        {
 
            var intsID = objs.Where(_ => _._id != null && (int)_._id != 0).ToList().Select(_ => _._id).ToList();
            var lstdbSet = dbSet.ToList();
            lstdbSet = lstdbSet.Where(_ => intsID.Contains((int)_._id)).ToList();
            if (lstdbSet.Count > 0)
                return Tuple.Create(false, lstdbSet.ToArray());

            dbSet.AddRange(objs);
            Save();
            return Tuple.Create(true, objs);
        }

        //public virtual List<T> Insert(T[] objs, int? check = null)
        //{

        //    int _check = 0;
        //    if (check != null) _check = check.Value;
        //    if (_check == 1)
        //    {
        //        var intsID = objs.Where(_ => _._id != null && (int)_._id != 0).ToList().Select(_ => _._id).ToList();
        //        var lstdbSet = dbSet.ToList();
        //        lstdbSet = lstdbSet.Where(_ => intsID.Contains((int)_._id)).ToList();
        //        if (lstdbSet.Count > 0)
        //            return lstdbSet;
        //    }

        //    dbSet.AddRange(objs);
        //    Save();
        //    return new List<T>();
        //}

        //public virtual List<T> Insert(Wrap<T> wrap, int? check = null)
        //{
        //    var objs = wrap.arr;

        //    int _check = wrap.check;
        //    if (check != null) _check = check.Value;
        //    if (_check == 1)
        //    {
        //        var intsID = objs.Where(_ => _._id != null && (int)_._id != 0).ToList().Select(_ => _._id).ToList();
        //        var lstdbSet = dbSet.ToList();
        //        lstdbSet = lstdbSet.Where(_ => intsID.Contains((int)_._id)).ToList();
        //        if (lstdbSet.Count > 0)
        //            return lstdbSet;
        //    }

        //    dbSet.AddRange(objs);
        //    Save();
        //    return null;
        //}


        public virtual bool Update(T obj)
        {
            var istance = GetById(obj._id);
            if (istance == null)
            {
                return false;
            }

            dbContext.Add(obj).State = EntityState.Added;
            //dbContext.Entry(obj).State = EntityState.Modified;
            Save();
            return true;
        }

        public virtual void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
