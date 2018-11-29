using System.Collections.Generic;
using System.IO;
using System.Linq;
using DtCollect.Core.Domain;
using DtCollect.Core.Entity;

namespace DtCollect.Core.Service.Impl
{
    public class UserService: IUserService
    {
        private readonly IRepo<User> _repo;

        public UserService(IRepo<User> repo)
        {
            _repo = repo;
        }
        public User Create(User created)
        {
            if (string.IsNullOrEmpty(created.Username) || string.IsNullOrWhiteSpace(created.Username))
            {
                throw new InvalidDataException("Username is missing");
            }
            return _repo.Create(created);
        }

        public List<User> ReadAll()
        {
            return _repo.ReadAll().ToList();
        }

        public User GetById(int id)
        {
            return _repo.Get(id);
        }

        public User Update(int id, User update)
        {
            if (string.IsNullOrEmpty(update.Username) || string.IsNullOrWhiteSpace(update.Username))
            {
                throw new InvalidDataException("Username is missing");
            }
            update.Id = id;
            return _repo.Update(update);
        }

        public User Delete(int id)
        {
            var user = _repo.Get(id);
            return _repo.Delete(user);
        }
    }
}