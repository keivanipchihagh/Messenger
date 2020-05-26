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
</head>
<body class="w3-theme-l5" style="background-image: url('assets/img/loginBg.jpg'); background-size: cover">

    <!-- Navbar -->
    <div class="w3-top">
        <div class="w3-bar w3-theme-d2 w3-left-align w3-large" style="background-color: white">
            <a class="w3-bar-item w3-button w3-hide-medium w3-hide-large w3-right w3-padding-large w3-hover-white w3-large w3-theme-d2" href="javascript:void(0);" onclick="openNav()"><i class="fa fa-bars"></i></a>
            <a href="#" class="w3-bar-item w3-button w3-padding-large" style="background-color: lightgray">
                <img src="assets/img/favicon.png" style="height: 30px; width: 30px" />
                <span style="font-size: 20px">Messenger</span>
            </a>
            <a href="#" class="w3-bar-item w3-padding-large nav-item" title="Log Out" style="float: right" onclick="window.location.replace('http://messenger.keivanipchihagh.ir/')"><i class="fa fa-sign-out"></i></a>
            <a href="#" class="w3-bar-item w3-padding-large nav-item" title="Refresh" style="float: right" onclick="location.reload()"><i class="fa fa-refresh"></i></a>
            <a href="#" class="w3-bar-item w3-padding-large nav-item" onclick="document.getElementById('logs').style.display='block'; fetchLogs()" title="Logs" style="float: right"><i class="fa fa-history"></i></a>
            <div class="w3-dropdown-hover w3-hide-small" style="float: right">
                <button class="w3-button w3-padding-large nav-item" title="Notifications"><i class="fa fa-bell"></i><span class="w3-badge w3-right w3-small w3-green">3</span></button>
                <div class="w3-dropdown-content w3-card-4 w3-bar-block">
                    <a href="#" class="w3-bar-item w3-button menuItem"><i class="fa fa-certificate" style="padding-right: 10px"></i>Security Update 1.0.7</a>
                    <a href="#" class="w3-bar-item w3-button menuItem"><i class="fa fa-certificate" style="padding-right: 10px"></i>Version 1.0.7 is Here!</a>
                    <a href="#" class="w3-bar-item w3-button menuItem"><i class="fa fa-certificate" style="padding-right: 10px"></i>Policy Rules Updated</a>
                </div>
            </div>
        </div>
    </div>

    <!-- Navbar on small screens -->
    <div id="navDemo" class="w3-bar-block w3-theme-d2 w3-hide w3-hide-large w3-hide-medium w3-large">
        <a href="#" class="w3-bar-item w3-button w3-padding-large">Link 1</a>
        <a href="#" class="w3-bar-item w3-button w3-padding-large">Link 2</a>
        <a href="#" class="w3-bar-item w3-button w3-padding-large">Link 3</a>
        <a href="#" class="w3-bar-item w3-button w3-padding-large">My Profile</a>
    </div>

    <!-- Page Container -->
    <div class="w3-container w3-content" style="max-width: 1400px; margin-top: 80px">
        <!-- The Grid -->
        <div class="w3-row">
            <!-- Left Column -->
            <div class="w3-col l3">
                <!-- Profile -->
                <div class="w3-card w3-round w3-white">
                    <div class="w3-container">
                        <h4 id="fullname" name="fullname" runat="server" class="w3-center"></h4>
                        <p class="w3-center">
                            <div class="container">
                                <img src="assets/img/profile-img.jpg" alt="Avatar" class="image w3-circle" style="width: 100%; height: 100%">
                                <div class="middle">
                                    <div class="text">Change</div>
                                </div>
                            </div>
                        </p>
                        <hr />
                        <p><i class="fa fa-server fa-fw w3-margin-right w3-text-theme"></i>Status: <span style="color: limegreen; font-weight: bold">Online</span></p>
                        <p id="username" runat="server"><i class="fa fa-user-o fa-fw w3-margin-right w3-text-theme"></i>Username: </p>
                        <p id="email" runat="server"><i class="fa fa-envelope-o fa-fw w3-margin-right w3-text-theme"></i>Email: </p>
                        <input type="hidden" id="user_ID" name="user_ID" runat="server" />
                        <input type="hidden" id="user_fullname" name="user_fullname" runat="server" />
                        <input type="hidden" id="user_username" name="user_username" runat="server" />
                        <input type="hidden" id="user_email" name="user_email" runat="server" />
                    </div>
                </div>
                <br>
                <!-- Accordion -->
                <div class="w3-card w3-round">
                    <div class="w3 w3-round">
                        <button class="w3-button w3-round w3-block w3-theme-l1 w3-left-align accordion" onclick="myFunction('contacts')"><i class="fa fa-address-book-o fa-fw w3-margin-right"></i>Contacts</button>
                        <div id="contacts" name="contacts" runat="server" class="w3-hide w3-container contacts">
                        </div>
                    </div>
                </div>
                <br>
                <!-- End Left Column -->
            </div>

            <!-- Middle Column -->
            <div class="w3-col l9">
                <div class="w3-container w3-card w3-white w3-round" style="height: 590px; margin: 0px 16px 16px 16px">
                    <br>
                    <div style="width: 100%; height: 10%">
                        <img src="assets/img/profile-img.jpg" class="w3-circle" style="height: 50px; width: 50px; display: inline-block" alt="Avatar">
                        <span class="w3-right w3-opacity" style="font-weight: bold;">Online</span>
                        <h4 style="display: inline-block; margin-left: 10px">Hidden User</h4>
                        <br>
                    </div>
                    <hr style="margin: 0px">
                    <div id="chat" runat="server" style="width: 100%; height: 77%; overflow-y: auto; padding: 10px">
                        <div style="width: 100%">
                            <div class="chatBox chatBox-left">
                                <div class="chatBox-container-left">
                                    <p class="chatBox-content">Hey... ummmm I'm board. Let's do something, like literally anything</p>
                                    <div class="chatBox-time-container-left">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-right">
                                <div class="chatBox-container-right">
                                    <p class="chatBox-content">LOL, Seems like our tech support is board 😂</p>
                                    <div class="chatBox-time-container-right">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-left">
                                <div class="chatBox-container-left">
                                    <p class="chatBox-content">I was coding for like 3 hours straight, give me a break. I have a dirty idea though. We've like 40 messages. Wanna read them? 😁</p>
                                    <div class="chatBox-time-container-left">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-right">
                                <div class="chatBox-container-right">
                                    <p class="chatBox-content">For god's sake u r tech support. U should be supporting NOT READING PRIVATE MESSAGES, you dumb-ass</p>
                                    <div class="chatBox-time-container-right">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-left">
                                <div class="chatBox-container-left">
                                    <p class="chatBox-content">But it's fun, come on I ain't got the database pass. plz plz plz</p>
                                    <div class="chatBox-time-container-left">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-right">
                                <div class="chatBox-container-right">
                                    <p class="chatBox-content">Even if we do, they are encrypted remember?</p>
                                    <div class="chatBox-time-container-right">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-left">
                                <div class="chatBox-container-left">
                                    <p class="chatBox-content">I have a backdoor 🤪</p>
                                    <div class="chatBox-time-container-left">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-right">
                                <div class="chatBox-container-right">
                                    <p class="chatBox-content">Oh boy, Look who i've trusted my security with. I'm deleting your account. U r officially fired 😂😂</p>
                                    <div class="chatBox-time-container-right">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-left">
                                <div class="chatBox-container-left">
                                    <p class="chatBox-content">NOOOOooooo. fine whatever you say, you are the boss</p>
                                    <div class="chatBox-time-container-left">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="chatBox chatBox-right">
                                <div class="chatBox-container-right">
                                    <p class="chatBox-content">yeah, you bet</p>
                                    <div class="chatBox-time-container-right">
                                        <span class="chatBox-time">13:10:15
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="width: 90%; margin: auto; height: 40px; padding-top: 5px">
                        <div class="w3-col l11 text-input-wrapper" style="">
                            <input class="text-input" type="text" placeholder="Write Your Message" />
                        </div>

                        <button class="w3-col l1 sendBtn">Send</button>
                    </div>
                </div>
                <!-- End Middle Column -->
            </div>
            <!-- End Grid -->
        </div>
        <!-- End Page Container -->
    </div>
    <br>

    <!-- Footer -->
    <footer class="w3-container w3-theme-d3 w3-padding-16">
        <span>© Copyright <a href="http://keivanipchihagh.ir/" target="_blank">Keivan Ipchi Hagh</a> | All Rights Reserved</span><br />
    </footer>

    <!-- Modals -->
    <div id="logs" class="w3-modal">
        <div class="w3-modal-content">
            <div class="w3-container">
                <span onclick="document.getElementById('logs').style.display='none'" class="w3-button w3-display-topright">&times;</span>
            </div>
            <div class="w3-container" id="logsDataset" style="padding: 30px">
                <h2 style="margin: 0px;">Most Recent Logs</h2>
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
