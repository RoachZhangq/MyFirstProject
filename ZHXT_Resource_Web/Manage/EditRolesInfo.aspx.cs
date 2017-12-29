using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZHXT_Resource_Web.Manage
{
    public partial class EditRolesInfo : System.Web.UI.Page
    {
        public string RoelsModel_Json { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string rolesId = Request["rolesId"];
            RoelsModel_Json=JsonConvert.SerializeObject(new Bll.RolesBll().GetRolesById(Convert.ToInt32(rolesId)));
        }
    }
}