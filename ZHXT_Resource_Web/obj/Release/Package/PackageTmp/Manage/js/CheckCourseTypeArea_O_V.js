
//检查是否有设置到册号 或 讲次
function CheckCourseTypeArea_O_V(courseTypeAreaArr, type) {
    var result = false;
    if (type == "order") {
        $.each(courseTypeAreaArr, function (index, value) { 
            if (value.OrderID != null) {
                result = true; 
            }
        });
    } else if (type == "vloume") {
        $.each(courseTypeAreaArr, function (index, value) { 
            if (value.VloumeID != null) {
                result = true; 
            }
        });
    } 
    return result;
}