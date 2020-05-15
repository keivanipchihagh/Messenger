!(function ($) {
    "use strict";

    // Hero typed
    if ($('.typed').length) {
        var typed_strings = $(".typed").data('typed-items');
        typed_strings = typed_strings.split(',');
        new Typed('.typed', {
            strings: typed_strings,
            loop: true,
            typeSpeed: 100,
            backSpeed: 50,
            backDelay: 2000
        });
    }

    // Smooth scroll for the navigation menu and links with .scrollto classes
    $(document).on('click', '.nav-menu a, .scrollto', function (e) {
        if (location.pathname.replace(/^\//, '') === this.pathname.replace(/^\//, '') && location.hostname === this.hostname) {
            e.preventDefault();
            var target = $(this.hash);
            if (target.length) {

                var scrollto = target.offset().top;

                $('html, body').animate({
                    scrollTop: scrollto
                }, 1500, 'easeInOutExpo');

                if ($(this).parents('.nav-menu, .mobile-nav').length) {
                    $('.nav-menu .active, .mobile-nav .active').removeClass('active');
                    $(this).closest('li').addClass('active');
                }

                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                }
                return false;
            }
        }
    });

    $(document).on('click', '.mobile-nav-toggle', function (e) {
        $('body').toggleClass('mobile-nav-active');
        $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
    });

    $(document).click(function (e) {
        var container = $(".mobile-nav-toggle");
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            if ($('body').hasClass('mobile-nav-active')) {
                $('body').removeClass('mobile-nav-active');
                $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
            }
        }
    });

    // Navigation active state on scroll
    var nav_sections = $('section');
    var main_nav = $('.nav-menu, #mobile-nav');

    $(window).on('scroll', function () {
        var cur_pos = $(this).scrollTop() + 10;

        nav_sections.each(function () {
            var top = $(this).offset().top,
                bottom = top + $(this).outerHeight();

            if (cur_pos >= top && cur_pos <= bottom) {
                if (cur_pos <= bottom) {
                    main_nav.find('li').removeClass('active');
                }
                main_nav.find('a[href="#' + $(this).attr('id') + '"]').parent('li').addClass('active');
            }
            if (cur_pos < 200) {
                $(".nav-menu ul:first li:first").addClass('active');
            }
        });
    });

    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });

    $('.back-to-top').click(function () {
        $('html, body').animate({
            scrollTop: 0
        }, 1500, 'easeInOutExpo');
        return false;
    });

})(jQuery);

