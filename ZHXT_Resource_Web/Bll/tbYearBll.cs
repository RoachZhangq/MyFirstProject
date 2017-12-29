using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class tbYearBll
    {
        tbYearDal dal = new tbYearDal();
        public List<tbYear> GetYearAll()
        {
            return dal.GetYearAll();
        }

        public tbYear GetYearById(int id)
        {
            return dal.GetYearById(id);
        }

        public List<tbYear> GettbYearList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage, bool disabled = false)
        {
            return dal.GettbYearList(pageIndex, pageSize, ref pageCount, ref totalPage, disabled);
        }

        public bool UpdatetbYearDisplayIndex(int id, int value)
        {
            return dal.UpdatetbYearDisplayIndex(id, value);
        }

        public bool UpdatetbYearDisabled(int id, bool value)
        { return dal.UpdatetbYearDisabled(id, value); }
        }
}