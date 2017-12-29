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
    /// GetAllData 的摘要说明
    /// </summary>
    public class GetAllData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(true, "");
            string type = context.Request["type"];
            switch (type)
            {
                case "resourceclass":
                    result.list = new ResourceClassBll().GetResourceClassListAll();
                    break;
                case "year":
                    result.list = new tbYearBll().GetYearAll();
                    break;
                case "coursetype":
                    result.list = new CourseTypeBll().GetCourseTypeAll();
                    break;
                case "subject":
                    result.list = new SubjectBll().GetSubjectAll();
                    break;
                case "grade":
                    result.list = new GradeBll().GetGradeAll();
                    break;
                case "order":
                    result.list = new tbOrderBll().GetOrderAll();
                    break;
                case "vloume":
                    result.list = new VloumeBll().GetVloumeAll();
                    break;
                default:
                    break;
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