using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script;
using System.Web.SessionState;
using ZHXT_Resource_Web.Common;
using Models;
using Newtonsoft.Json;
using SqlSugar;
using ZHXT_Resource_Web.ModelsEx;
using ZHXT_Resource_Web.Dao;
using ZHXT_Resource_Web.Bll;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// GetResourceClassAll 的摘要说明
    /// </summary>
    public class GetResourceClassAll : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, ""); 
            result.list=   new Bll.ResourceClassBll().GetResourceClassListAll();
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