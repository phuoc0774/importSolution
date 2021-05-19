using ModelClassLibrary.connection;
using ModelClassLibrary.model;
using ModelClassLibrary.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreService.service
{
    public interface IKhoaSoTramBTS : IReponsitory<KhoaSoTramBTS>
    {
        IEnumerable<KhoaSoTramBTS> GetByQueryString(int? thang = null, int? nam = null);
    }


    public class KhoaSoTramBTSImpl : ReponsitoryImpl<KhoaSoTramBTS>, IKhoaSoTramBTS
    {
        public KhoaSoTramBTSImpl(DataContext context) : base(context)
        {
        }

        public IEnumerable<KhoaSoTramBTS> GetByQueryString(int? thang = null, int? nam = null)
        {
            var res = m_table.ToList();
            res = res.Where(_ => (thang == null || _.thang == thang.Value) && (nam == null || _.nam == nam.Value)).ToList();
            return res;
        }
    }
}
