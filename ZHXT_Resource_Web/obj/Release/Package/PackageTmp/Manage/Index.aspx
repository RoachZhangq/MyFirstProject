<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ManageMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="scripts/bootstrap-paginator.js"></script>
    <style>
        .module .table td:nth-child(4),
        .module .table td:nth-child(5) {
            text-align: center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="group">
        <div class="group-row" style="display: none;">
            <div class="head">
                <h4>时间：</h4>
            </div>
            <div class="body">
                <div class="item clearfix" data-type="year">&nbsp; <%--<a class="on" href="#">全部</a> <a href="#">2017</a> <a href="#">2016</a> <a href="#">2015</a> <a href="#">2014</a> <a href="#">2013</a> <a href="#">2012</a> <a href="#">2011</a> <a href="#">2010</a> <a href="#">2009</a> --%></div>
            </div>
        </div>
        <div class="group-row" style="display: none;">
            <div class="head">
                <h4>课程：</h4>
            </div>
            <div class="body">
                <div class="item clearfix" data-type="coursetype">&nbsp;<%-- <a href="#">全部</a> <a href="#">常规班</a> <a href="#">暑假班</a> <a href="#">寒假班</a> <a href="#">短期冲刺班</a> <a href="#">专题课程及练习</a>--%></div>
            </div>
        </div>
        <div class="group-row" style="display: none;">
            <div class="head">
                <h4>科目：</h4>
            </div>
            <div class="body">
                <div class="item clearfix" data-type="subject">&nbsp; <%--<a href="#">全部</a> <a href="#">培优班</a> <a href="#">精英班</a> <a href="#">超常班</a> <a href="#">语文版</a> <a href="#">辅修课&amp;其他</a>--%></div>
            </div>
        </div>
        <div class="group-row" style="display: none;">
            <div class="head">
                <h4>年级：</h4>
            </div>
            <div class="body">
                <div class="item clearfix" data-type="grade">&nbsp;<%--<a href="#">全部</a> <a href="#">KA</a> <a href="#">KB</a> <a href="#">KC</a> <a href="#">JA</a> <a href="#">JB</a><a href="#">PC</a><a href="#">PD</a><a href="#">PE</a>--%></div>
            </div>
        </div>
        <div class="group-row" style="display: none;">
            <div class="head">
                <h4>册号：</h4>
            </div>
            <div class="body">
                <div class="item clearfix" data-type="vloume"><%--<a href="#">全部</a> <a href="#">第16讲</a> <a href="#">第15讲</a> <a href="#">第14讲</a> <a href="#">第13讲</a> <a href="#">第12讲</a><a href="#">第11讲</a><a href="#">第10讲</a><a href="#">第9讲</a><a href="#">第8讲</a><a href="#">第7讲</a><a href="#">第6讲</a><a href="#">第5讲</a><a href="#">第4讲</a><a href="#">第3讲</a><a href="#">第2讲</a><a href="#">第1讲</a>--%></div>
            </div>
        </div>
        <div class="group-row" style="display: none;">
            <div class="head">
                <h4>讲次：</h4>
            </div>
            <div class="body">
                <div class="item clearfix" data-type="order"><%--<a href="#">全部</a> <a href="#">第16讲</a> <a href="#">第15讲</a> <a href="#">第14讲</a> <a href="#">第13讲</a> <a href="#">第12讲</a><a href="#">第11讲</a><a href="#">第10讲</a><a href="#">第9讲</a><a href="#">第8讲</a><a href="#">第7讲</a><a href="#">第6讲</a><a href="#">第5讲</a><a href="#">第4讲</a><a href="#">第3讲</a><a href="#">第2讲</a><a href="#">第1讲</a>--%></div>
            </div>
        </div>
        <div class="group-row " style="text-align: right;">
            <input id="searchName" type="text" placeholder="资源名模糊查询" style="margin-top: 10px;" />
            <a class="btn" id="search">搜索</a>
        </div>
    </div>

    <div>
        <a id="upload" class="btn" href="javascript:;" style="display: none;">上传资料</a>
        &nbsp;
          <a id="BatchDownload" class="btn" href="javascript:;" style="display: none;">批量下载</a>
    </div>
    <br />
    <div class="module">

        <table id="table1" cellpadding="0" cellspacing="0" border="0" class="table table-striped table-bordered table-condensed" width="100%">
            <colgroup>
                <col width="2%" data-type="firstCol" style="display: none;">
                <col width="5%">
                <col width="15%">
                <col width="5%">
                <col width="5%">
                <col width="5%">
                <col width="5%" data-type='yearEx' style="display: none;">
                <col width="5%" data-type='vloumeEx' style="display: none;">
                <col width="5%" data-type='orderEx' style="display: none;">
                <col width="10%">
                <col width="5%">
                <col width="5%" style="display: none;" data-type="admin">
                <col width="5%">
                <col width="10%">
                <col width="15%">
            </colgroup>
            <thead>
                <tr>
                    <th style="display: none;" data-type="firstCol"></th>
                    <th>序号</th>
                    <th>资源名</th>
                    <th>课程</th>
                    <th>科目</th>
                    <th>级别</th>
                    <th style="display: none;" data-type='yearEx'>年份</th>
                    <th style="display: none;" data-type='vloumeEx'>册号</th>
                    <th style="display: none;" data-type='orderEx'>讲次</th>
                    <th>文件名</th>
                    <th>大小</th>
                    <th style="display: none;" data-type="admin">查看数<br />
                        下载数</th>
                    <th>上传人</th>
                    <th>上传时间</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <div id="example" style="text-align: center;"></div>
    </div>
    <script src="js/CommonEx.js"></script>
    <script src="js/CheckCourseTypeArea_O_V.js"></script>
    <script>
        var $divYear = $("[data-type='year']");
        var $divCourseType = $("[data-type='coursetype']");
        var $divSubject = $("[data-type='subject']");
        var $divGrade = $("[data-type='grade']");
        var $divVloume=$("[data-type='vloume']");
        var $divOrder = $("[data-type='order']");
        var $search = $("#search");
        var $upload=$("#upload");
        var $table = $("#table1");
        var $searchName=$("#searchName");

        var resourceClassId = "<%=resourceClassId%>";
        var resourceClassModel_Json=<%=resourceClassModel_Json%>;
        var allowUpload="<%=AllowUpload%>";//是否允许上传
        var allowDownload="<%=AllowDownload%>";//是否允许下载
         
       

        var pageindex = 1, pagesize = 10;

        $(function () { 
            if(!resourceClassModel_Json.NotExistCourseType){
                bindCourseType();
                $divCourseType.parent().parent().show();
            }
            if(!resourceClassModel_Json.NotExistYear){
                bindYear();
                $divYear.parent().parent().show();
                $("[data-type='yearEx']").show();
            } 
          
            $search.click(function () {
                GetData(); 
            });
            $upload.click(function(){
                Upload();
            });
            if(allowDownload=="True"){
                BatchDownload();//批量下载
                $("#BatchDownload").show();
                $("[data-type='firstCol']").show();
            }
          
            if(allowUpload=="True")$upload.show();else $upload.remove();
        });
        //绑定年份
        function bindYear() {
            var o = new objYear();
            o.getAll(function (data) {
                appendHtml(data, $divYear);
                $divYear.find("[data-id]").click(function () {
                    $(this).addClass("on").siblings().removeClass("on");
                    GetData(); //查询
                });
            });
        }
        //绑定课程
        function bindCourseType() {
            var o = new objCourseType();
            o.getAll(function (data) {
                appendHtml(data, $divCourseType); 
                
                $divCourseType.find("[data-id]").click(function () {
                    var courseTypeId = $(this).attr("data-id");
                    $(this).addClass("on").siblings().removeClass("on");
                    $divGrade.html("&nbsp;");
                    GetData(); //查询
                    if(!resourceClassModel_Json.NotExistSubject){ 
                        bindSubject(courseTypeId);
                        $divSubject.parent().parent().show();
                        //隐藏 册号、讲次
                        $divOrder.find("a").remove();
                        $divOrder.parent().parent().hide();
                        $divVloume.find("a").remove();
                        $divVloume.parent().parent().hide();
                        if(!resourceClassModel_Json.NotExistOrder)$("[data-type='orderEx']").show();
                        if(!resourceClassModel_Json.NotExistVloume) $("[data-type='vloumeEx']").show(); 
                        //查询讲次 和册号 范围
                        GetCourseTypeArea(courseTypeId,function(data){ 
                            if(data.list.length>0){
                                var courseTypeAreaArr=data.list;//课程类型限制册号、讲次 的数据
                                //CheckCourseTypeArea_O_V 检查是否有设置到册号 或 讲次
                                if(CheckCourseTypeArea_O_V(courseTypeAreaArr,"order")){
                                    if(!resourceClassModel_Json.NotExistOrder){
                                        bindOrder(courseTypeAreaArr);
                                        $divOrder.parent().parent().show();
                                        //$("[data-type='orderEx']").show();
                                    }
                                }
                                if(CheckCourseTypeArea_O_V(courseTypeAreaArr,"vloume")){
                                    if(!resourceClassModel_Json.NotExistVloume){
                                        bindVloume(courseTypeAreaArr);
                                        $divVloume.parent().parent().show();
                                        //$("[data-type='vloumeEx']").show(); 
                                    } 
                                }
                            } 
                           
                        }); 
                    }
                });
                $divCourseType.find("[data-id]").first().click(); 
            });
        }
        //绑定科目
        function bindSubject(courseTypeId) {
            var o = new objSubject();
            o.getAll(courseTypeId,function (data) { 
                appendHtml(data, $divSubject); 
                $divSubject.find("[data-id]").click(function () {
                    var subjectId = $(this).attr("data-id");
                    var courseTypeId = $divCourseType.find(".on").attr("data-id");
                    $(this).addClass("on").siblings().removeClass("on"); 
                    GetData(); //查询
                    if(!resourceClassModel_Json.NotExistGrade){
                        bindGrade(courseTypeId, subjectId);
                        $divGrade.parent().parent().show();
                    }
                });
                $divSubject.find("[data-id]").first().click(); 
            });
        } 
        //绑定年级
        function bindGrade(courseTypeId,subjectId) {
            var o = new objGrade();
            o.getAll(courseTypeId,subjectId, function (data) {
                appendHtml(data, $divGrade);
                $divGrade.find("[data-id]").click(function () {
                    $(this).addClass("on").siblings().removeClass("on");
                    GetData(); //查询
                });
            });
        } 
        //绑定册号
        function bindVloume(courseTypeAreaArr) {
            var o = new objVloume();
            o.getAll(function (data) { 
                var data_list=[];
                for (var i = 0; i < data.list.length; i++) {
                    var item=data.list[i];
                    for (var j = 0; j < courseTypeAreaArr.length; j++) {
                        var itemCYA=courseTypeAreaArr[j];
                        if(itemCYA.VloumeID==item.ID){
                            //判断是否已存在
                            var exist=false;
                            $.each(data_list,function(index,value){ 
                                if(value.ID==item.ID){
                                    exist=true; 
                                }
                            });
                            if(!exist) data_list.push(item);
                        }
                    }
                }
                data.list=data_list;
                appendHtml(data, $divVloume);
                $divVloume.find("[data-id]").click(function () {
                    $(this).addClass("on").siblings().removeClass("on");
                    GetData(); //查询
                });
            });
        }
        //绑定讲次
        function bindOrder(courseTypeAreaArr) {
            var o = new objOrder();
            o.getAll(function (data) { 
                var data_list=[];
                for (var i = 0; i < data.list.length; i++) {
                    var item=data.list[i];
                    for (var j = 0; j < courseTypeAreaArr.length; j++) {
                        var itemCYA=courseTypeAreaArr[j];
                        if(itemCYA.OrderID==item.ID){
                            //判断是否已存在
                            var exist=false;
                            $.each(data_list,function(index,value){ 
                                if(value.ID==item.ID){
                                    exist=true; 
                                }
                            });
                            
                            if(!exist) data_list.push(item); 
                        }
                    }
                }
                data.list=data_list;
                appendHtml(data, $divOrder);
                $divOrder.find("[data-id]").click(function () {
                    $(this).addClass("on").siblings().removeClass("on");
                    GetData(); //查询
                });
            });
        }
        //填充
        function appendHtml(data,$obj) {
            var html = '<a class="on" data-type="all" data-id="0" href="javascript:;">全部</a>';
            if (data.list != null) {
                for (var i = 0; i < data.list.length; i++) {
                    var item = data.list[i];
                    html += '<a data-type="item" data-id="' + item.ID + '" href="javascript:;">' + item.Name + '</a>';
                }
                $obj.html(html);
            }
          
        }


        //获取数据
        function GetData() { 
            var year =!resourceClassModel_Json.NotExistYear? $divYear.find(".on").attr("data-id"):"0";
            var courseType = !resourceClassModel_Json.NotExistCourseType?$divCourseType.find(".on").attr("data-id"):"0";
            var subject =!resourceClassModel_Json.NotExistSubject? $divSubject.find(".on").attr("data-id"):"0";
            var grade =!resourceClassModel_Json.NotExistGrade? $divGrade.find(".on").attr("data-id"):"0";
            var order = !resourceClassModel_Json.NotExistOrder?$divOrder.find(".on").attr("data-id"):"0";
            var vloume = !resourceClassModel_Json.NotExistVloume?$divVloume.find(".on").attr("data-id"):"0";
            var name=$.trim($searchName.val());

            $.ajax({  
                type: "post",  
                url: "/Manage/Ajax/GetResourceInfoList.ashx",  
                cache:false,  
                async:false,  
                data:{ year: year, courseType: courseType,
                    subject: subject, grade: grade,vloume:vloume, order: order
            , resourceClassId: resourceClassId,name:name, pageindex: pageindex, pagesize: pagesize},
                dataType: "json",  
                success: function(data){ 
                    if (data.list != null) {
                        $table.find("tbody").find("tr").remove(); 
                        if(data.list.length==0){
                            $('#example').hide();
                            return;
                        }
                        //console.table(data.list) 
                        for (var i = 0; i < data.list.length; i++) {
                            var item = data.list[i];
                            var size = "";
                            if(item.FileContentLength < 1024){
                                size =  item.FileContentLength  + "B";
                            }
                            else if (item.FileContentLength >= 1024 && item.FileContentLength < (1024 * 1024)) {
                                size = (item.FileContentLength / 1024).toFixed(2) + "KB";
                            } else if (item.FileContentLength >= (1024 * 1024)) {
                                size = (item.FileContentLength / (1024 * 1024)).toFixed(2) + "M";
                            } 
                            var index = pageindex * pagesize - pagesize + i + 1;//正序
                           
                            $table.find("tbody").append('<tr data-id="' + item.ID + '" >'
                                 +(allowDownload=="True"?'<td><label class="checkbox inline"><input type="checkbox" name="cb" value="" > </label></td>':"")
                                 + ' <td>' +index+ '</td>'
                                 + ' <td>' + item.Name+ '</td>'
                                 + ' <td>' + item.CourseTypeName + '</td>'
                                 + ' <td>' + item.SubjectName+ '</td>' 
                                 + ' <td>' + item.GradeName + '</td>' 
                                 +  (resourceClassModel_Json.NotExistYear==false?" <td> "+(item.YearName==null?"":item.YearName)+"</td>":"") 
                                 +  (resourceClassModel_Json.NotExistOrder==false?" <td> "+(item.VloumeName==null?"":item.VloumeName)+"</td>":"") 
                                 +  (resourceClassModel_Json.NotExistVloume==false?" <td> "+(item.OrderName==null?"":item.OrderName)+"</td>":"") 
                                 + ' <td>' + item.FileName + '</td>'
                                 + ' <td>' + size + '</td>'
                                 + (IsAdmin?' <td><a data-type="number" href="javascript:;" data-enum="select"></a><br/><a data-type="number2" href="javascript:;" data-enum="download"></a></td>':"")
                                 + ' <td>' + item.UserName + '</td>'
                                 + ' <td>'+item.CreationDateStr+'</td>' 
                                 + ' <td><a href="javascript:;" data-type="select">查看</a>'
                                 +(item.PrintUrl==""?"": ' <a href="'+item.PrintUrl+'" target="_blank" >打印</a>')
                                 +(allowDownload=="True"?' <a href="javascript:;" data-href="DownloadFilePage?path='+encodeURI(item.FileNamePath)+'&name='+encodeURI(item.FileName)+'&ids='+ item.ID +'" data-type="download">下载</a>':'')
                                 +' <a style="display:none;" target="_blank" data-limits="byUser"  href="UpdateResourceInfo?resourceInfoId='+ item.ID +'">修改</a>'
                                 +' <a style="display:none;" data-type="delete" data-limits="byUser"  href="javascript:;">删除</a></td>'
                                 + ' </tr>');
                            $table.find("tbody").find("tr").last().data("data",item);
                        } 
                        var currentPage = data.message;
                        var pageCount = data.messageEx;
                        //初始化事件
                        InitEvent();
                        //分页控件 初始化
                        var options = {
                            bootstrapMajorVersion: 2,
                            currentPage: currentPage,
                            totalPages: pageCount,
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
                                pageindex = page;
                                GetData();
                            }
                        }
                        $('#example').show();
                        $('#example').bootstrapPaginator(options);
                    }
                } 

            });
          
        }

        //列表中事件初始化
        function InitEvent() {
            //查看
            $table.find("[data-type='select']").each(function(){
                $(this).click(function () {
                    var id = $(this).parent().parent().attr("data-id");
                    var $a=$(this).parent().parent().find("[data-type='number']")
                    $a.html(parseInt($a.html())+1);//页面浏览数加1
                    $.get("/Manage/Ajax/Pretreatment.ashx", {id:id}, function (data) {
                        if (data.result) { 
                            window.open(data.message);      
                        }
                    }, "json");
                
                });
            
            });
            //下载 
            $table.find("[data-type='download']").each(function(){
                $(this).click(function () {
                    var href = $(this).attr("data-href"); 
                    var $a=$(this).parent().parent().find("[data-type='number2']")
                    $a.html(parseInt($a.html())+1);//页面下载数加1
                    location.href=href;
                });
            
            });
            //检查是否有 修改和删除的权限
            $table.find("tbody").find("tr").each(function(){
                var $this=$(this);
                var id=$this.attr("data-id");
                $.get("/Manage/Ajax/CheckUser_UpdateResourceInfo.ashx",
                    {resourceInfoId:id},
                    function(data){
                        if(data.result){
                            $this.find("[data-limits='byUser']").show();
                        }else{
                            $this.find("[data-limits='byUser']").remove();
                        }
                
                    },"json");
            });
            //查询浏览数、下载数 
            if(IsAdmin){ 
                $table.find("tbody").find("tr").each(function(){
                    var $this=$(this);
                    var id=$this.attr("data-id");
                    $.get("/Manage/Ajax/GetResourceRecordCount.ashx",
                        {id:id},
                        function(data){
                            if(data.result){
                                $this.find("[data-type='number']").html(data.message);
                                $this.find("[data-type='number2']").html(data.messageEx);
                                if(parseInt(data.message)>0){
                                    $this.find("[data-enum]").click(function(){
                                        var dataenum=$(this).attr("data-enum");
                                        layer.open({
                                            title: (dataenum=="select"?"浏览":"下载")+'记录查看',
                                            type: 2,
                                            area: ['700px', '450px'],
                                            fixed: false, //不固定
                                            maxmin: true,
                                            content: 'ResourceRecordManage?id=' + id +"&type="+dataenum
                                        });
                                    });
                                }
                           
                            } 
                
                        },"json");
                });
            }
            //删除事件
            $table.find("tbody").find("[data-type='delete']").click(function(){
                if (window.confirm('确定删除吗？')) {
                    var $this=$(this);
                    var id = $(this).parent().parent().attr("data-id");
                    $.post("/Manage/Ajax/UpdateDisabled.ashx", { type: "resourceinfo", id: id, value: "True" }, 
                        function(data){
                            console.log(data);
                            if(data.result){
                                GetData();//刷新
                            }  
                        },"json"); 
                }
              
            });
        }

        //上传资料
        function Upload(){ 
            var year =!resourceClassModel_Json.NotExistYear? $divYear.find(".on").attr("data-id"):"0";
            var courseType = !resourceClassModel_Json.NotExistCourseType?$divCourseType.find(".on").attr("data-id"):"0";
            var subject =!resourceClassModel_Json.NotExistSubject? $divSubject.find(".on").attr("data-id"):"0";
            var grade =!resourceClassModel_Json.NotExistGrade? $divGrade.find(".on").attr("data-id"):"0";
            var order="0";
            var vloume ="0";

            var orderBool=true;
            var vloumeBool=true;
            //检查 课程类型限制的 讲次、册号
            GetCourseTypeArea(courseType,function(data){
                if(data.result){
                    if(data.list.length>0){
                        if(CheckCourseTypeArea_O_V(data.list,"order")){
                            order = !resourceClassModel_Json.NotExistOrder?$divOrder.find(".on").attr("data-id"):"0";
                            if(!resourceClassModel_Json.NotExistOrder&&order=="0"){ 
                                orderBool=false; 
                            }
                        }
                        if(CheckCourseTypeArea_O_V(data.list,"vloume")){
                            vloume = !resourceClassModel_Json.NotExistVloume?$divVloume.find(".on").attr("data-id"):"0";
                            if(!resourceClassModel_Json.NotExistVloume&&vloume=="0"){ 
                                vloumeBool=false; 
                            } 
                        }
                        
                        
                    }
                }
            });
           
            //视频上限大小
            var maxVideoSize=$("#togglePages").find("[data-id='"+resourceClassId+"']").attr("data-remark"); 

            if(!resourceClassModel_Json.NotExistYear&&year=="0"){
                alert("请选择具体的年份！");
                return;
            }
            if(!resourceClassModel_Json.NotExistCourseType&&courseType=="0"){
                alert("请选择具体的课程！");
                return;
            }
            if(!resourceClassModel_Json.NotExistSubject&&subject=="0"){
                alert("请选择具体的科目！");
                return;
            }
            if(!resourceClassModel_Json.NotExistGrade&&grade=="0"){
                alert("请选择具体的年级！");
                return;
            }
            if(!vloumeBool){
                alert("请选择具体的册号！");
                return;
            }
            if(!orderBool){
                alert("请选择具体的讲次！");
                return;
            }
         
            layer.open({
                title:'资源上传',
                type: 2,
                area: ['700px', '450px'],
                fixed: false, //不固定
                maxmin: true,
                content: 'UploadPage?resourceClassId='+resourceClassId
                    +'&year='+year
                    +"&courseType="+courseType 
                    +"&subject="+subject
                    +"&grade="+grade 
                    +"&order="+order 
                    +"&vloume="+vloume 
                    +"&maxVideoSize="+maxVideoSize
            });
        }
        
        //批量下载
        function BatchDownload(){
            $("#BatchDownload").click(function(){
                var arr = [];
                var ids="";
                $("input[name='cb']:checked").each(function () {
                    var info=$(this).parent().parent().parent().data("data");  
                    arr.push({Name:info.Name,FileName:info.FileName,FilePath:info.FileNamePath});
                    ids+=info.ID+",";
                    var $a=$(this).parent().parent().parent().find("[data-type='number2']")
                    $a.html(parseInt($a.html())+1);//页面下载数加1
                });
                if (arr.length == 0) {
                    alert("请勾选需要下载的资源！");
                    return;
                }
                //console.log(arr);
                var obj= JSON.stringify(arr); 
                layer.load();
                $.post("/Manage/Ajax/ZipFiles.ashx",{ResourceInfoObjList:obj},function(data){
                    layer.closeAll('loading');
                    if(data.result){  
                        location.href="DownloadFilePage?path="+data.message+"&name=批量下载文件.rar&ids="+ids;
                    }else{
                        alert(data.message);
                    }
                },"json");
            });

        }

        //查询 课程类型对应的讲次、册号范围
        function GetCourseTypeArea(courseTypeId,Callback){  
            $.ajax({  
                type: "post",  
                url: "/Manage/Ajax/GetCourseTypeArea.ashx",  
                cache:false,  
                async:false,  
                data:{courseTypeId:courseTypeId},
                dataType: "json",  
                success: function(data){ 
                    Callback(data);
                } 

            });
        }
    </script>
</asp:Content>
