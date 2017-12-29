using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZHXT_Resource_Web.Manage
{
    public partial class EditUserRolesInfo : System.Web.UI.Page
    {
        public string userModel_Json { get; set; } 

        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Request["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                 userModel_Json = JsonConvert.SerializeObject(new Bll.UserBll().GetUserById(Convert.ToInt32(userId))); 
      
            }
         }
    }
}