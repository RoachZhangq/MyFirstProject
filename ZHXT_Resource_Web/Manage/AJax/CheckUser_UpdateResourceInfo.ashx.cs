using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// CheckUser_UpdateResourceInfo 的摘要说明
    /// </summary>
    public class CheckUser_UpdateResourceInfo : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string resourceInfoId = context.Request["resourceInfoId"];
            ResultMessageJson result = new ResultMessageJson(false, "");
            var user = context.Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {
                result.result= Common.Common.CheckUser_UpdateResourceInfo(user.ID,Convert.ToInt32(resourceInfoId));
            }
              
            context.Response.Write(JsonConvert.SerializeObject(result));
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