using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ZHXT_Resource_Web
{
    public class Global : HttpApplication
    {    





        //后台用户Session名
        public readonly static string Session_ManagerUser = "Session_ManagerUser";
        //前台用户Session名
        public readonly static string Session_ClientUser = "Session_ClientUser";
        //站点url
        public static string SiteUrl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];

        //设置各个表不插入(或更新)的列  (ModelEx文件夹 对应部分类中的扩展字段)
        public readonly static string[] DisableInsertColumns_Campus = new string[] {   };
        public readonly static string[] DisableInsertColumns_CourseType = new string[] {   };
        public readonly static string[] DisableInsertColumns_Grade = new string[] {   };
        public readonly static string[] DisableInsertColumns_ResourceClass = new string[] {   };
        public readonly static string[] DisableInsertColumns_ResourceInfo = new string[] { "SubjectName", "CourseTypeName", "GradeName", "CreationDateStr" , "UserName" , "PrintUrl" };
        public readonly static string[] DisableInsertColumns_ResourceRecord = new string[] { "UserName", "CreationDateStr" };
        public readonly static string[] DisableInsertColumns_Roles = new string[] { "ResourceClassIDList", "RolesAreaList" };
        public readonly static string[] DisableInsertColumns_RolesArea = new string[] { "ResourceClassName" };
        public readonly static string[] DisableInsertColumns_Subject = new string[] {   };
        public readonly static string[] DisableInsertColumns_tbOrder = new string[] {   };
        public readonly static string[] DisableInsertColumns_tbYear = new string[] {   };
        public readonly static string[] DisableInsertColumns_User = new string[] { "CreationDateStr" };
        public readonly static string[] DisableInsertColumns_UserArea = new string[] { "ResourceClassName", "GradeName", "SubjectName", "CourseTypeName" };
        public readonly static string[] DisableInsertColumns_UserRole = new string[] { "Code" };
        public readonly static string[] DisableInsertColumns_CourseTypeArea = new string[] { "OrderName", "VloumeName" };

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
           
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SESSIONID";

                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }

            }
            catch (Exception)
            {
            }


        }
        void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (cookie == null)
            {
                HttpCookie cookie1 = new HttpCookie(cookie_name, cookie_value);
                Response.Cookies.Add(cookie1);

            }
            else
            {
                cookie.Value = cookie_value;
                HttpContext.Current.Request.Cookies.Set(cookie);
            }
        }
    }
}