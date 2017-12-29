using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using SqlSugar;
 

namespace ZHXT_Resource_Web.Dao
{
    public class SugarDao
    {

        //禁止实例化
        private SugarDao()
        {

        }



        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //public static string ConnectionString
        //{
        //    get
        //    {

        //        string reval = "server=.;uid=sa;pwd=123456;database=SqlSugarTest"; //这里可以动态根据cookies或session实现多库切换
        //        return reval;
        //    }
        //}
        public static SqlSugarClient GetInstance()
        {

            var db = new SqlSugarClient(ConnectionString);
            db.IsEnableLogEvent = true;//启用日志事件
            db.LogEventStarting = (sql, par) => {
                // Console.WriteLine(sql + " " + par + "\r\n");
                sql = sql.Replace("\n", " ");
                //Common.LogHelper.WriteLog_info(typeof(SugarDao),"【SQL】："+ sql + (string.IsNullOrEmpty(par)?"": "【参数】： "+par));
            };
            return db;
        }
    }
}