/* Switch Layouts (AJAX) */
function switchLayout(page) {
    if (page === 'signup') {
        document.getElementById('mainContainer').innerHTML = '<div class="container-login100"><div class="wrap-login100" style="box-shadow: white 0px 0px 10px 3px"><form class="login100-form" onsubmit="return false"><input name="identifier" id="identifier" type="hidden" value="signup" /><!-- Header --><span class="login100-form-title p-b-26"><img class="loginImg" src="assets/img/favicon.png" /><h3 style="float: left">Messenger</h3><p style="font-family: \'Raleway\'">Web-Based Messaging platform</p></span><div id="alertBox"></div><div class="wrap-input100"><input class="input100" type="text" name="fullname" id="fullname" maxlength="50" required="required" placeholder="Full Name"></div><div class="wrap-input100"><input class="input100" type="text" name="username" id="username" maxlength="30" required="required" placeholder="User Name"></div><div class="wrap-input100"><input class="input100" type="text" name="email" id="email" maxlength="50" maxlength="30" required="required" placeholder="Email Address"></div><div class="wrap-input100"><span class="btn-show-pass" style=\"background-color: white\"><i class="fa fa-eye" aria-hidden="true" style=\"margin: 3px\" title=\"Show/Hide Password\" onclick="this.classList.toggle(\'fa-eye-slash\'); changePassVisual()"></i><i class="fa fa-shield" title="Generate Random Password" aria-hidden="true" style=\"margin: 3px\" onclick=\"suggestRandomPassword()\"></i></span><input class="input100" type="password" name="pass" maxlength="50" id="pass" maxlength="50 required="required" placeholder="Password"></div><div class="wrap-input100"><input class="input100" type="password" name="confirm" id="confirm" maxlength="50" required="required" placeholder="Confirm Password"></div><!-- Footer --><div class="container-login100-form-btn"><div class="wrap-login100-form-btn"><div class="login100-form-bgbtn"></div><button class="login100-form-btn" onclick="signup()">Sign up</button></div></div><div class="text-center p-t-20"><span class="txt1">Already have an account?</span><a class="txt2" href="#" onclick="switchLayout(\'login\')"> Login!</a></div></form></div></div>';
        document.getElementById('title').innerText = 'Messenger | Signup';
    } else if (page === 'login') {
        document.getElementById('mainContainer').innerHTML = '<div class="container-login100"><div class="wrap-login100" style="box-shadow: white 0px 0px 10px 3px"><form class="login100-form validate-form" id="form" name="form" action="Home.aspx" method="post" onsubmit="return login()"><input name="identifier" id="identifier" type="hidden" value="login" /><span class="login100-form-title p-b-26"><img class="loginImg" src="assets/img/favicon.png" /><h3 style="float: left">Messenger</h3><p style="font-family: \'Raleway\'">Web-Based Messaging platform</p></span><div id="alertBox"></div><div class="wrap-input100 validate-input"><input class="input100" type="text" name="email" id="email" maxlength="50" placeholder="Email Address" required="required"></div><div class="wrap-input100 validate-input"><span class="btn-show-pass"><i class="fa fa-eye" aria-hidden="true" title="Show/Hide Password" onclick="this.classList.toggle(\'fa-eye-slash\'); changePassVisual()"></i></span><input class="input100" type="password" name="pass" id="pass" maxlength="50" placeholder="Password" required="required"></div><div class="container-login100-form-btn"><div class="wrap-login100-form-btn"><div class="login100-form-bgbtn"></div><button class="login100-form-btn">Login</button></div></div><div class="text-center p-t-20"><span class="txt1">Not a Member yet?</span><a class="txt2" onclick="switchLayout(\'signup\')"> Sign up!</a><br /><span class="txt1">Forgot Password?</span><a class="txt2" onclick="switchLayout(\'recoverPassword\')"> Recover Now!</a></div></form></div></div>';
        document.getElementById('title').innerText = 'Messenger | Login';
    } else if (page === 'activate') {
        document.getElementById('mainContainer').innerHTML = '<div class="container-login100"><div class="wrap-login100" style="box-shadow: white 0px 0px 10px 3px"><form class="login100-form validate-form" id="form" name="form" action="Home.aspx" method="post" onsubmit="return login()"><input name="identifier" id="identifier" type="hidden" value="login" /><span class="login100-form-title p-b-26"><img class="loginImg" src="assets/img/favicon.png" /><h3 style="float: left">Messenger</h3><p style="font-family: \'Raleway\'">Web-Based Messaging platform</p></span><div id="alertBox"></div><div style="text-align: center"><h4 style="margin: 10px; font-family: \'Raleway\'">Account Created Successfully!</h4><p style="font-family: \'Raleway\'; color: black; font-size: small"><input type=\"hidden\" id=\"email\" name=\"email\" value=\"\">An Activation Code has been send to \'' + document.getElementById('email').value + '\'. Code expires in 24 Hours</p></div><div class="text-center p-t-10" style="border-top: 1px solid #666666; margin-top: 10px"><span class="txt1">No activation code?</span><a class="txt2" onclick="reSendActivationCode()"> Resend!</a></div></form></div></div>';
        document.getElementById('title').innerText = 'Messenger | Activation';
    } else if (page === 'recoverPassword') { 
        document.getElementById('mainContainer').innerHTML = "<div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><span class=\"login100-form-title p-b-26\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3 style=\"float: left\">Messenger</h3><p style=\"font-family: 'Raleway'\">Web-Based Messaging platform</p></span><div id=\"alertBox\"></div><div style=\"width: 100%; text-align: center; font-family: 'Raleway'; margin: 5px\"><h3>Password Recovery</h3><p style=\"font-family: \'Raleway\'\">No worries, Instructions will be sent to you</p></div><div class=\"wrap-input100 validate-input\"><input class=\"input100\" type=\"text\" name=\"email\" id=\"email\" placeholder=\"Email Address\" maxlength=\"50\" required=\"required\"></div><div class=\"container-login100-form-btn\"><div class=\"wrap-login100-form-btn\"><div class=\"login100-form-bgbtn\"></div><button class=\"login100-form-btn\" onclick=\"recoverPassword()\">Send Recovery E-Mail</button></div></div><div class=\"text-center p-t-20\"><span class=\"txt1\">Remember Password?</span><a class=\"txt2\" onclick=\"switchLayout('login')\"> Login!</a></div></div></div>";
        document.getElementById('title').innerText = 'Messenger | Password Recovery';
    }
}

