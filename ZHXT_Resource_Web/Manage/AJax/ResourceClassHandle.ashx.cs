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
    /// ResourceClassHandle 的摘要说明
    /// </summary>
    public class ResourceClassHandle : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "保存失败！");

            string type = context.Request["type"];
            string id = context.Request["id"];
            string name = context.Request["name"];
            string displayindex = context.Request["displayindex"] ?? "0";
            string year = context.Request["year"];
            string vloume = context.Request["vloume"];
            string order = context.Request["order"];
            string remark = context.Request["remark"];
            
            if (!string.IsNullOrEmpty(type)
                && !string.IsNullOrEmpty(name)
                &&!string.IsNullOrEmpty(remark))
            {
                using (var db = Dao.SugarDao.GetInstance())
                {
                    if (type.Equals("add"))
                    {
                        ResourceClass model = new ResourceClass();
                        model.Name = name;
                        model.Disabled = false;
                        model.DisplayIndex = Convert.ToInt32(displayindex); 
                        model.NotExistYear = Convert.ToBoolean(year) ? false : true;
                        model.NotExistVloume = Convert.ToBoolean(vloume) ? false : true;
                        model.NotExistOrder = Convert.ToBoolean(order) ? false : true;
                        model.NotExistCourseType = false;
                        model.NotExistSubject = false;
                        model.NotExistGrade = false;
                        model.Remark = remark; 
                        model.CreationDate = DateTime.Now;
                        db.DisableInsertColumns = Global.DisableInsertColumns_ResourceClass;
                        var ResourceClassID= db.Insert<ResourceClass>(model);
                        //给超级管理员 添加此类型权限
                        RolesArea rolesArea = new RolesArea();
                        rolesArea.RolesID = 43;
                        rolesArea.ResourceClassID = Convert.ToInt32(ResourceClassID);
                        rolesArea.AllowDownload = true;
                        rolesArea.AllowUpload = true;
                        rolesArea.Disabled = false;
                        rolesArea.CreationDate = DateTime.Now;
                        db.DisableInsertColumns = Global.DisableInsertColumns_RolesArea;
                        db.Insert<RolesArea>(rolesArea);
                        result.result = true;
                    }else if (type.Equals("edit"))
                    {
                        int _id = Convert.ToInt32(id);
                       bool _NotExistYear = Convert.ToBoolean(year) ? false : true;
                       bool _NotExistVloume = Convert.ToBoolean(vloume) ? false : true;
                       bool _NotExistOrder = Convert.ToBoolean(order) ? false : true;
                        int _DisplayIndex = Convert.ToInt32(displayindex);
                        db.Update<ResourceClass>(new {
                            Name =name,
                            DisplayIndex =displayindex,
                            NotExistYear= _NotExistYear,
                            NotExistVloume= _NotExistVloume,
                            NotExistOrder=_NotExistOrder ,
                            Remark = remark
                        },r=>r.ID== _id);
                        result.result = true;
                    }

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