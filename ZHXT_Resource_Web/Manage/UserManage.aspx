<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.UserManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <script src="scripts/bootstrap-paginator.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="module">
         <div style="float:right;">
              <input id="searchName" type="text" placeholder="名字和工号 模糊查询"  style="margin-top:10px;" />
              <a class="btn" id="search">搜索</a>
         </div>
        <br />
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
                  <th>工号</th>
                  <th>名字</th>  
                  <th>创建时间</th> 
                  <th>操作</th> 
                 
                </tr>
              </thead>
              <tbody>
             
              </tbody>
            </table>
               <div id="example" style="text-align:center;"></div>
          </div>


    <script>
        var pageIndex = 1;
        var pageSize = 15;
        var $table1 = $("#table1");
        var type = "user"
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
                    + '<td>' + item.UserCode + '</td>'
                    + '<td>' + item.Name + '</td>'
                    + '<td>' + item.CreationDateStr + '</td>'
                    + '<td><a data-type="editroles" href="javascript:;">角色管理</a>&nbsp;&nbsp;<a data-type="edittag" href="javascript:;">标签访问范围管理</a></td>'
                    + ' </tr>'); 
            }
            InitEvent();
        }

        function InitEvent() {
            //检索不可操作的用户
            $table1.find("tbody").find("tr").each(function () {
                var $this=$(this);
                var id = $this.attr("data-id");
                //AdministratorsCheck
                $.get("/Manage/Ajax/AdministratorsCheck.ashx", {id:id}, function (data) {
                    if (data.result) {
                        $this.find("[data-type='editroles']").remove(); 
                        $this.find("[data-type='edittag']").remove(); 
                    }
                }, "json");
            });
            //角色管理
            $table1.find("tbody").find("[data-type='editroles']").each(function () {
                var $this = $(this);
                var id = $this.parent().parent().attr("data-id");
                $this.click(function () { 
                    layer.open({
                        title: '用户角色管理',
                        type: 2,
                        area: ['700px', '450px'],
                        fixed: false, //不固定
                        maxmin: true,
                        content: 'EditUserRolesInfo?userId=' + id
                    });
                });
            });
            //标签访问范围管理
            $table1.find("tbody").find("[data-type='edittag']").each(function () {
                var $this = $(this);
                var id = $this.parent().parent().attr("data-id");
                $this.click(function () {
                    layer.open({
                        title: '标签访问范围管理',
                        type: 2,
                        area: ['700px', '450px'],
                        fixed: false, //不固定
                        maxmin: true,
                        content: 'EditUserAreaInfo?userId=' + id
                    });
                });

            });
        }
    </script>
</asp:Content>
