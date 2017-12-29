using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class UserDal
    {

        public List<User> GetUserList(string likeName, int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            List<User> list = new List<User>();
            using (var db = SugarDao.GetInstance())
            {
                var queryable1 = db.Queryable<User>().Where(o => o.Disabled == false);
                if (!string.IsNullOrEmpty(likeName))
                {
                    queryable1.Where(o=>o.Name.Contains(likeName)||o.UserCode.Contains(likeName));
                }
                queryable1.OrderBy(o => o.ID, OrderByType.Desc);
                list = queryable1.ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }


        public User GetUserById(int userId)
        {
            User model = null;
            using (var db = SugarDao.GetInstance())
            {
                model = db.Queryable<User>().Where(o => o.ID == userId).FirstOrDefault();
            }
            return model;
        }
    }
}