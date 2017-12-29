using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class ResourceInfoBll
    {
        ResourceInfoDal dal = new ResourceInfoDal();
        public ResourceInfo GetResourceInfoById(int id)
        {
            return dal.GetResourceInfoById(id);
        }

        public bool UpdateResourceInfoDisabled(int id, bool value)
        {
            return dal.UpdateResourceInfoDisabled(id, value);

        }
   }
}