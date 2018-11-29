using System.Collections.Generic;
using DtCollect.Core.Entity;

namespace DtCollect.Core.Service
{
    public interface IUserService
    {
        User Create(User created);
        
        List<User> ReadAll();
        
        User GetUser(LoginInput login);
        
        User GetById(int id);
        
        User Update(int id, User update);
        
        User Delete(int id);
    }
}