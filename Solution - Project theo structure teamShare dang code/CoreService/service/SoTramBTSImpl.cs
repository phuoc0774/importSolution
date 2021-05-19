using ModelClassLibrary.connection;
using ModelClassLibrary.model;
using ModelClassLibrary.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreService.service
{
    public interface ISoTramBTS : IReponsitory<SoTramBTS>
    {
        IEnumerable<SoTramBTS> GetByThangNam(int thang, int nam);
        Tuple<bool, SoTramBTS[]> InsertCheckKhoaBang(SoTramBTS[] objs, int? check = null);
    }
    public class SoTramBTSImpl: ReponsitoryImpl<SoTramBTS>, ISoTramBTS
    {
        public SoTramBTSImpl(DataContext context) : base(context)
        {
        }

        public IEnumerable<SoTramBTS> GetByThangNam(int thang, int nam)
        {
            var res = m_table.ToList();
            var res2 = res.Where(_ => _.thang == thang && _.nam == nam);
            return res2;

        }

        public virtual Tuple<bool, SoTramBTS[]> InsertCheckKhoaBang(SoTramBTS[] objs, int? check)
        {
            var intNam = objs[0].nam ?? 1;
            var intThang = objs[0].thang ?? 1;
            var lstKhoaSoTramBTS = m_context.Set<KhoaSoTramBTS>();
            //khóa theo tháng
            var lstKhoaSoTramBTS_KT = lstKhoaSoTramBTS.Where(_ => 
                (_.trang_thai == 1) &&
                (_.thang == intThang && _.nam == intNam)).ToList();
            //Khóa giai đoạn trước
            var lstKhoaSoTramBTS_KGD = lstKhoaSoTramBTS.Where(_ => 
                (_.trang_thai == 2 && _.nam != null && _.thang != null) &&
                (_.nam < intNam || (_.nam == intNam && _.thang <= intThang))
            ).ToList(); 
            //Check khóa bảng
            if(lstKhoaSoTramBTS_KT.Count > 0 || lstKhoaSoTramBTS_KGD.Count > 0)
                return Tuple.Create(false, objs);

            //EXE
            m_table.AddRange(objs);
            Save();
            return Tuple.Create(true, objs);


            //if (_check == 1)
            //{
            //    var intsID = objs.Where(_ => _._id != null && (int)_._id != 0).ToList().Select(_ => _._id).ToList();
            //    var lstdbSet = m_table.ToList();
            //    lstdbSet = lstdbSet.Where(_ => intsID.Contains((int)_._id)).ToList();
            //    if (lstdbSet.Count > 0)
            //        return Tuple.Create(false, lstdbSet.ToArray());
            //}
        }

    }
}
