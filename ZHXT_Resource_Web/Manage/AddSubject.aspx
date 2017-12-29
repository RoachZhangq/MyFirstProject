<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="AddSubject.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.AddSubject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="content">
          <div class="module">
            <div class="module-head">
              <h3>新增科目
              </h3>
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
                            <label class="control-label" for="basicinput">排序值：</label>
                            <div class="controls">
                                <input id="displayindex" type="number" value="0" />
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
    <script src="js/BasicAdd.js"></script>
    <script>
        $(function () {
            Init("subject");
        });
    </script>
</asp:Content>
