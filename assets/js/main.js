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
        //document.getElementById('navBar').style.paddingLeft = '320px';
    });

    $(document).click(function (e) {
        var container = $(".mobile-nav-toggle");
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            if ($('body').hasClass('mobile-nav-active')) {
                $('body').removeClass('mobile-nav-active');
                $('.mobile-nav-toggle i').toggleClass('icofont-navigation-menu icofont-close');
                //document.getElementById('navBar').style.paddingLeft = '20px';
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

/* Sign Up Section */
function signup() {

    // Check Required Fields
    if (document.getElementById('fullname').value === '' || document.getElementById('username').value === '' || document.getElementById('email').value === '' || document.getElementById('pass').value === '' || document.getElementById('confirm').value === '' || document.getElementById('termsOfService').checked === false) {
        triggerAlert("warning", "Fill All The Fields In Order To Proceed", 3000);
        return false;
    }    

    // Check FullName Validation
    if (/\d/.test(document.getElementById('fullname').value)) {
        triggerAlert("error", "'Full Name' Cannot Contain Numerics. Try Again", 3000);
        document.getElementById('fullname').value = '';
        return false;
    }

    // Check UserName Validation
    if (document.getElementById('username').value.includes(' ')) {
        triggerAlert('error', 'Username Can Not Contain Spaces. Try Again', 4000);
        document.getElementById('username').value = '';
        return false;
    }

    // Check Email Validation
    if (/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(document.getElementById('email').value) === false) {
        triggerAlert("error", "'Email Address' Is Invalid. Try Again", 3000);
        document.getElementById('email').value = '';
        return false;
    }

    // Check Passwords Validation
    if (document.getElementById('pass').value !== document.getElementById('confirm').value) {
        triggerAlert("error", "Passwords Do Not Match. Try Again", 3000);
        document.getElementById('pass').value = '';
        document.getElementById('confirm').value = '';
        return false;
    }

    // Check Password Strength
    if (checkPasswordStrength() === false)
        return false;

    // All Good, Sign Up User
    if (document.getElementById('identifier').value === 'signup') {
        var request = new XMLHttpRequest();

        request.open('GET', 'responder.aspx?Action=' + document.getElementById('identifier').value + "&FullName=" + document.getElementById('fullname').value + "&UserName=" + document.getElementById('username').value + "&Email=" + document.getElementById('email').value + "&Password=" + getMD5Hash(document.getElementById('pass').value), true);

        request.send(); // Send request to server
        var success = true;
        request.onreadystatechange = function () {
            if (request.readyState === 4 && request.status === 200) {
                if (request.responseText === 'Code 1') {
                    var email = document.getElementById('email').value;
                    triggerAlert("success", "Confirm The Activation Link Sent To Your Inbox To Complete Your Sign Up.", 10000);
                    document.getElementById('email').value = email;
                } else if (request.responseText === 'Code 0') {                    
                    triggerAlert('error', 'Username Is Already Taken; Try Another Username', 4000);
                    document.getElementById('username').value = '';
                    document.getElementById('pass').value = '';
                    document.getElementById('confirm').value = '';
                } else if (request.responseText === 'Code 2') {
                    triggerAlert('error', 'Email Address Is Already Assigned; Report Abuse', 4000);
                    document.getElementById('email').value = '';
                    document.getElementById('pass').value = '';
                    document.getElementById('confirm').value = '';
                } else {
                    triggerAlert('error', 'MSS Protocal Failed. Disable Proxy / Check Connection', 5000);
                    document.getElementById('pass').value = '';
                    document.getElementById('confirm').value = '';
                }
            } else
                success = false;
        };
        if (success === false)
            triggerAlert('error', 'Oops! Something Weird Went Wrong! Might Be Your Connection', 5000);
    }
}

/* Login Section */
function login() {

    // Check Required Fields
    if (document.getElementById('email').value === '' || document.getElementById('pass').value === '') {
        triggerAlert("warning", "Fill All The Fields In Order To Proceed", 3000);
        return false;
    }

    if (document.getElementById('identifier').value === 'login') {
        var request = new XMLHttpRequest();
        request.open('GET', 'responder.aspx?Action=' + document.getElementById('identifier').value + '&Email=' + document.getElementById('email').value + '&Password=' + getMD5Hash(document.getElementById('pass').value), true);        
        
        request.send(); // Send request to server
        
        var success = true;
        request.onreadystatechange = function () {            
            if (request.readyState === 4 && request.status === 200) {
                if (request.responseText === 'Code 1') {
                    triggerAlert('success', 'EP Signature Confirmed, Redirecting Your To Your Account...', 4000);
                    document.getElementById('form').submit();
                } else if (request.responseText === 'Code 2') {
                    triggerAlert('warning', 'Account Has Not Been Activated Yet. Activate In Order To Proceed', 10000);
                    document.getElementById('pass').value = '';
                } else if (request.responseText === 'Code 3') {
                    triggerAlert('error', 'Too many Invalid attempts!!! You have been banned for 15 minutes.', 10000);
                    document.getElementById('email').value = '';
                    document.getElementById('pass').value = '';
                } else if (request.responseText === 'Code 4') {
                    triggerAlert('error', 'You Are Still Prohibited To Access The Website. Please Be Paitient.', 4000);
                    document.getElementById('email').value = '';
                    document.getElementById('pass').value = '';
                } else {
                    triggerAlert('error', 'Incorrect Email / Password. Try Again', 4000);
                    document.getElementById('pass').value = '';
                }
            } else
                success = false;
        };
        
        if (success === false)
            triggerAlert('error', 'Oops! Something Weird Went Wrong! Might Be Your Connection', 5000);
    }

    return false;
}

/* Recover Password Section */
function recoverPassword() {
    var request = new XMLHttpRequest();
    request.open('GET', 'responder.aspx?Action=' + document.getElementById('identifier').value + '&Email=' + document.getElementById('email').value, true);

    request.send(); // Send request to server
    
    var success = true;
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            if (request.responseText === 'Code 1') {
                triggerAlert('success', 'Your Request Has Been Received And Will Be Processed Shortly.', 4000);
            } else
                triggerAlert('error', 'MSS Protocol Failed. Disable Proxy / Check Connection', 5000);
        } else
            success = false;
    };

    if (success === false)
        triggerAlert('error', 'Oops! Something Weird Went Wrong! Might Be Your Connection', 5000);
}

/* Resend Activation Code Rection */
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
        triggerAlert('error', 'Oops! Something Weird Went Wrong! Might Be Your Connection', 5000);
}

