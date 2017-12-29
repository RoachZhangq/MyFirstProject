<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZHXT_Resource_Web.Manage.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>至慧后台登录</title>
<link type="text/css" href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
<link type="text/css" href="bootstrap/css/bootstrap-responsive.min.css" rel="stylesheet">
<link type="text/css" href="css/theme.css" rel="stylesheet">
<link type="text/css" href="images/icons/css/font-awesome.css" rel="stylesheet">
</head>
<body>
<div class="navbar navbar-fixed-top">
  <div class="navbar-inner">
    <div class="container"> <a class="btn btn-navbar" data-toggle="collapse" data-target=".navbar-inverse-collapse"> <i class="icon-reorder shaded"></i> </a> <a class="brand" href="#"><img src="images/zh-logo.png"></a> 
      <!-- /.nav-collapse --> 
    </div>
  </div>
  <!-- /navbar-inner --> 
</div>
<!-- /navbar -->
<div class="wrapper">
  <div class="container"> 	
    <div class="row">
      <div class="module module-login span4 offset4">
        <form class="form-vertical">
          <div class="module-head">
            <h3>账号登录</h3>
          </div>
          <div class="module-body">
            <div class="control-group">
              <div class="controls row-fluid">
                <input class="span12" type="text" value="" id="inputName" placeholder="用户名"/>
              </div>
            </div>
            <div class="control-group">
              <div class="controls row-fluid">
                <input class="span12" type="password" value="" id="inputPassword" placeholder="密码"/>
              </div>
            </div>
          </div>
          <div class="module-foot">
            <div class="control-group">
              <div class="controls clearfix">
                <a id="login"  href="javascript:;" class="btn btn-primary pull-right">登录</a>
                <%--<label class="checkbox">
                  <input type="checkbox">
                  记住账号 </label>--%>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</div>
<!--/.wrapper--> 
<script src="scripts/jquery-1.9.1.min.js" type="text/javascript"></script> 
<script src="scripts/jquery-ui-1.10.1.custom.min.js" type="text/javascript"></script> 
<script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>


    <script>
        var $inputName = $("#inputName");
        var $inputPassword = $("#inputPassword");
        var $login = $("#login");
        $login.click(function () {
            var name = $inputName.val();
            var pwd = $inputPassword.val();
            $.post("/Manage/Ajax/Login.ashx", {name:name,pwd:pwd}, function (data) {
                if (data.result) {
                    location.href = "/Manage/Home";
                } else {
                    alert("账户或密码错误！");
                }
            }, "json");
        });
        $("body").keydown(function () {
            if (event.keyCode == "13") {
                $login.click();
            }
        });
    </script>
</body>
</html>
