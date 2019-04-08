<%@ Page Language="c#" Inherits="ISC064.KPA.Nav" CodeFile="Nav.aspx.cs" %>

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
        <div class="sidebar-header" style="background: #6576f1">
            <p id="comp" runat="server" style="border-bottom: solid white 2px; margin-bottom: 5px;"></p>
            <div class="sidebar-logo">
                <img src="/Media/icon_kpr.png" alt="kpa">
                <p>
                    KPR
                </p>
            </div>
        </div>
        <div class="sidebar-content">
            <form id="Form1" method="post" runat="server">
                <p style="display: none; font: bold 8pt; color: gray; padding-left: 5; padding-bottom: 3">Security</p>
                <ul class="sidebar-menu">
                    <span class="fa fa-bell-o sb-ico"></span>
                    <li>
                        <input value="Reminder" onclick="go(this, 'Reminder.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-pie-chart sb-ico"></span>
                    <li>
                        <input value="Laporan" onclick="go(this, 'Lap.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-university sb-ico"></span>
                    <li>
                        <input value="Setup Bank" onclick="go(this, 'Acc.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-cog sb-ico"></span>
                    <li>
                        <input value="Setup Kategori Retensi" onclick="go(this, 'Retensi.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-tasks sb-ico"></span>
                    <li>
                        <input value="Master KPR" onclick="go(this, 'Kontrak.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-tasks sb-ico"></span>
                    <li>
                        <input value="Proses KPR" class="navmenu" type="button" onclick="opensub(this, 'subProsesKpr');"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subProsesKpr')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subProsesKpr'>
                            <li>
                                <input value="Cek Berkas" onclick="go(this, 'KontrakBerkas.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Wawancara" onclick="go(this, 'KontrakWawancaraEdit.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="OTS" onclick="go(this, 'KontrakOTSEdit.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="LPA" onclick="go(this, 'KontrakLPAEdit.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="SP3K" onclick="go(this, 'KontrakSP3KEdit.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Akad" onclick="go(this, 'KontrakAkadEdit.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-list-alt sb-ico"></span>
                    <li>
                        <input value="Pengajuan" onclick="go(this, 'Pengajuan.aspx'); opensub(this, 'subPengajuan');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subPengajuan')"></span>
                        <ul class="nav-submenu"  style="display: none;" id='subPengajuan'>
                            <li>
                                <input value="Registrasi Pengajuan" onclick="go(this, 'KontrakPengajuan.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Realisasi" onclick="go(this, 'Realisasi.aspx'); opensub(this, 'subRealisasi');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subRealisasi')"></span>
                        <ul class="nav-submenu" style="display: none;" id="subRealisasi">
                            <li>
                                <input value="Realisasi Retensi" onclick="go(this, 'KontrakRealisasi.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-file-o sb-ico"></span>
                    <li>
                        <input value="Log File" onclick="go(this, 'Log.aspx')" class="navmenu" type="button"></li>
                </ul>
                <script language="javascript">
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
                    function over(foo) {
                        foo.style.background = 'silver';
                    }
                    function out(foo) {
                        foo.style.background = '#EEEEEE';
                    }
                    function bold(foo) {
                        for (i = 1; i < document.Form1.elements.length - 2; i++) {
                            document.Form1.elements[i].style.fontWeight = 'normal';
                        }
                        foo.style.fontWeight = 'bold';
                    }
                    function expand(id) {
                        x = true;
                        i = 1;
                        while (x) {
                            if (document.getElementById(id + i)) {
                                document.getElementById(id + i).style.display = '';
                                i++;
                            } else {
                                x = false;
                            }
                        }
                    }
                    function hide(id) {
                        x = true;
                        i = 1;
                        while (x) {
                            if (document.getElementById(id + i)) {
                                if (document.getElementById(id + i).style.display == 'none')
                                    document.getElementById(id + i).style.display = '';
                                else
                                    document.getElementById(id + i).style.display = 'none';
                                i++;
                            } else {
                                x = false;
                            }
                        }
                    }
                </script>

            </form>

        </div>
    </div>
</body>
</html>
