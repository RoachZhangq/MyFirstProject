<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="RolesManage.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.RolesManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="scripts/bootstrap-paginator.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div><a href="AddRolesInfo" class="btn" >新增角色</a>

          <a class="btn" id="search" style="float:right;">搜索</a>
         <input id="searchName" type="text" placeholder="名称和简称 模糊查询"  style="float:right;" /> 
    </div>
      
    <br />
       <div class="module">
         
            <table id="table1" cellpadding="0" cellspacing="0" border="0" class="table table-striped table-bordered table-condensed" width="100%">
              <colgroup>
                  <col width="20%">
                  <col width="20%">
                  <col width="20%"> 
                   <col width="20%">
                   <col width="20%">
              </colgroup>
              <thead>
                <tr>
                   <th>序号</th> 
                  <th>名称</th>
                  <th>简称</th>  
                  <th>访问资源类型范围</th> 
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

    <script>
        var pageIndex = 1;
        var pageSize = 15;
        var $table1 = $("#table1");
        var type = "roles"
        $(function () {
            GetData();
            $("#search").click(function () {
                GetData();
            });
        });
        function GetData() {
            var likeName = $.trim($("#searchName").val());
            $.get("/Manage/Ajax/GetAllDataByPage.ashx", {likeName:likeName, pageIndex: pageIndex, pageSize: pageSize, type: type },
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
                                    GetData();
                                }
                            }
                            $('#example').show();
                            $('#example').bootstrapPaginator(options);
                        } else {
                            $table1.find("tbody").html("");
                            $('#example').hide();
                        }
                    }

                }, "json");
        }

        function appendHtml(data) {  
            $table1.find("tbody").html("");
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                var index = pageIndex * pageSize - pageSize + i + 1;//正序
                // index = data.pageCount - index + 1;//倒序
                $table1.find("tbody").append(' <tr data-id="' + item.ID + '"> '
                    + '<td>' + index + '</td>'
                    + '<td>' + item.Name + '</td>'
                    + '<td>' + item.Remark + '</td>' 
                    + '<td data-type="select" style="cursor:pointer;">' + (item.ResourceClassIDList != "" ? "查看" : "-") + '</td>'
                    + (item.ID==43?"<td style='color:red;'>禁止操作</td>":'<td><a data-type="edit" href="javascript:;">编辑</a>'+(item.Code==""?'&nbsp;&nbsp;<a data-type="delete" href="javascript:;">删除</a>':'')+'</td>')
                    + ' </tr>');
                $table1.find("tbody").find("[data-type='select']").last().data("RolesAreaList", item.RolesAreaList);
               
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
                            //layer.tips($this.attr("data-classNames"), $this);
                            //layer.tips($this.attr("data-classNames"), $this, {
                            //    tips: [1, '#3595CC'],
                            //    time: 4000
                            //})
                            layer.alert($this.attr("data-classNames"))
                        });
                    }
                }
                 
            });
            //编辑
            $table1.find("tbody").find("[data-type='edit']").click(function () {
                var id = $(this).parent().parent().attr("data-id");
                layer.open({
                    title: '角色信息编辑',
                    type: 2,
                    area: ['700px', '450px'],
                    fixed: false, //不固定
                    maxmin: true,
                    content: 'EditRolesInfo?rolesId=' + id 
                });
            });
            //删除
            $table1.find("tbody").find("[data-type='delete']").click(function () {
                var id = $(this).parent().parent().attr("data-id");
                if (window.confirm('关联的用户也会同步删除此角色，确定删除吗？')) {
                    $.post("/Manage/Ajax/UpdateDisabled.ashx", { type: "roles", id: id, value: "True" },
                                   function (data) {
                                       if (data.result) {
                                           alert("删除成功！");
                                           GetData();//刷新
                                       } else {
                                           alert(data.message);
                                       }

                                   }, "json");
                }
            });
        }
    </script>
</asp:Content>
