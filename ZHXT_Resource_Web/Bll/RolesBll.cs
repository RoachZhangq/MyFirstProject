using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class RolesBll
    {
        RolesDal dal = new RolesDal();
        public Roles GetRolesById(int rolesId)
        {
            return dal.GetRolesById(rolesId);
        }
        public List<Roles> GetRolesList(string[] intArray)
        {
            return dal.GetRolesList(intArray);
        }

        public List<Roles> GetRolesList(string likeName, int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            return dal.GetRolesList(likeName, pageIndex, pageSize, ref pageCount, ref totalPage);
        }

        public bool UpdateRolesDisabled(int id, bool value)
        {
            return dal.UpdateRolesDisabled(id, value);
        }









    }


}