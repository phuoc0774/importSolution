using ModelClassLibrary.area.auth.model;
using ModelClassLibrary.connection;
using ModelClassLibrary.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreService.service
{
    public interface IRole : IReponsitory<Roles>
    {
    }
    public class RoleImpl: PermissionReponsitoryImpl<Roles>, IRole
    {
        public RoleImpl(PermissionContext context) : base(context)
        {
        }

        public override bool Update(Roles obj)
        {
            var istance = GetById(obj._id);
            if (istance == null)
            {
                return false;
            }

            istance.rolename = obj.rolename;

            Save();
            return true;
        }
    }
}
