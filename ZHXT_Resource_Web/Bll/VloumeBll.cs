using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class VloumeBll
    {
        VloumeDal dal = new VloumeDal();
        public List<Vloume> GetVloumeAll()
        {
            return dal.GetVloumeAll();
        }

        public Vloume GetVloumeById(int id)
        {
            return dal.GetVloumeById(id);
        }

        public List<Vloume> GetVloumeList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage)
        {
            return dal.GetVloumeList(pageIndex, pageSize, ref pageCount, ref totalPage);
        }


        public bool UpdateVloumeDisplayIndex(int id, int value)
        {
            return dal.UpdateVloumeDisplayIndex(id, value);

        }
        public bool UpdateVloumeDisabled(int id, bool value)
        {
            return dal.UpdateVloumeDisabled(id, value);
        }
        }
}