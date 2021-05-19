using Microsoft.EntityFrameworkCore;
using ModelClassLibrary.connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelClassLibrary.reponsitory;

namespace ModelClassLibrary.reponsitory
{
    public class PermissionReponsitoryImpl<T> : IReponsitory<T> where T : class, IModel
    {

        protected PermissionContext m_context = null;
        protected DbSet<T> m_table = null;

        public PermissionReponsitoryImpl(PermissionContext dbContext)
        {
            this.m_context = dbContext;
            this.m_table = dbContext.Set<T>();
        }



        public virtual bool Delete(object id)
        {
            T obj = m_table.Find(id);
            if (obj != default(T) && obj != null)
            {
                m_table.Remove(obj);
                Save();
                return true;
            }

            return false;
        }

        public virtual IEnumerable<T> GetAll()
        {
            var res = m_table.ToList();
            return res;
        }

        public virtual T GetById(object id)
        {
            var res = m_table.Find(id);
            return res;
        }

        public virtual bool Insert(T obj)
        {
            var istance = GetById(obj._id);
            if (istance == null)
            {
                m_table.Add(obj);
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
                var lstdbSet = m_table.ToList();
                lstdbSet = lstdbSet.Where(_ => intsID.Contains((int)_._id)).ToList();
                if (lstdbSet.Count > 0)
                    return Tuple.Create(false, lstdbSet.ToArray());
            }

            m_table.AddRange(objs);
            Save();
            return Tuple.Create(true, objs);
        }



        //public virtual Tuple<bool, T[]> InsertOrUpdate(T[] objs)
        //{

        //    var intsID = objs.Where(_ => _._id != null && (int)_._id != 0).ToList().Select(_ => _._id).ToList();
        //    var lstdbSet = dbSet.ToList();
        //    lstdbSet = lstdbSet.Where(_ => intsID.Contains((int)_._id)).ToList();
        //    if (lstdbSet.Count > 0)
        //        return Tuple.Create(false, lstdbSet.ToArray());

        //    dbSet.AddRange(objs);
        //    Save();
        //    return Tuple.Create(true, objs);
        //}


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


        public  virtual bool Update(T obj)
        {
            //var istance = GetById(obj._id);
            //if (istance == null)
            //{
            //    return false;
            //}

            //var istance = await m_table.SingleAsync(x => x._id == obj._id);
            //if (istance == null)
            //{
            //    return false;
            //}

            m_table.Update(obj).State = EntityState.Modified;

            //m_table.Attach(obj).State = EntityState.Modified; 
            //m_context.Entry(obj).State = EntityState.Modified;

            //m_context.SaveChanges();


            //m_context.Add(obj).State = EntityState.Modified;
            //dbContext.Entry(obj).State = EntityState.Modified;
            Save();
            return true;
        }

        public virtual void Save()
        {
            m_context.SaveChanges();
        }

        public bool DeleteAll(List<T> id)
        {
            throw new NotImplementedException();
        }



        //protected readonly PermissionContext m_context;
        //protected DbSet<T> m_table = null;
        //public PermissionReponsitoryImpl(PermissionContext context)
        //{
        //    m_context = context;
        //    m_table = m_context.Set<T>();
        //}
        //public void delete(object id)
        //{
        //    T obj = m_table.Find(id);
        //    m_table.Remove(obj);
        //    save();
        //}
        //public void insert(T obj)
        //{
        //    //m_table.Add(obj);
        //    m_context.Add(obj).State = EntityState.Added;
        //    save();
        //}
        //public void save()
        //{
        //    m_context.SaveChanges();
        //}
        //public IEnumerable<T> getAll()
        //{
        //    return m_table.ToList();
        //}
        //public T getById(object id)
        //{
        //    return m_table.Find(id);
        //}
        //public void update(T obj)
        //{
        //    m_table.Update(obj);
        //    save();
        //}
    }
}
