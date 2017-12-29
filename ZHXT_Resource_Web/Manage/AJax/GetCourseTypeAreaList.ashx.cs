using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// GetCourseTypeAreaList 的摘要说明
    /// </summary>
    public class GetCourseTypeAreaList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJsonEx result = new ResultMessageJsonEx();
            string courseTypeId = context.Request["courseTypeId"];
            if (!string.IsNullOrEmpty(courseTypeId))
            {
                result.pageIndex = Convert.ToInt32(context.Request["pageIndex"] ?? "1");
                result.pageSize = Convert.ToInt32(context.Request["pageSize"] ?? "10");
                int pageCount = 0;
                int totalPage = 0;

                result.list = new Bll.CourseTypeAreaBll().GetCourseTypeAreaList(Convert.ToInt32(courseTypeId), result.pageIndex, result.pageSize, ref pageCount, ref totalPage);
                result.result = true;
                result.pageCount = pageCount;
                result.totalPage = totalPage;
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