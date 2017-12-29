<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="ResourceClassManage.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.ResourceClassManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="scripts/bootstrap-paginator.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div><a href="AddRecourceClass" class="btn" >新增课程类型</a>
          &nbsp;
        <a  data-id="disabled" href="javascript:;" data-disabled="true" class="btn" >禁用勾选</a> 
     &nbsp;
        <a data-id="disabled" href="javascript:;" data-disabled="false" class="btn" >启用勾选</a>

     </div>
     <br />
     <div class="module">
            <div><span style="color:dodgerblue">&nbsp;&nbsp;提示：排序值越大，越靠前！</span></div>
            <table id="table1" cellpadding="0" cellspacing="0" border="0" class="table table-striped table-bordered table-condensed" width="100%">
              <colgroup>
                 <col width="10%">
                  <col width="15%">
                  <col width="25%">
                  <col width="25%">
                  <col width="25%">
              </colgroup>
              <thead>
                <tr>
                     <th></th> 
                     <th>序号</th> 
                  <th>名称</th>
                  <th>排序</th> 
                    <th>操作</th> 
                </tr>
              </thead>
              <tbody>
              <%-- 
                   <tr>
                  <td>培优班</td>
                  <td>常规班</td>
                  <td>KA</td>
                  <td>【2017JA精英超常班】第一册10讲至慧大擂台/</td>
                  <td>【2017JA精英超常班】第一册10讲至慧大擂台.pptx</td>
                  <td>--</td>
                  <td><a href="#">1</a></td>
                  <td>皱丽琴</td>
                  <td>2017.03.03 10:52</td>
                  <td><a href="#">查看</a> <a href="#">修改</a> <a href="#">删除</a></td>
                </tr>
                --%>
              </tbody>
            </table>
               <div id="example" style="text-align:center;"></div>
          </div>
    <script src="js/BasicManage.js"></script>
    <script>
        InitEventEx = function ($table1) {
            var editList = $table1.find("[data-type='edit']");
            editList.show();
            editList.each(function () {
                $(this).click(function () { 
                    var id = $(this).parent().parent().attr("data-id"); 
                    window.location.href = "/Manage/EditResourceClass?id=" + id;
                });
            });
        }
        $(function () {

            Init("resourceclass", 1, 10,"true");
        });

    </script>

</asp:Content>
