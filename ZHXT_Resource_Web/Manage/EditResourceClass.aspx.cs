using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZHXT_Resource_Web.Manage
{
    public partial class EditResourceClass : System.Web.UI.Page
    {
        public string ResourceClass_JSON { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request["id"];
            if (!string.IsNullOrEmpty(id))
            {
                ResourceClass_JSON=JsonConvert.SerializeObject( new Bll.ResourceClassBll().GetResourceClassById(Convert.ToInt32(id)));
            }
        }
    }
}