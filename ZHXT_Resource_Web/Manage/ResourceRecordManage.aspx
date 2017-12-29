<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceRecordManage.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.ResourceRecordManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link type="text/css" href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link type="text/css" href="css/theme.css" rel="stylesheet" />
    <link type="text/css" href="images/icons/css/font-awesome.css" rel="stylesheet" />
    <script src="scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
      <script src="scripts/bootstrap-paginator.js"></script>
</head>
<body>
    <form id="form1" runat="server"> 
     <br />
     <div class="module">
             
            <table id="table1" cellpadding="0" cellspacing="0" border="0" class="table table-striped table-bordered table-condensed" width="100%">
              <colgroup>
                  <col width="25%">
                  <col width="25%">
                  <col width="25%">
                  <col width="25%">
              </colgroup>
              <thead>
                <tr>
                  <th>序号</th> 
                  <th>名称</th>
                  <th><%=TypeName%>时间</th>  
                </tr>
              </thead>
              <tbody>
             
              </tbody>
            </table>
               <div id="example" style="text-align:center;"></div>
          </div>
     

    </form>
    <script>
        var pageIndex = 1;
        var pageSize = 10;
        var $table1 = $("#table1");
        var type = "resourcerecord";
        var resourceRecordTypeEnum = "<%=Type%>";
        var id = "<%=ID%>";
        $(function () {
            GetData();
        });
        function GetData() {
            $.get("/Manage/Ajax/GetAllDataByPage.ashx", { id: id, pageIndex: pageIndex, pageSize: pageSize, type: type, resourceRecordTypeEnum: resourceRecordTypeEnum },
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
                            $('#example').bootstrapPaginator(options);
                        }
                    }

                }, "json");
        }
        //填充内容
        function appendHtml(data) {
            $table1.find("tbody").html("");
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                var index = pageIndex * pageSize - pageSize + i + 1;//正序
                  index = data.pageCount - index + 1;//倒序
                $table1.find("tbody").append(' <tr data-id="' + item.ID + '"> '
                    + '<td>' + index + '</td>'
                    + '<td>' + item.UserName + '</td>'
                     + '<td>' + item.CreationDateStr + '</td>'
                      + ' </tr>');
            }
            
        }
    </script>
</body>
</html>
