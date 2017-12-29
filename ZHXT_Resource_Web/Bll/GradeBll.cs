using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class GradeBll
    {
        GradeDal dal = new GradeDal();
        public int GetGradeId(string name)
        {
            return dal.GetGradeId(name);
        }
        public List<Grade> GetGradeAll()
        {
            return dal.GetGradeAll();
        }

        public Grade GetGradeById(int id)
        {
            return dal.GetGradeById(id);
        }

        public List<Grade> GetGradeListByIdArr(string[] intArray)
        {
            return dal.GetGradeListByIdArr(intArray);
        }


        public List<Grade> GetGradeList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            return dal.GetGradeList(pageIndex, pageSize, ref pageCount, ref totalPage);
        }


        public bool UpdateGradeDisplayIndex(int id, int value)
        {
            return dal.UpdateGradeDisplayIndex(id, value);

        }
        public bool UpdateGradeDisabled(int id, bool value)
        {
            return dal.UpdateGradeDisabled(id, value);

        }
   }
}