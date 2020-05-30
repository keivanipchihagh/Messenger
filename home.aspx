<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Messenger.Home" %>

<!DOCTYPE html>
<html>
<head>
    <title>Messenger | Home</title>
    <!-- Favicons -->
    <link href="assets/img/favicon.png" rel="icon">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/home/main.css" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="assets/js/main.js"></script>

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">
</head>
<body class="w3" style="background-image: url('assets/img/loginBg.jpg'); background-size: cover" onload="checkConnection()">

    <!-- Page Container -->
    <div class="w3-container w3-content" style="max-width: 1400px">
        <!-- Header -->
        <div class="w3-row">
            <div style="width: 100%; color: white; font-family: 'Raleway'; text-align: center; padding: 0px">
                <h1 style="padding: 0px; font-family: 'Raleway'; font-weight: 600; letter-spacing: 3px; font-size: 3vw">
                    <img src="assets/img/favicon.png" style="width: 60px; height: 60px; vertical-align: middle; margin: 10px;" /><span>MESSENGER</span></h1>
            </div>
        </div>
        <!-- User Control Panel -->
        <div class="w3-row">
            <div class="w3-container" style="height: 50px; width: 80%; background-color: rgba(255, 255, 255, 0.5); margin: auto; margin-top: 10px; padding: 0px">
                <div style="float: left; height: 100%; margin-left: 10px">
                    <h6 id='fullnameBox' runat="server" style="font-weight: bold">Initializing...</h6>
                </div>
                <div class="w3-dropdown-hover nav-item" style="float: right; height: 100%; background-color: rgb(220, 220, 220)">
                    <button class="w3-button w3-padding-large" style="height: 100%; padding: 0px 15px!important"><i class="fa fa-bell"></i><span class="w3-badge w3-right w3-small w3-red" style="padding: 1px 5px 1px 5px !important">3</span></button>
                    <div class="w3-dropdown-content w3-card-4 w3-bar-block" style="min-width: max-content">
                        <a class="w3-bar-item w3-button menuItem" onclick="document.getElementById('privacyPolicy').style.display='block'"><i class="fa fa-certificate" style="padding-right: 10px"></i>Privacy Policy</a>
                        <a class="w3-bar-item w3-button menuItem" onclick="document.getElementById('versionInfo').style.display='block'"><i class="fa fa-certificate" style="padding-right: 10px"></i>Version 1.2.7</a>
                        <a class="w3-bar-item w3-button menuItem"><i class="fa fa-certificate" style="padding-right: 10px"></i>Friend Requests</a>
                    </div>
                </div>

                <a href="#" class="w3-bar-item w3-padding-large nav-item" title="Log Out" style="float: right; height: 100%" onclick="window.location.replace('http://messenger.keivanipchihagh.ir/')"><i class="fa fa-sign-out"></i></a>
                <a href="#" class="w3-bar-item w3-padding-large nav-item" title="Refresh" style="float: right; height: 100%" onclick="location.reload()"><i class="fa fa-refresh"></i></a>
                <a href="#" class="w3-bar-item w3-padding-large nav-item" onclick="document.getElementById('logs').style.display='block'; fetchLogs()" title="Activities" style="float: right; height: 100%"><i class="fa fa-history"></i></a>

                <div class="w3-dropdown-hover nav-item" style="float: right; height: 100%" title="Contacts">
                    <button class="nav-item" style="height: 100%; padding: 0px 24px !important; border: none; cursor: pointer"><i class="fa fa-users"></i></button>
                    <div id="contacts" runat="server" class="w3-dropdown-content w3-card-4 w3-bar-block" style="max-height: 300px; overflow-y: auto">
                    </div>
                </div>

                <a class="w3-bar-item w3-padding-large" title="Connection Strength" style="float: right; height: 100%; cursor: not-allowed"><i id="connectionStatus" class="fa fa-signal" style="color: lawngreen; font-size: 20px; text-decoration: underline; font-weight: bold"></i></a>
            </div>
            <input type="hidden" id="user_ID" name="user_ID" runat="server" />
            <input type="hidden" id="user_fullname" name="user_fullname" runat="server" />
            <input type="hidden" id="user_username" name="user_username" runat="server" />
            <input type="hidden" id="user_email" name="user_email" runat="server" />
        </div>
        <!-- Main Chat -->
        <div class="w3-row" style="height: 500px">
            <input id="friend_ID" name="friend_ID" type="hidden" />
            <div id="chat" class="w3-container" style="height: 90%; width: 80%; background-color: rgba(255, 255, 255, 0.2); margin: auto; margin-top: 10px; opacity: 0.95; padding: 15px; overflow-y: auto">
            </div>

            <div style="width: 80%; margin: auto; height: 10%; background-color: rgba(255, 255, 255, 0.2); opacity: 0.95">
                <div style="width: 90%; height: 100%; margin: auto; padding-bottom: 10px">
                    <div class="text-input-wrapper" style="width: 90%; float: left">
                        <input id="input" class="text-input" type="text" placeholder="Write Your Message" style="overflow: scroll" maxlength="200" onkeydown="if (event.keyCode == 13) document.getElementById('sendBtn').click()" />
                    </div>
                    <button id="sendBtn" class="sendBtn" style="width: 10%" onclick="sendMessage()">Send</button>
                </div>
            </div>
        </div>
        <!-- End Page Container -->
    </div>

    <!-- Footer -->
    <footer class="w3-container w3-theme-d3 w3-padding-16" style="margin-top: 20px">
        <span>© Copyright <a href="http://keivanipchihagh.ir/" target="_blank">Keivan Ipchi Hagh</a> | All Rights Reserved</span><br />
    </footer>

    <!-- Modals -->
    <div id="logs" class="w3-modal">
        <div class="w3-modal-content">
            <div class="w3-container">
                <span onclick="document.getElementById('logs').style.display='none'" class="w3-button w3-display-topright">&times;</span>
            </div>
            <div class="w3-container" id="logsDataset" style="padding: 30px">
                <h2 style="margin: 0px; font-weight: bold">Loading Logs...</h2>
            </div>
        </div>
    </div>

    <div id="privacyPolicy" class="w3-modal">
        <div class="w3-modal-content">
            <div class="w3-container">
                <span onclick="document.getElementById('privacyPolicy').style.display='none'" class="w3-button w3-display-topright">&times;</span>
            </div>
            <div class="w3-container" style="padding: 30px">
                <h2 style="font-size: 25px; padding: 10px; font-weight: bold">Website Privacy Policy & Terms of Use</h2>
                <hr style="margin: 5px 0px 5px 0px" />
                <h5 style="font-size: 20px; padding: 5px 0px 5px 10px">1. Terms</h5>
                <p style="font-size: 15px">By accessing this Website, accessible from <a href="http://messenger.keivanipchihagh.ir/" target="_blank">messenger.keivanipchihagh.ir</a>, you are agreeing to be bound by these Website Terms and Conditions of Use and agree that you are responsible for the agreement with any applicable local laws. If you disagree with any of these terms, you are prohibited from accessing this site. The materials contained in this Website are protected by copyright and trade mark law.</p>
                <h5 style="font-size: 20px; padding: 5px 0px 5px 10px">2. Disclaimer</h5>
                <p style="font-size: 15px">All of the personal information submitted in this website are well encrypted and proper security measures have been put in action in order maintain users privacy as isolated as possible. However, this website cannot grand 100% security for its users as it's not an enterprise project. Proper counter-measures have been put in effect to create a safe sandbox for users. Security updates will be released every month to update to latest technologies and add features to the platform.</p>
                <h5 style="font-size: 20px; padding: 5px 0px 5px 10px">3. Limitations</h5>
                <p style="font-size: 15px">Messenger or its suppliers will not be hold accountable for any damages that will arise with the use or inability to use the materials on Messenger's Website, even if Messenger or an authorize representative of this Website has been notified, orally or written, of the possibility of such damage. Some jurisdiction does not allow limitations on implied warranties or limitations of liability for incidental damages, these limitations may not apply to you.</p>
                <h5 style="font-size: 20px; padding: 5px 0px 5px 10px">4. Revisions and Errata</h5>
                <p style="font-size: 15px">The materials appearing on Messengers Website may include technical, typographical, or photographic errors. Messenger will not promise that any of the materials in this Website are accurate, complete, or current. Messenger may change the materials contained on its Website at any time without notice. Messenger does not make any commitment to update the materials.</p>
                <hr style="margin: 5px 0px 5px 0px" />
            </div>
        </div>
    </div>

    <div id="versionInfo" class="w3-modal">
        <div class="w3-modal-content">
            <div class="w3-container">
                <span onclick="document.getElementById('versionInfo').style.display='none'" class="w3-button w3-display-topright">&times;</span>
            </div>
            <div class="w3-container" style="padding: 30px">
                <h2 style="font-size: 25px; padding: 10px; font-weight: bold">What's New & What's about to come...</h2>
                <hr style="margin: 5px 0px 5px 0px" />
                This project was intended to be as compact as possible as part of our journey to discover new technologies and technics. However, for the sake of the platform itself, we are adding new features once in a while.<br /><br />What's new with 1.2.7?
                <ul>
                    <li>Secure entry point with email confirmation service</li>
                    <li>With 'Activity Section', you can monitor and report any suspicious account activities</li>
                </ul>
                What's about to come?
                <ul>
                    <li>Upgraded exception handling system</li>
                    <li>Major Security updates</li>
                    <li>Recommendation system</li>
                    <li>Optimized functionalities</li>
                    <li>Minor fixes</li>
                </ul>
                <hr style="margin: 5px 0px 5px 0px" />
            </div>
        </div>
    </div>

    <script>
        // Accordion
        function myFunction(id) {
            var x = document.getElementById(id);
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += " w3-theme-d1";
            } else {
                x.className = x.className.replace("w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-theme-d1", "");
            }
        }

        // Used to toggle the menu on smaller screens when clicking on the menu button
        function openNav() {
            var x = document.getElementById("navDemo");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
            } else {
                x.className = x.className.replace(" w3-show", "");
            }
        }
    </script>

</body>
</html>
