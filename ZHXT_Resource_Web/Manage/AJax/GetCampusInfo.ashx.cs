using System;
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
    /// GetCampusInfo 的摘要说明
    /// </summary>
    public class GetCampusInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "");
            string type = context.Request["type"];
            string campusId = context.Request["campusId"];

            if (type=="first")
            {
                result.list = new CampusBll().GetCampus_ZHXT_First();
                result.result = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(campusId))
                {
                    result.list = new CampusBll().GetCampus_ZHXT_Second(campusId);
                    result.result = true;
                }
               
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