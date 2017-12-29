using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZHXT_Resource_Web.Manage
{
    public partial class UploadPage : System.Web.UI.Page
    {
        public string resourceClassObj = "null";
        public string yearObj = "null";
        public string courseTypeObj = "null";
        public string subjectObj = "null";
        public string gradeObj = "null";
        public string orderObj = "null";
        public string vloumeObj = "null";
        public string maxVideoSize { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string resourceClassId = Request["resourceClassId"];
            string yearId = Request["year"];
            string courseTypeId = Request["courseType"];
            string subjectId = Request["subject"];
            string gradeId = Request["grade"];
            string orderId = Request["order"];
            string vloumeId = Request["vloume"];

            maxVideoSize = Request["maxVideoSize"];

            if (!string.IsNullOrEmpty(resourceClassId)&& resourceClassId!="0")
            {
                ResourceClass resourceClass = new Bll.ResourceClassBll().GetResourceClassById(Convert.ToInt32(resourceClassId));
                resourceClassObj = JsonConvert.SerializeObject(resourceClass) ;
            } 
            if(!string.IsNullOrEmpty(yearId)&& yearId != "0")
            {
                tbYear year = new Bll.tbYearBll().GetYearById(Convert.ToInt32(yearId));
                yearObj = JsonConvert.SerializeObject(year);
            }
            if (!string.IsNullOrEmpty(courseTypeId) && courseTypeId != "0")
            {
                CourseType courseType = new Bll.CourseTypeBll().GetCourseTypeById(Convert.ToInt32(courseTypeId));
                courseTypeObj = JsonConvert.SerializeObject(courseType);
            }
            if (!string.IsNullOrEmpty(subjectId) && subjectId != "0")
            {
                Subject subject = new Bll.SubjectBll().GetSubjectById(Convert.ToInt32(subjectId));
                subjectObj = JsonConvert.SerializeObject(subject);
            }
            if (!string.IsNullOrEmpty(gradeId) && gradeId != "0")
            {
                Grade grade = new Bll.GradeBll().GetGradeById(Convert.ToInt32(gradeId));
                gradeObj = JsonConvert.SerializeObject(grade);
            }
            if (!string.IsNullOrEmpty(orderId) && orderId != "0")
            {
                tbOrder order = new Bll.tbOrderBll().GettbOrderById(Convert.ToInt32(orderId));
                orderObj = JsonConvert.SerializeObject(order);
            }
            if (!string.IsNullOrEmpty(vloumeId) && vloumeId != "0")
            {
                Vloume vloume = new Bll.VloumeBll().GetVloumeById(Convert.ToInt32(vloumeId));
                vloumeObj = JsonConvert.SerializeObject(vloume);
            }

        }
    }
}