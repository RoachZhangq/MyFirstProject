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
    /// GetResourceClassByUser 的摘要说明
    /// </summary>
    public class GetResourceClassByUser : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "");
            var user= context.Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {
                List<UserRole> userRoleList = new UserRoleBll().GetUserRoleListByUserId(user.ID);
                List<string> UserRoleIDList = new List<string>();
                foreach (var item in userRoleList)
                {
                    UserRoleIDList.Add(item.RolesID.ToString());
                }
                List<Roles> rolesList = new RolesBll().GetRolesList(UserRoleIDList.ToArray());

                List<string> ResourceClassIDList = new List<string>();
                //string ResourceClassIDList = "";
                foreach (var item in rolesList)
                {
                    foreach (var classid in item.ResourceClassIDList)
                    {
                        ResourceClassIDList.Add(classid);
                    }  
                }
                // rolesModel.ResourceClassIDList = rolesModel.ResourceClassIDList == null ? "" : rolesModel.ResourceClassIDList; 
                // var intArray = ResourceClassIDList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var intArray = ResourceClassIDList.ToArray();
                List < ResourceClass > resourceClassList= new ZHXT_Resource_Web.Bll.ResourceClassBll().GetResourceClassList(intArray);
                result.result = true;
                result.list = resourceClassList;
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