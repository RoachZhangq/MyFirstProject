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
    public partial class EditCourseTypeArea : System.Web.UI.Page
    {
         public string CourseTypeModel_Json { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string coursetypeId = Request["coursetypeId"];
            if (!string.IsNullOrEmpty(coursetypeId))
            {
                 CourseTypeModel_Json = JsonConvert.SerializeObject(new Bll.CourseTypeBll().GetCourseTypeById(Convert.ToInt32(coursetypeId)));
            }
        }
    }
}