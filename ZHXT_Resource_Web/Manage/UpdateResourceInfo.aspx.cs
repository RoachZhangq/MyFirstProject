using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZHXT_Resource_Web.Manage
{
    public partial class UpdateResourceInfo : System.Web.UI.Page
    {

        public string resourceInfoObj { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string resourceInfoId = Request["resourceInfoId"];
            if (!string.IsNullOrEmpty(resourceInfoId))
            {
                ResourceInfo resourceInfo = new Bll.ResourceInfoBll().GetResourceInfoById(Convert.ToInt32(resourceInfoId));
                resourceInfoObj= JsonConvert.SerializeObject(resourceInfo);
            }
        }
    }
}