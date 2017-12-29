using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;
using Models;

namespace ZHXT_Resource_Web.Bll
{
    public class CourseTypeBll
    {
        CourseTypeDal dal = new CourseTypeDal();
        public int GetCourseTypeId(int upcCourseTypeId)
        {
            return dal.GetCourseTypeId(upcCourseTypeId);
        }

        public List<CourseType> GetCourseTypeAll()
        {
            return dal.GetCourseTypeAll();
        }


        public CourseType GetCourseTypeById(int id)
        {
            return dal.GetCourseTypeById(id);
        }


        public List<CourseType> GetCourseTypeListByIdArr(string[] intArray)
        {
            return dal.GetCourseTypeListByIdArr(intArray);
        }
        public List<CourseType> GetCourseTypeList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage, bool disabled = false)
        {
            return dal.GetCourseTypeList(pageIndex, pageSize, ref pageCount, ref totalPage, disabled);
        }
        public bool UpdateCourseTypeDisplayIndex(int id, int value)
        {
            return dal.UpdateCourseTypeDisplayIndex(id,  value);
        }

        public bool UpdateCourseTypeDisabled(int id, bool value)
        { return dal.UpdateCourseTypeDisabled(id, value); }
        }
}