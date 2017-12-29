<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="EditCourseTypeArea.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.EditCourseTypeArea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="scripts/bootstrap-paginator.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="module">
            <div class="module-head">
                <h3>编辑课程类型的讲次、册号范围
                </h3>
                <br />
                <%--             <div><span style="color:dodgerblue">&nbsp;&nbsp;提示： </span></div>--%>
            </div>
            <div class="module-body">
                <div class="form-horizontal row-fluid">
                    <div class="control-group" data-type="remark">
                        <label class="control-label" for="basicinput">名字：</label>
                        <div class="controls">
                            <input id="name" type="text" value="" disabled="disabled" />
                        </div>
                    </div>


                     


                 
                </div>
                <hr />
                <div class="form-horizontal row-fluid">

                    <div class="control-group" data-type="name">
                        <label class="control-label" for="basicinput">讲次：</label>
                        <div class="controls" data-type="order">
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="basicinput">册号：</label>
                        <div class="controls" data-type="vloume">
                        </div>
                    </div>

                <%--    <div class="control-group">
                        <div class="controls">
                            <a href="javascript:;" id="save" class="btn btn-primary">保存</a>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
        <script src="js/Common.js"></script>
    <script>
         var CourseTypeModel_Json=<%=CourseTypeModel_Json%>;

        var $table1=null;
        var $save=$("#save");
        var $name=$("#name");
        var $order=$("[data-type='order']");
        var $vloume=$("[data-type='vloume']");

        var pageIndex = 1;
        var pageSize = 10000;

        $(function(){
            $name.val(CourseTypeModel_Json.Name);
           
            GetVloume(function(){ GetOrder(function(){GetCourseTypeAreaList(function(){
                $vloume.find("input:checkbox").change(function(){
                    var checked=$(this).is(':checked');
                    var id=$(this).val();
                    var option=checked?"add":"delete";
                    Change(CourseTypeModel_Json.ID,"vloume",id,option);
                });
                $order.find("input:checkbox").change(function(){
                    var checked=$(this).is(':checked');
                    var id=$(this).val();
                    var option=checked?"add":"delete";
                    Change(CourseTypeModel_Json.ID,"order",id,option);
                });
            });});});
            
            
        });
        function GetCourseTypeAreaList(Callback){ 
            $.get("/Manage/Ajax/GetCourseTypeAreaList.ashx", {courseTypeId:CourseTypeModel_Json.ID, pageIndex: pageIndex, pageSize: pageSize },
        function (data) {
            if (data.result) {
                if (data.list.length > 0) { 
                    $.each(data.list, function (index, value) { 
                        if(value.VloumeID!=null){
                            $vloume.find("input[value='"+value.VloumeID+"']").attr("checked",true);
                        }
                        if(value.OrderID!=null){ 
                            $order.find("input[value='"+value.OrderID+"']").attr("checked",true);
                        }
                    }); 
                }
            }
            Callback();

        }, "json");
        }
        
        function appendHtml_ByObj(obj,data){
            obj.html("");
            for (var i = 0; i < data.list.length; i++) {
                var item=data.list[i];
                obj.append("<label class='checkbox inline'><input type='checkbox'  name='"+obj.attr("data-type")+"' value='"+item.ID+"' >"+item.Name+" </label></td>");
            }
        }
        function GetOrder(Callback){
            var o = new objOrder();
            o.getAll(function (data) { 
                appendHtml_ByObj($order,data);
                Callback()
            });
        }
        function GetVloume(Callback){
            var o = new objVloume();
            o.getAll(function (data) { 
                appendHtml_ByObj($vloume,data);
                Callback()
            });
        }
     
        function Change(coursetypeId,type,id,option){
            console.log(coursetypeId,type,id,option);
            $.post("/Manage/Ajax/SetCourseTypeAreaInfo.ashx",{
                coursetypeId:coursetypeId,
                type:type,
                id:id,
                option:option},
                function(data){
                    if(data.result){
                        alert("操作成功");                        
                    } else{
                        alert("操作失败");
                        location.reload();
                    }
                }
                ,"json");
        }
    </script>
</asp:Content>
