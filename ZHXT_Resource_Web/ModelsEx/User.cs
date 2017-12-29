using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public partial class User
    {
        public string CreationDateStr
        {
            get
            {
                return this.CreationDate.ToString();
            }
        }

       
    }
}