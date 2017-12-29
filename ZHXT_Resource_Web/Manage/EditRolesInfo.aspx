<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditRolesInfo.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.EditRolesInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link type="text/css" href="css/theme.css" rel="stylesheet" />
    <link type="text/css" href="images/icons/css/font-awesome.css" rel="stylesheet" />
    <script src="scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
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
                            <label class="control-label" for="basicinput">名称：</label>
                            <div class="controls">
                                <input id="name" type="text" value="" />
                            </div>
                        </div>
                        <div class="control-group" data-type="remark">
                            <label class="control-label" for="basicinput">简称：</label>
                            <div class="controls">
                                <input id="remark" type="text" value="" />
                            </div>
                        </div>
                        

                        <div class="control-group">
                            <label class="control-label" for="basicinput">访问资源类型范围：</label>
                            <div class="controls">
                                <table id="table1" cellpadding="0" cellspacing="0" border="0" class="table table-striped table-bordered table-condensed" width="100%">
                                    <colgroup>
                                        <col width="25%">
                                        <col width="25%"> 
                                        <col width="25%">
                                        <col width="25%"> 
                                    </colgroup>
                                    <thead>
                                        <tr> 
                                            <th>名称</th>
                                            <th>访问权限</th> 
                                            <th>上传权限</th> 
                                            <th>下载权限</th> 
                                        </tr>
                                    </thead>
                                    <tbody> 
                                     
                                    </tbody>
                                </table>
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

    <script>
        var RoelsModel_Json=<%=RoelsModel_Json%>; 
        //console.log(RoelsModel_Json)
        var $name=$("#name");
        var $remark=$("#remark");
        var $table1=$("#table1"); 
        var $save=$("#save");
       
        $(function (){
            GetResourceClassAll();
            $name.val(RoelsModel_Json.Name);
            $remark.val(RoelsModel_Json.Remark);
            //$("input[type=radio][name=download][value="+RoelsModel_Json.AllowDownload+"]").attr("checked",true);  
            //$("input[type=radio][name=upload][value="+RoelsModel_Json.AllowUpload+"]").attr("checked",true);  
            Save();
        });

        function GetResourceClassAll(){
            $.get("/Manage/Ajax/GetResourceClassAll.ashx",{},function(data){
                if(data.result){
                    for (var i = 0; i <data.list.length; i++) {
                        var item=data.list[i]; 
                        $table1.find("tbody").append("<tr data-id='"+item.ID+"'>"
                            +"<td>"+item.Name+"</td>"
                            +"<td><label class='checkbox inline'><input type='checkbox' name='fw' value=''>允许 </label></td>"
                            +"<td><label class='checkbox inline'><input type='checkbox'  name='upload' value=''>允许 </label></td>"
                            +"<td><label class='checkbox inline'><input type='checkbox'  name='download' value=''>允许 </label></td>"
                            +"</tr>");
                    }
                    if(RoelsModel_Json.RolesAreaList.length>0){
                        var arr=RoelsModel_Json.RolesAreaList;
                        for (var i = 0; i < arr.length; i++) {
                            $table1.find("tbody").find("[data-id='"+arr[i].ResourceClassID+"']").find("input[type=checkbox][name=fw]").attr("checked",true);  
                            if(arr[i].AllowUpload)
                                 $table1.find("tbody").find("[data-id='"+arr[i].ResourceClassID+"']").find("input[type=checkbox][name=upload]").attr("checked",true);  
                            if(arr[i].AllowDownload)
                            $table1.find("tbody").find("[data-id='"+arr[i].ResourceClassID+"']").find("input[type=checkbox][name=download]").attr("checked",true);  

                        }
                    } 
                }
            },"json");
        }

        function  Save(){
            $save.click(function(){
                var name=$.trim($name.val());
                var remark=$.trim($remark.val());
                var objArr=[];
                $table1.find("tbody").find("tr").each(function(){
                    var $tr=$(this);
                    var obj={
                        id:$tr.attr("data-id"),
                        visit:$tr.find("input[name=fw]").is(':checked'),
                        upload:$tr.find("input[name=upload]").is(':checked'),
                        download:$tr.find("input[name=download]").is(':checked'),
                    } 
                    objArr.push(obj);
                });
                var data={id:RoelsModel_Json.ID,name:name,remark:remark,UpdateRolesInfo_AreaList:objArr}
                $.post("/Manage/Ajax/UpdateRolesInfo.ashx",{data:JSON.stringify(data)},function(d){
                    if(d.result){
                        alert("修改成功！");
                        location.reload();
                    }else{
                        alert(d.message);
                    }
                },"json");
            });
        }
    </script>
</body>
</html>
