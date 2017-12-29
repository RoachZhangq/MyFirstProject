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
    /// GetAllDataEx 的摘要说明
    /// </summary>
    public class GetAllDataEx : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(true, "");
            string type = context.Request["type"];
            string coursetypeId = context.Request["coursetypeId"];
            string subjectId = context.Request["subjectId"];
            var user = context.Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {
                List<UserArea> UserAreaList = new UserAreaBll().GetUserAreaAll(user.ID);
                switch (type)
                {
                    case "year":
                        result.list = new tbYearBll().GetYearAll();
                        break;
                    case "coursetype":
                        List<CourseType> list = new CourseTypeBll().GetCourseTypeAll();
                        List<CourseType> listNew = new List<CourseType>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if(UserAreaList.Count==0)
                            {
                                listNew.Add(list[i]);
                                continue;
                            }
                            bool isHas = false;
                            foreach (var item in UserAreaList)
                            {
                                if (item.CourseTypeID == list[i].ID)
                                {
                                    isHas = true;
                                    break;
                                }
                            }
                            if (isHas) listNew.Add(list[i]);
                        }
                        result.list = listNew;
                        break;
                    case "subject":
                       
                        if (!string.IsNullOrEmpty(coursetypeId)) {
                            var list2 = new SubjectBll().GetSubjectAll();
                            List<Subject> listNew2 = new List<Subject>();
                            for (int i = 0; i < list2.Count; i++)
                            {
                                if (UserAreaList.Count == 0)
                                {
                                    listNew2.Add(list2[i]);
                                    continue;
                                }
                                bool isHas = false;
                                foreach (var item in UserAreaList)
                                {
                                    if (coursetypeId != "0")
                                    {
                                        if (item.CourseTypeID == Convert.ToInt32(coursetypeId) && list2[i].ID == item.SubjectID)
                                        {
                                            isHas = true;
                                            break;
                                        }
                                    }else
                                    {
                                        if ( list2[i].ID == item.SubjectID)
                                        {
                                            isHas = true;
                                            break;
                                        }
                                    }
                                   
                                }
                                if (isHas) listNew2.Add(list2[i]);
                            }
                            result.list = listNew2;
                        }
                     
                        break;
                    case "grade":
                        if (!string.IsNullOrEmpty(coursetypeId)&&!string.IsNullOrEmpty(subjectId))
                        {
                            var list3= new GradeBll().GetGradeAll();
                            List<Grade> listNew3 = new List<Grade>();
                            for (int i = 0; i < list3.Count; i++)
                            {
                                if (UserAreaList.Count == 0)
                                {
                                    listNew3.Add(list3[i]);
                                    continue;
                                }
                                bool isHas = false;
                                foreach (var item in UserAreaList)
                                {
                                    bool condition = coursetypeId == "0" ? true : item.CourseTypeID == Convert.ToInt32(coursetypeId);
                                    if (condition)
                                    {
                                        if (subjectId != "0")
                                        {
                                            if (item.SubjectID == Convert.ToInt32(subjectId) && list3[i].ID == item.GradeID)
                                            {
                                                isHas = true;
                                                break;
                                            }
                                        }else
                                        {
                                            if (list3[i].ID == item.GradeID)
                                            {
                                                isHas = true;
                                                break;
                                            }

                                        }
                                      
                                   
                                    }
                                }
                                if (isHas) listNew3.Add(list3[i]);
                            }
                            result.list = listNew3;
                        }
                           
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