<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Messenger.login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Messenger | Login</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>

    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

    <link rel="stylesheet" type="text/css" href="css/util.css">
    <link rel="stylesheet" type="text/css" href="css/main.css">
    <!--===============================================================================================-->
</head>
<body>
    <div id="mainContainer" class="limiter">
        <div class="container-login100">
            <div class="wrap-login100" style="box-shadow: white 0px 0px 10px 3px">
                <form class="login100-form validate-form">
                    <input name="identifier" id="identifier" type="hidden" value="login" />
                    <span class="login100-form-title p-b-26">
                        <img class="loginImg" src="assets/img/favicon.png" />
                        <h3 style="float: left">Messenger</h3>
                        <p style="font-family: 'Raleway'">Web-Based Messaging platform</p>
                    </span>

                    <div class="wrap-input100 validate-input">
                        <input class="input100" type="text" name="email" id="email" placeholder="Email Address">
                    </div>

                    <div class="wrap-input100 validate-input">
                        <span class="btn-show-pass">
                            <i class="fa fa-eye" aria-hidden="true" onclick="this.classList.toggle('fa-eye-slash');"></i>
                        </span>
                        <input class="input100" type="password" name="pass" id="pass" placeholder="Password">
                    </div>

                    <div class="container-login100-form-btn">
                        <div class="wrap-login100-form-btn">
                            <div class="login100-form-bgbtn"></div>
                            <button class="login100-form-btn">
                                Login
                            </button>
                        </div>
                    </div>

                    <div class="text-center p-t-20">
                        <span class="txt1">Don’t have an account?</span>
                        <a class="txt2" onclick="switchDefault()">Sign up!</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
