using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// AdministratorsCheck 的摘要说明
    /// </summary>
    public class AdministratorsCheck : IHttpHandler
    {
       static string Administrators_Ids = System.Configuration.ConfigurationManager.AppSettings["Administrators_Ids"];

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"];
            ResultMessageJson result = new ResultMessageJson(false, "异常错误！");
            var Administrators_List= Administrators_Ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (Administrators_List.Contains(id))
            {
                result.result = true;
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