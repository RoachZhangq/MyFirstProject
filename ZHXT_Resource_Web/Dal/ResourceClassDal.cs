using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class ResourceClassDal
    {
        /// <summary>
        /// 根据ID（数组）查询
        /// </summary>
        /// <param name="intArray"></param>
        /// <returns></returns>
        public List<ResourceClass> GetResourceClassList(string[] intArray)
        {
            List<ResourceClass> list = new List<ResourceClass>();
            using (var db = SugarDao.GetInstance())
            {
                if (intArray.Length == 0)
                {
                    //查询全部
                   // list = db.Queryable<ResourceClass>().Where(r => true && r.Disabled == false).OrderBy(r=>r.DisplayIndex,OrderByType.Desc).ToList();
                }
                else
                {
                    //查询 
                    list = db.Queryable<ResourceClass>().In("ID", intArray).Where(r=>r.Disabled==false).OrderBy(r => r.DisplayIndex, OrderByType.Desc).ToList();
                }
            }
            return list;
        }

        public  ResourceClass GetResourceClassById(int id)
        {
            ResourceClass model = null;
            using (var db = SugarDao.GetInstance())
            {
                model=db.Queryable<ResourceClass>().Where(r=>r.ID==id&& r.Disabled == false).FirstOrDefault();
            }
             return model;
        }


        public List<ResourceClass> GetResourceClassListAll()
        {
            List<ResourceClass> list = new List<ResourceClass>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<ResourceClass>().Where(r => true && r.Disabled == false).OrderBy(r => r.DisplayIndex, OrderByType.Desc).ToList();

            }
            return list;
        }

        public List<ResourceClass> GetResourceClassList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage, bool disabled = false)
        {
            List<ResourceClass> list = new List<ResourceClass>();
            using (var db = SugarDao.GetInstance())
            {
                var queryable1 = db.Queryable<ResourceClass>();
                if (!disabled) queryable1.Where(o => o.Disabled == false);
                queryable1.OrderBy(o => o.DisplayIndex, OrderByType.Desc);
                   list = queryable1.ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }
        public bool UpdateResourceClassDisplayIndex(int id, int value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<ResourceClass>(new { DisplayIndex = value }, o => o.ID == id);
            }
            return result;
        }

        public bool UpdateResourceClassDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<ResourceClass>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }
    }
}