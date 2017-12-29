using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using ZHXT_Resource_Web.ModelsEx;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// GetHomeChart 的摘要说明
    /// </summary>
    public class GetHomeChart : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(true, "");
            using (var db = SugarDao.GetInstance())
            {
                result.list = db.SqlQuery<HomeChart>("exec HomeChart ").ToList();

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