/* Reset Password Section */
function resetPassword() {

    // Check Passwords Validation
    if (document.getElementById('pass').value !== document.getElementById('confirm').value) {
        triggerAlert("error", "Passwords Do Not Match. Try Again", 3000);
        document.getElementById('pass').value = '';
        document.getElementById('confirm').value = '';
        return false;
    }

    // Check Password Strength
    if (checkPasswordStrength() === false)
        return false;

    var request = new XMLHttpRequest();
    request.open('GET', 'responder.aspx?Action=resetPassword&Email=' + document.getElementById('email').value + '&ActivationCode=' + document.getElementById('code').value + '&Password=' + getMD5Hash(document.getElementById('pass').value), true);

    request.send(); // Send request to server

    var success = true;
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            if (request.responseText === 'Code 1')
                document.getElementById('content').innerHTML = 'Yey! Your password has been successfully updated. You may login now...<a href="http://messenger.keivanipchihagh.ir/" style="display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px">Login</a>';
            else
                document.getElementById('content').innerHTML = 'This link contains Invalid signeratures. Session has been terminated due to EA violations; Your link might be expired. Please request a new link form:<a href="http://messenger.keivanipchihagh.ir/" style="display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px">Request New Link</a>';
        } else
            success = false;
    };

    if (success === false)
        triggerAlert('error', 'Oops! Something Weird Went Wrong! Might Be Your Connection', 5000);
}

