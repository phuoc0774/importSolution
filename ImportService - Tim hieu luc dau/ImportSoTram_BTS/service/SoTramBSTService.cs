using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportSoTram_BTS.data;
using ModelClassLibrary.common;
using ModelClassLibrary.model;

namespace ImportSoTram_BTS.service
{
    public class SoTramBSTService : Responsitory<SoTramBST> , ISoTramBSTService
    {
        public SoTramBSTService(DataContext context) : base(context)
        {
        }


        //public virtual List<SoTramBST> Insert(SoTramBSTs arrs, int check = 0)
        //{
        //    //var istance = GetById(obj.id);
        //    //if (istance == null)
        //    //{
        //    //    dbSet.Add(obj);
        //    //    Save();
        //    //    return true;
        //    //}
        //    //return false;

        //    var objs = arrs.arrs;
        //    if (check == 1)
        //    {
        //        var intsID = objs.Where(_ => _.id != 0 && _.id != null).ToList().Select(_ => _.id).ToList();
        //        var lstdbSet = dbSet.Where(_ => intsID.Contains((int)_.id)).ToList();
        //        if (lstdbSet.Count > 0)
        //            return lstdbSet;
        //    }

        //    dbSet.AddRange(objs);
        //    Save();
        //    return null;
        //}
    }
}
