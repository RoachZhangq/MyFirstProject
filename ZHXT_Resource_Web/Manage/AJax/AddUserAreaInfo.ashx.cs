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
    /// AddUserAreaInfo 的摘要说明
    /// </summary>
    public class AddUserAreaInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "保存失败！");

            string userid = context.Request["userid"];
            string resourceClassId = context.Request["resourceClassId"];
            string courseTypeId = context.Request["courseTypeId"];
            string subjectId = context.Request["subjectId"];
            string gradeId = context.Request["gradeId"];

            try
            {
                if (!string.IsNullOrEmpty(userid)
                    && !string.IsNullOrEmpty(resourceClassId)
                     && !string.IsNullOrEmpty(courseTypeId)
                      && !string.IsNullOrEmpty(subjectId)
                       && !string.IsNullOrEmpty(gradeId))
                {
                    if(Check(Convert.ToInt32(userid), Convert.ToInt32(resourceClassId), Convert.ToInt32(courseTypeId), Convert.ToInt32(subjectId), Convert.ToInt32(gradeId)))
                    {
                        result.result = false;
                        result.message = "已存在相同的标签！";
                    }
                    else
                    {
                        using (var db = Dao.SugarDao.GetInstance())
                        {

                            //新增角色
                            db.DisableInsertColumns = Global.DisableInsertColumns_UserArea;
                            UserArea model = new UserArea();
                            model.UserID = Convert.ToInt32(userid);
                            model.ResourceClassID = Convert.ToInt32(resourceClassId);
                            model.CourseTypeID = Convert.ToInt32(courseTypeId);
                            model.SubjectID = Convert.ToInt32(subjectId);
                            model.GradeID = Convert.ToInt32(gradeId);
                            model.Disabled = false;
                            model.CreationDate = DateTime.Now;
                            db.Insert<UserArea>(model);

                            result.result = true;


                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                result.result = false;
                result.message = ex.Message;
            }

            context.Response.Write(JsonConvert.SerializeObject(result));
        }


        private bool Check(int userid,int resourceClassId,int courseTypeId,int subjectId,int gradeId)
        {
            bool result = false;
            using (var db = Dao.SugarDao.GetInstance())
            {
                //检查是否有重复
               int count= db.Queryable<UserArea>().Where(u => u.UserID == userid && u.ResourceClassID == resourceClassId &&
                u.CourseTypeID == courseTypeId && u.SubjectID == subjectId && u.GradeID == gradeId&&u.Disabled==false).Count() ;
                result = count > 0 ? true :false;
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