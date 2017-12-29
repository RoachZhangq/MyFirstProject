<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="AddResourceInfo.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.AddResourceInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="/Manage/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/Manage/uploadify/jquery.uploadify.js"></script>
      <script type="text/javascript">  
        $(function () {  
            /*************setting***************/  
            var definedData = [];  
            definedData.auth = "@(Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value)";  
            definedData.ASPSESSID = "@Session.SessionID";  
            definedData.fileTypeExts = "*.*;*.doc;*.docx;*.xls;*.xlsx;*.pdf;*.ppt;*.txt;*.rar;*.zip;*.exe";    //上传类型  
            definedData.uploader = "/Manage/Ajax/UploadHandler.ashx";    //后台处理路径  
            definedData.fileSizeLimit = "2048MB";  //上传大小  
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
                }  
            });  
        });  
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
          <div class="module">
            <div class="module-head">
              <h3>资源上传</h3>
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
                  <label class="control-label" for="basicinput">校区：</label>
                  <div class="controls">
                    <select tabindex="1" id="selCampusFirst" data-placeholder="选择区域" class="span8">
                      <option value="">Select here..</option>
                      <option value="Category 1">First Row</option>
                      <option value="Category 2">Second Row</option>
                      <option value="Category 3">Third Row</option>
                      <option value="Category 4">Fourth Row</option>
                    </select>
                      <br /> <br />
                        <select tabindex="1" id="selCampusSecond" data-placeholder="选择校区" class="span8">
                      <option value="">请选择</option>
                     <%-- <option value="Category 1">First Row</option>
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
                             <span style="color: #8FBC8F">(文件不能超过2G)</span>
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
                      &nbsp;&nbsp;&nbsp;
                      <label class="checkbox inline">
                      <input type="checkbox" id="chkRecord" value="">
                     保存成功后记录页面信息 </label>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
    <script>
        var $name = $("#name");
        var $selResourceClass = $("#selResourceClass");
        var $selCampusFirst = $("#selCampusFirst");
        var $selCampusSecond = $("#selCampusSecond");
        var $selGrade = $("#selGrade");
        var $selSubject = $("#selSubject");
        var $selCourseType = $("#selCourseType");
        var $selYear = $("#selYear");
        var $selOrder = $("#selOrder");
        var FileName = "";//原文件名
        var NewFileName = "";//新文件文件
        var FileContentLength = "";//文件字节长度
        var $txtRemark = $("#txtRemark");
        var $chkRecord = $("#chkRecord");

        var $save = $("#save");
        $(function () {
            GetResourceClass($selResourceClass);
            GetCampus_ZHXT_First($selCampusFirst, $selCampusSecond);
            GetGrade($selGrade);
            GetSubject($selSubject);
            GetCourseType($selCourseType);
            GetYear($selYear);
            GetOrder($selOrder);
            SaveInfo($save, $name, $selResourceClass, $selCampusSecond, $selGrade, $selSubject, $selCourseType, $selYear, $selOrder, $txtRemark, $chkRecord);
        });
    </script>
    <script src="js/AddResourceInfoJS.js"></script>
    <script src="js/Common.js"></script>
    
</asp:Content>
