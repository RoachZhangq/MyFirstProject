<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUserRolesInfo.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.EditUserRolesInfo" %>

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
                            <label class="control-label" for="basicinput">角色列表：</label>
                            <div class="controls">
                                <table id="table1" cellpadding="0" cellspacing="0" border="0" class="table table-striped table-bordered table-condensed" width="100%">
                                    <colgroup>
                                        <col width="15%">
                                        <col width="25%">
                                        <col width="25%">
                                        <col width="25%">
                                    </colgroup>
                                    <thead>
                                        <tr>
                                            <th>序号</th>
                                            <th>角色名称</th>
                                            <th>角色范围</th>
                                            <th>是否拥有</th>
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
                </div>
            </div>
        </div>
    </form>

    <script>
        var userModel_Json=<%=userModel_Json%>;
        var userRoles_Json=null;
        var $table1=$("#table1"); 
        var $save=$("#save");
        var $name=$("#name"); 
        var $usercode=$("#usercode");
        var pageIndex = 1;
        var pageSize = 15;
        var type = "roles"


        $(function(){
            $name.val(userModel_Json.Name);
            $usercode.val(userModel_Json.UserCode);
            //获取用户角色 最新
            GetUserRoleList(userModel_Json.ID,function(data){
                userRoles_Json=data.list;
                GetRolesAll();
            });
            
        });

        function GetRolesAll(){
            $.get("/Manage/Ajax/GetAllDataByPage.ashx", { pageIndex: pageIndex, pageSize: pageSize, type: type },
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
                                 GetRolesAll();
                             }
                         }
                         $('#example').bootstrapPaginator(options);
                     }
                 }

             }, "json");
        }


        function appendHtml(data){ 
            $table1.find("tbody").html(""); 
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                var index = pageIndex * pageSize - pageSize + i + 1;//正序
                // index = data.pageCount - index + 1;//倒序
                
                $table1.find("tbody").append(' <tr data-id="' + item.ID + '" > '
                    +"<td>"+index+"</td>"
                     +"<td>"+item.Name+"</td>"
                      +"<td data-type='select' style='cursor:pointer;'>"+(item.ResourceClassIDList != "" ? "查看" : "-") +"</td>"
                       +"<td><label class='checkbox inline'><input type='checkbox'  name='have' value='' data-WithUPC='false' >拥有 </label></td>"
                    +"</tr>");
                $table1.find("tbody").find("[data-type='select']").last().data("RolesAreaList", item.RolesAreaList);
            }
            //已有角色绑定
            if(userRoles_Json.length>0){
                for (var i = 0; i < userRoles_Json.length; i++) {
                    var item=userRoles_Json[i];
                    var $checkbox=$table1.find("tbody").find("[data-id='"+item.RolesID+"']").find("input[type=checkbox][name=have]");
                    $checkbox.attr("checked",true);
                    //此角色是否来之UPC 如果是 不允许取消
                    if(item.WithUPC){
                        $checkbox.attr("data-WithUPC","true").attr("disabled","disabled");  
                        $checkbox.parent().parent().append("<span style='color:red;font-size:8px;'>(来自UPC)</span>");
                    }
                        
                }

            }
             
            InitEvent();
        }

        function InitEvent() {
            //查看
            $table1.find("tbody").find("[data-type='select']").each(function () {
                var $this = $(this);
                var rolesarealist = $this.data("RolesAreaList");
                if (rolesarealist.length > 0) {
                    var classNames = "";
                    for (var i = 0; i < rolesarealist.length; i++) {
                        var item = rolesarealist[i];
                        classNames += (i + 1) + ". " + item.ResourceClassName + " " + (item.AllowUpload ? "<span style='color:#43CD80'>允许上传</span>" : "<span style='color:#CD0000'>禁止上传</span>") + " " + (item.AllowDownload ? "<span style='color:#43CD80'>允许下载</span>" : "<span style='color:#CD0000'>禁止下载</span>") + "<br/>"
                        $this.attr("data-classNames", classNames);
                        $this.unbind("click").click(function () { 
                            layer.alert($this.attr("data-classNames"))
                        });
                    }
                }
                 
            });
            
            //检查  去修改
            $("input[type=checkbox][name=have]").change(function(){
                var $this=$(this);
                if($this.attr("data-WithUPC")=="true"){
                    alert("此角色是账户在UPC中的角色，不得取消！") 
                }
                var status=$this.is(':checked');
                var rolesId=$this.parent().parent().parent().attr("data-id");
                if(rolesId!=""){
                    $.post("/Manage/Ajax/ChangeUserRoleInfo.ashx",{
                        userId:userModel_Json.ID,
                        rolesId:rolesId,
                        status:status
                    },function(data){
                        if(data.result){
                            alert("操作成功");
                            $table1.find("tbody").html(""); 
                            GetUserRoleList(userModel_Json.ID,function(data){
                                userRoles_Json=data.list;
                                GetRolesAll();//刷新列表
                            });
                              
                        }else{
                            alert("操作失败！");
                        }
                    },"json");
                }
              
            });
        }


        //获取用户权限
        function GetUserRoleList(userId,callback){
            $.get("/Manage/Ajax/GetUserRoleList.ashx",{userId:userId},function(data){
                callback(data);
            },"json");
        }
    </script>
</body>
</html>
