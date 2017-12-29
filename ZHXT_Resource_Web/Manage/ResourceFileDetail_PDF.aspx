<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceFileDetail_PDF.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.ResourceFileDetail_PDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        body {margin: 0px;padding: 0px;} 
    </style>
    <script src="scripts/jquery-1.9.1.min.js"></script>
    <script>
       
    </script>
</head>
<body>
     <iframe id="iframe1" src="PDF_js/web/viewer.html?url=<%=FileUrl%>" ></iframe> 
    <script>
        var width = $(document.body).width()-20;
        var height = $(document).height()-20;
        var $iframe1 = $("#iframe1");
        $iframe1.attr("width", width).attr("height", height);
    </script>
</body>

   
</html>
