﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OfficeWeb365.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.OfficeWeb365" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>文件浏览-至慧资源管理平台</title>
    <script src="scripts/jquery-1.9.1.min.js"></script>
     <style>
        body {margin: 0px;padding: 0px;}  
    </style>
</head>
<body>
    <iframe id="iframe1" src="http://ow365.cn/?i=<%=OfficeWeb365_ID%>&furl=<%=FileUrl%>" ></iframe> 
      <script>
        var width = $(document.body).width()-20;
        var height = $(document).height()-20;
        var $iframe1 = $("#iframe1");
        $iframe1.attr("width", width).attr("height", height);
    </script>
</body>
</html>
