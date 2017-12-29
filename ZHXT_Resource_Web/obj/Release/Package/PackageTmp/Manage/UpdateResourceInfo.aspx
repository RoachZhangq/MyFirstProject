<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="UpdateResourceInfo.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.UpdateResourceInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link href="/Manage/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/Manage/uploadify/jquery.uploadify.js"></script>
      <script type="text/javascript">  
        var _maxVideoSize="";
        var maxVideoSize=0; //视频上限大小
        var _maxOtherSize="50";
        var maxOtherSize=(_maxOtherSize==""?50:parseInt(_maxOtherSize))*1024*1024;
        $(function () {  
            /*************setting***************/  
            var definedData = [];  
            definedData.auth = "@(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value)";  
            definedData.ASPSESSID = "@Session.SessionID";  
            definedData.fileTypeExts = "*.*;*.doc;*.docx;*.xls;*.xlsx;*.pdf;*.ppt;*.pptx;*.txt;*.rar;*.zip;*.mp4;*.mp3;*.swf;*.flv;*.avi;";    //上传类型  
            definedData.uploader = "/Manage/Ajax/UploadHandler.ashx";    //后台处理路径  
            definedData.fileSizeLimit = "1000MB";  //上传大小  
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
                        //alert(dataJson.Message);  
                        console.log(dataJson);
                        FileName = dataJson.Message;
                        NewFileName = dataJson.Message2;
                        FileContentLength = dataJson.ContentLength;
                        $("#filePrompt").html("已成功上传："+dataJson.Message);
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
                }  ,
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
          <div class="module">
            <div class="module-head">
              <h3>资源修改</h3>
            </div>
            <div class="module-body"> 
              <div class="form-horizontal row-fluid">
                <div class="control-group">
                  <label class="control-label" for="basicinput">资源名：</label>
                  <div class="controls">
                    <input type="text" id="name" placeholder="输入资源名..." class="span8">
                   <%-- <span class="help-inline">Minimum 5 Characters</span>--%> </div>
                </div>
                  <div class="control-group">
                  <label class="control-label" for="basicinput">资源类型：</label>
                  <div class="controls">
                    <select id="selResourceClass" tabindex="1" data-placeholder="选择资源类型" class="span8">
                     <%-- <option value="">Select here..</option> --%>
                    </select>
                  </div>
                </div>

                    <div class="control-group">
                  <label class="control-label" for="basicinput">时间：</label>
                  <div class="controls">
                    <select tabindex="1" id="selYear" data-placeholder="选择时间" class="span8">
                    <%--  <option value="">Select here..</option>
                      <option value="Category 1">First Row</option>
                      <option value="Category 2">Second Row</option>
                      <option value="Category 3">Third Row</option>
                      <option value="Category 4">Fourth Row</option>--%>
                    </select>
                  </div>
                </div>

                     <div class="control-group">
                  <label class="control-label" for="basicinput">课程：</label>
                  <div class="controls">
                    <select tabindex="1" id="selCourseType" data-placeholder="选择课程" class="span8">
                    <%--  <option value="">Select here..</option>
                      <option value="Category 1">First Row</option>
                      <option value="Category 2">Second Row</option>
                      <option value="Category 3">Third Row</option>
                      <option value="Category 4">Fourth Row</option>--%>
                    </select>
                  </div>
                </div>

                  
                    <div class="control-group">
                  <label class="control-label" for="basicinput">科目：</label>
                  <div class="controls">
                    <select tabindex="1" id="selSubject" data-placeholder="选择科目" class="span8">
                   <%--   <option value="">Select here..</option>
                      <option value="Category 1">First Row</option>
                      <option value="Category 2">Second Row</option>
                      <option value="Category 3">Third Row</option>
                      <option value="Category 4">Fourth Row</option>--%>
                    </select>
                  </div>
                </div>
                   <div class="control-group">
                  <label class="control-label" for="basicinput">年级：</label>
                  <div class="controls">
                    <select tabindex="1" id="selGrade" data-placeholder="选择年级" class="span8">
                    <%--  <option value="">Select here..</option>
                      <option value="Category 1">First Row</option>
                      <option value="Category 2">Second Row</option>
                      <option value="Category 3">Third Row</option>
                      <option value="Category 4">Fourth Row</option>--%>
                    </select>
                  </div>
                </div>
                     
                    <div class="control-group">
                  <label class="control-label" for="basicinput">册号：</label>
                  <div class="controls">
                    <select tabindex="1" id="selVloume" data-placeholder="选择讲次" class="span8">
                     <%-- <option value="">Select here..</option>
                      <option value="Category 1">First Row</option>
                      <option value="Category 2">Second Row</option>
                      <option value="Category 3">Third Row</option>
                      <option value="Category 4">Fourth Row</option>--%>
                    </select>
                  </div>
                </div>
                    <div class="control-group">
                  <label class="control-label" for="basicinput">讲次：</label>
                  <div class="controls">
                    <select tabindex="1" id="selOrder" data-placeholder="选择讲次" class="span8">
                     <%-- <option value="">Select here..</option>
                      <option value="Category 1">First Row</option>
                      <option value="Category 2">Second Row</option>
                      <option value="Category 3">Third Row</option>
                      <option value="Category 4">Fourth Row</option>--%>
                    </select>
                  </div>
                </div>
                <div class="control-group">
                  <label class="control-label" for="basicinput">文件上传</label>
                  <div class="controls">
                         <p><input type="file" name="file_upload" id="file_upload" />
                             <span style="color: #8FBC8F" id="hintSize">(文件不能超过50MB)</span>
                             <span style="color: black" id="filePrompt"> </span>
                         </p>  
            <p> 
                <a  class="btn " style="cursor: pointer;" onclick="javascript:$('#file_upload').uploadify('upload')" >开始上传 </a>  
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
                    <a  href="javascript:;" id="save" class="btn btn-primary">保存</a> 
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
    <script src="js/Common.js"></script>
    <script src="js/CheckCourseTypeArea_O_V.js"></script>
    <script src="js/UpdateResourceInfo.js"></script>
    <script>
        var resourceInfoId=0;
        var $name = $("#name");
        var $selResourceClass = $("#selResourceClass");
        var $selYear = $("#selYear");
        var $selCourseType = $("#selCourseType");
        var $selSubject = $("#selSubject");
        var $selGrade = $("#selGrade");
        var $selOrder = $("#selOrder"); 
        var $selVloume = $("#selVloume"); 
       
        var FileName = "";//原文件名
        var NewFileName = "";//新文件文件
        var FileContentLength = "";//文件字节长度
        var $txtRemark = $("#txtRemark");
        

        var $save = $("#save");
        var resourceInfoObj=<%=resourceInfoObj%>;

        $(function () {
            resourceInfoId= resourceInfoObj.ID;
            $name.val(resourceInfoObj.Name);
            $("#filePrompt").html("&nbsp&nbsp文件："+resourceInfoObj.FileName);
            $txtRemark.html(resourceInfoObj.Remark);
            FileName=resourceInfoObj.FileName;
            NewFileName=resourceInfoObj.FileNamePath;
            FileContentLength=resourceInfoObj.FileContentLength; 
            GetResourceClass($selResourceClass);

            Save();

        });

    
    </script>
</asp:Content>
