using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class CampusBll
    {
        CampusDal dal = new CampusDal();
        public int GetCampusId(string code)
        {
            return dal.GetCampusId(code);
        }

        public List<Campus> GetCampus_ZHXT_First()
        {
            return dal.GetCampus_ZHXT_First();
        }

        public List<Campus> GetCampus_ZHXT_Second(string campusId)
        {
            return dal.GetCampus_ZHXT_Second(campusId);
        }



    }
}