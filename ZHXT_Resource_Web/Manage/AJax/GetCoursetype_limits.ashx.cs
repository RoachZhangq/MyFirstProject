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
    /// GetCoursetype_limits 的摘要说明
    /// </summary>
    public class GetCoursetype_limits : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(true, "");
            string resourceClassID = context.Request["resourceClassID"];
            var user = context.Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {
                var userAreaList=  new Bll.UserAreaBll().GetUserAreaAll(user.ID);
                if (userAreaList.Count == 0)//没有限制
                {
                    //查询全部Coursetype
                    result.list = new CourseTypeBll().GetCourseTypeAll();
                }else
                {
                    int _resourceClassID = Convert.ToInt32(resourceClassID);
                    userAreaList = userAreaList.Where(u => u.ResourceClassID == _resourceClassID).ToList();
                    //按权限限制的查询
                    List<string> courseTypeIDList = new List<string>();
                    foreach (var item in userAreaList)
                    {
                        courseTypeIDList.Add(item.CourseTypeID.ToString());
                    }
                    result.list=  new Bll.CourseTypeBll().GetCourseTypeListByIdArr(courseTypeIDList.ToArray());
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