using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZHXT_Resource_Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string objectName =Request["objectName"]??"Dog";
            //Assembler.AddType("Dog", typeof(Dog));
            //Assembler assembler = new Assembler();
            //IObject model = assembler.Create(objectName);
            //model.Name = objectName;

            //Response.Write(model.GetObjectList());
        }
    }

    // 

    public interface IObject
    {
         string Name { get; set; }
        string GetObjectList();
    }

    public class People : IObject
    {
        public string Name { get; set; }
        public string GetObjectList() { return "我是人类！我叫:"+this.Name; }
    }

    public class Dog : IObject
    {
        public string Name { get; set; }
        public string GetObjectList() { return "我是小狗！我叫:" + this.Name; }
    }


    public class Assembler
    {
        private static Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
        static Assembler()
        {
            dictionary.Add("People", typeof(People));
            //dictionary.Add("Dog", typeof(Dog));
        }

       public static void AddType(string name,Type t)
        {
            if (t != null&& !dictionary.ContainsKey(name))
            {
                dictionary.Add(name, t);
            }
        }

        public static void RemoveType(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                dictionary.Remove(name);
            }
        }

        public IObject Create(string type)
        {
             
            if (type != null)
            {
                Type targetType = dictionary[type];
                return (IObject)Activator.CreateInstance(targetType);
            }
            return null;
        }

    }
}