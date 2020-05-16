<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Messenger.Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title id="title">Messenger | Login</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>

    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <script type="text/javascript"> 
        /** Disable Back Button */
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

    <!-- StyleSheetes -->
    <link rel="stylesheet" type="text/css" href="css/util.css">
    <link rel="stylesheet" type="text/css" href="css/main.css">
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <!-- Popper JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
</head>
<body>
    <div id="mainContainer" class="limiter">
        <div class="container-login100">
            <div class="wrap-login100" style="box-shadow: white 0px 0px 10px 3px">
                <form class="login100-form validate-form" id="form" name="form" action="Home.aspx" method="post" onsubmit="return login()">
                    <input name="identifier" id="identifier" type="hidden" value="login" />

                    <span class="login100-form-title p-b-20" style="text-align: left">
                        <img class="loginImg" src="assets/img/favicon.png" />
                        <h3>Messenger</h3>
                        <p style="font-family: 'Raleway'">Web-Based Messaging platform</p>
                    </span>

                    <div id="alertBox"></div>

                    <div class="wrap-input100 validate-input">
                        <input class="input100" type="text" name="email" id="email" maxlength="50" placeholder="Email Address" required="required">
                    </div>

                    <div class="wrap-input100 validate-input">
                        <span class="btn-show-pass"><i class="fa fa-eye" aria-hidden="true" title="Show/Hide Password" onclick="this.classList.toggle('fa-eye-slash'); changePassVisual()"></i></span>
                        <input class="input100" type="password" name="pass" maxlength="50" id="pass" placeholder="Password" required="required">
                    </div>

                    <div class="container-login100-form-btn">
                        <div class="wrap-login100-form-btn">
                            <div class="login100-form-bgbtn"></div>
                            <button class="login100-form-btn">Login</button>
                        </div>
                    </div>

                    <div class="text-center p-t-20">
                        <span class="txt1">Not a Member yet?</span>
                        <a class="txt2" onclick="switchLayout('signup')">Sign up!</a>
                        <br />
                        <span class="txt1">Forgot Password?</span>
                        <a class="txt2" onclick="switchLayout('recoverPassword')">Recover Now!</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <div class="modal" id="resendEmailModel">
        <div class="modal-dialog" style="box-shadow: white 0px 0px 10px 3px; border-radius: .3rem">
            <div class="modal-content" style="border: 1.5px solid white;">

                <!-- Modal Header -->
                <div class="modal-header">
                    <div id="alertBox"></div>
                    <h4 class="modal-title">Resend Activation Email</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="wrap-input100 validate-input">
                        <input class="input100" type="text" name="emailModel" id="emailModel" maxlength="50" placeholder="Email Address" required="required">
                    </div>
                    Note: Email will be sent to registerd acoounts only!
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-success" data-dismiss="modal" onclick="reSendActivationCode(document.getElementById('emailModel').value)">Send</button>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
