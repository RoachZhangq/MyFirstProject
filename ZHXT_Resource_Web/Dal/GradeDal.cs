using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;
namespace ZHXT_Resource_Web.Dal
{
    public class GradeDal
    {
        public int GetGradeId(string name)
        {
            int id = -1;
            using (var db = SugarDao.GetInstance())
            {
                var model = db.Queryable<Grade>().Where(c => c.Name == name).FirstOrDefault();
                if (model != null)
                {
                    id = model.ID;
                }
            }

            return id;
        }


        public List<Grade> GetGradeAll()
        {
            List<Grade> list = new List<Grade>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<Grade>().Where(y => y.Disabled == false).OrderBy(y => y.DisplayIndex, OrderByType.Desc).ToList();
            }
            return list;
        }

        public Grade GetGradeById(int id)
        {
            Grade model = null;
            using(var db = SugarDao.GetInstance())
            {
                model = db.Queryable<Grade>().Where(g=>g.ID==id).FirstOrDefault();
            }
            return model;
        }

        public List<Grade> GetGradeListByIdArr(string[] intArray)
        {

            List<Grade> list = new List<Grade>();
            using (var db = SugarDao.GetInstance())
            {
                if (intArray.Length > 0)
                {
                    //查询 
                    list = db.Queryable<Grade>().In("ID", intArray).Where(r => r.Disabled == false).OrderBy(r => r.DisplayIndex, OrderByType.Desc).ToList();
                }
            }
            return list;
        }


        public List<Grade> GetGradeList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            List<Grade> list = new List<Grade>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<Grade>().Where(o => o.Disabled == false).OrderBy(o => o.DisplayIndex, OrderByType.Desc)
                    .ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }

        public bool UpdateGradeDisplayIndex(int id, int value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<Grade>(new { DisplayIndex = value }, o => o.ID == id);
            }
            return result;
        }
        public bool UpdateGradeDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<Grade>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }

    }
}