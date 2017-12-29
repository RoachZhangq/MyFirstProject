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
    /// UpdateRolesInfo 的摘要说明
    /// </summary>
    public class UpdateRolesInfo : IHttpHandler
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
                        if (model.id == 43)
                        {
                            result.message = "此角色禁止修改！";

                        }else
                        {
                            using (var db = Dao.SugarDao.GetInstance())
                            {
                                //更新名称和备注
                                db.Update<Roles>(new { Name = model.name, Remark = model.remark }, r => r.ID == model.id);
                                //更新RoelsArea表
                                db.Delete<RolesArea>(r => r.RolesID == model.id);//删除旧的权限
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
                                result.result = true;
                            }
                       
                        }
                       
                    }

                }

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

    public class UpdateRolesInfo_Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string remark { get; set; }
        public List<UpdateRolesInfo_Area> UpdateRolesInfo_AreaList { get; set; }
    }

    public class UpdateRolesInfo_Area
    {
        public int id { get; set; }
        public bool visit { get; set; }
        public bool upload { get; set; }
        public bool download { get; set; }
    }
}