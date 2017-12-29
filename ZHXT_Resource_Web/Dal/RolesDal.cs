using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class RolesDal
    {
        public Roles GetRolesById(int rolesId)
        {
            Roles model = null;
            using (var db = SugarDao.GetInstance())
            {
                model = db.Queryable<Roles>().Where(u => u.ID == rolesId && u.Disabled == false).FirstOrDefault();
            }
            return model;
        }

        public List<Roles> GetRolesList(string[] intArray)
        {
            List<Roles> list = new List<Roles>();
            using (var db = SugarDao.GetInstance())
            {
                if (intArray.Length > 0)
                {
                    //查询 
                    list = db.Queryable<Roles>().In("ID", intArray).Where(r => r.Disabled == false).OrderBy(r => r.CreationDate, OrderByType.Desc).ToList();
                }
            }
            return list;
        }

        public List<Roles> GetRolesList(string likeName, int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            List<Roles> list = new List<Roles>();
            using (var db = SugarDao.GetInstance())
            {
                var queryable1=  db.Queryable<Roles>().Where(o => o.Disabled == false);
                if (!string.IsNullOrEmpty(likeName))
                {
                    queryable1.Where(o => o.Name.Contains(likeName) || o.Remark.Contains(likeName));
                }
                queryable1.OrderBy(o => o.ID, OrderByType.Desc);
                list = queryable1.ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }


        public bool UpdateRolesDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<Roles>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }
    }
}