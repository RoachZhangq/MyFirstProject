using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZHXT_Resource_Web.Dal;

namespace ZHXT_Resource_Web.Bll
{
    public class SubjectBll
    {
        SubjectDal dal = new SubjectDal();
        public List<Subject> GetSubjectAll()
        {
            return dal.GetSubjectAll();
        }

        public Subject GetSubjectById(int id)
        {
            return dal.GetSubjectById(id);
        }

        public List<Subject> GetSubjectListByIdArr(string[] intArray)
        {
            return dal.GetSubjectListByIdArr(intArray);
        }

        public List<Subject> GetSubjectList(int pageIndex, int pageSize, ref int pageCount, ref int totalPage, bool disabled = false)
        {
            return dal.GetSubjectList(pageIndex,  pageSize, ref  pageCount, ref  totalPage, disabled);
        }

        public bool UpdateSubjectDisplayIndex(int id, int value)
        {
            return dal.UpdateSubjectDisplayIndex(id,  value);
        }

        public bool UpdateSubjectDisabled(int id, bool value)
        {

            return dal.UpdateSubjectDisabled(id, value);
        }


        }
    }