using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Common;
using SqlSugar;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// AddCourseTypeAreaInfo 的摘要说明
    /// </summary>
    public class AddCourseTypeAreaInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "保存失败！");

            string coursetypeId = context.Request["coursetypeId"];
            string orderId = context.Request["orderId"]; 
            string vloumeId = context.Request["vloumeId"];
            if (!string.IsNullOrEmpty(coursetypeId))
            {
                CourseTypeArea model = new CourseTypeArea();
                model.CourseTypeID = Convert.ToInt32(coursetypeId);
                if(!string.IsNullOrEmpty(orderId)) model.OrderID = Convert.ToInt32(orderId); 
                if (!string.IsNullOrEmpty(vloumeId)) model.VloumeID = Convert.ToInt32(vloumeId);
                model.Disabled = false;
                model.CreationDate = DateTime.Now;
                if(Check(coursetypeId, orderId, vloumeId))
                {
                    result.result = false;
                    result.message = "已存在相同的限制范围！";
                }
                else
                {
                    if (new Bll.CourseTypeAreaBll().AddCourseTypeArea(model))
                    {
                        result.result = true;
                    }
                }
               
            }
            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        private bool Check(string coursetypeId, string orderId, string vloumeId)
        {
            bool result = false;
            using (var db = Dao.SugarDao.GetInstance())
            {
                //检查是否有重复
                int _coursetypeId = Convert.ToInt32(coursetypeId);
                var queryable1 = db.Queryable<CourseTypeArea>().Where(o=>o.Disabled==false&&o.CourseTypeID== _coursetypeId);
                if (!string.IsNullOrEmpty(orderId))
                {
                    int _orderId = Convert.ToInt32(orderId);
                    queryable1.Where(o => o.OrderID == _orderId);
                }
                if (!string.IsNullOrEmpty(vloumeId))
                {
                    int _vloumeId = Convert.ToInt32(vloumeId);
                    queryable1.Where(o => o.VloumeID == _vloumeId);
                }
                int count = queryable1.Count();
                result = count > 0 ? true : false;
            }
            return result;
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