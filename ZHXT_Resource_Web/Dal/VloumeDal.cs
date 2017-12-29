using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class VloumeDal
    {
        public List<Vloume> GetVloumeAll()
        {
            List<Vloume> list = new List<Vloume>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<Vloume>().Where(y => y.Disabled == false).OrderBy(y => y.DisplayIndex, OrderByType.Desc).ToList();
            }
            return list;
        }

        public Vloume GetVloumeById(int id)
        {
            Vloume model = null;
            using(var db = SugarDao.GetInstance())
            {
                model = db.Queryable<Vloume>().Where(v=>v.ID==id).FirstOrDefault();
            }
            return model;
        }

        public List<Vloume> GetVloumeList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            List<Vloume> list = new List<Vloume>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<Vloume>().Where(o => o.Disabled == false).OrderBy(o => o.DisplayIndex, OrderByType.Desc)
                    .ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }


        public bool UpdateVloumeDisplayIndex(int id, int value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<Vloume>(new { DisplayIndex = value }, o => o.ID == id);
            }
            return result;
        }

        public bool UpdateVloumeDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<Vloume>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }
    }
}