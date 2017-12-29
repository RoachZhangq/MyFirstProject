//获取资料类型
function GetResourceClass(obj) {
    $.get("/Manage/Ajax/GetResourceClassByUser.ashx", {}, function (data) {
        if (data.result) {
            var html = '';
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                html += '<option value="' + item.ID + '">' + item.Name + '</option>';
            }
            obj.html(html);
        }
    }, "json");
}

//获取校区 第一级
function GetCampus_ZHXT_First(obj, obj2) {
    $.get("/Manage/Ajax/GetCampusInfo.ashx", { type: "first" }, function (data) {
        if (data.result) {
            var html = '<option value="-1">请选择</option>';
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                html += '<option value="' + item.CampusID + '">' + item.Name + '</option>';
            }
            obj.html(html);
            obj.change(function () {
                var id = $(this).val();
                GetCampus_ZHXT_Second(obj2, id);
            });
        }
    }, "json");
}
//获取校区 第二级
function GetCampus_ZHXT_Second(obj, campusId) {
    $.get("/Manage/Ajax/GetCampusInfo.ashx", { type: "second", campusId: campusId }, function (data) {
        if (data.result) {
            var html = '<option value="-1">请选择</option>';
            for (var i = 0; i < data.list.length; i++) {
                var item = data.list[i];
                html += '<option value="' + item.ID + '">' + item.Name + '</option>';
            }
            obj.html(html);
        }
    }, "json");
}


function GetGrade(obj) {
    var o = new objGrade();
    o.getAll(function (data) {
        appendHtml(data, obj);
    });
}
function GetSubject(obj) {
    var o = new objSubject();
    o.getAll(function (data) {
        appendHtml(data, obj);
    });
}
function GetCourseType(obj) {
    var o = new objCourseType();
    o.getAll(function (data) {
        appendHtml(data, obj);
    });
}
function GetYear(obj) {
    var o = new objYear();
    o.getAll(function (data) {
        appendHtml(data, obj);
    });
}
function GetOrder(obj) {
    var o = new objOrder();
    o.getAll(function (data) {
        appendHtml(data, obj);
    });
}
function appendHtml(data, obj) {
    var html = '<option value="-1">请选择</option>';
    for (var i = 0; i < data.list.length; i++) {
        var item = data.list[i];
        html += '<option value="' + item.ID + '">' + item.Name + '</option>';
    }
    obj.html(html);
}

//保存资源
function SaveInfo(save, name, selResourceClass, selCampusSecond, selGrade, selSubject, selCourseType, selYear, selOrder,  txtRemark, chkRecord) {
    save.click(function () { 
        var nameVal =$.trim(name.val());
        var selResourceClassVal = selResourceClass.val();
        var selCampusSecondVal = selCampusSecond.val();
        var selGradeVal = selGrade.val();
        var selSubjectVal = selSubject.val();
        var selCourseTypeVal = selCourseType.val();
        var selYearVal = selYear.val();
        var selOrderVal = selOrder.val();
        var txtRemarkVal = txtRemark.val();
        var chkRecordVal = chkRecord.is(':checked');
        //console.log(nameVal);
        //console.log(selResourceClassVal);
        //console.log(selCampusSecondVal);
        //console.log(selGradeVal);
        //console.log(selSubjectVal);
        //console.log(selCourseTypeVal);
        //console.log(selYearVal);
        //console.log(selOrderVal);
        //console.log(txtRemarkVal);
        //console.log(FileName);
        //console.log(NewFileName);
        //console.log(FileContentLength);
        if (nameVal == "") {
            alert("请输入资源名称");
            return;
        }
        if (selResourceClassVal <= 0) {
            alert("请选择资源类型");
            return;
        }
        if (selCampusSecondVal <= 0) {
            alert("请选择校区");
            return;
        }
        if (selGradeVal <= 0) {
            alert("请选择年级");
            return;
        }
        if (selSubjectVal <= 0) {
            alert("请选择科目");
            return;
        }
        if (selCourseTypeVal <= 0) {
            alert("请选择课程");
            return;
        }
        if (selYearVal <= 0) {
            alert("请选择年份");
            return;
        }
        if (selOrderVal <= 0) {
            alert("请选择讲次");
            return;
        }
        if (FileName == "" || NewFileName == "") {
            alert("请先上传文件");
            return;
        }
        $.post("/Manage/Ajax/AddResourceInfo.ashx",
            {
                NameVal: nameVal,
                ResourceClassVal: selResourceClassVal,
                CampusSecondVal: selCampusSecondVal,
                GradeVal: selGradeVal,
                SubjectVal: selSubjectVal,
                CourseTypeVal: selCourseTypeVal,
                YearVal: selYearVal,
                OrderVal: selOrderVal,
                RemarkVal: txtRemarkVal,
                FileName: FileName,
                NewFileName: NewFileName,
                FileContentLength: FileContentLength
            },
            function (data) {
                if (data.result) {
                    alert("保存成功！");
                    location.reload();
                    if (!chkRecordVal) {
                        location.reload();
                    } else {
                        FileName = "";
                        NewFileName = "";
                        FileContentLength = "";
                        $("#filePrompt").html("");
                    }
                } else {
                    alert(data.message);
                }
         }, "json");

    });
}