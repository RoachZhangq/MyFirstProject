
//ResourceClass
function objResourceClass() {

}
objResourceClass.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllData.ashx", { type: "resourceclass" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}

//Year
function objYear() {

}
objYear.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllData.ashx", {type:"year"}, function (data) {
        if (callback != null)
            callback(data); 
    }, "json");
}

//CourseType
function objCourseType() {

}
objCourseType.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllData.ashx", { type: "coursetype" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}
//Subject
function objSubject() {

}
objSubject.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllData.ashx", { type: "subject" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}

//Grade
function objGrade() {

}
objGrade.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllData.ashx", { type: "grade" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}


//Order
function objOrder() {

}
objOrder.prototype.getAll = function (callback) { 
    $.get("/Manage/Ajax/GetAllData.ashx", { type: "order" }, function (data) { 
        if (callback != null)
            callback(data);
    }, "json");
}


//Vloume
function objVloume() {

}
objVloume.prototype.getAll = function (callback) {
    $.get("/Manage/Ajax/GetAllData.ashx", { type: "vloume" }, function (data) {
        if (callback != null)
            callback(data);
    }, "json");
}
