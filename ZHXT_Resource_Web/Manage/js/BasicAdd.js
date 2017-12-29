

 
    var $name = $("#name");
    var $displayindex = $("#displayindex");
    var type = "";

  

    function Init(t) {
        type = t;
        $("#save").click(function () {
            var nameVal = $.trim($name.val());
            var displayindexVal = $displayindex.val();
            if (nameVal == "") {
                alert("请输入名称");
                return;
            }
            $.post("/Manage/Ajax/AddTableInfo.ashx", { name: nameVal, displayindex: displayindexVal, type: type },
                function (data) {
                    if (data.result) {
                        alert("添加成功");
                        location.reload();
                    } else {
                        alert(data.message);
                    }
                }, "json");
        });
    }
 