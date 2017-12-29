using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Bll;

namespace Models
{
    public partial class Roles
    {
       

        public List<RolesArea> RolesAreaList {
            get {
              if (this.ID == 0) return new List<RolesArea>();
              return  new RolesAreaBll().GetRolesAreaListByRolesId(this.ID);
            }
        }

        public string[] ResourceClassIDList
        {
            get
            { 
                int[] iNums = this.RolesAreaList.Select(o => o.ResourceClassID).ToArray();
                List<string> list = new List<string>();
                foreach (var item in iNums)
                {
                    list.Add(item.ToString());
                }
                return list.ToArray();
            }
        }
    }
}