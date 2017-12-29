using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZHXT_Resource_Web.Bll;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage
{
    public partial class OfficeWeb365 : System.Web.UI.Page
    {
        public string FileUrl { get; set; }
        public string OfficeWeb365_ID { get{ return OfficeWeb365_Common.OfficeWeb365_ID; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            User userModel = Session[Global.Session_ManagerUser] as User;
            if (userModel == null)
            {
                //检查 
                if (Request.Cookies["cookie_UserInfo"] != null)
                {
                    string _id = Request.Cookies["cookie_UserInfo"]["userId"];
                    userModel = new Bll.UserBll().GetUserById(Convert.ToInt32(_id));
                    Session[Global.Session_ManagerUser] = userModel;
                }
                else
                {
                    Response.Redirect("/Manage/Login");
                }

            }




            string id = Request["id"];
            var model = new ResourceInfoBll().GetResourceInfoById(Convert.ToInt32(id));
            //FileUrl = OfficeWeb365_Common.URLEncrypt("http://ot.zhihuixuetang.org:8765" + model.FileNamePath, "97047873", "69147113");

            FileUrl = OfficeWeb365_Common.URLEncrypt(Global.SiteUrl+model.FileNamePath, OfficeWeb365_Common.OfficeWeb365_IV, OfficeWeb365_Common.OfficeWeb365_Key);
           


            
            
        }
    }
}