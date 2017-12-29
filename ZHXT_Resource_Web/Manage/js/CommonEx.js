//Year
function objYear() {

}
objYear.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllDataEx.ashx", { type: "year" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}

//CourseType
function objCourseType() {

}
objCourseType.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllDataEx.ashx", { type: "coursetype" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}
//Subject
function objSubject() {

}
objSubject.prototype.getAll = function (courseTypeId,callback) {
    $.get("/Manage/Ajax/GetAllDataEx.ashx", { type: "subject", coursetypeId: courseTypeId }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}

//Grade
function objGrade() {

}
objGrade.prototype.getAll = function (courseTypeId,subjectId, callback) {
    $.get("/Manage/Ajax/GetAllDataEx.ashx", { type: "grade", coursetypeId: courseTypeId,subjectId: subjectId }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}


//Order
function objOrder() {

}
objOrder.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllDataEx.ashx", { type: "order" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}


//Vloume
function objVloume() {

}
objVloume.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllDataEx.ashx", { type: "vloume" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}
