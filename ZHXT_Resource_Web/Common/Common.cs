using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Bll;
using ZHXT_Resource_Web.Dao;
using SqlSugar;

namespace ZHXT_Resource_Web.Common
{
    public class Common
    {
        public static int AdminRolesId = 43;//管理员角色ID  
        /// <summary>
        /// 判断资源 用户是否有权限修改
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="resourceInfoId"></param>
        /// <returns></returns>
        public static bool CheckUser_UpdateResourceInfo(int userId,int resourceInfoId)
        {
            bool result = false;
            bool isadmin = IsAdminCheck(userId);
            if (isadmin) result = true;
            else{
                //判断当前资源 是否是当前用户上传的
                var resourceInfo= new ResourceInfoBll().GetResourceInfoById(resourceInfoId);

                double totalDays = (DateTime.Now - resourceInfo.CreationDate).TotalDays;
                //如果离创建时间超过七天 则不让修改
                if (resourceInfo.OwnerID == userId&& totalDays <= 7) result = true;
            }
            return result;
        }


        /// <summary>
        /// 检查是否是本系统的超级管理员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool IsAdminCheck(int userId)
        {
            bool result = false;
            using (var db = Dao.SugarDao.GetInstance())
            {
                List<UserRole> userRoleList = new UserRoleBll().GetUserRoleListByUserId(userId);
                result = userRoleList.Any(o => o.RolesID == AdminRolesId);
            }
            return result;
        }

        /// <summary>
        /// 检查是否有权限访问
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        public static bool CheckUrl(string filePath, bool isAdmin)
        {
            filePath = filePath.ToLower();
            bool result = false;
            if (isAdmin) result = true;
            else
            {
                var Admin_Url = new string[] {
                "/Manage/YearManage",
                "/Manage/CourseTypeManage",
                "/Manage/SubjectManage",
                "/Manage/ResourceClassManage",
                "/Manage/RolesManage",
                "/Manage/UserManage",
                "/Manage/AddCourseType",
                "/Manage/AddRecourceClass",
                "/Manage/AddRolesInfo",
                "/Manage/AddSubject",
                "/Manage/AddYear" };
                List<string> list = new List<string>();
                foreach (var item in Admin_Url)
                {
                    list.Add(item.ToLower());
                }
                result = list.Contains(filePath) ? false : true;
            }

            return result;
        }

        /// <summary>
        /// 检查用户是否不是upc关联的账户 （与UPC无关的账户）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static User GetUser_UserWithUpcNothing(string name ,string pwd)
        {
            User user = null;
            using (var db = SugarDao.GetInstance())
            {
                var usermodel=db.Queryable<User>().Where(u => u.UserCode == name&&u.Password==pwd).FirstOrDefault();
                if (usermodel == null) user = null;
                else
                {
                    int count = db.Queryable<UserWithUpcNothing>().Where(u => u.UserId == usermodel.ID).Count();
                    if (count > 0) user = usermodel;
                }
              
            }
            return user;
        }


    }
}