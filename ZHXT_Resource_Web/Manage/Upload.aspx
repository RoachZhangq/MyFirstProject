<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="/Manage/scripts/jquery-1.9.1.min.js"></script>
    <link href="/Manage/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/Manage/uploadify/jquery.uploadify.min.js"></script>
     <script type="text/javascript">  
        $(function () {  
            /*************setting***************/  
            var definedData = [];  
            definedData.auth = "@(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value)";  
            definedData.ASPSESSID = "@Session.SessionID";  
            definedData.fileTypeExts = "*.*;*.doc;*.docx;*.xls;*.xlsx;*.pdf;*.ppt;*.txt;*.rar;*.zip;*.exe";    //上传类型  
            definedData.uploader = "/Manage/Ajax/UploadHandler.ashx";    //后台处理路径  
            definedData.fileSizeLimit = "2MB";  //上传大小  
            definedData.fileObjName = "file_upload";    //控件名  
            definedData.queueSizeLimit = 1;      //允许上传个数文件  
            var data = { 'ASPSESSID': definedData.ASPSESSID, 'AUTHID': definedData.auth };    //firefox用swf上传丢失session  
  
            var errorData = [];  
            errorData.err100 = "文件个数超出系统限制，只允许上传" + definedData.queueSizeLimit + "个文件！";  
            errorData.err110 = "文件超出系统限制的大小，限制文件大小" + definedData.fileSizeLimit + "！";  
            errorData.err120 = "文件大小异常！";  
            errorData.err130 = "文件类型不正确，只允许上传后缀名" + definedData.fileTypeExts + "！";  
            /*************setting***************/  
            $("#file_upload").uploadify({  
                'buttonText': '选择资源',  
                'swf': '/Manage/uploadify/uploadify.swf',
                'uploader': definedData.uploader,  
                'auto': false, //当文件被添加到队列时，自动上传  
                'formData': data, //上传时传递数据  
                'fileObjName': definedData.fileObjName,  
                'queueSizeLimit': definedData.queueSizeLimit,  
                'fileTypeExts': definedData.fileTypeExts,  
                'fileSizeLimit': definedData.fileSizeLimit,  
                'onUploadSuccess': function(file, data, response) {  
                   // $('#file_upload').uploadify('cancel', '*'); //隐藏进度条</span>  
                    var dataJson = JSON.parse(data);  
                    if (dataJson.Status) {  
                        //上传成功  
                        alert(dataJson.Message);  
                    } else {  
                        //上传失败  
                        alert(dataJson.Message);  
                    }  
                },  
                //返回一个错误，选择文件的时候触发  
                'onSelectError': function (file, errorCode, errorMsg) {  
                    switch (errorCode) {  
                        case -100:  
                            alert(errorData.err100);  
                            break;  
                        case -110:  
                            alert(errorData.err110);  
                            break;  
                        case -120:  
                            alert(errorData.err120);  
                            break;  
                        case -130:  
                            alert(errorData.err130);  
                            break;  
                    }  
                },  
                //检测FLASH失败调用    
                'onFallback': function () {  
                    alert("您未安装FLASH控件，无法上传！请安装FLASH控件后再试。");  
                }  
            });  
        });  
    </script>  
</head>
<body>
    <form id="form1" runat="server">  
        <div>  
            <p><input type="file" name="file_upload" id="file_upload" /></p>  
            <p> 
                <img src="/Manage/uploadify/upload.jpg" alt="开始上传" style="cursor: pointer;" onclick="javascript:$('#file_upload').uploadify('upload')" />  
            </p>  
        </div>  
    </form>  
</body>
</html>
