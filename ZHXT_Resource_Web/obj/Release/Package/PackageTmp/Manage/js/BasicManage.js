var pageIndex = 1;
var pageSize = 10;
var $table1 = $("#table1");
var type = "";
var disabled = "";
var $disabled = $("[data-id='disabled']");

var InitEventEx = null;
function Init(t,i,s,d) {
    type = t;
    pageIndex = i;
    pageSize = s;
    disabled = d;
    GetData();
    disabledFunc();//禁用
}
function GetData() {
    $.get("/Manage/Ajax/GetAllDataByPage.ashx", { pageIndex: pageIndex, pageSize: pageSize, type: type, disabled: disabled },
        function (data) {
            console.log(data);
            if (data.result) {
                if (data.list.length > 0) {
                    appendHtml(data);
                    var options = {
                        bootstrapMajorVersion: 2,
                        currentPage: data.pageIndex,
                        totalPages: data.totalPage,
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
                            pageIndex = page;
                            GetData();
                        }
                    }
                    $('#example').bootstrapPaginator(options);
                }
            }

        }, "json");
}

//填充内容
function appendHtml(data) {
    $table1.find("tbody").html("");
    for (var i = 0; i < data.list.length; i++) {
        var item = data.list[i];
        var index = pageIndex * pageSize - pageSize + i + 1;//正序
        var disabled = item.Disabled;
        // index = data.pageCount - index + 1;//倒序
        $table1.find("tbody").append(' <tr data-id="' + item.ID + '" class="' + (disabled ? "disabled" : "enabled") + '"> '
             + '<td><label class="checkbox inline"><input type="checkbox" name="cb" value="" > </label></td>'
            + '<td>' + index + '</td>'
            + '<td>' + item.Name + '</td>'
            + '<td><input data-type="displayindex" data-value="' + item.DisplayIndex + '" style="width:50px;" type="number" value="' + item.DisplayIndex + '" /></td>'
            + '<td><a data-type="edit" style="display:none;" href="javascript:;">编辑</a>&nbsp;&nbsp;<a data-type="delete" href="javascript:;" data-disabled="' + disabled + '">' + (disabled ? "启用" : "禁用") + '</a>&nbsp;&nbsp;<a data-type="editcoursetypearea" style="display:none;" href="javascript:;">册号讲次范围管理</a></td>'
            + ' </tr>');
    }
    InitEvent();
}
//初始化事件
function InitEvent() {
    var $displayindex = $table1.find("[data-type='displayindex']");
    var $delete = $table1.find("[data-type='delete']");
    $displayindex.blur(function () {
        var id = $(this).parent().parent().attr("data-id");
        var value = $(this).val();
        if (value != $(this).attr("data-value")) {
            $.post("/Manage/Ajax/UpdateDisplayIndex.ashx", { type: type, id: id, value: value },
          function (data) {
              if (data.result) {
                  alert("修改成功！");
                  GetData();//刷新
              }
          }, "json")
        }

    });
    $delete.click(function () {

        if (window.confirm('确定'+$(this).html()+'吗？')) {
            var id = $(this).parent().parent().attr("data-id");
            var disabled = $(this).attr("data-disabled") == "true" ? "false" : "true";
            $.post("/Manage/Ajax/UpdateDisabled.ashx", { type: type, id: id, value: disabled },
                function (data) {
                    if (data.result) {
                        alert("操作成功！");
                        GetData();//刷新
                    }

                }, "json");
        }
    }); 
    if (InitEventEx != null) {
        InitEventEx($table1);
    }
}

//禁用&启用 选中
function disabledFunc() {
    $disabled.click(function () {
        var arr = [];
        var $this=$(this);
        $("input[name='cb']:checked").each(function () {
            arr.push($(this).parent().parent().parent().attr("data-id"));
        });
        if (arr.length == 0) {
            alert("请勾选操作的数据!");
            return;
        }
        if (window.confirm('确定' + $this.html()+ '吗？')) {
            var disabled = $this.attr("data-disabled");
            for (var i = 0; i < arr.length; i++) {
                var id = arr[i];
                $.post("/Manage/Ajax/UpdateDisabled.ashx", { type: type, id: id, value: disabled },
               function (data) {
                   if (data.result) { 
                       GetData();//刷新
                   }

               }, "json");
            }
        }
        
    })
}