/* Check Password Strength */
function checkPasswordStrength() {
    if (document.getElementById('pass').value.length < 8) {
        triggerAlert('warning', 'Password Must Be Atleast 8 Characters Long. Try Again', 4000);
        return false;
    } else if (/\d/.test(document.getElementById('pass').value) === false) {
        triggerAlert('warning', 'Password Must Contain Digits. Try Again', 4000);
        return false;
    } else if (/[a-z]/.test(document.getElementById('pass').value) === false) {
        triggerAlert('warning', 'Password Must Contain Lower Charecters. Try Again', 4000);
        return false;
    } else if (/[A-Z]/.test(document.getElementById('pass').value) === false) {
        triggerAlert('warning', 'Password Must Contain Upper Charecters. Try Again', 4000);
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
    document.getElementById('alertBox').innerHTML = '<div class=\"alert ' + className + '\"><i class=\"' + icon + '\" style=\"padding: 5px\" aria-hidden="true"></i>' + content + '</div>';

    // Auto Fade
    setTimeout(function () {
        var close = document.getElementsByClassName("alert");
        for (var i = 0; i < close.length; i++)
            close[i].style.display = "none";
    }, interval);
}

/* MD5 Hashing */
function getMD5Hash(value) {
    var MD5 = function (d) { var r = M(V(Y(X(d), 8 * d.length))); return r.toLowerCase() }; function M(d) { for (var _, m = "0123456789ABCDEF", f = "", r = 0; r < d.length; r++)_ = d.charCodeAt(r), f += m.charAt(_ >>> 4 & 15) + m.charAt(15 & _); return f } function X(d) { for (var _ = Array(d.length >> 2), m = 0; m < _.length; m++)_[m] = 0; for (m = 0; m < 8 * d.length; m += 8)_[m >> 5] |= (255 & d.charCodeAt(m / 8)) << m % 32; return _ } function V(d) { for (var _ = "", m = 0; m < 32 * d.length; m += 8)_ += String.fromCharCode(d[m >> 5] >>> m % 32 & 255); return _ } function Y(d, _) { d[_ >> 5] |= 128 << _ % 32, d[14 + (_ + 64 >>> 9 << 4)] = _; for (var m = 1732584193, f = -271733879, r = -1732584194, i = 271733878, n = 0; n < d.length; n += 16) { var h = m, t = f, g = r, e = i; f = md5_ii(f = md5_ii(f = md5_ii(f = md5_ii(f = md5_hh(f = md5_hh(f = md5_hh(f = md5_hh(f = md5_gg(f = md5_gg(f = md5_gg(f = md5_gg(f = md5_ff(f = md5_ff(f = md5_ff(f = md5_ff(f, r = md5_ff(r, i = md5_ff(i, m = md5_ff(m, f, r, i, d[n + 0], 7, -680876936), f, r, d[n + 1], 12, -389564586), m, f, d[n + 2], 17, 606105819), i, m, d[n + 3], 22, -1044525330), r = md5_ff(r, i = md5_ff(i, m = md5_ff(m, f, r, i, d[n + 4], 7, -176418897), f, r, d[n + 5], 12, 1200080426), m, f, d[n + 6], 17, -1473231341), i, m, d[n + 7], 22, -45705983), r = md5_ff(r, i = md5_ff(i, m = md5_ff(m, f, r, i, d[n + 8], 7, 1770035416), f, r, d[n + 9], 12, -1958414417), m, f, d[n + 10], 17, -42063), i, m, d[n + 11], 22, -1990404162), r = md5_ff(r, i = md5_ff(i, m = md5_ff(m, f, r, i, d[n + 12], 7, 1804603682), f, r, d[n + 13], 12, -40341101), m, f, d[n + 14], 17, -1502002290), i, m, d[n + 15], 22, 1236535329), r = md5_gg(r, i = md5_gg(i, m = md5_gg(m, f, r, i, d[n + 1], 5, -165796510), f, r, d[n + 6], 9, -1069501632), m, f, d[n + 11], 14, 643717713), i, m, d[n + 0], 20, -373897302), r = md5_gg(r, i = md5_gg(i, m = md5_gg(m, f, r, i, d[n + 5], 5, -701558691), f, r, d[n + 10], 9, 38016083), m, f, d[n + 15], 14, -660478335), i, m, d[n + 4], 20, -405537848), r = md5_gg(r, i = md5_gg(i, m = md5_gg(m, f, r, i, d[n + 9], 5, 568446438), f, r, d[n + 14], 9, -1019803690), m, f, d[n + 3], 14, -187363961), i, m, d[n + 8], 20, 1163531501), r = md5_gg(r, i = md5_gg(i, m = md5_gg(m, f, r, i, d[n + 13], 5, -1444681467), f, r, d[n + 2], 9, -51403784), m, f, d[n + 7], 14, 1735328473), i, m, d[n + 12], 20, -1926607734), r = md5_hh(r, i = md5_hh(i, m = md5_hh(m, f, r, i, d[n + 5], 4, -378558), f, r, d[n + 8], 11, -2022574463), m, f, d[n + 11], 16, 1839030562), i, m, d[n + 14], 23, -35309556), r = md5_hh(r, i = md5_hh(i, m = md5_hh(m, f, r, i, d[n + 1], 4, -1530992060), f, r, d[n + 4], 11, 1272893353), m, f, d[n + 7], 16, -155497632), i, m, d[n + 10], 23, -1094730640), r = md5_hh(r, i = md5_hh(i, m = md5_hh(m, f, r, i, d[n + 13], 4, 681279174), f, r, d[n + 0], 11, -358537222), m, f, d[n + 3], 16, -722521979), i, m, d[n + 6], 23, 76029189), r = md5_hh(r, i = md5_hh(i, m = md5_hh(m, f, r, i, d[n + 9], 4, -640364487), f, r, d[n + 12], 11, -421815835), m, f, d[n + 15], 16, 530742520), i, m, d[n + 2], 23, -995338651), r = md5_ii(r, i = md5_ii(i, m = md5_ii(m, f, r, i, d[n + 0], 6, -198630844), f, r, d[n + 7], 10, 1126891415), m, f, d[n + 14], 15, -1416354905), i, m, d[n + 5], 21, -57434055), r = md5_ii(r, i = md5_ii(i, m = md5_ii(m, f, r, i, d[n + 12], 6, 1700485571), f, r, d[n + 3], 10, -1894986606), m, f, d[n + 10], 15, -1051523), i, m, d[n + 1], 21, -2054922799), r = md5_ii(r, i = md5_ii(i, m = md5_ii(m, f, r, i, d[n + 8], 6, 1873313359), f, r, d[n + 15], 10, -30611744), m, f, d[n + 6], 15, -1560198380), i, m, d[n + 13], 21, 1309151649), r = md5_ii(r, i = md5_ii(i, m = md5_ii(m, f, r, i, d[n + 4], 6, -145523070), f, r, d[n + 11], 10, -1120210379), m, f, d[n + 2], 15, 718787259), i, m, d[n + 9], 21, -343485551), m = safe_add(m, h), f = safe_add(f, t), r = safe_add(r, g), i = safe_add(i, e) } return Array(m, f, r, i) } function md5_cmn(d, _, m, f, r, i) { return safe_add(bit_rol(safe_add(safe_add(_, d), safe_add(f, i)), r), m) } function md5_ff(d, _, m, f, r, i, n) { return md5_cmn(_ & m | ~_ & f, d, _, r, i, n) } function md5_gg(d, _, m, f, r, i, n) { return md5_cmn(_ & f | m & ~f, d, _, r, i, n) } function md5_hh(d, _, m, f, r, i, n) { return md5_cmn(_ ^ m ^ f, d, _, r, i, n) } function md5_ii(d, _, m, f, r, i, n) { return md5_cmn(m ^ (_ | ~f), d, _, r, i, n) } function safe_add(d, _) { var m = (65535 & d) + (65535 & _); return (d >> 16) + (_ >> 16) + (m >> 16) << 16 | 65535 & m } function bit_rol(d, _) { return d << _ | d >>> 32 - _ }
    return MD5(value);
}

/* Set Article */
function setArticle(articleID) {
    switch (articleID) {
        case 1: document.getElementById('article2').innerHTML = ''; document.getElementById('article3').innerHTML = ''; document.getElementById('article1').innerHTML = '<form id="form" name="form" action="home.aspx" method="post"><input id="identifier" name="identifier" type="hidden" value="login" /><h3 class="legend" style="font-weight: bold">~ Login ~</h3><div id="alertBox"></div><div class="input"><span class="fa fa-envelope-o" aria-hidden="true"></span><input type="email" placeholder="Email Address" name="email" id="email" required="required" maxlength="50" /></div><div class="input"><span class="fa fa-key" aria-hidden="true"></span><input type="password" placeholder="Password" name="pass" id="pass" required="required" maxlength="50" /><i class="fa fa-eye" aria-hidden="true" title="Show/Hide Password" onclick="this.classList.toggle(\'fa-eye-slash\'); changePassVisual()"></i></div><div style="padding: 5px"><div style="float: right"><label for="rememberMe" style="cursor: pointer; padding: 0px; border: none; display: inline-block; font-size: initial; width: 100%"><input id="rememberMe" name="rememberMe" type="checkbox" style="margin-right: 3px" />Remember Me</label></div></div><button type="submit" class="btn submit" onclick="return login()">Login</button><a class="bottom-text-w3ls" style="margin-top: 22px; cursor: pointer">Account Not Activated Yet?</a></form>'; break;
        case 2: document.getElementById('article1').innerHTML = ''; document.getElementById('article3').innerHTML = ''; document.getElementById('article2').innerHTML = '<div id="form"><input id="identifier" name="identifier" type="hidden" value="signup" /><h3 class="legend" style="font-weight: bold">~ Sign Up ~</h3><div id="alertBox"></div><div class="input"><span class="fa fa-user-o" aria-hidden="true"></span><input type="text" placeholder="Full Name" name="fullname" id="fullname" autocomplete="off" required="required" maxlength="50" /><span class="fa fa-user-o" aria-hidden="true"></span><input type="text" placeholder="User Name" name="username" id="username" autocomplete="off" required="required" maxlength="30" /></div><div class="input"><span class="fa fa-envelope-o" aria-hidden="true"></span><input type="text" placeholder="Email Address" name="email" id="email" autocomplete="off" required="required" maxlength="50" /></div><div class="input" style="margin-bottom: 0px"><span class="fa fa-key" aria-hidden="true"></span><input type="password" placeholder="Password" name="pass" id="pass" autocomplete="off" required="required" maxlength="50" /><span class="fa fa-key" aria-hidden="true"></span><input type="password" placeholder="Confirm Password" name="confirm" id="confirm" autocomplete="off" style="width: 95%" required="required" maxlength="50" /><i class="fa fa-eye" aria-hidden="true" title="Show/Hide Password" onclick="this.classList.toggle(\'fa-eye-slash\'); changePassVisual()"></i></div><div class="input100 validate-input" style="text-align: center; height: auto; margin-top: 10px; text-align: center; width: 100%"><label for="termsOfService" style="cursor: pointer; padding: 0px; border: none; display: inline-block; font-size: initial; width: 100%"><input type="checkbox" name="termsOfService" id="termsOfService" required="required" style="cursor: pointer; margin-right: 5px; font-size: 14px">I agree with <a style="text-decoration: underline; color: #149ddd; font-weight: bold" onclick="document.getElementById(\'termsOfServiceModel\').classList.toggle(\'hideModel\');">Terms Of Services</a></label></div><button type="submit" class="btn submit" style="margin-top: 8.5px" onclick="signup()">Sign Up</button></div>'; break;
        case 3: document.getElementById('article1').innerHTML = ''; document.getElementById('article2').innerHTML = ''; document.getElementById('article3').innerHTML = '<div id="form"><input id="identifier" name="identifier" type="hidden" value="recoverPassword" /><h3 class="legend last" style="font-weight: bold">~ Reset Password ~</h3><div id="alertBox"></div><p class="para-style">No worries, Enter your email address below and we\'ll send you an email with instructions <u>if your email is registered</u>.</p> <p class="para-style-2"><strong>Note: </strong> Consider in mind, the email link will expire in 24 hours.</p> <div class="input"><span class="fa fa-envelope-o" aria-hidden="true"></span><input type="email" placeholder="Email Address" name="email" id="email" required="required" maxlength="50" /></div> <button type="submit" class="btn submit last-btn" style="margin-bottom: 4px" onclick="recoverPassword()">Send Reset Link</button></div>'; break;
    }
}

/* Check Password Recovery Request Expired Date */
function checkPasswordRecoveryExpireDate() {
    var request = new XMLHttpRequest();
    request.open('GET', 'responder.aspx?Action=checkReocveryPasswordExpiredDate&Email=' + document.getElementById('email').value + '&ActivationCode=' + document.getElementById('code').value, true);

    request.send(); // Send request to server

    var success = true;
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            if (request.responseText === 'Code 2')
                document.getElementById('content').innerHTML = 'You are 24 hours left, Link is expired. Want to get a new one?<a href="http://messenger.keivanipchihagh.ir/" style="display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px">Request New Link</a>';
            else if (request.responseText === 'Code 0')
                document.getElementById('content').innerHTML = 'Link Not Found - Err 404';
        } else
            success = false;
    };

    if (success === false)
        triggerAlert('error', 'Oops! Something Weird Went Wrong! Might Be Your Connection', 5000);
}

/* Check Activation Email Request Expired Date */
function checkActivationEmailExpireDate() {
    var request = new XMLHttpRequest();
    request.open('GET', 'responder.aspx?Action=checkActivationEmailExpiredDate&Email=' + document.getElementById('email').value + '&ActivationCode=' + document.getElementById('code').value, true);

    request.send(); // Send request to server

    var success = true;
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            if (request.responseText === 'Code 2')
                document.getElementById('content').innerHTML = 'You are 24 hours left, Link is expired. Want to get a new one?<a href="http://messenger.keivanipchihagh.ir/" style="display: block; background-color: #149ddd; padding: 14px 30px; text-align: center; font-weight: bold; color: white; font-size: 15px; margin: 20px 0px 0px 10px">Request New Link</a>';
            else if (request.responseText === 'Code 0')
                document.getElementById('content').innerHTML = 'Link Not Found - Err 404';
        } else
            success = false;
    };

    if (success === false)
        triggerAlert('error', 'Oops! Something Weird Went Wrong! Might Be Your Connection', 5000);
}

/* Disable Back & Forward Button */
setTimeout("preventBack()", 0);
window.onunload = function () { null; };