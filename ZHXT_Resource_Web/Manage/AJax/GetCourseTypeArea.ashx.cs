using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// GetCourseTypeArea 的摘要说明
    /// </summary>
    public class GetCourseTypeArea : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string courseTypeId = context.Request["courseTypeId"];
            ResultMessageJson result = new ResultMessageJson(true, "");

            if (!string.IsNullOrEmpty(courseTypeId))
            {
                result.list = new Bll.CourseTypeAreaBll().GetCourseTypeAreaByCourseTypeId(Convert.ToInt32(courseTypeId));

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