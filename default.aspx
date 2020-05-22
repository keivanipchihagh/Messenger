<!--Author: Keivan Ipchi Hagh
Author URL: http://keivanipchihagh.ir/
-->
<!DOCTYPE HTML>
<html lang="en">

<head>
    <title>Messenger | Login</title>
    <!-- Meta tag Keywords -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta charset="UTF-8" />
    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <!-- Meta tag Keywords -->
    <!-- css files -->
    <link rel="stylesheet" href="css/default/main.css" type="text/css" media="all" />
    <link rel="stylesheet" href="css/default/style.css" type="text/css" media="all" />
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>

    <script>
        addEventListener("load", function () {
            setTimeout(hideURLbar, 0);
        }, false);

        function hideURLbar() {
            window.scrollTo(0, 1);
        }

    </script>
</head>

<body>
    <div class="main-bg container-login100" style="padding-bottom: 0px">
        <div class="sub-main-w3 wrap-login100">
            <!-- title -->
            <div style="width: 100%; padding: 1.5em">
                <h1 style="padding: 0px"><img src="assets/img/favicon.png" style="width: 60px; height: 60px; vertical-align: middle; margin: 10px" /><span>Messenger</span></h1>
            </div>
            <!-- vertical tabs -->
            <div class="vertical-tab">
                <div id="section1" class="section-w3ls" style="margin-bottom: 1px">
                    <input type="radio" name="sections" id="option1" checked>
                    <label for="option1" class="icon-left-w3pvt" onclick="setArticle(1)"><span class="fa fa-user-circle" aria-hidden="true"></span>Login</label>
                    <article id="article1">
                        <form id="form" name="form" action="home.aspx" method="post">
                            <input id="identifier" name="identifier" type="hidden" value="login" />
                            <h3 class="legend" style="font-weight: bold">~ Login ~</h3>
                            <div id="alertBox"></div>
                            <div class="input">
                                <span class="fa fa-envelope-o" aria-hidden="true"></span>
                                <input type="email" placeholder="Email Address" name="email" id="email" required="required" maxlength="50" />
                            </div>
                            <div class="input">
                                <span class="fa fa-key" aria-hidden="true"></span>
                                <input type="password" placeholder="Password" name="pass" id="pass" required="required" maxlength="50" />
                                <i class="fa fa-eye" aria-hidden="true" title="Show/Hide Password" onclick="this.classList.toggle('fa-eye-slash'); changePassVisual()"></i>
                            </div>
                            <div style="padding: 5px">
                                <div style="float: right">
                                    <label for="rememberMe" style="cursor: pointer; padding: 0px; border: none; display: inline-block; font-size: initial; width: 100%"><input id="rememberMe" name="rememberMe" type="checkbox" style="margin-right: 3px" />Remember Me</label>
                                </div>
                            </div>
                            <button type="submit" class="btn submit" onclick="return login()">Login</button>
                            <a class="bottom-text-w3ls" style="margin-top: 22px; cursor: pointer">No Activation Email Yet?</a>
                        </form>
                    </article>
                </div>
                <div id="section2" class="section-w3ls" style="margin-bottom: 1px">
                    <input type="radio" name="sections" id="option2">
                    <label for="option2" class="icon-left-w3pvt" onclick="setArticle(2)"><span class="fa fa-pencil-square" aria-hidden="true"></span>Sign Up</label>
                    <article id="article2">
                        <%--<div id="form">
                            <input id="identifier" name="identifier" type="hidden" value="signup" />
                            <h3 class="legend" style="font-weight: bold">~ Sign Up ~</h3>
                            <div id="alertBox"></div>
                            <div class="input">
                                <span class="fa fa-user-o" aria-hidden="true"></span>
                                <input type="text" placeholder="Full Name" name="fullname" id="fullname" autocomplete="off" required="required" maxlength="50" />
                                <span class="fa fa-user-o" aria-hidden="true"></span>
                                <input type="text" placeholder="User Name" name="username" id="username" autocomplete="off" required="required" maxlength="30" />
                            </div>
                            <div class="input">
                                <span class="fa fa-user-o" aria-hidden="true"></span>
                                <input type="text" placeholder="Email Address" name="email" id="email" autocomplete="off" required="required" maxlength="50" />
                            </div>
                            <div class="input" style="margin-bottom: 0px">
                                <span class="fa fa-key" aria-hidden="true"></span>
                                <input type="password" placeholder="Password" name="pass" id="pass" autocomplete="off" required="required" maxlength="50" />
                                <span class="fa fa-key" aria-hidden="true"></span>
                                <input type="password" placeholder="Confirm Password" name="confirm" id="confirm" autocomplete="off" style="width: 95%" required="required" maxlength="50" />
                                <i class="fa fa-eye" aria-hidden="true" title="Show/Hide Password" onclick="this.classList.toggle('fa-eye-slash'); changePassVisual()"></i>
                            </div>
                            <div class="input100 validate-input" style="text-align: center; height: auto; margin-top: 10px; text-align: center; width: 100%">
                                <label for="termsOfService" style="cursor: pointer; padding: 0px; border: none; display: inline-block; font-size: initial; width: 100%"><input type="checkbox" name="termsOfService" id="termsOfService" required="required" style="cursor: pointer; margin-right: 5px; font-size: 14px">I agree with <a style="text-decoration: underline; color: #149ddd; font-weight: bold" onclick="document.getElementById('termsOfServiceModel').classList.toggle('hideModel');">Terms Of Services</a></label>
                            </div>
                            <button type="submit" class="btn submit" style="margin-top: 8.5px" onclick="signup()">Sign Up</button>
                        </div>--%>
                    </article>
                </div>
                <div id="section3" class="section-w3ls" style="margin-bottom: 1px">
                    <input type="radio" name="sections" id="option3">
                    <label for="option3" class="icon-left-w3pvt" onclick="setArticle(3)"><span class="fa fa-lock" aria-hidden="true"></span>Forgot Password?</label>
                    <article id="article3">
                        <%--<div id="form">
                            <input id="identifier" name="identifier" type="hidden" value="recoverPassword" />
                            <h3 class="legend last" style="font-weight: bold">~ Reset Password ~</h3>
                            <div id="alertBox"></div>
                            <p class="para-style">No worries, Enter your email address below and we'll send you an email with instructions <u>if your email is registered</u>.</p>
                            <p class="para-style-2">
                                <strong>Note: </strong> Consider in mind, the email link will expire in 24 hours.
                            </p>
                            <div class="input">
                                <span class="fa fa-envelope-o" aria-hidden="true"></span>
                                <input type="email" placeholder="Email Address" name="email" id="email" required="required" maxlength="50" />
                            </div>
                            <button type="submit" class="btn submit last-btn" style="margin-bottom: 4px" onclick="recoverPassword()">Send Reset Link</button>
                        </div>--%>
                    </article>
                </div>
            </div>
            <!-- //vertical tabs -->
            <div class="clear"></div>
        </div>

        <!-- Terms of Services -->
        <div class="modal-dialog hideModel" id="termsOfServiceModel" style="background-color: white; margin: auto; width: 90%; border-radius: 5px">
            <div style="padding: 20px">
                <h2 style="font-size: 25px; padding: 10px; font-weight: bold">Website Terms and Conditions of Use</h2>
                <hr style="margin: 5px 0px 5px 0px" />
                <h5 style="font-size: 20px; padding: 5px 0px 5px 10px">1. Terms</h5>
                <p style="font-size: 15px">By accessing this Website, accessible from <a href="http://messenger.keivanipchihagh.ir/" target="_blank">messenger.keivanipchihagh.ir</a>, you are agreeing to be bound by these Website Terms and Conditions of Use and agree that you are responsible for the agreement with any applicable local laws. If you disagree with any of these terms, you are prohibited from accessing this site. The materials contained in this Website are protected by copyright and trade mark law.</p>
                <h5 style="font-size: 20px; padding: 5px 0px 5px 10px">2. Disclaimer</h5>
                <p style="font-size: 15px">All of the personal informations submitted in this website are well encrypted and proper security measures have been put in action in order maintain users privacy as isolated as possible. However, this website cannot grand 100% security for its users as it\'s not an enterprise project. Proper counter-meassures have been put in effect to create a safe sandbox for users. Security updates will be realeased every month to update to latest technologies and add features to the platform.</p>
                <h5 style="font-size: 20px; padding: 5px 0px 5px 10px">3. Limitations</h5>
                <p style="font-size: 15px">Messenger or its suppliers will not be hold accountable for any damages that will arise with the use or inability to use the materials on Messenger\'s Website, even if Messenger or an authorize representative of this Website has been notified, orally or written, of the possibility of such damage. Some jurisdiction does not allow limitations on implied warranties or limitations of liability for incidental damages, these limitations may not apply to you.</p>
                <h5 style="font-size: 20px; padding: 5px 0px 5px 10px">4. Revisions and Errata</h5>
                <p style="font-size: 15px">The materials appearing on Messengers Website may include technical, typographical, or photographic errors. Messenger will not promise that any of the materials in this Website are accurate, complete, or current. Messenger may change the materials contained on its Website at any time without notice. Messenger does not make any commitment to update the materials.</p>
                <hr style="margin: 5px 0px 5px 0px" />
                <div style="width: 100%; margin-bottom: 30px">
                    <input type="button" value="Close" id="closeModel" style="float: right; padding: 10px; background-color: #149ddd; border-radius: 5px; cursor: pointer" onclick="document.getElementById('termsOfServiceModel').classList.toggle('hideModel');" />
                </div>
            </div>
        </div>

        <!-- copyright -->
        <footer style="position: fixed; bottom: 0;">
            <div class="copyright">
                <h2>&copy; 2020 Messenger. All rights reserved | Design by
                    <a href="http://keivanipchihagh.ir/" target="_blank" style="text-decoration: underline">Keivan Ipchi Hagh</a>
                </h2>
            </div>
        </footer>
        <!-- //copyright -->
    </div>

</body>

</html>
