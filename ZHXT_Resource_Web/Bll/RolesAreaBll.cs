using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class RolesAreaBll
    {
        RolesAreaDal dal = new RolesAreaDal();

        public List<RolesArea> GetRolesAreaListByRolesId(int rolesId)
        {
            return dal.GetRolesAreaListByRolesId(rolesId);
        }
        }
}