using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class ResourceInfoDal
    {
        public ResourceInfo GetResourceInfoById(int id)
        {
            ResourceInfo model = null;
            using (var db = SugarDao.GetInstance())
            {
                model = db.Queryable<ResourceInfo>().Where(r => r.ID == id).FirstOrDefault(); 
            }
            return model;
        }


        public bool UpdateResourceInfoDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<ResourceInfo>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }
    }
}