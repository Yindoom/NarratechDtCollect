using System.Collections;
using System.Collections.Generic;
using DtCollect.Core.Entity;

namespace DtCollect.Core.Domain
{
    public interface IRepo<T>
    {
        IEnumerable<T> ReadAll();
        T Get(int id);
        T Create(T created);
        T Update(T update);
        T Delete(T delete);
        User GetByUsername(LoginInput login);

    }
}