using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class RolesAreaDal
    {
        public List<RolesArea> GetRolesAreaListByRolesId(int rolesId)
        {
            List<RolesArea> list = new List<RolesArea>();
            using (var db=SugarDao.GetInstance())
            {
                list = db.Queryable<RolesArea>()
                    .JoinTable<ResourceClass>((o,r)=>o.ResourceClassID==r.ID)
                    .Where(o => o.RolesID == rolesId && o.Disabled == false)
                    .Select("o.*,r.Name ResourceClassName").ToList();
            }
            return list;
        }
    }
}