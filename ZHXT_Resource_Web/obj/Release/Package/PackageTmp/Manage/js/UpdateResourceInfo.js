var NotExistYear = "true";
var NotExistCourseType = "true";
var NotExistSubject = "true";
var NotExistGrade = "true";
var NotExistVloume = "true";
var NotExistOrder = "true";

//获取资料类型
function GetResourceClass(obj) {
    $.get("/Manage/Ajax/GetResourceClassByUser.ashx", {}, function (data) {
        if (data.result) {
            var html = '<option value="0">请选择</option>';
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                html += '<option value="' + item.ID
                    + '"  data-NotExistGrade="' + item.NotExistGrade
                    + '" data-NotExistSubject="' + item.NotExistSubject
                    + '" data-NotExistCourseType="' + item.NotExistCourseType
                    + '"data-NotExistYear="' + item.NotExistYear
                    + '"data-NotExistOrder="' + item.NotExistOrder
                    + '"data-NotExistVloume="' + item.NotExistVloume
                    + '"data-Remark="' + item.Remark
                    + '">' + item.Name + '</option>';
            }
            obj.html(html);
            //change事件
            obj.unbind("change").change(function () {
                var id = $(this).val();
                if (resourceInfoObj != null)
                {
                    if (id != resourceInfoObj.ResourceClassID) {
                        resourceInfoObj = null;
                    }
                }
                //清空
                $selCourseType.html("");
                $selSubject.html("");
                $selGrade.html("");
                var current = obj.find("[value='" + id + "']");
                
                _maxVideoSize = current.attr("data-remark")
                maxVideoSize = (_maxVideoSize == "" ? 100 : parseInt(_maxVideoSize)) * 1024 * 1024;
                $("#hintSize").html("(普通文件不能超过50MB,视频文件不得超过：" + _maxVideoSize + "MB)");

                 NotExistYear = current.attr("data-NotExistYear");
                 NotExistCourseType = current.attr("data-NotExistCourseType");
                 NotExistSubject = current.attr("data-NotExistSubject");
                 NotExistGrade = current.attr("data-NotExistGrade");
                 NotExistVloume = current.attr("data-NotExistVloume");
                 NotExistOrder = current.attr("data-NotExistOrder");

                 if (NotExistYear == "true") $selYear.parent().parent().hide(); else
                 {
                     $selYear.parent().parent().show();
                     GetYear($selYear);
                 }
                if (NotExistCourseType == "true") $selCourseType.parent().parent().hide(); else $selCourseType.parent().parent().show();
                if (NotExistSubject == "true") $selSubject.parent().parent().hide(); else $selSubject.parent().parent().show();
                if (NotExistGrade == "true") $selGrade.parent().parent().hide(); else $selGrade.parent().parent().show();
                //console.log(NotExistOrder);
               
                GetCourseType(id);
            });
            if (resourceInfoObj != null)
            {
                obj.find("[value='" + resourceInfoObj.ResourceClassID + "']").attr("selected", true); 
                obj.trigger("change");
            }
          
           
        }
    }, "json");
}

