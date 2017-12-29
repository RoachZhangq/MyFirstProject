using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZHXT_Resource_Web.ModelsEx
{
    public class ZHXT_AdminInfo
    {
        public Guid adminID { get; set; }
        public Guid campusID { get; set; }
        public string campusName { get; set; }
        public string userRolesID { get; set; }
        public string typename { get; set; }
        public string userRolesCode { get; set; }
        public string areaid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string right { get; set; }
        public string trueName { get; set; }
        public string usercode { get; set; }
        public string kass { get; set; }
        public int id { get; set; }

        public string Bigregion { get; set; }
    }


    
}