using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class UserRoleDal
    {
        public List<UserRole> GetUserRoleListByUserId(int userId)
        {
            List<UserRole> list = null;
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<UserRole>().Where(u=>u.UserID==userId&&u.Disabled==false).ToList();
            }
            return list;
        } 
        public bool DeleteUserRoleByRolesId(int rolesId)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result= db.Update<UserRole>(new { Disabled = true }, u => u.RolesID == rolesId);
            }
            return result;
        }
    }
}