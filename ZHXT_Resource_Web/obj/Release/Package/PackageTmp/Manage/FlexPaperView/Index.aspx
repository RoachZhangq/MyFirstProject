<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.FlexPaperView.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<%-- <script type="text/javascript">
     var swfFile = '<%=FileUrl%>'
     var _width =1250;
     var _height = 550; 
    </script>
    <script src="js/swfobject/swfobject.js" type="text/javascript"></script>
    <script src="js/flexpaper_flash_debug.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script> 
    <script src="js/InsusDocumentView.js" type="text/javascript"></script>--%>

    <style>
        body { font: 12px "Myriad Pro", "Lucida Grande", sans-serif; text-align: center; padding-top: 5%; }
   .obj { width: 80%; }
    </style>
</head>
<body style="background-color:#488e3e;">
    
    <%--<div style="  left: 3px; top: 3px; " align="center">
        <div id="flashContent" style="">
            
        </div>
    </div>--%>
    <object class="obj"   data-type="swf" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" width="850" height="460" id="Untitled-1" align="middle">
        <param name="allowScriptAccess" value="sameDomain" />
        <param name="movie" value="mymovie.swf" />
        <param name="quality" value="high" />
        <param name="bgcolor" value="#ffffff" />
        <embed src="<%=FileUrl%>" quality="high" bgcolor="#ffffff" width="850" height="460" name="mymovie" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
    </object>
     
</body>
</html>
