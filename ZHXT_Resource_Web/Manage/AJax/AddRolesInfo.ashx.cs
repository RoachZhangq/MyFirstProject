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
    /// AddRolesInfo 的摘要说明
    /// </summary>
    public class AddRolesInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "异常错误！");
            string data = context.Request["data"];
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    UpdateRolesInfo_Data model = JsonConvert.DeserializeObject<UpdateRolesInfo_Data>(data);
                    if (model != null)
                    {
                        using (var db = Dao.SugarDao.GetInstance())
                        {
                            //新增角色
                            db.DisableInsertColumns = Global.DisableInsertColumns_Roles;
                            Roles roles = new Roles();
                            roles.Name = model.name;
                            roles.Code = "";
                            roles.AutoGetArea = false;
                            roles.Remark = model.remark;
                            roles.Disabled = false;
                            roles.DisplayIndex = 0;
                            roles.CreationDate = DateTime.Now;

                            model.id=Convert.ToInt32( db.Insert<Roles>(roles));
                            //RoelsArea表
                         
                            List<RolesArea> RolesAreaList = new List<RolesArea>();
                            foreach (var item in model.UpdateRolesInfo_AreaList)
                            {
                                //允许访问
                                if (item.visit)
                                {
                                    RolesArea rolesArea = new RolesArea();
                                    rolesArea.RolesID = model.id;
                                    rolesArea.ResourceClassID = item.id;
                                    rolesArea.AllowUpload = item.upload;
                                    rolesArea.AllowDownload = item.download;
                                    rolesArea.Disabled = false;
                                    rolesArea.CreationDate = DateTime.Now;
                                    RolesAreaList.Add(rolesArea);
                                }
                            }
                            db.DisableInsertColumns = Global.DisableInsertColumns_RolesArea;
                            //批量插入
                            db.InsertRange(RolesAreaList);
                        }
                        result.result = true;
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}