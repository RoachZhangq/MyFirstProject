using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// GetResourceClassListByIDArray 的摘要说明
    /// </summary>
    public class GetResourceClassListByIDArray : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "");
            string resourceClassIDList = context.Request["resourceClassIDList"];

            string[] stringArr = resourceClassIDList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            result.list = new Bll.ResourceClassBll().GetResourceClassList(stringArr);
            result.result = true;

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