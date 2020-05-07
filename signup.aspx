<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="Messenger.signup" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Messenger | SingUp</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">

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
                <form class="login100-form" id="form" name="form" action="home.aspx" method="post">
                    <input name="identifier" id="identifier" type="hidden" value="signup" />

                    <!-- Header -->
                    <span class="login100-form-title p-b-26">
                        <img class="loginImg" src="assets/img/favicon.png" />
                        <h3 style="float: left">Messenger</h3>
                        <p style="font-family: 'Raleway'">Web-Based Messaging platform</p>
                    </span>

                    <!-- Full Name -->
                    <div class="wrap-input100">
                        <input class="input100" type="text" name="fullname" id="fullname" required="required" placeholder="Full Name">
                    </div>

                    <!-- User Name -->
                    <div class="wrap-input100">
                        <input class="input100" type="text" name="username" id="username" required="required" placeholder="User Name">
                    </div>

                    <!-- Email -->
                    <div class="wrap-input100">
                        <input class="input100" type="text" name="email" id="email" required="required" placeholder="Email Address">
                    </div>

                    <!-- Password -->
                    <div class="wrap-input100">
                        <span class="btn-show-pass">
                            <i class="fa fa-eye" aria-hidden="true" onclick="this.classList.toggle('fa-eye-slash');"></i>
                        </span>
                        <input class="input100" type="password" name="pass" id="pass" required="required" placeholder="Password">
                    </div>

                    <!-- Confirmation -->
                    <div class="wrap-input100">
                        <input class="input100" type="password" name="confirm" id="confirm" required="required" placeholder="Confirm Password">
                    </div>

                    <!-- Footer -->
                    <div class="container-login100-form-btn">                        
                        <div class="wrap-login100-form-btn">                            
                            <div class="login100-form-bgbtn"></div>                            
                            <button class="login100-form-btn" onclick="validateSubmit()">Sign up</button>
                        </div>
                    </div>

                    <div class="text-center p-t-20">
                        <span class="txt1">Already have an account?</span>
                        <a class="txt2" href="#" onclick="switchDefault()">Login!</a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</body>
</html>