using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SqlSugar;
using ZHXT_Resource_Web.Bll;

namespace ZHXT_Resource_Web.Manage
{
    public partial class ManageMaster : System.Web.UI.MasterPage
    {
        public string UserJson { get; set; }
        public string resourceClassId { get; set; }

        public bool IsAdmin { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            User userModel = Session[Global.Session_ManagerUser] as User;
            if (userModel == null)
            {
                //检查 
                if (Request.Cookies["cookie_UserInfo"] != null)
                {
                    string id = Request.Cookies["cookie_UserInfo"]["userId"];
                    userModel = new Bll.UserBll().GetUserById(Convert.ToInt32(id));
                    Session[Global.Session_ManagerUser] = userModel; 
                    Response.Write("<script>location.reload();</script>");
                }
                else
                {
                    Response.Redirect("/Manage/Login");
                }
            }
            UserJson = JsonConvert.SerializeObject(userModel);
            resourceClassId = Request["resourceClassId"];
            IsAdmin =Common.Common.IsAdminCheck(userModel.ID);

            string filePath = Request.FilePath;
            if(!Common.Common.CheckUrl(filePath, IsAdmin))
            {
                Response.Write("<script>alert('您没有权限访问该页面！');window.location.href ='/Manage/Home'</script>");
            }
        }

      
    }
}