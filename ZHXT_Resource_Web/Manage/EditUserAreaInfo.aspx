<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUserAreaInfo.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.EditUserAreaInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link type="text/css" href="css/theme.css" rel="stylesheet" />
    <link type="text/css" href="images/icons/css/font-awesome.css" rel="stylesheet" />
    <link href="layer-v3.0.3/mobile/need/layer.css" rel="stylesheet" />

    <script src="scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="scripts/bootstrap-paginator.js"></script>
    <script src="layer-v3.0.3/layer.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <div class="module">
                <div class="module-head">
                    <h3 data-type="resourceClass"></h3>
                </div>
                <div class="module-body">
                    <div class="form-horizontal row-fluid">

                        <div class="control-group" data-type="name">
                            <label class="control-label" for="basicinput">工号：</label>
                            <div class="controls">
                                <input id="usercode" type="text" value="" disabled="disabled" />
                            </div>
                        </div>
                        <div class="control-group" data-type="remark">
                            <label class="control-label" for="basicinput">名字：</label>
                            <div class="controls">
                                <input id="name" type="text" value="" disabled="disabled" />
                            </div>
                        </div>


                        <div class="control-group">
                            <label class="control-label" for="basicinput">已限制的标签：</label>
                            <div class="controls">
                                <table id="table1" cellpadding="0" cellspacing="0" border="0" class="table table-striped table-bordered table-condensed" width="100%">
                                    <colgroup>
                                        <col width="10%">
                                        <col width="20%">
                                        <col width="20%">
                                        <col width="20%">
                                        <col width="20%">
                                        <col width="10%">
                                    </colgroup>
                                    <thead>
                                        <tr>
                                            <th>序号</th>
                                            <th>资源类型</th>
                                            <th>课程</th>
                                            <th>科目</th>
                                            <th>年级</th>
                                            <th>操作</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                                <div id="example" style="text-align: center;"></div>
                            </div>
                        </div>


                        <%--<div class="control-group">
                            <div class="controls">
                                <a href="javascript:;" id="save" class="btn btn-primary">保存</a>
                            </div>
                        </div>--%>
                    </div>
                    <hr />
                    <div class="form-horizontal row-fluid">

                        <div class="control-group" data-type="name">
                            <label class="control-label" for="basicinput">资源类型：</label>
                            <div class="controls" data-type="ResourceClass">
                                
                            </div>
                        </div>
                        <div class="control-group" >
                            <label class="control-label" for="basicinput">课程：</label>
                            <div class="controls" data-type="CourseType">
                               
                            </div>
                        </div>
                        <div class="control-group" data-type="remark">
                            <label class="control-label" for="basicinput">科目：</label>
                            <div class="controls" data-type="Subject">
                                 
                            </div>
                        </div>
                        <div class="control-group" data-type="remark">
                            <label class="control-label" for="basicinput">年级：</label>
                            <div class="controls" data-type="Grade">
                                 
                            </div>
                        </div>
                        <div class="control-group">
                            <div class="controls">
                                <a href="javascript:;" id="save" class="btn btn-primary">保存</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        
    </form>
    <script src="js/Common.js"></script>
    <script>
        var userModel_Json=<%=userModel_Json%>;
        var userArea_Json=null;
        var $table1=$("#table1"); 
        var $save=$("#save");
        var $name=$("#name"); 
        var $usercode=$("#usercode");
        var pageIndex = 1;
        var pageSize = 15;
        var type = "userarea"
        var $ResourceClass=$("[data-type='ResourceClass']");
        var $CourseType=$("[data-type='CourseType']");
        var $Subject=$("[data-type='Subject']");
        var $Grade=$("[data-type='Grade']"); 

        $(function(){
            $name.val(userModel_Json.Name);
            $usercode.val(userModel_Json.UserCode);
            GetUserAreaList();
            GetResourceClass();
            GetCourseType();
            GetSubject();
            GetGrade();

            Save();
        });

        

        function GetUserAreaList(){
            $table1.find("tbody").html(""); 
            $.get("/Manage/Ajax/GetUserAreaList.ashx", {userId:userModel_Json.ID, pageIndex: pageIndex, pageSize: pageSize, type: type },
        function (data) {
            if (data.result) {
                if (data.list.length > 0) {
                    appendHtml(data);
                    var options = {
                        bootstrapMajorVersion: 2,
                        currentPage: data.pageIndex,
                        totalPages: data.totalPage,
                        itemTexts: function (type, page, current) {
                            switch (type) {
                                case "first":
                                    return "首页";
                                case "prev":
                                    return "上一页";
                                case "next":
                                    return "下一页";
                                case "last":
                                    return "末页";
                                case "page":
                                    return page;
                            }
                        },
                        onPageClicked: function (event, originalEvent, type, page) {
                            //console.log(type +":"+ page);
                            pageIndex = page;
                            GetUserAreaList();
                        }
                    }
                    $('#example').bootstrapPaginator(options);
                }
            }

        }, "json");
        }

        
        function GetResourceClass(){
            var o = new objResourceClass();
            o.getAll(function (data) {
                console.log("GetResourceClass")
                console.log(data.list)
                appendHtml_ByObj($ResourceClass,data);
            });
        }
        function GetCourseType(){
            var o = new objCourseType();
            o.getAll(function (data) {
                console.log("GetCourseType")
                console.log(data.list)
                appendHtml_ByObj($CourseType,data);
            });
        }

        function GetSubject(){
            var o = new objSubject();
            o.getAll(function (data) {
                console.log("GetSubject")
                console.log(data.list)
                appendHtml_ByObj($Subject,data);
            });
        }

        function GetGrade(){
            var o = new objGrade();
            o.getAll(function (data) {
                console.log("GetGrade")
                console.log(data.list)
                appendHtml_ByObj($Grade,data);
            });
        }
        function appendHtml(data){ 
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                var index = pageIndex * pageSize - pageSize + i + 1;//正序
                // index = data.pageCount - index + 1;//倒序
                
                $table1.find("tbody").append(' <tr data-id="' + item.ID + '" > '
                    +"<td>"+index+"</td>"
                     +"<td>"+item.ResourceClassName+"</td>"
                     +"<td>"+item.CourseTypeName+"</td>"
                     +"<td>"+item.SubjectName+"</td>"
                     +"<td>"+item.GradeName+"</td>"
                      +"<td><a href='javascript:;' data-type='delete'>删除</a></td>"
                     +"</tr>");
               
            }
            $table1.find("[data-type='delete']").click(function(){
                var id=$(this).parent().parent().attr("data-id");
                if (window.confirm('确定删除吗？')) {
                    $.post("/Manage/Ajax/UpdateDisabled.ashx",{id:id,value:"True",type:"userarea"},function(data){
                        if(data.result){
                            alert("删除成功！");
                            GetUserAreaList();
                        }else{
                            alert("删除失败！");
                        }
                    },"json");
                }
            });
        }

        function appendHtml_ByObj(obj,data){
            obj.html("");
            for (var i = 0; i < data.list.length; i++) {
                var item=data.list[i];
                obj.append("<label class='checkbox inline'><input type='radio'  name='"+obj.attr("data-type")+"' value='"+item.ID+"' >"+item.Name+" </label></td>");
            }
        }


        function Save(){
            $save.click(function(){
                var userid=userModel_Json.ID;
                var resourceClassId=$("input[name=ResourceClass]:checked").val();
                var courseTypeId=$("input[name=CourseType]:checked").val();
                var subjectId=$("input[name=Subject]:checked").val();
                var gradeId=$("input[name=Grade]:checked").val();
                if(resourceClassId==undefined){
                    alert("请选择资源类型");
                    return;
                }
                if(courseTypeId==undefined){
                    alert("请选择课程");
                    return;
                }
                if(subjectId==undefined){
                    alert("请选择科目");
                    return;
                }
                if(gradeId==undefined){
                    alert("请选择年级");
                    return;
                }
                $.post("/Manage/Ajax/AddUserAreaInfo.ashx",{
                    userid:userid,
                    resourceClassId:resourceClassId,
                    courseTypeId:courseTypeId,
                    subjectId:subjectId,
                    gradeId:gradeId
                },function(data){
                    if(data.result){
                        alert("保存成功！") ;
                        GetUserAreaList();
                    }else{
                        alert(data.message);
                    }
                },"json");
            });
        }
    </script>
</body>
</html>
