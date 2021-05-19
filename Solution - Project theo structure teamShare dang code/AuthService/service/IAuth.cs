using ModelClassLibrary.area.respond;
using ModelClassLibrary.area.model;

namespace AuthServer.service
{
    public interface IAuth
    {
        DataRespond login(Users user,string lang);
        Users getUser(Users user);
    }
}
