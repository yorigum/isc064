<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Nav" CodeFile="Nav.aspx.cs" %>

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
        <div class="sidebar-header" style="background: #d8689a">
            <p id="comp" runat="server" style="border-bottom: solid white 2px; margin-bottom: 5px;"></p>
            <div class="sidebar-logo">
                <img src="/Media/icon_collection.png">
                <p>COLLECTION</p>
            </div>
        </div>
        <div class="sidebar-content">
            <form id="Form1" method="post" runat="server">
                <p style="display: none; font: bold 8pt; color: gray; padding-left: 5px; padding-bottom: 3px">Collection</p>
                <ul class="sidebar-menu">
                    <li><span class="fa fa-bell-o sb-ico"></span>
                        <input value="Reminder" onclick="go(this,'Reminder.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-bar-chart sb-ico"></span>
                    <li>
                        <input value="Dashboard" onclick="go(this,'DashboardCollection.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-pie-chart sb-ico"></span>
                    <li>
                        <input value="Laporan" onclick="go(this,'Lap.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-cog sb-ico"></span>                    
					<li>
                        <input value="Setup Follow Up" class="navmenu" type="button" onclick="opensub(this, 'subSetupFU')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetupFU')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetupFU'>
                            <li><input value="Grouping" onclick="go(this, 'GroupingFollowUp.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-cog sb-ico"></span>
                    <li>
                        <input value="Customer Information File" onclick="go(this,'Customer.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-phone sb-ico"></span>
                    <li>
                        <input value="Follow Up" class="navmenu" type="button" onclick="go(this,'MasterFollowUp.aspx');opensub(this,'subSetup1');"><span class="fa fa-plus-square addons" onclick="go(this,'MasterFollowUp.aspx');opensub(this,'subSetup1');"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetup1'>
                            <li>
                                <input value="Registrasi Follow Up" onclick="go(this,'FollowUp.aspx')" class="navmenu" type="button"></li>                            
                        </ul>
                    </li>
                    <span class="fa fa-money sb-ico"></span>
                    <li>
                        <input value="Realisasi Denda" onclick="go(this,'RealisasiDenda.aspx');" class="navmenu" type="button"></li>
                    <span class="fa fa-money sb-ico"></span>
                        <input value="Pemutihan Denda" onclick="go(this,'PemutihanDenda1.aspx');" class="navmenu" type="button"></li>                    
                    <li>
                        <span class="fa fa-envelope sb-ico"></span>
                        <span class="fa fa-envelope sb-ico"></span>
                        <input value="Jatuh Tempo" class="navmenu" type="button" onclick="go(this,'PJT.aspx');opensub(this,'subSetup');"><span class="fa fa-plus-square addons" onclick="go(this,'PJT.aspx');opensub(this,'subSetup');"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetup'>
                            <li>
                                <input value="Registrasi P. Jatuh Tempo" onclick="go(this,'PJTRegistrasi.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Print Jatuh Tempo" onclick="openPrint('PrintPJTBatch.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Export Data" onclick="go(this,'PJTDownload.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-exclamation-triangle sb-ico"></span>
                    <li>
                        <input value="Surat Peringatan" class="navmenu" type="button" onclick="go(this,'Tunggakan.aspx');opensub(this,'subTunggakan');"><span class="fa fa-plus-square addons" onclick="go(this,'Tunggakan.aspx');opensub(this,'subTunggakan');"></span>
                        <ul class="nav-submenu" style="display: none;" id="subTunggakan">
                            <li>
                                <input value="Registrasi Surat Peringatan" onclick="go(this,'STRegistrasi.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Print Surat Peringatan" onclick="openPrint('PrintSTBatch.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-check-square sb-ico"></span>
                    <li>
                        <input value="Keterangan Lunas" class="navmenu" type="button" onclick="go(this,'KeteranganLunas.aspx');opensub(this,'subKelun');"><span class="fa fa-plus-square addons" onclick="go(this,'KeteranganLunas.aspx');opensub(this,'subKelun');"></span>
                        <ul class="nav-submenu" style="display: none;" id='subKelun'>
                            <li>
                                <input value="Registrasi SKL" onclick="go(this,'LunasRegistrasi.aspx')" class="navmenu" type="button"></li>
                        </ul>
                        <span class="fa fa-file-o sb-ico"></span>
                        <li>
                            <input value="Log File" onclick="go(this,'Log.aspx')" class="navmenu" type="button"></li>
                </ul>
            </form>
        </div>
    </div>
    <script type="text/javascript">
	    function opensub(sender,id){
	        // console.log(id);
	        var sub = document.getElementById(id);
	        var parent = sender.parentElement.getElementsByTagName('span')[0];
	        console.log(parent);
	        if(sub.style.display === 'none'){
	            sub.style.display = "block";
	            parent.className = "";
	            parent.className = "fa fa-minus-square addons"
	        }else{
	            sub.style.display = "none";
	            parent.className = "";
	            parent.className = "fa fa-plus-square addons";
	        }
	    }
	    function go(foo,href) {
			bold(foo);
			top.content.location.href=href;
		}
		function bold(foo) {
			for(i=1;i<document.Form1.elements.length-2;i++) {
				document.Form1.elements[i].style.fontWeight='normal';
			}
			foo.style.fontWeight='bold';
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
