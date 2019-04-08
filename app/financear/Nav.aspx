<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Nav" CodeFile="Nav.aspx.cs" %>

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
    <script type="text/javascript" src="/Js/Common.js"></script>
    <div class="sidebar">
        <div class="sidebar-header" style="background: #6eb123">
            <p id="comp" runat="server" style="border-bottom: solid white 2px; margin-bottom: 5px;"></p>
            <div class="sidebar-logo">
                <img src="/Media/icon_finance.png">
                <p style="font-size: 16pt; line-height: 25px;">FINANCE &
                    <br />
                    AR</p>
            </div>
        </div>
        <div class="sidebar-content">
            <form id="Form1" method="post" runat="server">
                <p style="display: none; font: bold 8pt; color: gray; padding-left: 5px; padding-bottom: 3px">Finance & AR</p>
                <ul class="sidebar-menu">
                    <span class="fa fa-bell-o sb-ico"></span>
                    <li>
                        <input value="Reminder" onclick="go(this, 'Reminder.aspx')" class="navmenu" type="button">
                    </li>

                    <span class="fa fa-bar-chart sb-ico"></span>
                    <li>
                        <input value="Dashboard" onclick="go(this, 'DashboardFinanceAR.aspx')" class="navmenu" type="button">
                    </li>

                    <span class="fa fa-pie-chart sb-ico"></span>
                    <li>
                        <input value="Laporan" onclick="go(this, 'Lap.aspx')" class="navmenu" type="button">
                    </li>
                    <span class="fa fa-credit-card sb-ico"></span>
                    <li>
                        <input value="Setup Rekening" class="navmenu" type="button" onclick="opensub(this, 'subSetupRek')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetupRek')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetupRek'>
                            <li>
                                <input value="Registrasi Rekening" onclick="go(this, 'Acc.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-user sb-ico"></span>
                    <li>
                        <input value="Customer Information File" onclick="go(this, 'Customer.aspx')" class="navmenu" type="button">
                    </li>

                    <span class="fa fa-credit-card sb-ico"></span><%--pindah ke modul faktur--%>
                        <li>
                            <input value="Faktur Pajak" class="navmenu" type="button" onclick="go(this, 'FP.aspx'); opensub(this, 'subFaktur');"><span class="fa fa-plus-square addons" onclick="go(this,'FP.aspx'); opensub(this,'subFaktur');"></span>
                            <ul class="nav-submenu" style="display: none;" id='subFaktur'>
                                <li>
                                    <input value="Upload Faktur Pajak" onclick="go(this, 'FPUpload.aspx')" class="navmenu" type="button"></li>
                                <li>
                                    <input value="Posting Faktur Pajak" onclick="go(this, 'FPPosting.aspx')" class="navmenu" type="button"></li>
                                <li>
                                    <input value="Export Faktur Pajak" onclick="go(this, 'Espt.aspx')" class="navmenu" type="button"></li>
                            </ul>
                        </li>

                    <span class="fa fa-list sb-ico"></span>
                    <li>
                        <input value="Kuitansi" class="navmenu" type="button" onclick="go(this, 'TTS.aspx'); opensub(this, 'subKwitansi');"><span class="fa fa-plus-square addons" onclick="go(this, 'TTS.aspx'); opensub(this, 'subKwitansi');"></span>
                        <ul class="nav-submenu" style="display: none;" id='subKwitansi'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'TTSRegistrasi.aspx')" class="navmenu" type="button"></li>
                            <%--<li><input value="Print Kuitansi" onclick="go(this,'PrintBKMBatch.aspx')" class="navmenu" type="button"></li> --%>
                            <li>
                                <input value="Print Kuitansi" onclick="openPopUp('PrintBKMBatch.aspx', '955', '650');" class="navmenu" type="button"></li>
                        </ul>
                    </li>

                    <span class="fa fa-exchange sb-ico"></span>
                    <li>
                        <input value="Transfer Anonim" class="navmenu" type="button" onclick="go(this, 'TransferAnonim.aspx'); opensub(this, 'subTrans');"><span class="fa fa-plus-square addons" onclick="go(this, 'TransferAnonim.aspx'); opensub(this, 'subTrans');"></span>
                        <ul class="nav-submenu" style="display: none;" id='subTrans'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'TransferAnonimRegistrasi.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>

                    <span class="fa fa-sticky-note sb-ico"></span>
                    <li>
                        <input value="Memo" class="navmenu" type="button" onclick="go(this, 'Memo.aspx'); opensub(this, 'subMemo');"><span class="fa fa-plus-square addons" onclick="go(this, 'Memo.aspx'); opensub(this, 'subMemo');"></span>
                        <ul class="nav-submenu" style="display: none;" id='subMemo'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'MemoRegistrasi.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>

                    <span class="fa fa-address-card-o sb-ico"></span>
                    <li>
                        <input value="Virtual Account" class="navmenu" type="button" onclick="go(this, 'VA.aspx'); opensub(this, 'subVirtual');"><span class="fa fa-plus-square addons" onclick="go(this, 'VA.aspx'); opensub(this, 'subVirtual');"></span>
                        <ul class="nav-submenu" style="display: none;" id='subVirtual'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'VARegis.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Upload No VA" onclick="go(this, 'NoVAUpload.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Import Transaksi" onclick="go(this, 'VAImport.aspx')" class="navmenu" type="button" /></li>
                            <li>
                                <input value="Export Transaksi" onclick="go(this, 'VAEkspor.aspx')" class="navmenu" type="button" /></li>
                        </ul>
                    </li>

                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Refund Lebih Bayar" class="navmenu" type="button" onclick="go(this, 'MasterCB.aspx'); opensub(this, 'subCash');"><span class="fa fa-plus-square addons" onclick="go(this, 'MasterCB.aspx'); opensub(this, 'subCash');"></span>
                        <ul class="nav-submenu" style="display: none;" id='subCash'>
                            <li>
                                <input value="Registrasi" onclick="go(this, 'CBRegistrasi1.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>

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
            console.log(parent);
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
