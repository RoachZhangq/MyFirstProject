using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;
namespace ZHXT_Resource_Web.Bll
{
    public class UserRoleBll
    {
        UserRoleDal dal = new UserRoleDal();
        public List<UserRole> GetUserRoleListByUserId(int userId)
        {
            return dal.GetUserRoleListByUserId(userId);
        }

        public bool DeleteUserRoleByRolesId(int rolesId)
        {
            return dal.DeleteUserRoleByRolesId(rolesId);

        }
    }
}