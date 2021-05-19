using ModelClassLibrary.area.model;
using ModelClassLibrary.connection;
using ModelClassLibrary.reponsitory;
using ModelClassLibrary.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreService.service
{
    // Authu ==> login, login , refeefreeeefreshton 
    // User Servive 
    public interface IUser : IReponsitory<Users>
    {
        Users getUserByUsername(string username);
        Boolean createFirstUser();
        Boolean checkUserExist(string username);

        //dynamic searchUser(DataFilter dataFilter);
        //void changePass(DataFilter data);
        //Boolean checkPass(DataFilter data);
    }


    public class UserImpl : PermissionReponsitoryImpl<Users>, IUser
    {
        private IHashPass m_hashPass;
        public UserImpl(PermissionContext context, IHashPass hashPass) : base(context)
        {
            m_hashPass = hashPass;
        }

   
        public bool checkUserExist(string username)
        {
            List<Users> user = GetAll().Where(m => m.username == username).ToList();
            if (user.Count > 1)
            {
                return false;
            }
            return true;
        }
        public Boolean createFirstUser()
        {
            if (GetAll().Count() != 0)
            {
                return false;
            }
            else
            {
                Users user = new Users();
                user.username = "admin";
                user.password = m_hashPass.hashPass("123456");
                user.role = 1;
                Insert(user);
                return true;
            }
        }

        public Users getUserByUsername(string username)
        {
            return GetAll().Where(m => m.username == username).FirstOrDefault();
        }

        //public dynamic searchUser(DataFilter dataFilter)
        //{
        //    var filterby = (dataFilter.filter ?? "").Trim().ToLowerInvariant();
        //    var log = getAll()
        //            .AsQueryable()
        //            .Where(n =>
        //                    n.username.ToString().ToLowerInvariant().Contains(filterby)
        //                || n.email.ToString().ToLowerInvariant().Contains(filterby)
        //                || n.phone.ToString().ToLowerInvariant().Contains(filterby)
        //                || n.usid.ToString().ToLowerInvariant().Contains(filterby)
        //    );
        //    return log;
        //}
        //public void changePass(DataFilter data)
        //{
        //    var user = getAll().FirstOrDefault(m => m.usid == data.usid);
        //    user.password = m_hashPass.hashPass(data.newpass);
        //    update(user);
        //}

        //public bool checkPass(DataFilter data)
        //{
        //    var user = getAll().FirstOrDefault(m => m.usid == data.usid);
        //    return m_hashPass.checkPass(user.password, data.oldpass);
        //}

    }

}
