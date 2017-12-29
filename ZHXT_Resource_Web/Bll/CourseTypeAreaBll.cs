using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;
using Models;


namespace ZHXT_Resource_Web.Bll
{
    public class CourseTypeAreaBll
    {
        CourseTypeAreaDal dal = new CourseTypeAreaDal();
        public List<CourseTypeArea> GetCourseTypeAreaByCourseTypeId(int courseTypeId)
        {

            return dal.GetCourseTypeAreaByCourseTypeId(courseTypeId);
        }
        public bool AddCourseTypeArea(CourseTypeArea model)
        {
            return dal.AddCourseTypeArea(model);
        }

        public bool UpdateCourseTypeAreaDisabled(int id, bool value)
        {
            return dal.UpdateCourseTypeAreaDisabled(id, value);
        }
        public List<CourseTypeArea> GetCourseTypeAreaList(int courseTypeId, int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            return dal.GetCourseTypeAreaList(courseTypeId,pageIndex, pageSize, ref pageCount, ref totalPage);

        }
    }
}