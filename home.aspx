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
<body class="w3" style="background-image: url('assets/img/loginBg.jpg'); background-size: cover">

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
            <div class="w3-container" style="height: 50px; width: 80%; background-color: white; margin: auto; margin-top: 10px; padding: 0px">
                <div style="float: left; height: 100%; margin-left: 10px">
                    <h6 id='fullnameBox' runat="server" style="font-weight: bold">Initializing...</h6>
                </div>
                <div class="w3-dropdown-hover nav-item" style="float: right; height: 100%; background-color: rgb(220, 220, 220)">
                    <button class="w3-button w3-padding-large" style="height: 100%; padding: 0px 15px!important"><i class="fa fa-bell"></i><span class="w3-badge w3-right w3-small w3-red" style="padding: 1px 5px 1px 5px !important">3</span></button>
                    <div class="w3-dropdown-content w3-card-4 w3-bar-block" style="min-width: max-content">
                        <a href="#" class="w3-bar-item w3-button menuItem"><i class="fa fa-certificate" style="padding-right: 10px"></i>Privacy Policy</a>
                        <a href="#" class="w3-bar-item w3-button menuItem"><i class="fa fa-certificate" style="padding-right: 10px"></i>Version 1.0.7</a>
                        <a href="#" class="w3-bar-item w3-button menuItem"><i class="fa fa-certificate" style="padding-right: 10px"></i>Friend Requests</a>
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

                <a class="w3-bar-item w3-padding-large" title="Connected" style="float: right; height: 100%; cursor: not-allowed"><i class="fa fa-signal" style="color: lawngreen; font-size: 20px; text-decoration: underline; font-weight: bold"></i></a>
            </div>
            <input type="hidden" id="user_ID" name="user_ID" runat="server" />
            <input type="hidden" id="user_fullname" name="user_fullname" runat="server" />
            <input type="hidden" id="user_username" name="user_username" runat="server" />
            <input type="hidden" id="user_email" name="user_email" runat="server" />
        </div>
        <!-- Main Chat -->
        <div class="w3-row" style="height: 500px">
            <input id="friend_ID" name="friend_ID" type="hidden" />
            <div id="chat" class="w3-container" style="height: 90%; width: 80%; background-color: white; margin: auto; margin-top: 10px; opacity: 0.95; padding: 15px; overflow-y: auto">
            </div>

            <div style="width: 80%; margin: auto; height: 10%; background-color: white; opacity: 0.95">
                <div style="width: 90%; height: 100%; margin: auto; padding-bottom: 10px">
                    <div class="text-input-wrapper" style="width: 90%; float: left">
                        <input id="input" class="text-input" type="text" placeholder="Write Your Message" style="overflow: scroll" maxlength="200" onkeydown = "if (event.keyCode == 13) document.getElementById('sendBtn').click()" />
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