/* Validate Inputs */
function validate(signup) {

    var emailPattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/;

    if (signup === true && document.getElementById('fullname').value === '') // Full name 
        return false;
    if (signup === true && /[.-_=+'";:!@,<>/\|#$%^&*()~`]+/.test(document.getElementById('fullname').value)) {
        triggerAlert("error", "FullName Contains Invalid Characters", 4000);
        return false;
    }

    if (signup === true && document.getElementById('username').value === '') // User name 
        return false;

    if (signup === true && document.getElementById('username').value.includes(' ')) {
        triggerAlert('error', 'Username Can Not Contain Spaces', 4000);
        document.getElementById('username').value = '';
        return false;
    }
    if (signup === true && /[.-_=+'";:!@,<>/\|#$%^&*()~`]+/.test(document.getElementById('username').value)) {
        triggerAlert("error", "UserName Contains Invalid Characters", 4000);
        return false;
    }
    
    if (document.getElementById('email').value === '') // Email         
        return false;
    else if (!emailPattern.test(document.getElementById('email').value)) {
        triggerAlert('error', 'Invalid Email Address', 4000);
        document.getElementById('email').value = '';
        return false;
    }

    if (signup === true && (document.getElementById('pass').value !== document.getElementById('confirm').value || document.getElementById('pass').value === '' || document.getElementById('confirm').value === '')) { // Password & Confirmation 
        triggerAlert('error', 'Passwords Do Not Match', 4000);
        document.getElementById('pass').value = '';
        document.getElementById('confirm').value = '';
        return false;
    }

    if (signup === true && document.getElementById('pass').value.length < 8) {
        triggerAlert('warning', 'Password Must Be 8 Characters Minimum', 4000);
        document.getElementById('pass').value = '';
        document.getElementById('confirm').value = '';        
        return false;
    }

    return true;
}

/* Sign Up (AJAX) */
function signup() {

    if (validate(true) === true && document.getElementById('identifier').value === 'signup' && checkPasswordStrength()) {
        
        var request = new XMLHttpRequest();
        
        request.open('GET', 'responder.aspx?Action=' + document.getElementById('identifier').value + "&FullName=" + document.getElementById('fullname').value + "&UserName=" + document.getElementById('username').value + "&Email=" + document.getElementById('email').value + "&Password=" + document.getElementById('pass').value, true);

        request.send(); // Send request to server
        var success = true;
        request.onreadystatechange = function () {
            if (request.readyState === 4 && request.status === 200) {
                if (request.responseText === 'Code 1') {
                    var email = document.getElementById('email').value;
                    switchLayout('activate');    // Show Login Page
                    document.getElementById('email').value = email;
                } else if (request.responseText === 'Code 0') {                    
                    triggerAlert('error', 'Username Is Already Taken', 4000);
                    document.getElementById('username').value = '';
                    document.getElementById('pass').value = '';
                    document.getElementById('confirm').value = '';
                } else if (request.responseText === 'Code 2') {
                    triggerAlert('error', 'Email Address Is Already Assigned', 4000);
                    document.getElementById('email').value = '';
                    document.getElementById('pass').value = '';
                    document.getElementById('confirm').value = '';
                } else {
                    triggerAlert('error', 'MSS Failure; Disable Proxy / Check Connection', 5000);
                    document.getElementById('pass').value = '';
                    document.getElementById('confirm').value = '';
                }
            } else
                success = false;
        };

        if (success === false)
            triggerAlert('error', 'Connection Timed Out', 5000);
    }
}

/* Login (AJAX) */
function login() {

    if (validate(false) === true && document.getElementById('identifier').value === 'login') {
        var request = new XMLHttpRequest();
        request.open('GET', 'responder.aspx?Action=' + document.getElementById('identifier').value + '&Email=' + document.getElementById('email').value + '&Password=' + document.getElementById('pass').value, true);

        request.send(); // Send request to server

        var success = true;
        request.onreadystatechange = function () {            
            if (request.readyState === 4 && request.status === 200) {
                if (request.responseText === 'Code 1') {
                    triggerAlert('success', 'EP Signature Confirmed, Loging In...', 4000);
                    document.getElementById('form').submit();
                } else if (request.responseText === 'Code 2') {
                    triggerAlert('warning', 'Account Has Not Been Activated Yet', 5000);
                    document.getElementById('pass').value = '';
                } else {
                    triggerAlert('error', 'Incorrect Email / Password', 4000);
                    document.getElementById('pass').value = '';
                }
            } else
                success = false;
        };

        if (success === false)
            triggerAlert('error', 'Connection Timed Out', 5000);
    }

    return false;
}

/* Recover Password (AJAX) */
function recoverPassword() {
    var request = new XMLHttpRequest();
    request.open('GET', 'responder.aspx?Action=recoverPassword&Email=' + document.getElementById('email').value, true);

    request.send(); // Send request to server

    var success = true;
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            if (request.responseText === 'Code 1') {
                triggerAlert('success', 'Recovery Eamil Has Been Sent', 4000);
                document.getElementById('form').submit();
            } else if (request.responseText === 'Code 2') {
                triggerAlert('error', 'Invalid Email Address', 4000);
                document.getElementById('email').value = '';
            } else
                triggerAlert('error', 'MSS Failure; Disable Proxy / Check Connection', 5000);
        } else
            success = false;
    };

    if (success === false)
        triggerAlert('error', 'Connection Timed Out', 5000);
}

