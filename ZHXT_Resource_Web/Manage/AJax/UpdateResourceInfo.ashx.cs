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

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// UpdateResourceInfo 的摘要说明
    /// </summary>
    public class UpdateResourceInfo : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "保存失败！");
            var user = context.Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {
                string resourceInfoId = context.Request["resourceInfoId"];
                string NameVal = context.Request["NameVal"];
                string ResourceClassVal = context.Request["ResourceClassVal"];
                string GradeVal = context.Request["GradeVal"];
                string SubjectVal = context.Request["SubjectVal"];
                string CourseTypeVal = context.Request["CourseTypeVal"];
                string YearVal = context.Request["YearVal"];
                string OrderVal = context.Request["OrderVal"];
                string VloumeVal = context.Request["VloumeVal"];
                string RemarkVal = context.Request["RemarkVal"];
                string FileName = context.Request["FileName"];
                string NewFileName = context.Request["NewFileName"];
                string FileContentLength = context.Request["FileContentLength"];
                try
                {
                    using (var db = SugarDao.GetInstance())
                    {
                        db.DisableUpdateColumns = Global.DisableInsertColumns_ResourceInfo;
                        ResourceInfo model = new ResourceInfo();
                        model.Name = NameVal;
                        model.ResourceClassID = Convert.ToInt32(ResourceClassVal);
                        if (GradeVal != "0") model.GradeID = Convert.ToInt32(GradeVal); else model.GradeID = null;
                        if (SubjectVal != "0") model.SubjectID = Convert.ToInt32(SubjectVal); else model.SubjectID = null;
                        if (CourseTypeVal != "0") model.CourseTypeID = Convert.ToInt32(CourseTypeVal); else model.CourseTypeID = null;
                        if (YearVal != "0") model.tbYearID = Convert.ToInt32(YearVal); else model.tbYearID = null;
                        if (OrderVal != "0") model.tbOrderID = Convert.ToInt32(OrderVal); else model.tbOrderID = null;
                        if (VloumeVal != "0") model.VloumeID = Convert.ToInt32(VloumeVal); else model.VloumeID = null;
                        model.FileType = System.IO.Path.GetExtension(FileName);
                        model.FileName = FileName;
                        model.FileNamePath = NewFileName.IndexOf("/Manage/ResourceInfo_Uploads") > -1 ? NewFileName : "/Manage/ResourceInfo_Uploads/" + NewFileName; 
                        model.FileContentLength = FileContentLength;
                        model.Remark = RemarkVal;
                        int _resourceInfoId = Convert.ToInt32(resourceInfoId);

                        var oldModel= new Bll.ResourceInfoBll().GetResourceInfoById(_resourceInfoId);
                        model.CreationDate = oldModel.CreationDate;
                        model.Disabled = oldModel.Disabled;
                        model.OwnerID = oldModel.OwnerID;

                        db.Update<ResourceInfo>(model, r => r.ID == _resourceInfoId);
                    }
                    result.result = true;
                }
                catch (Exception ex)
                {
                    result.result = false;
                    result.message = ex.Message;
                    throw;
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