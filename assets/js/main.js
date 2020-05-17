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
        document.getElementById('mainContainer').innerHTML = '<div class="container-login100"><div class="wrap-login100" style="box-shadow: white 0px 0px 10px 3px"><form class="login100-form" onsubmit="return false"><input name="identifier" id="identifier" type="hidden" value="signup" /><!-- Header --><span class="login100-form-title p-b-26" style=\"text-align: left\"><img class="loginImg" src="assets/img/favicon.png" /><h3>Messenger</h3><p style="font-family: \'Raleway\'">Web-Based Messaging platform</p></span><div id="alertBox"></div><div class="wrap-input100"><input class="input100" type="text" name="fullname" id="fullname" maxlength="50" required="required" placeholder="Full Name"></div><div class="wrap-input100"><input class="input100" type="text" name="username" id="username" maxlength="30" required="required" placeholder="User Name"></div><div class="wrap-input100"><input class="input100" type="text" name="email" id="email" maxlength="50" maxlength="30" required="required" placeholder="Email Address"></div><div class="wrap-input100"><span class="btn-show-pass" style=\"background-color: white\"><i class="fa fa-eye" aria-hidden="true" style=\"margin: 3px\" title=\"Show/Hide Password\" onclick="this.classList.toggle(\'fa-eye-slash\'); changePassVisual()"></i><i class="fa fa-shield" title="Generate Random Password" aria-hidden="true" style=\"margin: 3px\" onclick=\"suggestRandomPassword()\"></i></span><input class="input100" type="password" name="pass" maxlength="50" id="pass" maxlength="50 required="required" placeholder="Password"></div><div class="wrap-input100"><input class="input100" type="password" name="confirm" id="confirm" maxlength="50" required="required" placeholder="Confirm Password"></div><div class="input100 validate-input" style="text-align: center; height: auto"><input type="checkbox" name="termsOfService" id="termsOfService" required="required" style="cursor: pointer"><label for="termsOfService" style="font-family: \'Raleway\'; cursor: pointer; padding: 5px">I agree with <a style="font-family: \'Raleway\'; text-decoration: underline; color: #149ddd; font-weight: bold" data-toggle="modal" data-target="#termsOfServiceModel">Terms Of Services</a></label></div><div class="container-login100-form-btn"><div class="wrap-login100-form-btn"><div class="login100-form-bgbtn"></div><button class="login100-form-btn" onclick="signup()">Sign up</button></div></div><div class="text-center p-t-20"><span class="txt1">Already have an account?</span><a class="txt2" href="#" onclick="switchLayout(\'login\')"> Login!</a></div></form><div class="modal" id="termsOfServiceModel"><div class="modal-dialog"><div class="modal-content" style="padding: 20px"><h4>Website Terms and Conditions of Use</h4><h5>1. Terms</h5><p>By accessing this Website, accessible from <a href="http://messenger.keivanipchihagh.ir/" target="_blank">messenger.keivanipchihagh.ir</a>, you are agreeing to be bound by these Website Terms and Conditions of Use and agree that you are responsible for the agreement with any applicable local laws. If you disagree with any of these terms, you are prohibited from accessing this site. The materials contained in this Website are protected by copyright and trade mark law.</p><h5>2. Disclaimer</h5><p>All of the personal informations submitted in this website are well encrypted and proper security measures have been put in action in order maintain users privacy as isolated as possible. However, this website cannot grand 100% security for its users as it\'s not an enterprise project. Proper counter-meassures have been put in effect to create a safe sandbox for users. Security updates will be realeased every month to update to latest technologies and add features to the platform.</p><h5>3. Limitations</h5><p>Messenger or its suppliers will not be hold accountable for any damages that will arise with the use or inability to use the materials on Messenger\'s Website, even if Messenger or an authorize representative of this Website has been notified, orally or written, of the possibility of such damage. Some jurisdiction does not allow limitations on implied warranties or limitations of liability for incidental damages, these limitations may not apply to you.</p><h5>4. Revisions and Errata</h5><p>The materials appearing on Messengers Website may include technical, typographical, or photographic errors. Messenger will not promise that any of the materials in this Website are accurate, complete, or current. Messenger may change the materials contained on its Website at any time without notice. Messenger does not make any commitment to update the materials.</p><div class="modal-footer"><button type="button" class="btn btn-danger" data-dismiss="modal">Close</button></div></div></div></div></div></div>';
        document.getElementById('title').innerText = 'Messenger | Signup';
    } else if (page === 'login') {
        document.getElementById('mainContainer').innerHTML = '<div class="container-login100"><div class="wrap-login100" style="box-shadow: white 0px 0px 10px 3px"><form class="login100-form validate-form" id="form" name="form" action="Home.aspx" method="post" onsubmit="return login()"><input name="identifier" id="identifier" type="hidden" value="login" /><span class="login100-form-title p-b-26" style=\"text-align: left\"><img class="loginImg" src="assets/img/favicon.png" /><h3>Messenger</h3><p style="font-family: \'Raleway\'">Web-Based Messaging platform</p></span><div id="alertBox"></div><div class="wrap-input100 validate-input"><input class="input100" type="text" name="email" id="email" maxlength="50" placeholder="Email Address" required="required"></div><div class="wrap-input100 validate-input"><span class="btn-show-pass"><i class="fa fa-eye" aria-hidden="true" title="Show/Hide Password" onclick="this.classList.toggle(\'fa-eye-slash\'); changePassVisual()"></i></span><input class="input100" type="password" name="pass" id="pass" maxlength="50" placeholder="Password" required="required"></div><div class="container-login100-form-btn"><div class="wrap-login100-form-btn"><div class="login100-form-bgbtn"></div><button class="login100-form-btn">Login</button></div></div><div class="text-center p-t-20"><span class="txt1">Not a Member yet?</span><a class="txt2" onclick="switchLayout(\'signup\')"> Sign up!</a><br /><span class="txt1">Forgot Password?</span><a class="txt2" onclick="switchLayout(\'recoverPassword\')"> Recover Now!</a></div></form><div class="modal" id="resendEmailModel"><div class="modal-dialog" style="box-shadow: white 0px 0px 10px 3px; border-radius: .3rem"><div class="modal-content" style="border: 1.5px solid white; "><div class="modal-header"><div id="alertBox"></div><h4 class="modal-title">Resend Activation Email</h4><button type="button" class="close" data-dismiss="modal">&times;</button></div><div class="modal-body"><div class="wrap-input100 validate-input"><input class="input100" type="text" name="emailModel" id="emailModel" maxlength="50" placeholder="Email Address" required="required"></div>Note: Email Must Be Registered!</div><div class="modal-footer"><button type="button" class="btn btn-danger" data-dismiss="modal">Close</button><button type="button" class="btn btn-success" data-dismiss="modal" onclick="reSendActivationCode(document.getElementById(\'emailModel\').value)">Send</button></div></div></div></div></div></div>';
        document.getElementById('title').innerText = 'Messenger | Login';
    } else if (page === 'activate') {
        document.getElementById('mainContainer').innerHTML = '<div class="container-login100"><div class="wrap-login100" style="box-shadow: white 0px 0px 10px 3px"><form class="login100-form validate-form" id="form" name="form" action="Home.aspx" method="post" onsubmit="return login()"><input name="identifier" id="identifier" type="hidden" value="login" /><span class="login100-form-title p-b-26" style=\"text-align: left\"><img class="loginImg" src="assets/img/favicon.png" /><h3>Messenger</h3><p style="font-family: \'Raleway\'">Web-Based Messaging platform</p></span><div id="alertBox"></div><div style="text-align: center"><h4 style="margin: 10px; font-family: \'Raleway\'">Account Created Successfully!</h4><p style="font-family: \'Raleway\'; color: black; font-size: small"><input type=\"hidden\" id=\"email\" name=\"email\" value=\"\">An Activation Code has been send to \'' + document.getElementById('email').value + '\'. Code expires in 24 Hours</p></div><div class="text-center p-t-10" style="border-top: 1px solid #666666; margin-top: 10px"><span class="txt1">No activation code?</span><a class="txt2" onclick="reSendActivationCode(document.getElementById(\'email\').value)"> Resend!</a></div></form></div></div>';
        document.getElementById('title').innerText = 'Messenger | Activation';
    } else if (page === 'recoverPassword') { 
        document.getElementById('mainContainer').innerHTML = "<div class=\"container-login100\"><div class=\"wrap-login100\" style=\"box-shadow: white 0px 0px 10px 3px\"><span class=\"login100-form-title p-b-26\" style=\"text-align: left\"><img class=\"loginImg\" src=\"assets/img/favicon.png\" /><h3>Messenger</h3><p style=\"font-family: 'Raleway'\">Web-Based Messaging platform</p></span><div id=\"alertBox\"></div><div style=\"width: 100%; text-align: center; font-family: 'Raleway'; margin: 5px\"><h3>Password Recovery</h3><p style=\"font-family: \'Raleway\'\">No worries, Instructions will be sent to you</p></div><div class=\"wrap-input100 validate-input\"><input class=\"input100\" type=\"text\" name=\"email\" id=\"email\" placeholder=\"Email Address\" maxlength=\"50\" required=\"required\"></div><div class=\"container-login100-form-btn\"><div class=\"wrap-login100-form-btn\"><div class=\"login100-form-bgbtn\"></div><button class=\"login100-form-btn\" onclick=\"recoverPassword()\">Send Recovery E-Mail</button></div></div><div class=\"text-center p-t-20\"><span class=\"txt1\">Remember Password?</span><a class=\"txt2\" onclick=\"switchLayout('login')\"> Login!</a></div></div></div>";
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
        request.open('GET', 'responder.aspx?Action=' + document.getElementById('identifier').value + '&Email=' + document.getElementById('email').value + '&Password=' + getMD5Hash(document.getElementById('pass').value), true);        
        
        request.send(); // Send request to server

        var success = true;
        request.onreadystatechange = function () {            
            if (request.readyState === 4 && request.status === 200) {
                if (request.responseText === 'Code 1') {
                    triggerAlert('success', 'EP Signature Confirmed, Loging In...', 4000);
                    document.getElementById('form').submit();
                } else if (request.responseText === 'Code 2') {
                    triggerAlert('warning', 'Account Has Not Been Activated Yet. <a style=\"text-decoration: underline; cursor: pointer\" data-toggle="modal" data-target="#resendEmailModel" onclick=\"document.getElementById(\'emailModel\').value = document.getElementById(\'email\').value\">Resned Link?</a>', 10000);
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
function reSendActivationCode(email) {
    var request = new XMLHttpRequest();
    request.open('GET', 'responder.aspx?Action=resendActivationCode&Email=' + email, true);

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
            request.open('GET', 'responder.aspx?Action=resetPassword&Email=' + document.getElementById('email').value + '&ActivationCode=' + document.getElementById('code').value + '&Password=' + getMD5Hash(document.getElementById('pass').value), true);

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

/* Disable Back/Forward Button */
setTimeout("preventBack()", 0);
window.onunload = function () { null }; 

/* MD5 Hashing */
function getMD5Hash(value) {
    var MD5 = function (d) { var r = M(V(Y(X(d), 8 * d.length))); return r.toLowerCase() }; function M(d) { for (var _, m = "0123456789ABCDEF", f = "", r = 0; r < d.length; r++)_ = d.charCodeAt(r), f += m.charAt(_ >>> 4 & 15) + m.charAt(15 & _); return f } function X(d) { for (var _ = Array(d.length >> 2), m = 0; m < _.length; m++)_[m] = 0; for (m = 0; m < 8 * d.length; m += 8)_[m >> 5] |= (255 & d.charCodeAt(m / 8)) << m % 32; return _ } function V(d) { for (var _ = "", m = 0; m < 32 * d.length; m += 8)_ += String.fromCharCode(d[m >> 5] >>> m % 32 & 255); return _ } function Y(d, _) { d[_ >> 5] |= 128 << _ % 32, d[14 + (_ + 64 >>> 9 << 4)] = _; for (var m = 1732584193, f = -271733879, r = -1732584194, i = 271733878, n = 0; n < d.length; n += 16) { var h = m, t = f, g = r, e = i; f = md5_ii(f = md5_ii(f = md5_ii(f = md5_ii(f = md5_hh(f = md5_hh(f = md5_hh(f = md5_hh(f = md5_gg(f = md5_gg(f = md5_gg(f = md5_gg(f = md5_ff(f = md5_ff(f = md5_ff(f = md5_ff(f, r = md5_ff(r, i = md5_ff(i, m = md5_ff(m, f, r, i, d[n + 0], 7, -680876936), f, r, d[n + 1], 12, -389564586), m, f, d[n + 2], 17, 606105819), i, m, d[n + 3], 22, -1044525330), r = md5_ff(r, i = md5_ff(i, m = md5_ff(m, f, r, i, d[n + 4], 7, -176418897), f, r, d[n + 5], 12, 1200080426), m, f, d[n + 6], 17, -1473231341), i, m, d[n + 7], 22, -45705983), r = md5_ff(r, i = md5_ff(i, m = md5_ff(m, f, r, i, d[n + 8], 7, 1770035416), f, r, d[n + 9], 12, -1958414417), m, f, d[n + 10], 17, -42063), i, m, d[n + 11], 22, -1990404162), r = md5_ff(r, i = md5_ff(i, m = md5_ff(m, f, r, i, d[n + 12], 7, 1804603682), f, r, d[n + 13], 12, -40341101), m, f, d[n + 14], 17, -1502002290), i, m, d[n + 15], 22, 1236535329), r = md5_gg(r, i = md5_gg(i, m = md5_gg(m, f, r, i, d[n + 1], 5, -165796510), f, r, d[n + 6], 9, -1069501632), m, f, d[n + 11], 14, 643717713), i, m, d[n + 0], 20, -373897302), r = md5_gg(r, i = md5_gg(i, m = md5_gg(m, f, r, i, d[n + 5], 5, -701558691), f, r, d[n + 10], 9, 38016083), m, f, d[n + 15], 14, -660478335), i, m, d[n + 4], 20, -405537848), r = md5_gg(r, i = md5_gg(i, m = md5_gg(m, f, r, i, d[n + 9], 5, 568446438), f, r, d[n + 14], 9, -1019803690), m, f, d[n + 3], 14, -187363961), i, m, d[n + 8], 20, 1163531501), r = md5_gg(r, i = md5_gg(i, m = md5_gg(m, f, r, i, d[n + 13], 5, -1444681467), f, r, d[n + 2], 9, -51403784), m, f, d[n + 7], 14, 1735328473), i, m, d[n + 12], 20, -1926607734), r = md5_hh(r, i = md5_hh(i, m = md5_hh(m, f, r, i, d[n + 5], 4, -378558), f, r, d[n + 8], 11, -2022574463), m, f, d[n + 11], 16, 1839030562), i, m, d[n + 14], 23, -35309556), r = md5_hh(r, i = md5_hh(i, m = md5_hh(m, f, r, i, d[n + 1], 4, -1530992060), f, r, d[n + 4], 11, 1272893353), m, f, d[n + 7], 16, -155497632), i, m, d[n + 10], 23, -1094730640), r = md5_hh(r, i = md5_hh(i, m = md5_hh(m, f, r, i, d[n + 13], 4, 681279174), f, r, d[n + 0], 11, -358537222), m, f, d[n + 3], 16, -722521979), i, m, d[n + 6], 23, 76029189), r = md5_hh(r, i = md5_hh(i, m = md5_hh(m, f, r, i, d[n + 9], 4, -640364487), f, r, d[n + 12], 11, -421815835), m, f, d[n + 15], 16, 530742520), i, m, d[n + 2], 23, -995338651), r = md5_ii(r, i = md5_ii(i, m = md5_ii(m, f, r, i, d[n + 0], 6, -198630844), f, r, d[n + 7], 10, 1126891415), m, f, d[n + 14], 15, -1416354905), i, m, d[n + 5], 21, -57434055), r = md5_ii(r, i = md5_ii(i, m = md5_ii(m, f, r, i, d[n + 12], 6, 1700485571), f, r, d[n + 3], 10, -1894986606), m, f, d[n + 10], 15, -1051523), i, m, d[n + 1], 21, -2054922799), r = md5_ii(r, i = md5_ii(i, m = md5_ii(m, f, r, i, d[n + 8], 6, 1873313359), f, r, d[n + 15], 10, -30611744), m, f, d[n + 6], 15, -1560198380), i, m, d[n + 13], 21, 1309151649), r = md5_ii(r, i = md5_ii(i, m = md5_ii(m, f, r, i, d[n + 4], 6, -145523070), f, r, d[n + 11], 10, -1120210379), m, f, d[n + 2], 15, 718787259), i, m, d[n + 9], 21, -343485551), m = safe_add(m, h), f = safe_add(f, t), r = safe_add(r, g), i = safe_add(i, e) } return Array(m, f, r, i) } function md5_cmn(d, _, m, f, r, i) { return safe_add(bit_rol(safe_add(safe_add(_, d), safe_add(f, i)), r), m) } function md5_ff(d, _, m, f, r, i, n) { return md5_cmn(_ & m | ~_ & f, d, _, r, i, n) } function md5_gg(d, _, m, f, r, i, n) { return md5_cmn(_ & f | m & ~f, d, _, r, i, n) } function md5_hh(d, _, m, f, r, i, n) { return md5_cmn(_ ^ m ^ f, d, _, r, i, n) } function md5_ii(d, _, m, f, r, i, n) { return md5_cmn(m ^ (_ | ~f), d, _, r, i, n) } function safe_add(d, _) { var m = (65535 & d) + (65535 & _); return (d >> 16) + (_ >> 16) + (m >> 16) << 16 | 65535 & m } function bit_rol(d, _) { return d << _ | d >>> 32 - _ }
    alert(MD5(value));
    return MD5(value);
}