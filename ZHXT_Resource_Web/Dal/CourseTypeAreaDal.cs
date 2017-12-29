using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;
namespace ZHXT_Resource_Web.Dal
{
    public class CourseTypeAreaDal
    {
        /// <summary>
        /// 根据课程类型查询 范围
        /// </summary>
        /// <param name="courseTypeId"></param>
        /// <returns></returns>
        public List<CourseTypeArea> GetCourseTypeAreaByCourseTypeId(int courseTypeId)
        {
            List<CourseTypeArea> list = new List<CourseTypeArea>();
            using (var db = SugarDao.GetInstance())
            {
                var CourseTypeAreaList = db.Queryable<CourseTypeArea>().Where(c => c.CourseTypeID == courseTypeId && c.Disabled == false).ToList();
                if (CourseTypeAreaList != null)
                {
                    list = CourseTypeAreaList;

                }
            }
            return list;
        }

        public bool AddCourseTypeArea(CourseTypeArea model)
        {
            bool result = false;
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    db.DisableInsertColumns = Global.DisableInsertColumns_CourseTypeArea;
                    var _DisableInsertColumns = db.DisableInsertColumns;
                    if (model.OrderID == 0)
                    {
                        var list = _DisableInsertColumns.ToList();
                        list.Add("OrderID");
                        _DisableInsertColumns = list.ToArray();
                    }
                    if (model.VloumeID == 0)
                    {
                        var list = _DisableInsertColumns.ToList();
                        list.Add("VloumeID");
                        _DisableInsertColumns = list.ToArray();
                    }
                    db.DisableInsertColumns = _DisableInsertColumns;
                    db.Insert<CourseTypeArea>(model);
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            return result;
        }

        public bool UpdateCourseTypeAreaDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<CourseTypeArea>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }

        public List<CourseTypeArea> GetCourseTypeAreaList(int courseTypeId, int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            List<CourseTypeArea> list = new List<CourseTypeArea>();

            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<CourseTypeArea>()
                     .JoinTable<tbOrder>((o, tborder) => o.OrderID == tborder.ID)
                      .JoinTable<Vloume>((o, v) => o.VloumeID == v.ID)
                    .Where(o => o.Disabled == false && o.CourseTypeID == courseTypeId).OrderBy(o => o.CreationDate, OrderByType.Desc)
                    .Select(" o.*,tborder.Name OrderName,v.Name VloumeName")
                    .ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }




    }
}