/* Re-Send Activation Code (AJAX) */
function reSendActivationCode() {
    var request = new XMLHttpRequest();
    request.open('GET', 'responder.aspx?Action=resendActivationCode&Email=' + document.getElementById('email').value, true);

    request.send(); // Send request to server

    var success = true;
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            if (request.responseText === 'Code 1') {
                triggerAlert('success', 'Email Has Been Resent, Check Your Inbox', 5000);
            } else if (request.responseText === 'Code 2') {
                triggerAlert('error', 'Ooops! Something Weird Went Wrong', 4000);
                document.getElementById('email').value = '';
            } else
                triggerAlert('error', 'MSS Failure; Disable Proxy / Check Connection', 5000);
        } else
            success = false;
    };

    if (success === false)
        triggerAlert('error', 'Connection Timed Out', 5000);
}

/* Reset Password (AJAX) */
function resetPassword() {
    if (document.getElementById('pass').value === document.getElementById('confirm').value) {
        if (document.getElementById('pass').value.length >= 8) {
            var request = new XMLHttpRequest();
            request.open('GET', 'responder.aspx?Action=resetPassword&Email=' + document.getElementById('email').value + '&ActivationCode=' + document.getElementById('code').value + '&Password=' + document.getElementById('pass').value, true);

            request.send(); // Send request to server

            var success = true;
            request.onreadystatechange = function () {
                if (request.readyState === 4 && request.status === 200) {
                    if (request.responseText === 'Code 1') {
                        document.getElementById('mainContainer').innerHTML = '<div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><input name=\"identifier\" id=\"identifier\" type=\"hidden\" value=\"login\" /><span class=\"login100-form-title p-b-26\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3 style=\"float: left\">Messenger</h3><p style=\"font-family: \'Raleway\'\">Web-Based Messaging platform</p></span><div id=\"alertBox\"></div><div style=\"width: 100%; text-align: center; font-family: \'Raleway\'; margin: 5px\"><h3>Password Recovery</h3><p>Your Password Has Been Reset Successfully. You may login now...</p></div><div class=\"container-login100-form-btn\"><div class=\"wrap-login100-form-btn\"><div class=\"login100-form-bgbtn\"></div><button class=\"login100-form-btn\" onclick=\"switchLayout(\'login\')\">Login</button></div></div></div>';
                    } else {
                        triggerAlert('error', 'Ooops! Somthing Weird Went Wrong', 4000);
                        document.getElementById('pass').value = '';
                        document.getElementById('confirm').value = '';
                    }
                } else
                    success = false;
            };

            if (success === false)
                triggerAlert('error', 'Connection Timed Out', 5000);
        } else {
            triggerAlert('warning', 'Password Must Be 8 Characters Minimum', 4000);
            document.getElementById('pass').value = '';
            document.getElementById('confirm').value = '';
        }
    } else {
        triggerAlert('error', 'Passwords Do Not Match', 4000);
        document.getElementById('pass').value = '';
        document.getElementById('confirm').value = '';
    }
}

