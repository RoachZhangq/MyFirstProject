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
    /// GetResourceInfoList 的摘要说明
    /// </summary>
    public class GetResourceInfoList : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ResultMessageJson result = new ResultMessageJson(false, "");
            string year = context.Request["year"];
            string courseType = context.Request["courseType"];
            string subject = context.Request["subject"];
            string grade = context.Request["grade"];
            string vloume = context.Request["vloume"];
            string order = context.Request["order"];

            string resourceClassId = context.Request["resourceClassId"];
            string name = context.Request["name"];

            string pageindex = context.Request["pageindex"]??"1";
            string pagesize = context.Request["pagesize"]??"10";
            var user = context.Session[Global.Session_ManagerUser] as User;
            if (user != null)
            {

                int pagecount = 0;
                int totalPageNum = 0;
                result.list=  GetResourceInfoListByUser(Convert.ToInt32(year),Convert.ToInt32(order), 
                    Convert.ToInt32(resourceClassId), Convert.ToInt32(pageindex), Convert.ToInt32(pagesize),
                    Convert.ToInt32(courseType), Convert.ToInt32(subject),Convert.ToInt32(vloume), Convert.ToInt32(grade),
                    user.ID, name, ref pagecount,ref totalPageNum);
                result.message = pageindex;
                result.messageEx = totalPageNum.ToString();
            }
              context.Response.Write(JsonConvert.SerializeObject(result));
        }
        /// <summary>
        /// 获取权限范围
        /// </summary>
        /// <param name="courseType"></param>
        /// <param name="subject"></param>
        /// <param name="grade"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<UserArea> GetUserAreaList(int courseType,int subject,int grade,int userId)
        {
            List<UserArea> list = new List<UserArea>();
            using (var db = SugarDao.GetInstance())
            {
                var queryable1 = db.Queryable<UserArea>().Where(q=>q.UserID==userId);
                if (courseType > 0) queryable1.Where(q=>q.CourseTypeID== courseType);
                if (subject > 0) queryable1.Where(q => q.SubjectID == subject);
                if (grade > 0) queryable1.Where(q => q.GradeID == grade);
                list = queryable1.ToList();
            }
           return list;
        }


        private List<V_ResourceInfo_Valid> GetResourceInfoListByUser(int yearId,int orderid,int resourceClassId,
            int  pageIndex, int pageSize, int courseTypeId, int subjectId, int vloume, int gradeId, 
            int userId,string name, ref int pageCount, ref int totalPageNum)
        {
            List<V_ResourceInfo_Valid> list = new List<V_ResourceInfo_Valid>();
            //访问权限
            List<UserArea> userAreaList = GetUserAreaList(courseTypeId, subjectId, gradeId, userId);

            using (var db = SugarDao.GetInstance())
            { 
                string where = "";
                if (userAreaList.Count > 0)
                {
                    for (int i = 0; i < userAreaList.Count; i++)
                    {
                        var item = userAreaList[i];
                        string orStr = i == userAreaList.Count - 1 ? "" : " OR ";
                        string yearWhere = yearId > 0 ? " r.tbYearID= " + yearId + "   and" : "";
                        string orderWhere = orderid > 0 ? " r.tbOrderID=" + orderid + "  and" : "";
                        string vloumeWhere = vloume > 0 ? " r.VloumeID=" + vloume + "  and" : "";
                        where += " ( " + yearWhere+ vloumeWhere + orderWhere + " r.CourseTypeID= " + item.CourseTypeID + " and r.SubjectID=" + item.SubjectID + "  and r.GradeID=" + item.GradeID + " and r.ResourceClassID=" + resourceClassId + ") " + orStr;

                    }
                }
                else
                {
                    string yearWhere = yearId > 0 ? " r.tbYearID= " + yearId + "   and " : "";
                    string orderWhere = orderid > 0 ? " r.tbOrderID=" + orderid + "  and " : "";
                    string vloumeWhere = vloume > 0 ? " r.VloumeID=" + vloume + "  and" : "";
                    string courseTypeWhere = courseTypeId > 0 ? " r.CourseTypeID=" + courseTypeId + " and " : "";
                    string subjectWhere = subjectId > 0 ? " r.SubjectID=" + subjectId + " and " : "";
                    string gradeWhere = gradeId > 0 ? " r.GradeID=" + gradeId + " and " : ""; 
                    where +=  yearWhere + vloumeWhere + orderWhere + courseTypeWhere + subjectWhere + gradeWhere + " r.ResourceClassID=" + resourceClassId ;
                }

                var queryable1 = db.Queryable<V_ResourceInfo_Valid>()
                    .JoinTable<V_Subject_Valid>((r, s) => r.SubjectID == s.ID, JoinType.Inner)
                    .JoinTable<V_CourseType_Valid>((r, c) => r.CourseTypeID == c.ID, JoinType.Inner)
                    .JoinTable<V_Grade_Valid>((r, g) => r.GradeID == g.ID, JoinType.Inner)
                    .JoinTable<User>((r, u) => r.OwnerID == u.ID, JoinType.Inner)
                    .JoinTable<V_tbYear_Valid>((r, y) => r.tbYearID == y.ID, JoinType.Left)
                    .JoinTable<V_tbOrder_Valid>((r, o) => r.tbOrderID == o.ID, JoinType.Left)
                    .JoinTable<V_Vloume_Valid>((r, v) => r.VloumeID == v.ID, JoinType.Left);

                if (!string.IsNullOrEmpty(name)) queryable1.Where(r => r.Name.Contains(name));
                queryable1.Where(where);
                queryable1.Select(" distinct r.*,s.Name SubjectName,c.Name CourseTypeName,g.Name GradeName,u.Name UserName,y.Name YearName,o.Name OrderName,v.Name VloumeName");
                //.Where(q => q.tbYearID == yearId && q.tbOrderID == orderid);

                list = queryable1.OrderBy(r => r.CreationDate, OrderByType.Desc).ToPageList(pageIndex, pageSize, ref pageCount, ref totalPageNum);
              
            }
          
           return list;
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