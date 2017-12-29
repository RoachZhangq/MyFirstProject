<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPage.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.UploadPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link type="text/css" href="bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link type="text/css" href="css/theme.css" rel="stylesheet" />
    <link type="text/css" href="images/icons/css/font-awesome.css" rel="stylesheet" />
    <script src="scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="/Manage/uploadify/uploadify.css" rel="stylesheet" /> 
    <script src="uploadify/jquery.uploadify.js"></script>
    <script type="text/javascript">
        var _maxVideoSize="<%=maxVideoSize%>";
        var maxVideoSize=(_maxVideoSize==""?100:parseInt(_maxVideoSize))*1024*1024; //视频上限大小
        var _maxOtherSize="50";
        var maxOtherSize=(_maxOtherSize==""?50:parseInt(_maxOtherSize))*1024*1024;
        
        $(function () {
            /*************setting***************/
            var definedData = [];
            definedData.auth = "@(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value)";
            definedData.ASPSESSID = "@Session.SessionID";
            definedData.fileTypeExts = "*.doc;*.docx;*.xls;*.xlsx;*.pdf;*.ppt;*.pptx;*.txt;*.rar;*.zip;*.mp4;*.mp3;*.swf;*.flv;*.avi;";    //上传类型  
            definedData.uploader = "/Manage/Ajax/UploadHandler.ashx";    //后台处理路径  
            definedData.fileSizeLimit ="1000MB";  //上传大小  
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
                'onUploadSuccess': function (file, data, response) {
                    // $('#file_upload').uploadify('cancel', '*'); //隐藏进度条</span>  
                    var dataJson = JSON.parse(data);
                    if (dataJson.Status) {
                        //上传成功  
                        //alert(dataJson.Message);  
                        console.log(dataJson);
                        FileName = dataJson.Message;
                        if($.trim($("#name").val())=="")$("#name").val(FileName.substr(0,FileName.indexOf('.')));//资源名赋值
                        NewFileName = dataJson.Message2;
                        FileContentLength = dataJson.ContentLength;
                        $("#filePrompt").html("已成功上传：" + dataJson.Message);
                    } else {
                        FileName = "";
                        NewFileName = "";
                        FileContentLength = "";
                        //上传失败  
                        alert(dataJson.Message);
                    }
                },
                //返回一个错误，选择文件的时候触发  
                'onSelectError': function (file, errorCode, errorMsg) {
                    FileName = "";
                    NewFileName = "";
                    FileContentLength = "";
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
                },
                'onSelect' :function(file){
                    //取消
                    var myCancel =function(){ 
                        var id=$('#file_upload-queue').find(".uploadify-queue-item").first().attr("id");
                        $('#file_upload').uploadify('cancel', id)
                    }
                    //检索特殊字符
                    var checkChar=function(name){
                        var result=true;
                        if(name.indexOf("+")>-1||name.indexOf("/")>-1||name.indexOf("?")>-1||name.indexOf("%")>-1||name.indexOf("#")>-1||name.indexOf("&")>-1||name.indexOf("=")>-1){
                            result=false;
                        }
                        return result;
                    }
                    var fileName=file.name;//文件名
                    var fileSize=file.size;//文件大小
                    if(!checkChar(fileName)){
                        alert("文件名禁止有：+ / ? % # & =");
                        myCancel();
                        return false;
                    }
                    var suffix=fileName.toLowerCase().split('.').splice(-1);//后缀名
                    if(suffix=="flv"||suffix=="avi"||suffix=="swf"||suffix=="mp4"||suffix=="webm"||suffix=="ogv"||suffix=="mpg"){
                        if(fileSize>maxVideoSize){
                            alert("视频文件不得超过"+_maxVideoSize+"MB");
                            myCancel();
                        }
                    }
                    // *.doc;*.docx;*.xls;*.xlsx;*.pdf;*.ppt;*.pptx;*.txt;*.rar;*.zip;
                    else if(suffix=="doc"||suffix=="docx"||suffix=="xls"||suffix=="xlsx"||suffix=="pdf"||suffix=="ppt"||suffix=="pptx"
                        ||suffix=="txt"||suffix=="rar"||suffix=="zip"){
                        if(fileSize>maxOtherSize){
                            alert("文件不得超过"+_maxOtherSize+"MB");
                            myCancel();
                        }
                    }
                  
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="content">
            <div class="module">
                <div class="module-head">
                    <h3 data-type="resourceClass"></h3>
                </div>
                <div class="module-body">
                    <div class="form-horizontal row-fluid"> 

                        <div class="control-group" data-type="year" style="display:none;">
                            <label class="control-label" for="basicinput">时间：</label>
                            <div class="controls">
                            </div>
                        </div>
                        <div class="control-group" data-type="courseType" style="display:none;">
                            <label class="control-label" for="basicinput">课程：</label>
                            <div class="controls">
                            </div>
                        </div>
                        <div class="control-group"  data-type="subject" style="display:none;">
                            <label class="control-label" for="basicinput">科目：</label>
                            <div class="controls">
                            </div>
                        </div>
                        <div class="control-group" data-type="grade" style="display:none;">
                            <label class="control-label" for="basicinput">年级：</label>
                            <div class="controls">
                            </div>
                        </div>
                        <div class="control-group" data-type="vloume" style="display:none;">
                            <label class="control-label" for="basicinput">册号：</label>
                            <div class="controls">
                            </div>
                        </div>
                        <div class="control-group" data-type="order" style="display:none;">
                            <label class="control-label" for="basicinput">讲次：</label>
                            <div class="controls">
                            </div>
                        </div>

                        <div class="control-group">
                            <label class="control-label" for="basicinput">资源名：</label>
                            <div class="controls">
                                <input type="text" id="name" placeholder="输入资源名..." class="span8" />
                                <%--<span class="help-inline">Minimum 5 Characters</span>--%>
                            </div>
                        </div>

                        <div class="control-group">
                            <label class="control-label" for="basicinput">文件上传</label>
                            <div class="controls">
                                <p>
                                    <input type="file" name="file_upload" id="file_upload" />
                                    <span style="color: #8FBC8F" id="hintSize">(文件不能超过50MB)</span>
                                    <span style="color: black" id="filePrompt"></span>
                                </p>
                                <p>
                                    <a class="btn " style="cursor: pointer;" onclick="javascript:$('#file_upload').uploadify('upload')">开始上传 </a>
                                </p>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="basicinput">备注：</label>
                            <div class="controls">
                                <textarea class="span8" id="txtRemark" rows="5"></textarea>
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
    </form>


    <script>
        var FileName = "";//原文件名
        var NewFileName = "";//新文件文件
        var FileContentLength = "";//文件字节长度

        var $resourceClass=$('[data-type="resourceClass"]');
        var $year = $('[data-type="year"]');
        var $courseType = $('[data-type="courseType"]');
        var $subject = $('[data-type="subject"]');
        var $grade = $('[data-type="grade"]');
        var $order = $('[data-type="order"]');
        var $vloume = $('[data-type="vloume"]');

        var resourceClassObj=<%=resourceClassObj%>;
        var yearObj=<%=yearObj%>;
        var courseTypeObj=<%=courseTypeObj%>;
        var subjectObj=<%=subjectObj%>;
        var gradeObj=<%=gradeObj%>;
        var orderObj=<%=orderObj%>;
        var vloumeObj=<%=vloumeObj%>;

        var $name = $("#name");
        var $txtRemark = $("#txtRemark");
        var $save = $("#save");

        $(function(){
            $("#hintSize").html("(普通文件不能超过50MB,视频文件不得超过："+_maxVideoSize+"MB)");
            init();
        
        });
        function init(){
            if(resourceClassObj!=null) $resourceClass.html(resourceClassObj.Name);
            if(yearObj!=null) $year.show().find(".controls").html(yearObj.Name);
            if(courseTypeObj!=null) $courseType.show().find(".controls").html(courseTypeObj.Name);
            if(subjectObj!=null) $subject.show().find(".controls").html(subjectObj.Name);
            if(gradeObj!=null) $grade.show().find(".controls").html(gradeObj.Name);
            if(orderObj!=null) $order.show().find(".controls").html(orderObj.Name);
            if(vloumeObj!=null) $vloume.show().find(".controls").html(vloumeObj.Name);


            $save.click(function(){
                var p_NameVal =$.trim($name.val());
                var p_ResourceClassVal =resourceClassObj!=null? resourceClassObj.ID:0; 
                var p_YearVal =yearObj!=null? yearObj.ID:0;
                var p_CourseTypeVal = courseTypeObj!=null?courseTypeObj.ID:0; 
                var p_SubjectVal =subjectObj!=null?subjectObj.ID:0; 
                var p_GradeVal = gradeObj!=null?gradeObj.ID:0; 
                var p_OrderVal = orderObj!=null?orderObj.ID:0; 
                var p_VloumeVal = vloumeObj!=null? vloumeObj.ID:0; 
                var p_RemarkVal = $txtRemark.val();
                if (p_NameVal == "") {
                    alert("请输入资源名称");
                    return;
                }
                if (FileName == "" || NewFileName == "") {
                    alert("请先上传文件");
                    return;
                }

       $.post("/Manage/Ajax/AddResourceInfo.ashx",
           {
               NameVal: p_NameVal,
               ResourceClassVal: p_ResourceClassVal, 
               GradeVal: p_GradeVal,
               SubjectVal: p_SubjectVal,
               CourseTypeVal: p_CourseTypeVal,
               YearVal: p_YearVal,
               OrderVal: p_OrderVal,
               VloumeVal:p_VloumeVal,
               RemarkVal: p_RemarkVal, 
               FileName: FileName,
               NewFileName: NewFileName,
               FileContentLength: FileContentLength
           },
           function (data) {
               if (data.result) {
                   alert("保存成功！");
                   parent.GetData(); //查询
                   location.reload();
               } else {
                   alert(data.message);
               }
           }, "json");
            
            });
        }
    </script>
</body>
</html>
