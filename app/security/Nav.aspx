<%@ Page Language="c#" Inherits="ISC064.SECURITY.Nav" CodeFile="Nav.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Navigasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="0">
    <meta name="sec" content="Navigasi">
</head>
<body onunload="browserClose()" style="width: 97%">
    <script type="text/javascript" src="/Js/Common.js"></script>
    <div class="sidebar">
        <div class="sidebar-header" style="background: #f36d54">
            <p id="comp" runat="server" style="border-bottom: solid white 2px; margin-bottom: 5px;"></p>
            <div class="sidebar-logo">
                <img src="/Media/icon_security.png">
                <p>SECURITY</p>
            </div>
        </div>
        <div class="sidebar-content">
            <form id="Form1" method="post" runat="server">
                <p style="display: none; font: bold 8pt; color: gray; padding-left: 5px; padding-bottom: 3px">Security</p>
                <ul class="sidebar-menu">
                    <span class="fa fa-bell-o sb-ico"></span>
                    <li>
                        <input value="Reminder" onclick="go(this, 'Reminder.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-pie-chart sb-ico"></span>
                    <li>
                        <input value="Laporan" onclick="go(this, 'Lap.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-cog sb-ico"></span>
                    <li>
                        <input value="Setup" class="navmenu" type="button" onclick="opensub(this, 'subSetup')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetup')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetup'>
                            <li>
                                <input value="Mapping Program" onclick="go(this, 'Mapping.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Mapping Program Plugin" onclick="go(this, 'MappingPlugin.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Security Level" onclick="go(this, 'SecLevel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-user-o sb-ico"></span>
                    <li>
                        <input value="Username" onclick="go(this, 'Username.aspx'); opensub(this, 'subUsername');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subUsername')"></span>
                        <ul class="nav-submenu" style="display: none;" id="subUsername">
                            <li>
                                <input value="Pendaftaran..." onclick="go(this, 'Pendaftaran.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Set Password Baru..." onclick="go(this, 'SetPass.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Aktivasi..." onclick="go(this, 'Aktivasi.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Blokir..." onclick="go(this, 'Blokir.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-credit-card sb-ico"></span>
                    <li>
                        <input value="Matrikulasi Kode Akses" onclick="go(this, 'MKA.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-credit-card sb-ico"></span>
                    <li>
                        <input value="Matrikulasi Akses Project" onclick="go(this, 'MKP.aspx')" class="navmenu" type="button"
                            onmouseover="over(this)" onmouseout="out(this)">
                    </li>
                    <span class="fa fa-credit-card sb-ico"></span>
                    <li>
                        <input value="Matrikulasi Perusahaan" onclick="go(this, 'PT.aspx')" class="navmenu" type="button"
                            onmouseover="over(this)" onmouseout="out(this)">
                    </li>
                    <span class="fa fa-calendar sb-ico"></span>
                    <li>
                        <input value="Absensi" onclick="go(this, 'Absensi.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-credit-card sb-ico"></span>
                    <li>
                        <input value="Project" onclick="go(this, 'Project.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-user-o sb-ico"></span>
                    <li>
                        <input value="Launching" onclick=" opensub(this, 'subCounterLaunching');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subCounterLaunching')"></span>
                        <ul class="nav-submenu" style="display: none;" id="subCounterLaunching">
                            <li>
                                <input value="Counter..." onclick="go(this, 'CounterLaunching.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-database sb-ico"></span>
                    <li>
                        <input value="BackupDatabase" onclick="go(this, 'BackupDatabase.aspx')" class="navmenu" type="button" /></li>
                    <span class="fa fa-exclamation-circle sb-ico"></span>
                    <li>
                        <input value="Error" onclick="go(this, 'Problem.aspx')" class="navmenu" type="button" /></li>
                    <span class="fa fa-file-o sb-ico"></span>
                    <li>
                        <input value="Log File" onclick="go(this, 'Log.aspx')" class="navmenu" type="button"></li>
                </ul>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        function opensub(sender, id) {
            // console.log(id);
            var sub = document.getElementById(id);
            var parent = sender.parentElement.getElementsByTagName('span')[0];
            // console.log(parent);
            if (sub.style.display === 'none') {
                sub.style.display = "block";
                parent.className = "";
                parent.className = "fa fa-minus-square addons"
            } else {
                sub.style.display = "none";
                parent.className = "";
                parent.className = "fa fa-plus-square addons";
            }
        }
        function go(foo, href) {
            bold(foo);
            top.content.location.href = href;
        }
        function bold(foo) {
            for (i = 1; i < document.Form1.elements.length - 2; i++) {
                document.Form1.elements[i].style.fontWeight = 'normal';
            }
            foo.style.fontWeight = 'bold';
        }
    </script>
    <style type="text/css">
        ::-webkit-scrollbar {
            width: 7px;
            height: 7px;
        }

        ::-webkit-scrollbar-button {
            width: 0px;
            height: 0px;
        }

        ::-webkit-scrollbar-thumb {
            background: #5c9bd1;
            border: 0px none #ffffff;
            border-radius: 50px;
        }

            ::-webkit-scrollbar-thumb:hover {
                background: #5c9bd1;
            }

            ::-webkit-scrollbar-thumb:active {
                background: #5c9bd1;
            }

        ::-webkit-scrollbar-track {
            background: #ffffff;
            border: 0px none #000000;
            border-radius: 20px;
        }

            ::-webkit-scrollbar-track:hover {
                background: #ffffff;
            }

            ::-webkit-scrollbar-track:active {
                background: #ffffff;
            }

        ::-webkit-scrollbar-corner {
            background: transparent;
        }
    </style>
</body>
</html>
