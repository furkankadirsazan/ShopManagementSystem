using ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework;
using System;
using System.Linq;
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Extensions.Security
{
    public class ShopManagementSystemRoleProvider : RoleProvider
    {
        private readonly EfUnitOfWork uow;
        public ShopManagementSystemRoleProvider()
        {
            uow = new EfUnitOfWork();
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = uow.Users.Find(a => a.Username == username).FirstOrDefault();
            if (user==null)
            {
                //return new string[] { (uow.Students.Find(a => a.Ssn == username).FirstOrDefault()).Roles.Name };
                return new string[] { };
            }
            return new string[] { user.Roles.Name };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}