/* suggest Random Strong Password */
function suggestRandomPassword() {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()_-=+[]{};:<>';
    var charactersLength = characters.length;
    for (var i = 0; i < 25; i++)
        result += characters.charAt(Math.floor(Math.random() * charactersLength));

    document.getElementById('pass').value = document.getElementById('confirm').value = result;
}

/* Check Password Strength */
function checkPasswordStrength() {
    if (/\d/.test(document.getElementById('pass').value) === false) {
        triggerAlert('warning', 'Password Must Contain Digits', 4000);
        return false;
    } else if (/[a-z]/.test(document.getElementById('pass').value) === false) {
        triggerAlert('warning', 'Password Must Contain Lower Charecters', 4000);
        return false;
    } else if (/[A-Z]/.test(document.getElementById('pass').value) === false) {
        triggerAlert('warning', 'Password Must Contain Upper Charecters', 4000);
        return false;
    }

    return true;
}

/* Toggle Password Type */
function changePassVisual() {    
    if (document.getElementById('pass').type === 'text')
        document.getElementById('pass').type = 'password';
    else
        document.getElementById('pass').type = 'text';
}

/* Alerts */
function triggerAlert(className, content, interval) {
    var icon;
    switch (className) {
        case 'warning': icon = 'fa fa-exclamation-triangle'; break;
        case 'error': icon = 'fa fa-times'; break;
        case 'success': icon = 'fa fa-check-circle'; break;
    }
    document.getElementById('alertBox').innerHTML = '<div class=\"alert ' + className + '\"><i class=\"' + icon + '\" style=\"padding: 5px\" aria-hidden="true"></i>' + content + '</div class=\"alertContent\">';

    // Auto Fade
    setTimeout(function () {
        var close = document.getElementsByClassName("alert");
        for (var i = 0; i < close.length; i++)
            close[i].style.display = "none";
    }, interval);
}

/* Disable Back Button */
function preventBack() {
    window.history.forward();
}

setTimeout("preventBack()", 0);

window.onunload = function () { null }; 