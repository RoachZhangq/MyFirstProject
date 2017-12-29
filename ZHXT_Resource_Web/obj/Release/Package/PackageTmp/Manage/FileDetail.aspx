<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileDetail.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.FileDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #download{display:none !important;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div>
     <object classid="clsid27CDB6E-AE6D-11cf-96B8-444553540000"
            codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
            width="873" height="566" align="center">
        <param name="movie" value="images/move.swf">
        <param name="quality" value="high">
        <param name="wmode" value="transparent"> <!--这里代码可使Flash背景透明 -->
                 <embed src="/Manage/ResourceInfo_Uploads/<%=AttachmentPath %>" width="873" height="873"
                        align="center" quality="high"
                        pluginspage="http://www.macromedia.com/go/getflashplayer"
                        type="application/x-shockwave-flash" />
        </object>
    </div>--%>


        <div id="example1" style="height:500px;"></div>
        <hr />
         <div id="example2" style="height:500px;"></div>
          <hr />
         <div id="example3" style="height:500px;"></div>
    </form>
    <script src="PDFObject-master/pdfobject.js"></script>
    <script>
        PDFObject.embed("/Manage/ResourceInfo_Uploads/标题布局.pdf", "#example1");
        PDFObject.embed("/Manage/ResourceInfo_Uploads/精锐之道简述.pdf", "#example2");
        PDFObject.embed("/Manage/ResourceInfo_Uploads/销售报表.pdf", "#example3");
    </script>
</body>
</html>
