using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;
namespace ZHXT_Resource_Web.Dal
{
    public class ResourceRecordDal
    {
        public int AddResourceRecord(ResourceRecord model)
        {
            int id = 0;
            using (var db = SugarDao.GetInstance())
            {
                db.DisableInsertColumns = Global.DisableInsertColumns_ResourceRecord;
               
                id = Convert.ToInt32(db.Insert<ResourceRecord>(model));
            }
            return id;
        }

        /// <summary>
        /// 查询资源的 浏览量
        /// </summary>
        /// <param name="resourceRecordID"></param>
        /// <returns></returns>
        public int GetCountByResourceRecordID(int resourceRecordID, Common.ResourceRecordTypeEnum resourceRecordTypeEnum)
        {
            int count = 0;
            using (var db = SugarDao.GetInstance())
            {
                int type =(int)resourceRecordTypeEnum;
                count = db.Queryable<ResourceRecord>().Where(r => r.ResourceInfoID == resourceRecordID&&r.Type== type).Count();
            }
            return count;
        }

        public List<ResourceRecord> GetResourceRecordList(int resourceRecordID)
        {
            List<ResourceRecord> list = new List<ResourceRecord>();
            using (var db = SugarDao.GetInstance())
            {
                int type = (int)Common.ResourceRecordTypeEnum.Select;
                list = db.Queryable<ResourceRecord>().Where(r => r.ResourceInfoID == resourceRecordID && r.Type == type).OrderBy(r=>r.CreationDate,OrderByType.Desc).ToList();
            }
            return list;
        }


        public List<ResourceRecord> GetResourceRecordList(int resourceRecordID,int pageIndex, int pageSize, Common.ResourceRecordTypeEnum resourceRecordTypeEnum, ref int pageCount, ref int totalPage)
        {
            List<ResourceRecord> list = new List<ResourceRecord>();
            using (var db = SugarDao.GetInstance())
            {
                int type = (int)resourceRecordTypeEnum;
                list = db.Queryable<ResourceRecord>()
                    .JoinTable<User>((r, u) => r.OwnerID == u.ID)
                    .Where(r => r.Disabled == false
                && r.ResourceInfoID == resourceRecordID && r.Type == type).
                OrderBy(r => r.CreationDate, OrderByType.Desc)
                .Select("r.*,u.Name as UserName")
                    .ToPageList(pageIndex, pageSize, ref pageCount);
                totalPage = (pageCount + pageSize - 1) / pageSize;
            }
            return list;
        }
    }
}