<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Nav" CodeFile="Nav.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Navigasi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="0">
    <meta name="sec" content="Navigasi">
</head>
<body onunload="browserClose()" style="width: 97%">
    <script type="text/javascript" src="/Js/Common.js"></script>
    <div class="sidebar">
        <div class="sidebar-header" style="background: #32aee2">
            <p id="comp" runat="server" style="border-bottom: solid white 2px; margin-bottom: 5px;"></p>
            <div class="sidebar-logo">
                <img src="/Media/icon_setupsales.png" alt="setup sales">
                <p style="line-height: 25px;">
                    SETUP<br />
                    SALES
                </p>
            </div>
        </div>
        <div class="sidebar-content">
            <form id="Form2" method="post" runat="server">
                <p style="display: none; font: bold 8pt; color: gray; padding-left: 5px; padding-bottom: 3px">Security</p>
                <ul class="sidebar-menu">
                    <li>
                        <span class="fa fa-bell-o sb-ico"></span>
                        <input value="Reminder" onclick="go(this, 'Reminder.aspx')" class="navmenu" type="button"></li>
                    <li>
                        <span class="fa fa-bar-chart sb-ico"></span>
                        <input value="Dashboard" onclick="go(this, 'DashboardSetupSales.aspx')" class="navmenu" type="button"></li>
                    <li>
                        <span class="fa fa-pie-chart sb-ico"></span>
                        <input value="Laporan" onclick="go(this, 'Lap.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-list sb-ico"></span>
                    <li>
                        <input value="Skema Cara Bayar" class="navmenu" type="button" onclick="go(this, 'Skema.aspx')">
                    </li>
                    <span class="fa fa-cog sb-ico"></span>
                    <li>
                        <input value="Setup Sales" class="navmenu" type="button" onclick="opensub(this, 'subSetup')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetup')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetup'>
                            <li>
                                <input value="Tipe Sales" onclick="go(this, 'TipeSalesDaftar.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Level Sales" onclick="go(this, 'LevelSalesDaftar.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Tipe Unit" onclick="go(this, 'JenisDaftar.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Lokasi" onclick="go(this, 'LokasiDaftar.aspx')" class="navmenu" type="button"></li>
                            <%--                            <li>
                                <input value="Skema Komisi" onclick="go(this, 'SkemaKomisi.aspx')" class="navmenu" type="button"></li>
                           
                            
                            <li>
                                <input value="Jenis Complain" onclick="go(this, 'ComplainDaftar.aspx')" class="navmenu" type="button"></li>----%>
                        </ul>
                    </li>
                    <span class="fa fa-user sb-ico"></span>
                    <li>
                        <input value="Marketing" onclick="go(this, 'Agent.aspx'); opensub(this, 'subSales');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSales')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSales'>
                            <li>
                                <input value="Pendaftaran..." onclick="go(this, 'AgentDaftar.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Upload..." onclick="go(this, 'AgentUpload.aspx')" class="navmenu" type="button"></li>
                            <%--                            <li>
                                <input value="Target..." onclick="go(this, 'AgentTarget.aspx')" class="navmenu" type="button"></li>--%>
                            <%--                            <li>
                                <input value="Pendaftaran Level..." onclick="go(this, 'AgentLevelDaftar.aspx')" class="navmenu" type="button"></li>--%>
                        </ul>
                    </li>
                    <span class="fa fa-building-o sb-ico"></span>
                    <li>
                        <input value="Unit" onclick="go(this, 'Unit.aspx'); opensub(this, 'subUnit');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subUnit')"></span>
                        <ul class="nav-submenu" style="display: none;" id="subUnit">
                            <li>
                                <input value="Pendaftaran..." onclick="go(this, 'UnitDaftar.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Price List" onclick="go(this, 'PL.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Upload Unit..." onclick="go(this, 'UnitUpload.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Upload Price List..." onclick="go(this, 'UnitUploadPL.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Upload Price List Skema..." onclick="go(this, 'UnitUploadPLSkema.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Upload Floor Plan..." onclick="go(this, 'Peta.aspx')" class="navmenu" type="button" />
                            </li>
                            <li>
                                <input value="Upload Koordinat..." onclick="go(this, 'PetaUploadKoord.aspx')" class="navmenu" type="button" />
                            </li>
                        </ul>
                    </li>
                    <span class="fa fa-cog sb-ico"></span>
                    <li>
                        <input value="Setup Gimmick" class="navmenu" type="button" onclick="go(this, 'Gimmick.aspx'); opensub(this, 'subSetupGimmick');"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetupGimmick')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetupGimmick'>
                            <li>
                                <input value="Setup Tipe Gimmick" onclick="go(this, 'TipeGimmick.aspx')" class="navmenu" type="button" />
                            </li>
                            <li>
                                <input value="Pendaftaran Gimmick" onclick="go(this, 'GimmickDaftar.aspx')" class="navmenu" type="button" />
                            </li>
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li style="display: none;">
                        <input value="Komisi" onclick="opensub(this, 'subKomisi');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subKomisi')"></span>
                        <ul class="nav-submenu" style="display: none;" id="subKomisi">
                            <li>
                                <input value="Agent.." onclick="go(this, 'KomisiAgent.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Closing Fee.." onclick="go(this, 'KomisiClosingBonus.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Overriding.." onclick="go(this, 'KomisiOverriding.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <li>
                        <span class="fa fa-file-o sb-ico"></span>
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
            console.log(parent);
            if (sub.style.display === 'none') {
                sub.style.display = "block";
                //parent.className = "";
                parent.className = "fa fa-minus-square addons"
            } else {
                sub.style.display = "none";
                //parent.className = "";
                parent.className = "fa fa-plus-square addons";
            }
        }
        function go(foo, href) {
            bold(foo);
            top.content.location.href = href;
        }
        function bold(foo) {
            for (i = 1; i < document.Form2.elements.length - 2; i++) {
                document.Form2.elements[i].style.fontWeight = 'normal';
            }
            foo.style.fontWeight = 'bold';
        }
    </script>
    <style type="text/css">
        body {
            margin-bottom: 0px;
        }

        .sb-ico {
            position: absolute;
            color: #b2c0c9;
            line-height: 41px;
            font-size: 13pt;
            padding-left: 20px;
        }

        .addons {
            position: absolute;
            color: #669ccb;
            line-height: 41px;
            right: 30px;
            font-size: 11pt;
        }

        .frame-wrapper {
            min-height: 600px;
            margin-left: 20%;
            border: 1px solid red;
            margin-top: 70px;
        }

        .navmenu:hover {
            background: #fafbfd;
            /*font-style: italic;*/
        }

        .navmenu {
            width: 100%;
            border: 1px solid transparent;
            background: transparent;
            padding: 8px;
            padding-left: 45px;
            font-weight: 700 !important;
            font-size: 10pt;
            text-align: left;
            color: #636466;
        }

        .sidebar-menu {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        .sidebar-content {
            background: #f0f3f8;
            /*height: 350px;*/
            margin-top: 10px;
        }

        .sidebar-logo > p {
            margin: 0px;
            color: white;
            font-size: 18pt;
            float: left;
            padding-left: 20px;
            /*padding-top:10px;*/
            line-height: 50px;
            height: 63px;
        }

        .sidebar-logo > img {
            float: left;
            width: 60px;
        }

        .sidebar-logo {
            display: inline-block;
        }

        .sidebar-header > p {
            margin: 0px;
            color: white;
            padding-top: 10px;
            padding-bottom: 10px;
        }

        .sidebar-header {
            background: #5c9bd1;
            height: 120px;
            text-align: center;
        }

        .sidebar {
            /*height: 480px;*/
            width: 100%;
            border: 1px solid transparent;
            /*position: fixed;*/
            z-index: 100;
            float: left;
            /*margin-top:10px;*/
            font-family: 'Open Sans';
        }
    </style>
</body>
</html>
