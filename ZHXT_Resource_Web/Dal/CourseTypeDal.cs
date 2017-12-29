using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class CourseTypeDal
    {
        public int GetCourseTypeId(int upcCourseTypeId)
        {
            int id = -1;
            using (var db = SugarDao.GetInstance())
            {
                var model = db.Queryable<CourseType>().Where(c => c.CourseTypeID == upcCourseTypeId).FirstOrDefault();
                if (model != null)
                {
                    id = model.ID;
                }
            }
            return id;
        }


        public List<CourseType> GetCourseTypeAll()
        {
            List<CourseType> list = new List<CourseType>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<CourseType>().Where(y => y.Disabled == false).OrderBy(y => y.DisplayIndex, OrderByType.Desc).ToList();
            }
            return list;
        }

        public CourseType GetCourseTypeById(int id)
        {
            CourseType model = null;
            using (var db = SugarDao.GetInstance())
            {
                model = db.Queryable<CourseType>().Where(c => c.ID == id).FirstOrDefault();
            }
            return model;
        }
        public List<CourseType> GetCourseTypeListByIdArr(string[] intArray)
        {

            List<CourseType> list = new List<CourseType>();
            using (var db = SugarDao.GetInstance())
            {
                if (intArray.Length > 0)
                {
                    //查询 
                    list = db.Queryable<CourseType>().In("ID", intArray).Where(r => r.Disabled == false).OrderBy(r => r.DisplayIndex, OrderByType.Desc).ToList();
                }
            }
            return list;
        }

        public List<CourseType> GetCourseTypeList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage, bool disabled = false)
        {
            List<CourseType> list = new List<CourseType>();
            using (var db = SugarDao.GetInstance())
            {
                var queryable1 = db.Queryable<CourseType>();
                if (!disabled) queryable1.Where(o => o.Disabled == false);
                queryable1.OrderBy(o => o.DisplayIndex, OrderByType.Desc);
                list = queryable1.ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }

        public bool UpdateCourseTypeDisplayIndex(int id, int value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<CourseType>(new { DisplayIndex = value }, o => o.ID == id);
            }
            return result;
        }
        public bool UpdateCourseTypeDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<CourseType>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }
    }
}