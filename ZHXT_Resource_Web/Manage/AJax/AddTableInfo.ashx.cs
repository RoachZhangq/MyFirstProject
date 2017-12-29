using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Newtonsoft.Json;
using ZHXT_Resource_Web.Common;
using SqlSugar;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// AddTableInfo 的摘要说明
    /// </summary>
    public class AddTableInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string name = context.Request["name"];
            string displayindex = context.Request["displayindex"] ?? "0";
            string type = context.Request["type"];

            ResultMessageJson result = new ResultMessageJson(false, "异常错误！");
            if (!string.IsNullOrEmpty(name))
            {

                try
                {
                    using (var db = Dao.SugarDao.GetInstance())
                    {
                        switch (type)
                        {
                            case "year":
                                tbYear tbYearModel = new tbYear() { Name = name, DisplayIndex = Convert.ToInt32(displayindex), CreationDate = DateTime.Now, Disabled = false };
                                db.DisableInsertColumns = Global.DisableInsertColumns_tbYear;
                                db.Insert<tbYear>(tbYearModel);
                                result.result = true;
                                break;
                            case "coursetype":
                                CourseType CourseTypeModel = new CourseType() { Name = name, DisplayIndex = Convert.ToInt32(displayindex), CreationDate = DateTime.Now, Disabled = false };
                                db.DisableInsertColumns = Global.DisableInsertColumns_CourseType;
                                db.Insert<CourseType>(CourseTypeModel);
                                result.result = true;
                                break;
                            case "subject":
                                Subject SubjectModel = new Subject() { Name = name, DisplayIndex = Convert.ToInt32(displayindex), CreationDate = DateTime.Now, Disabled = false };
                                db.DisableInsertColumns = Global.DisableInsertColumns_Subject;
                                db.Insert<Subject>(SubjectModel);
                                result.result = true;
                                break;
                            default:
                                result.result = false;
                                break;
                        }
                    }

                }
                catch (Exception)
                {
                    result.result = false;

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