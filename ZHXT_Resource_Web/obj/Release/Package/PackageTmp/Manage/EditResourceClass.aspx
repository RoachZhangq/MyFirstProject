<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="EditResourceClass.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.EditResourceClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="content">
          <div class="module">
            <div class="module-head">
              <h3>编辑课程类型
              </h3>
                <br />
             <div><span style="color:dodgerblue">&nbsp;&nbsp;提示：课程类型默认拥有 课程标签、科目标签、年级标签 ！</span></div>

            </div>
            <div class="module-body">
                    <div class="form-horizontal row-fluid"> 
                        <div class="control-group" data-type="name">
                            <label class="control-label" for="basicinput">名称：</label>
                            <div class="controls">
                                <input id="name" type="text" value="" />
                            </div>
                        </div>
                           <div class="control-group" data-type="">
                            <label class="control-label" for="basicinput">拥有年份标签：</label>
                            <div class="controls">
                              <label class='checkbox inline'><input type='checkbox' name='year' value='' disabled="disabled">允许 </label>
                            </div>
                        </div>
                          <div class="control-group" data-type="">
                            <label class="control-label" for="basicinput">拥有册号标签：</label>
                            <div class="controls">
                               <label class='checkbox inline'><input type='checkbox' name='vloume' value=''  disabled="disabled">允许 </label>
                            </div>
                        </div>
                          <div class="control-group" data-type="">
                            <label class="control-label" for="basicinput">拥有讲次标签：</label>
                            <div class="controls">
                           <label class='checkbox inline'><input type='checkbox' name='order' value=''  disabled="disabled">允许 </label>
                            </div>
                        </div>
                        <div class="control-group" data-type="">
                            <label class="control-label" for="basicinput">排序值：</label>
                            <div class="controls">
                                <input id="displayindex" type="number" value="0" />
                            </div>
                        </div>  
                          <div class="control-group" data-type="">
                            <label class="control-label" for="basicinput">限制视频上限大小(单位:M)：</label>
                            <div class="controls">
                                <input id="remark" type="number" value="100" />
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
 
    <script>
        var $name = $("#name");
        var $displayindex = $("#displayindex");
        var ResourceClass_JSON=<%=ResourceClass_JSON%>;
        var $remark=$("#remark");

        $(function () {
            $name.val(ResourceClass_JSON.Name);
            $displayindex.val(ResourceClass_JSON.DisplayIndex);
            $remark.val(ResourceClass_JSON.Remark);
            if(!ResourceClass_JSON.NotExistYear)
            {
                $("input[name=year]").attr("checked",true);
            }
            if(!ResourceClass_JSON.NotExistOrder)
            {
                $("input[name=order]").attr("checked",true);
            }
            if(!ResourceClass_JSON.NotExistVloume)
            {
                $("input[name=vloume]").attr("checked",true);
            }

            $("#save").click(function () {
                var nameVal = $.trim($name.val());
                var displayindexVal = $displayindex.val();
                var _year = $("input[name=year]").is(':checked')
                var _vloume = $("input[name=vloume]").is(':checked')
                var _order = $("input[name=order]").is(':checked')
                var _remark = $remark.val();

                if (nameVal == "") {
                    alert("请输入名称");
                    return;
                }
                if (_remark == "") {
                    alert("请输入视频上限值");
                    return;
                }
                $.post("/Manage/Ajax/ResourceClassHandle.ashx", {
                    id:ResourceClass_JSON.ID,
                    name: nameVal,
                    displayindex: displayindexVal,
                    year: _year,
                    vloume: _vloume,
                    order: _order,
                    remark:_remark,
                    type:"edit"
                    
                },
                    function (data) {
                        if (data.result) {
                            alert("修改成功");
                            location.reload();
                        } else {
                            alert(data.message);
                        }
                    }, "json");
            });
        });
    </script>
</asp:Content>
