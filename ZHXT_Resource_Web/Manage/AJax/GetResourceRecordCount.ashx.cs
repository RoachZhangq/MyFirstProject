using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// GetResourceRecordCount 的摘要说明
    /// </summary>
    public class GetResourceRecordCount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(true, "");
            result.message = "0";
            string id = context.Request["id"];
            if (!string.IsNullOrEmpty(id))
            {
                result.message = new Bll.ResourceRecordBll().GetCountByResourceRecordID(Convert.ToInt32(id), Common.ResourceRecordTypeEnum.Select).ToString();
                result.messageEx = new Bll.ResourceRecordBll().GetCountByResourceRecordID(Convert.ToInt32(id), Common.ResourceRecordTypeEnum.Download).ToString();
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