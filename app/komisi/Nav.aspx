<%@ Page Language="c#" Inherits="ISC064.KOMISI.Nav" CodeFile="Nav.aspx.cs" %>

<!DOCTYPE html>
<html>
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
    <form id="Form1" method="post" runat="server">
        <script type="text/javascript" src="/Js/Common.js"></script>
        <div class="sidebar">
            <div class="sidebar-header" style="background: #9450a3">
                <p id="comp" runat="server" style="border-bottom: solid white 2px; margin-bottom: 5px;"></p>
                <div class="sidebar-logo">
                    <img src="/Media/icon_komisi.png">
                    <p>KOMISI</p>
                </div>
            </div>
            <div class="sidebar-content">
                <ul class="sidebar-menu">
                    <li>
                        <i class="fa fa-bell-o sb-ico"></i>
                        <input value="Reminder" onclick="go(this, 'Reminder.aspx')" class="navmenu" type="button">
                    </li>
                    <li style="display:none">
                        <i class="fa fa-bar-chart sb-ico"></i>
                        <input value="Dashboard" onclick="go(this, 'DashboardKomisi.aspx')" class="navmenu" type="button">
                    </li>
                    <li>
                        <i class="fa fa-pie-chart sb-ico"></i>
                        <input value="Laporan" onclick="go(this, 'Lap.aspx')" class="navmenu" type="button">
                    </li>
                    <span class="fa fa-cog sb-ico"></span>
					<li>
                        <input value="Setup" class="navmenu" type="button" onclick="opensub(this, 'subSetup')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetup')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetup'>
                            <li>
                                <input value="Skema Komisi" onclick="go(this, 'SkemaKomisi.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Termin Komisi" onclick="go(this, 'TerminKomisi.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Skema Closing Fee" onclick="go(this, 'SkemaCF.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Reward" onclick="go(this, 'SkemaReward.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Closing Fee" class="navmenu" type="button" onclick="go(this, 'CF.aspx'); opensub(this, 'subCF')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subCF')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subCF'>
                            <li>
                                <input value="Generate" onclick="go(this, 'CFRegis1.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False" onclick="go(this, 'CFDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Pengajuan Closing Fee" class="navmenu" type="button" onclick="go(this, 'CFP.aspx'); opensub(this, 'subCFP')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subCFP')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subCFP'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'CFPRegis1.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False" onclick="go(this, 'CFPDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Realisasi Closing Fee" class="navmenu" type="button" onclick="go(this, 'CFR.aspx'); opensub(this, 'subCFR')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subCFR')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subCFR'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'CFRRegis1.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False" onclick="go(this, 'CFRDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Komisi" class="navmenu" type="button" onclick="go(this, 'Komisi.aspx'); opensub(this, 'subKomisi')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subKomisi')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subKomisi'>
                            <li>
                                <input value="Generate" onclick="go(this, 'KomisiRegis1.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False" onclick="go(this, 'KomisiDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Pengajuan Komisi" class="navmenu" type="button" onclick="go(this, 'KomisiP.aspx'); opensub(this, 'subKomisiP')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subKomisiP')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subKomisiP'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'KomisiPRegis1.aspx')" class="navmenu" type="button"></li>
                             <li>
                                <input value="Clear False" onclick="go(this, 'KomisiPDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Realisasi Komisi" class="navmenu" type="button" onclick="go(this, 'KomisiR.aspx'); opensub(this, 'subKomisiR')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subKomisiR')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subKomisiR'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'KomisiRRegis1.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False" onclick="go(this, 'KomisiRDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-gift sb-ico"></span>
                    <li>
                        <input value="Reward" class="navmenu" type="button" onclick="go(this, 'Reward.aspx'); opensub(this, 'subReward')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subReward')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subReward'>
                            <li>
                                <input value="Generate" onclick="go(this, 'RewardRegis1.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False" onclick="go(this, 'RewardDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-gift sb-ico"></span>
                    <li>
                        <input value="Pengajuan Reward" class="navmenu" type="button" onclick="go(this, 'RewardP.aspx'); opensub(this, 'subRewardP')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subRewardP')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subRewardP'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'RewardPRegis1.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False" onclick="go(this, 'RewardPDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-gift sb-ico"></span>
                    <li>
                        <input value="Realisasi Reward" class="navmenu" type="button" onclick="go(this, 'RewardR.aspx'); opensub(this, 'subRewardR')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subRewardR')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subRewardR'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'RewardRRegis1.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Clear False" onclick="go(this, 'RewardRDel.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <li>
                        <i class="fa fa-file-o sb-ico"></i>
                        <input value="Log File" onclick="go(this, 'Log.aspx')" class="navmenu" type="button">
                    </li>
                </ul>
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

    </form>
</body>
</html>
