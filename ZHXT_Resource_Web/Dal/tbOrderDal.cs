using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class tbOrderDal
    {
        public List<tbOrder> GetOrderAll()
        {
            List<tbOrder> list = new List<tbOrder>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<tbOrder>().Where(y => y.Disabled == false).OrderBy(y => y.DisplayIndex, OrderByType.Desc).ToList();
            }
            return list;
        }


        public tbOrder GettbOrderById(int id)
        {
            tbOrder model = null;
            using(var db = SugarDao.GetInstance())
            {
                model = db.Queryable<tbOrder>().Where(o => o.ID == id).FirstOrDefault();
            }
            return model;
        }

        public List<tbOrder> GettbOrderList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            List<tbOrder> list = new List<tbOrder>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<tbOrder>().Where(o => o.Disabled == false).OrderBy(o => o.DisplayIndex, OrderByType.Desc)
                    .ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }

        public bool UpdatetbOrderDisplayIndex(int id, int value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<tbOrder>(new { DisplayIndex = value }, o => o.ID == id);
            }
            return result;
        }


        public bool UpdatetbOrderDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<tbOrder>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }
    }
}