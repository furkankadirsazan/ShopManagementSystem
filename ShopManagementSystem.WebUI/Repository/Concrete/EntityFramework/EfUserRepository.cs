using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfUserRepository : EfGenericRepository<Users>, IUserRepository
    {
        public EfUserRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public Users GetByEmail(string email)
        {
            return db.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        public Users GetByUsername(string username)
        {
            return db.Users.Where(u => u.Username == username).FirstOrDefault();
        }
        public Users GetByUsernameAndPassword(string username, string password)
        {
            return db.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
        }

        //ToDo Metodu Generic yap
        public bool HasSameRecords(Users entity)
        {
            if (entity != null)
            {
                try
                {
                    var usersInDb = db.Users.Where(a => a.Username == entity.Username || a.Email == entity.Email).ToList();
                    if (usersInDb != null)
                    {
                        if (usersInDb.Any())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}