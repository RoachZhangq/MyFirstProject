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
    public partial class VideoPage : System.Web.UI.Page
    {
        public string Url { get; set; }
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
            string FileUrl = OfficeWeb365_Common.URLEncrypt(model.FileNamePath, OfficeWeb365_Common.OfficeWeb365_IV, OfficeWeb365_Common.OfficeWeb365_Key);
            string extensionName = System.IO.Path.GetExtension(model.FileNamePath).ToLower();
            switch (extensionName)
            {
                case ".mp4":
                case ".ogg":
                case ".mp3":
                    Url = "/Manage/html5-video-player-mobile/index.html?url=" + FileUrl;
                    break;

                case ".flv":
                case ".swf":
                    Url = "/Manage/flowplayer-7.2.1/index.html?url=" + FileUrl;
                    break;
                default:
                    break;
            }
        }
    }
}