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
    /// GetAllDataByPage 的摘要说明
    /// </summary>
    public class GetAllDataByPage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJsonEx result = new ResultMessageJsonEx();
            string type = context.Request["type"];
            string id = context.Request["id"];
            string likeName = context.Request["likeName"];
            bool disabled = Convert.ToBoolean(context.Request["disabled"] ?? "false");
            string resourceRecordTypeEnum = context.Request["resourceRecordTypeEnum"] ?? "select";//查询资源的 浏或下载

            result.pageIndex =Convert.ToInt32 (context.Request["pageIndex"]??"1");
            result.pageSize = Convert.ToInt32(context.Request["pageSize"] ?? "10");
            int pageCount = 0;
            int totalPage = 0;
            try
            {
                switch (type)
                {
                    case "year":
                        result.list =new Bll.tbYearBll().GettbYearList(result.pageIndex, result.pageSize, ref pageCount, ref totalPage, disabled);
                        break;
                    case "coursetype":
                        result.list = new Bll.CourseTypeBll().GetCourseTypeList(result.pageIndex, result.pageSize, ref pageCount, ref totalPage, disabled);
                        break;
                    case "subject":
                        result.list = new Bll.SubjectBll().GetSubjectList(result.pageIndex, result.pageSize, ref pageCount, ref totalPage, disabled);
                        break;
                    case "grade":
                        result.list = new Bll.GradeBll().GetGradeList(result.pageIndex, result.pageSize, ref pageCount, ref totalPage);
                        break;
                    case "order":
                        result.list = new Bll.tbOrderBll().GettbOrderList(result.pageIndex, result.pageSize, ref pageCount, ref totalPage);
                        break;
                    case "vloume":
                        result.list = new Bll.VloumeBll().GetVloumeList(result.pageIndex, result.pageSize, ref pageCount, ref totalPage);
                        break;
                    case "roles":
                        result.list = new Bll.RolesBll().GetRolesList(likeName,result.pageIndex, result.pageSize, ref pageCount, ref totalPage);
                        break;
                    case "user":
                        result.list = new Bll.UserBll().GetUserList(likeName,result.pageIndex, result.pageSize, ref pageCount, ref totalPage);
                        break;
                    case "resourceclass":
                        result.list = new Bll.ResourceClassBll().GetResourceClassList(result.pageIndex, result.pageSize, ref pageCount, ref totalPage, disabled);
                        break;
                    case "resourcerecord":
                        ResourceRecordTypeEnum _resourceRecordTypeEnum = resourceRecordTypeEnum == "select" ? ResourceRecordTypeEnum.Select : ResourceRecordTypeEnum.Download;
                        result.list = new Bll.ResourceRecordBll().GetResourceRecordList(Convert.ToInt32(id),result.pageIndex, result.pageSize, _resourceRecordTypeEnum, ref pageCount, ref totalPage);
                        break;
                    default:
                        break;
                }
                result.result = true;
                result.pageCount = pageCount;
                result.totalPage = totalPage;
            }
            catch (Exception ex)
            {
                result.result = false;
                result.message = ex.Message;
                throw;
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