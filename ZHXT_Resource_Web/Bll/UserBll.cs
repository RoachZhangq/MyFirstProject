using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class UserBll
    {

        UserDal dal = new UserDal();
        public List<User> GetUserList(string likeName, int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            return dal.GetUserList(likeName,pageIndex, pageSize, ref pageCount, ref totalPage);

        }
        public User GetUserById(int userId)
        {
            return dal.GetUserById(userId);
        }
    }
}