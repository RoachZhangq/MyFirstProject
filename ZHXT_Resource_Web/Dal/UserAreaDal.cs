using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Dal
{
    public class UserAreaDal
    {

        public List<UserArea> GetUserAreaAll(int userId)
        {
            List<UserArea> list = new List<UserArea>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<UserArea>().Where(y => y.Disabled == false&&y.UserID== userId).ToList();
            }
            return list;
        }

        public List<UserArea> GetUserAreaList(int userId,int resourceClassID)
        {
            List<UserArea> list = new List<UserArea>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<UserArea>().Where(y => y.Disabled == false && y.UserID == userId&&y.ResourceClassID== resourceClassID).ToList();
            }
            return list;
        }


        public List<UserArea> GetUserAreaList(int userId,int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            List<UserArea> list = new List<UserArea>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<UserArea>()
                    .JoinTable<ResourceClass>((o,rc)=>o.ResourceClassID==rc.ID)
                    .JoinTable<Grade>((o, g) => o.GradeID == g.ID)
                    .JoinTable<Subject>((o, s) => o.SubjectID == s.ID)
                    .JoinTable<CourseType>((o, c) => o.CourseTypeID == c.ID)
                    .Where<ResourceClass>((o, rc) => rc.Disabled==false)
                    .Where<Grade>((o, g) => g.Disabled == false)
                    .Where<Subject>((o, s) => s.Disabled == false)
                    .Where<CourseType>((o, c) => c.Disabled == false) 
                    .Where(o => o.Disabled == false&&o.UserID== userId).OrderBy(o => o.ID, OrderByType.Desc)
                    .Select("o.*,rc.Name ResourceClassName,g.Name GradeName,s.Name SubjectName,c.Name CourseTypeName")
                    .ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }

        public bool UpdateUserAreaDisabled(int id, bool value)
        {
            bool result = false;
            using (var db = SugarDao.GetInstance())
            {
                result = db.Update<UserArea>(new { Disabled = value }, o => o.ID == id);
            }
            return result;
        }

    }
}