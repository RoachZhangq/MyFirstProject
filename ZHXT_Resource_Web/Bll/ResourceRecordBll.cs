using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;
namespace ZHXT_Resource_Web.Bll
{
    public class ResourceRecordBll
    {
        ResourceRecordDal dal = new ResourceRecordDal();
        public int AddResourceRecord(ResourceRecord model)
        {
            return dal.AddResourceRecord(model);
        }
        public int GetCountByResourceRecordID(int resourceRecordID, Common.ResourceRecordTypeEnum resourceRecordTypeEnum)
        {
            return dal.GetCountByResourceRecordID(resourceRecordID, resourceRecordTypeEnum);
        }

        public List<ResourceRecord> GetResourceRecordList(int resourceRecordID)
        {
            return dal.GetResourceRecordList(resourceRecordID); 
        }
        public List<ResourceRecord> GetResourceRecordList(int resourceRecordID, int pageIndex, int pageSize, Common.ResourceRecordTypeEnum resourceRecordTypeEnum, ref int pageCount, ref int totalPage)
        {
            return dal.GetResourceRecordList(resourceRecordID, pageIndex,  pageSize, resourceRecordTypeEnum, ref  pageCount, ref  totalPage);
        }

        }
}