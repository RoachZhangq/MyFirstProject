using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage.AJax.Other
{
    /// <summary>
    /// UnDes 的摘要说明
    /// </summary>
    public class UnDes : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string path = context.Request["path"];
            string message = "";
            var user = context.Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {
                message = OfficeWeb365_Common.URLDecrypt(path, OfficeWeb365_Common.OfficeWeb365_IV, OfficeWeb365_Common.OfficeWeb365_Key);
                //message = "/Manage/ResourceInfo_Uploads/" + message;
            }


            //ResultMessageJson result = new ResultMessageJson(false, "");
            //context.Response.Write(JsonConvert.SerializeObject(result));
            context.Response.Write(message);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}