using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;
namespace ZHXT_Resource_Web.Dal
{
    public class CampusDal
    {
        /// <summary>
        /// 获取校区ID
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int GetCampusId(string code)
        {
            int id = -1;
            using (var db = SugarDao.GetInstance())
            {
                var model = db.Queryable<Campus>().Where(c => c.CampusID == code).FirstOrDefault();
                if (model != null)
                {
                    id = model.ID;
                }
            }

            return id;
        }

        /// <summary>
        /// 获取至慧学堂校区 第一级别
        /// </summary>
        /// <returns></returns>
        public List<Campus> GetCampus_ZHXT_First()
        {
            List<Campus> list = new List<Campus>();
            using (var db = SugarDao.GetInstance())
            {
                list=db.Queryable<Campus>().Where(c=>c.F_campusID_index==null&&c.Bigregion=="ZHXT"&&c.Disabled==false).OrderBy(c=>c.DisplayIndex,OrderByType.Desc).ToList();
            }
            return list;
        }
        /// <summary>
        ///  获取至慧学堂校区 第二级别
        /// </summary>
        /// <param name="campusId"></param>
        /// <returns></returns>
        public List<Campus> GetCampus_ZHXT_Second(string campusId)
        {
            List<Campus> list = new List<Campus>();
            using (var db = SugarDao.GetInstance())
            {
                list = db.Queryable<Campus>().Where(c => c.F_campusID_index == campusId && c.Bigregion == "ZHXT" && c.Disabled == false).OrderBy(c => c.DisplayIndex, OrderByType.Desc).ToList();
            }
            return list;
        }
    }
}