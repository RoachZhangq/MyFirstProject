using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZHXT_Resource_Web.Bll;

namespace ZHXT_Resource_Web.Manage
{
    public partial class Index : System.Web.UI.Page
    {
        public string resourceClassId { get; set; }
        public string resourceClassModel_Json { get; set; }
        /// <summary>
        /// 允许上传
        /// </summary>
        public bool AllowUpload { get; set; }
        /// <summary>
        /// 允许下载
        /// </summary>
        public bool AllowDownload { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            resourceClassId = Request["resourceClassId"];
            resourceClassModel_Json = JsonConvert.SerializeObject(new Bll.ResourceClassBll().GetResourceClassById(Convert.ToInt32(resourceClassId)));

            //检查是否拥有上传、下载功能
            var user = Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {
                List<UserRole> userRoleList = new UserRoleBll().GetUserRoleListByUserId(user.ID);
                AllowUpload = CheckAllowUploadOrAllowDownload(userRoleList, resourceClassId, "Upload");
                AllowDownload = CheckAllowUploadOrAllowDownload(userRoleList, resourceClassId, "Download");
            }

        }

        /// <summary>
        /// 检查是否拥有上传、下载 权限
        /// </summary>
        /// <param name="userRoleList"></param>
        /// <returns></returns>
        private bool CheckAllowUploadOrAllowDownload(List<UserRole> userRoleList, string resourceClassId,string type)
        {
            bool result = false;
            List<string> UserRoleIDList = new List<string>();
            foreach (var item in userRoleList)
            {
                UserRoleIDList.Add(item.RolesID.ToString());
            }
            //查询角色
            List<Roles> rolesList = new RolesBll().GetRolesList(UserRoleIDList.ToArray());

            foreach (var item in rolesList)
            {
                foreach (var rolesArea in item.RolesAreaList)
                {
                    bool gist = type == "Upload" ? rolesArea.AllowUpload : rolesArea.AllowDownload;
                    if (gist) {
                        if(rolesArea.ResourceClassID.ToString()== resourceClassId)
                        {
                            result = true;
                            if (result) break;
                        }
                    }
                }
                if (result) break; 
            }
            return result;
        }
    }
}