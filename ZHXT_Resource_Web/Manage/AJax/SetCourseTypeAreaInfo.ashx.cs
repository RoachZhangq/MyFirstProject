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
    /// SetCourseTypeAreaInfo 的摘要说明
    /// </summary>
    public class SetCourseTypeAreaInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "保存失败！");
            string option = context.Request["option"];
            string type = context.Request["type"];
            string id = context.Request["id"];
            string coursetypeId = context.Request["coursetypeId"];

            if (option == "add")
            {

                CourseTypeArea model = new CourseTypeArea();
                model.CourseTypeID = Convert.ToInt32(coursetypeId);
                if (type == "order") model.OrderID = Convert.ToInt32(id);
                else model.VloumeID = Convert.ToInt32(id);
                model.Disabled = false;
                model.CreationDate = DateTime.Now;
                if (new Bll.CourseTypeAreaBll().AddCourseTypeArea(model))
                {
                    result.result = true;
                }
            }
            else if (option == "delete")
            {
                using (var db = Dao.SugarDao.GetInstance())
                {
                    var _coursetypeId = Convert.ToInt32(coursetypeId);
                    var _id = Convert.ToInt32(id);
                    List<CourseTypeArea> list = new List<CourseTypeArea>();
                    if (type == "order")
                    {

                        list = db.Queryable<CourseTypeArea>().Where(o => o.CourseTypeID == _coursetypeId && o.OrderID == _id).ToList();
                    }
                    else
                    {
                        list = db.Queryable<CourseTypeArea>().Where(o => o.CourseTypeID == _coursetypeId && o.VloumeID == _id).ToList();
                    }
                    List<int> idlist = new List<int>();
                    foreach (var item in list)
                    {
                        idlist.Add(item.ID);
                    }
                    db.Delete<CourseTypeArea, int>(idlist.ToArray());
                    result.result = true;
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