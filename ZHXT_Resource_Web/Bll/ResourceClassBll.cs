using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class ResourceClassBll
    {
        ResourceClassDal dal = new ResourceClassDal();
        public List<ResourceClass> GetResourceClassList(string[] intArray)
        {
            return dal.GetResourceClassList(intArray);
        }



        public ResourceClass GetResourceClassById(int id)
        {
            return dal.GetResourceClassById(id);
        }


        public List<ResourceClass> GetResourceClassListAll()
        {
            return dal.GetResourceClassListAll();
        }
        public List<ResourceClass> GetResourceClassList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage,bool disabled = false)
        {
            return dal.GetResourceClassList(pageIndex, pageSize, ref pageCount, ref totalPage, disabled);
        }
        public bool UpdateResourceClassDisplayIndex(int id, int value)
        {

            return dal.UpdateResourceClassDisplayIndex(id, value);
        }
        public bool UpdateResourceClassDisabled(int id, bool value)
        {
            return dal.UpdateResourceClassDisabled(id, value);
        }
    }
}