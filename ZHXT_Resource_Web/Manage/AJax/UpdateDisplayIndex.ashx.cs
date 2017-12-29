using System; 
using System.Web; 
using ZHXT_Resource_Web.Common; 
using Newtonsoft.Json; 

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// UpdateDisplayIndex 的摘要说明
    /// </summary>
    public class UpdateDisplayIndex : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "");
            string type = context.Request["type"];
            string id = context.Request["id"];
            string value = context.Request["value"];
            switch (type)
            {
                case "year":
                    result.result = new Bll.tbYearBll().UpdatetbYearDisplayIndex(Convert.ToInt32(id),Convert.ToInt32(value));
                    break;
                case "coursetype":
                    result.result = new Bll.CourseTypeBll().UpdateCourseTypeDisplayIndex(Convert.ToInt32(id), Convert.ToInt32(value));
                    break;
                case "subject":
                    result.result = new Bll.SubjectBll().UpdateSubjectDisplayIndex(Convert.ToInt32(id), Convert.ToInt32(value));
                    break;
                case "grade":
                    result.result = new Bll.GradeBll().UpdateGradeDisplayIndex(Convert.ToInt32(id), Convert.ToInt32(value));
                    break;
                case "order":
                    result.result = new Bll.tbOrderBll().UpdatetbOrderDisplayIndex(Convert.ToInt32(id), Convert.ToInt32(value));
                    break;
                case "vloume":
                    result.result = new Bll.VloumeBll().UpdateVloumeDisplayIndex(Convert.ToInt32(id), Convert.ToInt32(value));
                    break;
                case "resourceclass":
                    result.result = new Bll.ResourceClassBll().UpdateResourceClassDisplayIndex(Convert.ToInt32(id), Convert.ToInt32(value));
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