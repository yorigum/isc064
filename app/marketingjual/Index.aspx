﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Batavianet Business Application :: Security</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
	</head>
	<body style="overflow: hidden;">
		<div class="main-header">
	        <div id="background"></div>
	        <div id="labels">
	            <div class="logo-header" onclick="location.href='../gateway.aspx'">
	                <span class="v-helper"></span>
	                <img src="/Media/logo.png">
	            </div>
	            <div class="user-thumb" onclick="usermenu()">
	                <span class="v-helper"></span>
	                <img runat="server" style="border-radius:100%" id="gambar" src="">
	                <div class="user-menu" id="usermenu">
	                    <p onclick="OpenGantiPass();"><i class="fa fa-lock user-icon" aria-hidden="true"></i> &nbsp;Ganti Password</p>
                        <p onclick="OpenGantiFoto();"><i class="fa fa-lock user-icon" aria-hidden="true"></i> &nbsp;Ganti Foto</p>
	                    <p onclick="if(confirm('Apakah Anda ingin melakukan sign-out?\nProgram dan absensi aktif Anda akan ditutup.')){location.href='/SignOut.aspx'}"><i class="fa fa-power-off user-icon" aria-hidden="true"></i> &nbsp;Log Out</p>
	                </div>
	            </div>
	            <div class="user-info" onclick="usermenu()">
                    <p><% Response.Write(ISC064.Act.UserID); %> - <% Response.Write(ISC064.Act.SecLevel); %></p>
                    <p>IP Address : <% Response.Write(ISC064.Act.IP); %></p>
	            </div>
	        </div>
	    </div>
	    <div class="content-wrapper" style="height: 550px;">
	    	<div class="sidebar-frame">
				<iframe name="nav" src="Nav.aspx" style="border:none; width: 100%; min-height:535px;"></iframe>    		
	    	</div>
			<div class="frame-wrapper">
				<iframe name="content" src="Home.html" style="border:none;width: 100%; min-height:535px;overflow:hidden"></iframe>
	    	</div>
	    </div>
    	<div class="main-footer clear">
			<div class="copyright">
				2018 &copy; Batavianet. All Rights Reserved.
			</div>
			<div class="contact">
				<b style="letter-spacing: 1px">CONTACTS</b> Phone: +62 21 29020111, 54356161 (hunting) | Email : support@batavianet.com
			</div>
		</div>
	<script type="text/javascript">
        function OpenGantiPass() {
            if (navigator.userAgent.indexOf("MSIE") != -1) openModal('../GantiPass.aspx', '500', '310');
            window.open('../GantiPass.aspx', 'Ganti Password', "width=500px, height=310px, top=100px;, left=200px");
        }
        function usermenu() {
            var submenu = document.getElementById('usermenu');
            if (submenu) {
                submenu.style.display = submenu.style.display == "block" ? "" : "block";
            }
        }
        function openModal(id) {
            var modal = document.getElementById(id);
            modal.className += " modalopen";
            modal.style.display = 'block';
        }
        function dismissModal() {
            var a = document.getElementsByClassName('modalopen');
            while (a.length > 0) {
                a[0].style.display = 'none';
                a[0].className = 'modal';
            }
        }
        function OpenGantiFoto() {
            if (navigator.userAgent.indexOf("MSIE") != -1) openModal('../GantiFoto.aspx', '500', '310');
            window.open('../GantiFoto.aspx', 'Ganti Gambar', "width=500px, height=310px, top=170px;, left=430px");
        }
        document.addEventListener('click', function (e) {
            e = e || window.event;
            var target = e.target || e.srcElement;
            if (target.classList.contains('modal')) {
                dismissModal();
            };
        }, false);
	</script>
	</body>
</html>
