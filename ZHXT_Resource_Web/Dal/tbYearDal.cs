using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class tbYearDal
    {
        public List<tbYear> GetYearAll()
        {
            List<tbYear> list = new List<tbYear>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<tbYear>().Where(y => y.Disabled == false).OrderBy(y => y.DisplayIndex, OrderByType.Desc).ToList();
            }
            return list;
        }

        public tbYear GetYearById(int id)
        {
            tbYear model = null;
            using (var db = SugarDao.GetInstance())
            {
                model= db.Queryable<tbYear>().Where(y => y.ID == id).FirstOrDefault() ;
            }
            return model;
        }


        public List<tbYear> GettbYearList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage,bool disabled =false)
        {
            List<tbYear> list = new List<tbYear>();
            using (var db = SugarDao.GetInstance())
            {
                var queryable1 = db.Queryable<tbYear>();
                if(!disabled) queryable1.Where(o => o.Disabled == false); 
                queryable1.OrderBy(o => o.DisplayIndex, OrderByType.Desc);
                 list= queryable1.ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }


        public bool UpdatetbYearDisplayIndex(int id, int value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<tbYear>(new { DisplayIndex = value }, o => o.ID == id);
            }
            return result;
        }
        public bool UpdatetbYearDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<tbYear>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }
    }
}