//获取年级
function GetGrade(resourceClassID, courseTypeID,subjectID) {
    $.get("/Manage/Ajax/GetGrade_limits.ashx", { resourceClassID: resourceClassID, courseTypeID: courseTypeID, subjectID: subjectID }, function (data) {
        if (data.result) {
            appendHtml(data, $selGrade);
            if (resourceInfoObj != null) {
                $selGrade.find("[value='" + resourceInfoObj.GradeID + "']").attr("selected", true);
            }
        }
    }, "json")
}
//获取科目
function GetSubject(resourceClassID, courseTypeID) {
    $.get("/Manage/Ajax/GetSubject_limits.ashx", { resourceClassID: resourceClassID, courseTypeID: courseTypeID }, function (data) {
        if (data.result) {
            appendHtml(data, $selSubject);
            $selSubject.unbind("change").change(function () {
                var id = $(this).val();
                //清空  
                $selGrade.html("");
                GetGrade(resourceClassID, courseTypeID,id);
            });
            if (resourceInfoObj != null) {
                $selSubject.find("[value='" + resourceInfoObj.SubjectID + "']").attr("selected", true);
                $selSubject.trigger("change");
            }

        }
    }, "json")
}
//获取课程
function GetCourseType(resourceClassID) { 
    $.get("/Manage/Ajax/GetCoursetype_limits.ashx", { resourceClassID: resourceClassID }, function (data) {
        if (data.result) { 
            appendHtml(data, $selCourseType);
            $selCourseType.unbind("change").change(function () {
                var id = $(this).val();
                //清空 
                $selSubject.html("");
                $selGrade.html("");
                GetSubject(resourceClassID, id);
                GetCourseTypeArea(id, function (data) {
                    if (data.result) {

                        $selOrder.find("option").remove();
                        $selOrder.parent().parent().hide();
                        $selVloume.find("option").remove();
                        $selVloume.parent().parent().hide();

                        if (data.list.length > 0) {
                            var courseTypeAreaArr = data.list;

                            if (NotExistOrder == "true") {
                                $selOrder.find("option").remove();
                                $selOrder.parent().parent().hide();
                            }
                            else {
                                if (CheckCourseTypeArea_O_V(courseTypeAreaArr, "order")) {
                                    $selOrder.parent().parent().show();
                                    GetOrder($selOrder, courseTypeAreaArr);
                                }
                              
                            }

                            if (NotExistVloume == "true") {
                                $selVloume.find("option").remove();
                                $selVloume.parent().parent().hide();
                            }
                            else {
                                if (CheckCourseTypeArea_O_V(courseTypeAreaArr, "vloume")) { 
                                    $selVloume.parent().parent().show();
                                    GetVloume($selVloume, courseTypeAreaArr);
                                }
                             

                            }
                        }  
                    }
                   
                })

            

            });
            if (resourceInfoObj != null) {
                $selCourseType.find("[value='" + resourceInfoObj.CourseTypeID + "']").attr("selected", true);
                $selCourseType.trigger("change");
            }
        }
    }, "json")
}
//获取年份
function GetYear(obj) {
    if (NotExistYear == "false") {
        var o = new objYear();
        o.getAll(function (data) {
            appendHtml(data, obj);
            if (resourceInfoObj != null) {
                obj.find("[value='" + resourceInfoObj.tbYearID + "']").attr("selected", true);
            }
        });
    }
   
}
//获取讲次
function GetOrder(obj, courseTypeAreaArr) {
    if (NotExistOrder=="false")
    {
        var o = new objOrder();
        o.getAll(function (data) {
            //console.log(data.list);
            var data_list = [];
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                for (var j = 0; j < courseTypeAreaArr.length; j++) {
                    var itemCYA = courseTypeAreaArr[j];
                    if (itemCYA.OrderID == item.ID) {
                        //判断是否已存在
                        var exist = false;
                        $.each(data_list, function (index, value) { 
                            if (value.ID == item.ID) {
                                exist = true;
                            }
                        });
                        if (!exist) data_list.push(item);
                    }
                }
            }
            data.list = data_list;
            appendHtml(data, obj);
            if (resourceInfoObj != null) {
                obj.find("[value='" + resourceInfoObj.tbOrderID + "']").attr("selected", true);
            }
        });
    }
}
//获取册号
function GetVloume(obj, courseTypeAreaArr) {
    if (NotExistVloume == "false") {
        var o = new objVloume();
        o.getAll(function (data) {
            //console.log(data.list);
            var data_list = [];
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                for (var j = 0; j < courseTypeAreaArr.length; j++) {
                    var itemCYA = courseTypeAreaArr[j];
                    if (itemCYA.VloumeID == item.ID) {
                        //判断是否已存在
                        var exist = false;
                        $.each(data_list, function (index, value) { 
                            if (value.ID == item.ID) {
                                exist = true;
                            }
                        });
                        if (!exist) data_list.push(item);
                    }
                }
            }
            data.list = data_list;
            appendHtml(data, obj);
            if (resourceInfoObj != null) {
                obj.find("[value='" + resourceInfoObj.VloumeID + "']").attr("selected", true);
            }
        });
    }
}
//填充
function appendHtml(data, obj) {
    var html = '<option value="0">请选择</option>';
    for (var i = 0; i < data.list.length; i++) {
        var item = data.list[i];
        html += '<option value="' + item.ID + '">' + item.Name + '</option>';
    }
    obj.html(html);
}
 
function Save() {
    $save.click(function () {
        var resourceClass = $selResourceClass.val();
        var year = NotExistYear=="false" ? $selYear.val(): "0";
        var courseType = NotExistCourseType == "false" ? $selCourseType.val() : "0";
        var subject = NotExistSubject == "false" ? $selSubject.val() : "0";
        var grade = NotExistGrade == "false" ? $selGrade.val() : "0";
        var order =  "0";
        var vloume = "0";
        var p_NameVal = $.trim($name.val());
        var p_RemarkVal = $txtRemark.val();

        if (NotExistYear == "false" && year == "0") {
            alert("请选择具体的年份！");
            return;
        }
        if (NotExistCourseType == "false" && courseType == "0") {
            alert("请选择具体的课程！");
            return;
        }
        if (NotExistSubject == "false" && subject == "0") {
            alert("请选择具体的科目！");
            return;
        }
        if (NotExistGrade == "false"&& grade == "0") {
            alert("请选择具体的年级！");
            return;
        }
        var orderBool = true;
        var vloumeBool = true;
        //检查 课程类型限制的 讲次、册号
        GetCourseTypeArea(courseType, function (data) {
            if (data.result) {
                if (data.list.length > 0) {
                    order = NotExistOrder == "false" ? $selOrder.val() : "0";
                    vloume = NotExistVloume == "false" ? $selVloume.val() : "0";
                    if (NotExistOrder == "false" && order == "0") {
                        orderBool = false;
                    }
                    if (NotExistVloume == "false" && vloume == "0") {
                        vloumeBool = false;
                    }
                }
            }
        });
        if (!vloumeBool) {
            alert("请选择具体的册号！");
            return;
        }
        if (!orderBool) {
            alert("请选择具体的讲次！");
            return;
        }
        $.post("/Manage/Ajax/UpdateResourceInfo.ashx",
          {
              resourceInfoId:resourceInfoId,
              NameVal: p_NameVal,
              ResourceClassVal: resourceClass, 
              GradeVal: grade,
              SubjectVal: subject,
              CourseTypeVal: courseType,
              YearVal: year,
              OrderVal: order,
              VloumeVal:vloume,
              RemarkVal: p_RemarkVal,
              FileName: FileName,
              NewFileName: NewFileName,
              FileContentLength: FileContentLength
          },
          function (data) {
              if (data.result) {
                  alert("修改成功！");
                  location.reload(); 
              } else {
                  alert(data.message);
              }
          }, "json");
    });
}


//查询 课程类型对应的讲次、册号范围
function GetCourseTypeArea(courseTypeId, Callback) {
    $.ajax({
        type: "post",
        url: "/Manage/Ajax/GetCourseTypeArea.ashx",
        cache: false,
        async: false,
        data: { courseTypeId: courseTypeId },
        dataType: "json",
        success: function (data) {
            Callback(data);
        }

    });
}