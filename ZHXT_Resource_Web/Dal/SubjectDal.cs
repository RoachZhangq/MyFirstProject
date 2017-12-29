using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class SubjectDal
    {
        public List<Subject> GetSubjectAll()
        {
            List<Subject> list = new List<Subject>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<Subject>().Where(y => y.Disabled == false).OrderBy(y => y.DisplayIndex, OrderByType.Desc).ToList();
            }
            return list;
        }

        public Subject GetSubjectById(int id)
        {
            Subject model = null;
            using (var db = SugarDao.GetInstance())
            {
                model = db.Queryable<Subject>().Where(s => s.ID == id).FirstOrDefault();
            }
            return model;
        }

        public List<Subject> GetSubjectListByIdArr(string[] intArray)
        {

            List<Subject> list = new List<Subject>();
            using (var db = SugarDao.GetInstance())
            {
                if (intArray.Length > 0)
                {
                    //查询 
                    list = db.Queryable<Subject>().In("ID", intArray).Where(r => r.Disabled == false).OrderBy(r => r.DisplayIndex, OrderByType.Desc).ToList();
                }
            }
            return list;
        }


        public List<Subject> GetSubjectList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage, bool disabled = false)
        {
            List<Subject> list = new List<Subject>();
            using (var db = SugarDao.GetInstance())
            {
                var queryable1 = db.Queryable<Subject>();
                if (!disabled) queryable1.Where(o => o.Disabled == false);
                queryable1.OrderBy(o => o.DisplayIndex, OrderByType.Desc);
                list = queryable1.ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }



        public bool UpdateSubjectDisplayIndex(int id, int value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<Subject>(new { DisplayIndex = value }, o => o.ID == id);
            }
            return result;
        }

        public bool UpdateSubjectDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<Subject>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }
    }
}