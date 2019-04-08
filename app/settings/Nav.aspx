<%@ Page language="c#" Inherits="ISC064.SECURITY.Nav" CodeFile="Nav.aspx.cs" %>
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
	<body onunload="browserClose()" style="width:97%">
		<script type="text/javascript" src="/Js/Common.js"></script>
	<div class="sidebar">
        <div class="sidebar-header" style="background: #6576f1">
            <p id="comp" runat="server" style="border-bottom:solid white 2px; margin-bottom:5px;"></p>
            <div class="sidebar-logo">
                <img src="/Media/icon_settings.png">
                <p>SETTINGS</p>                
            </div>
        </div>
        <div class="sidebar-content">
			<form id="Form1" method="post" runat="server">
				<p style="display:none;font:bold 8pt;color:gray;padding-left:5px;padding-bottom:3px">Security</p>
				<ul class="sidebar-menu">
					<span class="fa fa-pencil sb-ico"></span>
					<li><input value="Html Editor" onclick="go(this, 'HtmlEditor.aspx')" class="navmenu" type="button"></li>

					<span class="fa fa-file-text-o sb-ico"></span>
					<li><input value="Numerator" onclick="go(this, 'Numerator.aspx')" class="navmenu" type="button"></li>
                    
<%--					<span class="fa fa-list sb-ico"></span>
					<li><input value="Skema Cara Bayar" onclick="go(this, 'Skema.aspx')" class="navmenu" type="button"></li>
					<span class="fa fa-cog sb-ico"></span>
					<li>
                        <input value="Setup Sales" class="navmenu" type="button" onclick="opensub(this, 'subSales')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSales')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSales'>
                            <li><input value="Tipe Sales" onclick="go(this, 'TipeSalesDaftar.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Level Sales" onclick="go(this, 'LevelSalesDaftar.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>--%>
					<span class="fa fa-cog sb-ico"></span>
					<li>
                        <input value="Setup Legal" class="navmenu" type="button" onclick="opensub(this, 'subLegal')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subLegal')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subLegal'>
                            <li><input value="PPJB" onclick="go(this, 'FormatPPJB.aspx')" class="navmenu" type="button"></li>
                            <li><input value="AJB" onclick="go(this, 'FormatAJB.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Target BAST" onclick="go(this, 'TargetBAST.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Kelengkapan Berkas PPJB" onclick="go(this, 'BerkasPPJB.aspx')" class="navmenu" type="button"></li>                            
                        </ul>
                    </li>
					<span class="fa fa-cog sb-ico"></span>
					<li>
                        <input value="Setup Unit" class="navmenu" type="button" onclick="opensub(this, 'subSetup')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetup')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetup'>
                            <li><input value="Tipe Unit" onclick="go(this, 'JenisDaftar.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Lokasi" onclick="go(this, 'LokasiDaftar.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Tipe Properti" onclick="go(this, 'JenisPropertiDaftar.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Format Unit" onclick="go(this, 'FormatUnit.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Format Warna Unit" onclick="go(this, 'FormatWarnaUnit.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
<%--                    <span class="fa fa-cog sb-ico"></span>
					<li>
                        <input value="Setup Follow Up" class="navmenu" type="button" onclick="opensub(this, 'subSetupFU')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetupFU')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetupFU'>
                            <li><input value="Grouping" onclick="go(this, 'FollowUp.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>--%>
                    <span class="fa fa-user-o sb-ico"></span>
                    <li>
                    	<input value="Setup Master Data" onclick="opensub(this, 'subMasterData');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subMasterData')"></span>
                    	<ul class="nav-submenu" style="display: none;" id="subMasterData">
                            <li><input value="Data Perusahaan" onclick="go(this, 'MasterData.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Lokasi Penjualan" onclick="go(this, 'LokasiKontrak.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
<%--                    <span class="fa fa-credit-card sb-ico"></span>
					<li>
                        <input value="Setup Rekening" class="navmenu" type="button" onclick="opensub(this, 'subSetupRek')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetupRek')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetupRek'>
                            <li><input value="Registrasi Rekening" onclick="go(this, 'Acc.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li> --%>
                    <span class="fa fa-cog sb-ico"></span>
                    <li>
                        <input value="Setup SMS Blast" class="navmenu" type="button" onclick="opensub(this, 'subSetupBlast')"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subSetupBlast')"></span>
                        <ul class="nav-submenu" style="display: none;" id='subSetupBlast'>
                            <li>
                                <input value="Setup Akun Satu Titik" onclick="go(this, 'SatuTitik.aspx')" class="navmenu" type="button"></li>
                            <li>
                                <input value="Setup Format SMS Blast" onclick="go(this, 'SmsFormat1.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>

                    <span class="fa fa-envelope-o sb-ico"></span>
                    <li>
                        <input value="Email" onclick="opensub(this, 'subEmail')" class="navmenu" type="button" ><span class="fa fa-plus-square addons" onclick="opensub(this, 'subEmail')"></span>
                        <ul class="nav-submenu" style="display:none" id="subEmail">
                            <li><input value="Setup" onclick="go(this, 'SetupEmail.aspx')" class="navmenu" type="button" /></li>
                            <li><input value="Alamat Email" onclick="go(this, 'AlamatEmail.aspx')" class="navmenu" type="button" /></li>
                        </ul>
                    </li>
                    <span class="fa fa-calendar-times-o sb-ico"></span>
                    <li><input value="Batas Reservasi" onclick="go(this, 'BatasReserve.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-credit-card sb-ico"></span>
                    <li><input value="Perumusan Denda" onclick="go(this, 'RumusDenda.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-usd sb-ico"></span>
                    <li><input value="Price List" onclick="go(this, 'PLIncludePPN.aspx')" class="navmenu" type="button"></li>
                    <span class="fa fa-exclamation-triangle sb-ico"></span>
                    <li>
                    	<input value="Mandatory" onclick="opensub(this, 'subMandatory');" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subMandatory')"></span>
                    	<ul class="nav-submenu" style="display: none;" id="subMandatory">
                            <li><input value="Pendaftaran Customer" onclick="go(this, 'MandatoryCustomer.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Pendaftaran Sales" onclick="go(this, 'MandatorySales.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Pendaftaran Unit" onclick="go(this, 'MandatoryUnit.aspx')" class="navmenu" type="button"></li>
                            <li><input value="Pendaftaran Rekening" onclick="go(this, 'MandatoryRekening.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-check sb-ico"></span>
                    <li>
                        <input value="Approval" onclick="opensub(this, 'subApproval')" class="navmenu" type="button"><span class="fa fa-plus-square addons" onclick="opensub(this, 'subApproval')"></span>
                        <ul class="nav-submenu" style="display: none" id="subApproval">
                            <li>
                                <input value="Setup" onclick="go(this, 'SetupApproval.aspx')" class="navmenu" type="button" /></li>
                            <li>
                                <input value="Approval" onclick="go(this, 'Approval.aspx')" class="navmenu" type="button"></li>
                        </ul>
                    </li>
                    <span class="fa fa-file-o sb-ico"></span>
                    <li><input value="Log File" onclick="go(this, 'Log.aspx')" class="navmenu" type="button"></li>
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
