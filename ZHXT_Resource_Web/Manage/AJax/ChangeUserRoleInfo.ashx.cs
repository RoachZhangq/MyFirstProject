using System;
using System.Web;
using ZHXT_Resource_Web.Common;
using Newtonsoft.Json;
using ZHXT_Resource_Web.Dao;
using SqlSugar;
using Models;

namespace ZHXT_Resource_Web.Manage.AJax
{
    /// <summary>
    /// ChangeUserRoleInfo 的摘要说明
    /// </summary>
    public class ChangeUserRoleInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "");
            string userId = context.Request["userId"];
            string rolesId = context.Request["rolesId"];
            string status = context.Request["status"];

            if (!string.IsNullOrEmpty(userId)
                && !string.IsNullOrEmpty(rolesId)
                && !string.IsNullOrEmpty(status))
            {
                bool isAdd = status == "true" ? true : false;

                using (var db = SugarDao.GetInstance())
                {

                    if (!isAdd)
                    {
                        #region 删除
                        int uid = Convert.ToInt32(userId);
                        int rid = Convert.ToInt32(rolesId);
                        db.Delete<UserRole>(u => u.UserID == uid && u.RolesID == rid&&u.WithUPC==false);
                        #endregion
                    }else
                    {
                        #region 新增
                        UserRole model = new UserRole();
                        model.UserID = Convert.ToInt32(userId);
                        model.RolesID = Convert.ToInt32(rolesId);
                        model.WithUPC = false;
                        model.Disabled = false;
                        model.CreationDate = DateTime.Now;
                        db.DisableInsertColumns = Global.DisableInsertColumns_UserRole;
                        db.Insert<UserRole>(model);
                        #endregion
                    }

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