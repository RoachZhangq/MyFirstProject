using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class UserAreaBll
    {
        UserAreaDal dal = new UserAreaDal();
        public List<UserArea> GetUserAreaAll(int userId)
        {
            return dal.GetUserAreaAll(userId);
        }

        public List<UserArea> GetUserAreaList(int userId, int resourceClassID)
        {
            return dal.GetUserAreaList(userId, resourceClassID);
        }


        public List<UserArea> GetUserAreaList(int userId, int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            return dal.GetUserAreaList(userId,pageIndex, pageSize, ref pageCount, ref totalPage);

        }
        public bool UpdateUserAreaDisabled(int id, bool value)
        {
            return dal.UpdateUserAreaDisabled(id,value);

        }
        }
}