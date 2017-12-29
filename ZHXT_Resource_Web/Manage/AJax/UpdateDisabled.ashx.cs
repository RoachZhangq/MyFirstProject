using System;
using System.Web;
using ZHXT_Resource_Web.Common;
using Newtonsoft.Json;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// UpdateDisabled 的摘要说明
    /// </summary>
    public class UpdateDisabled : IHttpHandler
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
                    result.result = new Bll.tbYearBll().UpdatetbYearDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "coursetype":
                    result.result = new Bll.CourseTypeBll().UpdateCourseTypeDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "subject":
                    result.result = new Bll.SubjectBll().UpdateSubjectDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "grade":
                    result.result = new Bll.GradeBll().UpdateGradeDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "order":
                    result.result = new Bll.tbOrderBll().UpdatetbOrderDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "vloume":
                    result.result = new Bll.VloumeBll().UpdateVloumeDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "roles":
                   var rolesModel= new Bll.RolesBll().GetRolesById(Convert.ToInt32(id));
                    if (!string.IsNullOrEmpty(rolesModel.Code))
                    {
                        result.result = false;
                        result.message = "此角色是关联与UPC系统，不能删除！";
                    }else
                    {
                        result.result = new Bll.RolesBll().UpdateRolesDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                        if (Convert.ToBoolean(value))
                        {
                            //关联的用户也会同步删除此角色(Disabled=true 禁用)
                            new Bll.UserRoleBll().DeleteUserRoleByRolesId(Convert.ToInt32(id));
                        }
                    }
                    break;
                case "userarea":
                    result.result = new Bll.UserAreaBll().UpdateUserAreaDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "resourceclass":
                    result.result = new Bll.ResourceClassBll().UpdateResourceClassDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "resourceinfo":
                    result.result = new Bll.ResourceInfoBll().UpdateResourceInfoDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
                    break;
                case "coursetypearea":
                    result.result = new Bll.CourseTypeAreaBll().UpdateCourseTypeAreaDisabled(Convert.ToInt32(id), Convert.ToBoolean(value));
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