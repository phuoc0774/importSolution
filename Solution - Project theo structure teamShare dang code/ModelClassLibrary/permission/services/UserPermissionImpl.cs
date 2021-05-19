using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.connection;
using ModelClassLibrary.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ModelClassLibrary.permission.services
{
    public interface IUserPermission : IReponsitory<Permissions> 
    {
        Boolean checkPermissionForUser(ClaimsPrincipal user, string permission);
    }

    public class UserPermissionImpl : PermissionReponsitoryImpl<Permissions>, IUserPermission
    {
        public UserPermissionImpl(PermissionContext context) : base(context)
        {
        }
        public bool checkPermissionForUser(ClaimsPrincipal user, string permission)
        {
            //public policy
            if (permission == "public")
            {
                return true;
            } 
            //get username
            var username = user.Claims.FirstOrDefault().Value;
            if(username == "admin")
            {
                return true;
            }
            //get roles for user
            var pm = from us in m_context.Users.Where(m => m.username == username)
                     join userper in m_context.UserPermissions
                     on us.usid equals userper.usid
                     join groups in m_context.Groups
                     on userper.groupid equals groups.groupid
                     join groupper in m_context.GroupPermissions
                     on groups.groupid equals groupper.groupid
                     join perm in m_context.Permissions.Where (n => n.policy == permission)
                     on groupper.perid equals perm.perid
                     select us;
            //check roles for user
            if (pm != null)
            {
                return true;
            }
            return true;
        }
     
    }
}
