using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class tbOrderBll
    {
        tbOrderDal dal = new tbOrderDal();
        public List<tbOrder> GetOrderAll()
        {
            return dal.GetOrderAll();
        }
        public tbOrder GettbOrderById(int id)
        {
            return dal.GettbOrderById(id);
        }

        public List<tbOrder> GettbOrderList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            return dal.GettbOrderList(pageIndex, pageSize, ref pageCount, ref totalPage);
        }
        public bool UpdatetbOrderDisplayIndex(int id, int value)
        {
            return dal.UpdatetbOrderDisplayIndex(id, value);
        }

        public bool UpdatetbOrderDisabled(int id, bool value)
        {
            return dal.UpdatetbOrderDisabled(id, value);

        } 
     }
}