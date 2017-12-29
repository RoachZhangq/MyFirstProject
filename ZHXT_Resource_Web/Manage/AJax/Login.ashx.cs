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
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "用户名或密码错误");
            string name = context.Request["name"];
            string pwd = context.Request["pwd"];
            try
            {
                pwd = pwd.Ext_GetStandMD5();
                User user = Common.Common.GetUser_UserWithUpcNothing(name, pwd);//检查下是不是跟upc无关的账户
                if (user == null) user = CheckUser(name, pwd);  //通过upc登录注册
                if (user != null)
                {
                    result.result = true;
                    context.Session[Global.Session_ManagerUser] = user;
                    //写入cookie
                    HttpCookie cookie = new HttpCookie("cookie_UserInfo");//初使化并设置Cookie的名称
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(0, 5, 0, 0, 0);//过期时间  5个小时
                    cookie.Expires = dt.Add(ts);//设置过期时间
                    cookie.Values.Add("userId", user.ID.ToString());
                    context.Response.AppendCookie(cookie);
                }
            }
            catch (Exception ex)
            {
                result.result = false;
                result.message = ex.Message;
            }


            context.Response.Write(JsonConvert.SerializeObject(result));
        }


        private User CheckUser(string code, string pwd)
        {

            string zhxtInterfaceUrl = System.Configuration.ConfigurationManager.AppSettings["zhxtInterfaceUrl"];
            zhxtInterfaceUrl += "?act=ZHXT_Resource_Login&hcode={0}&pwd={1}";
            string response = Common.HttpRequestJsonp.HttpRequestCommon.GetResponse(string.Format(zhxtInterfaceUrl, code, pwd));
            List<ZHXT_AdminInfo> list = JsonConvert.DeserializeObject<List<ZHXT_AdminInfo>>(response);
            User model = null;
            if (list != null)
            {
                if (list.Count > 0) model = GetAreaByUser(list[0]);
            }

            return model;
        }

        private User GetAreaByUser(ZHXT_AdminInfo admininfo)
        {
            // List<Roles> list = null;
            List<TeachGradeInfo> TeachGradeInfoList = null;
            User model = null;
            using (var db = SugarDao.GetInstance())
            {
                // list= db.Queryable<Roles>().Where(r => r.AutoGetArea == true&&r.Disabled==false).ToList();
                model = db.Queryable<User>().Where(u => u.UserCode == admininfo.usercode).FirstOrDefault();
                if (model == null)
                {
                    //注册用户
                    db.DisableInsertColumns = Global.DisableInsertColumns_User;
                    User user = new User();
                    user.Name = admininfo.trueName;
                    user.UserCode = admininfo.usercode;
                    user.Password = admininfo.password;
                    user.CreationDate = DateTime.Now;
                    db.Insert<User>(user);
                    model = db.Queryable<User>().Where(u => u.UserCode == admininfo.usercode).FirstOrDefault();
                }

                #region 更新角色
                List<Roles> roleModelList = new List<Roles>();
                if (!string.IsNullOrEmpty(admininfo.userRolesID))
                {
                    var arr = admininfo.userRolesID.Split(',');
                    //查询原来的全部角色(来自UPC)
                    var oldUserRoleList = db.Queryable<UserRole>()
                        .JoinTable<Roles>((u, r) => u.RolesID == r.ID)
                        .Where(u => u.UserID == model.ID && u.WithUPC == true)
                        .Select(" U.*,r.Code")
                        .ToList();

                    //删除不需要的
                    foreach (var oldModel in oldUserRoleList)
                    {
                        bool exists = ((System.Collections.IList)arr).Contains(oldModel.Code);
                        if (!exists)
                        {
                            db.Delete<UserRole, int>(oldModel.ID);
                        }
                    }


                    foreach (var userRolesID in arr)
                    {
                        bool exists = oldUserRoleList.Any(u => u.Code == userRolesID);
                        if (exists) continue;
                        //新增
                        var roleModel = db.Queryable<Roles>().Where(r => r.Code == userRolesID).FirstOrDefault();
                        if (roleModel != null)
                        {
                            UserRole userRole = new UserRole();
                            userRole.UserID = model.ID;
                            userRole.RolesID = roleModel.ID;
                            userRole.WithUPC = true;//标识 是通过upc获取来的权限
                            userRole.CreationDate = DateTime.Now;
                            db.DisableInsertColumns = Global.DisableInsertColumns_UserRole;
                            db.Insert<UserRole>(userRole);

                        }
                    }

                }
                //var roleModel = db.Queryable<Roles>().Where(r=>r.Code== admininfo.userRolesID.ToString()).FirstOrDefault();
                //if (roleModel != null)
                //{
                //   var userRoleModel= db.Queryable<UserRole>().Where(u => u.UserID == model.ID&&u.WithUPC==true).FirstOrDefault();
                //    //新
                //    UserRole userRole = new UserRole();
                //    userRole.UserID = model.ID;
                //    userRole.RolesID = roleModel.ID;
                //    userRole.WithUPC = true;//标识 是通过upc获取来的权限
                //    userRole.CreationDate = DateTime.Now;
                //    db.DisableInsertColumns = Global.DisableInsertColumns_UserRole;

                //    if (userRoleModel != null)
                //    {
                //        if(userRoleModel.RolesID!= roleModel.ID)
                //        {
                //            //角色更新了 删除掉原来的
                //            db.Delete<UserRole,int>(userRoleModel.ID);
                //            //创建新的角色
                //            db.Insert<UserRole>(userRole);
                //        }
                //    }else
                //    { 
                //        //创建新的角色 
                //        db.Insert<UserRole>(userRole);
                //    }

            }
            #endregion


            #region 作废 原本按upc来控制访问权限

            //if (list != null)
            //{
            //    //是否需要去upc获取
            //    bool autoGetArea = list.Any(l => l.Remark == admininfo.userRolesCode);

            //    if (autoGetArea)
            //    {
            //        string userRolesCode = admininfo.userRolesCode.ToUpper();
            //        string type = "";
            //        switch (userRolesCode)
            //        {
            //            case "TR":
            //                type = "teach";
            //                break;
            //            case "TRS":
            //                type = "teach";
            //                break;
            //            case "CR":
            //                type = "cr";
            //                break;
            //            case "CRS":
            //                type = "cr";
            //                break;
            //            default:
            //                break;
            //        }
            //        if (!string.IsNullOrEmpty(type))
            //        {
            //            //去查教室 或班主任的访问范围
            //            string zhxtInterfaceUrl = System.Configuration.ConfigurationManager.AppSettings["zhxtInterfaceUrl"];
            //            zhxtInterfaceUrl += "?act=ZHXT_Resource_GetTeachGradeInfo&type={0}&userid={1}";
            //            string response = Common.HttpRequestJsonp.HttpRequestCommon.GetResponse(string.Format(zhxtInterfaceUrl, type, admininfo.adminID));
            //            TeachGradeInfoList = JsonConvert.DeserializeObject<List<TeachGradeInfo>>(response);
            //            #region 更新UserArea
            //            List<UserArea> listUserArea = new List<UserArea>();
            //            foreach (var item in TeachGradeInfoList)
            //            {
            //                UserArea userArea = new UserArea();
            //                userArea.UserID = model.ID;
            //                userArea.CampusID = new ZHXT_Resource_Web.Bll.CampusBll().GetCampusId(admininfo.campusID.ToString());
            //                userArea.GradeID = new ZHXT_Resource_Web.Bll.GradeBll().GetGradeId(item.GradeName);
            //                userArea.SubjectID = item.SubjectID;
            //                userArea.CourseTypeID = new ZHXT_Resource_Web.Bll.CourseTypeBll().GetCourseTypeId(item.CourseTypeID);
            //                userArea.Disabled = false;
            //                userArea.CreationDate = DateTime.Now;
            //                if(userArea.GradeID>0&& userArea.CampusID>0 && userArea.CourseTypeID>0)
            //                     listUserArea.Add(userArea);
            //            } 
            //            if (listUserArea.Count > 0)
            //            {
            //                using (var db = SugarDao.GetInstance())
            //                {
            //                    //var oldUserAreaList=  db.Queryable<UserArea>().Where(u=>u.UserID==model.ID).ToList();
            //                    //if (oldUserAreaList!=null)
            //                    //{
            //                    ////取交集  （不做修改的）
            //                    //var jiaojiList = oldUserAreaList.Intersect(listUserArea).ToList();
            //                    ////取差集  (需要删的)
            //                    //var chaji1 = oldUserAreaList.Except(listUserArea).ToList();
            //                    ////反取差集 （需要新增的）
            //                    //var chaji2 = listUserArea.Except(oldUserAreaList).ToList();

            //                    // }
            //                    db.Delete<UserArea>(u => u.UserID == model.ID); 
            //                    db.DisableInsertColumns = Global.DisableInsertColumns_UserArea;
            //                    db.InsertRange<UserArea>(listUserArea);
            //                }
            //             }
            //            #endregion

            //        }
            //    }

            //}
            #endregion
            return model;
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