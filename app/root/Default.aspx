<%@ Reference Page="~/GantiPass.aspx" %>

<%@ Page Language="c#" Inherits="ISC064._Default" CodeFile="Default.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Batavianet Business Application</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">


    <script type="text/javascript" src="/Js/Pop.js"></script>

    <script type="text/javascript" src="/Js/MD5.js"></script>

    <script type="text/javascript" src="/Js/NumberFormat.js"></script>

    <meta name="ctrl" content="0">
    <meta name="sec" content="Halaman Login">
    <!--[if IE]>
	    <style type="text/css" rel="stylesheet">
	        img { z-index:-1; position:absolute; }
            div#container {  margin-top:14em }  
	    </style>
    <![endif]-->
</head>
<body style="background-color: #f0f3f8;">
    <div class="container">
        <div class="login-logo">
            <img src="Media/logo.png">
        </div>
        <div class="login-box" style="background-color: rgba(255,255,255,0.9);">
            <div class="login-header">
                <b id="comp" runat="server" class="login-title"></b>
            </div>
            <div class="login-body">
                <form id="Form1" method="post" runat="server">
                    <asp:TextBox ID="username" runat="server" CssClass="fa input-login" placeholder="&#xf007; Username"></asp:TextBox>
                    <asp:TextBox ID="pass" runat="server" CssClass="fa input-login" placeholder="&#xf023; Password" TextMode="Password"></asp:TextBox>

                    <div class="input-group" align="center">
                        <asp:Button ID="btn" runat="server" CssClass="btn-login" Text="Login" OnClick="btn_Click"></asp:Button>
                    </div>
                </form>
            </div>
            <div class="login-footer">
                <p style="margin-bottom: 5px; font-size: 13pt; font-weight: 300;">Property Developer System</p>
                <p style="font-size: 8pt; margin-top: -2px;"><i>Build on eProperty Engine version 17.8.1</i></p>
            </div>
        </div>
        <div class="underbox">
            <p class="">Copyright &copy; 2017 eProperty. Production 2007-2018 | <a href="http://www.softwareproperti.com">www.softwareproperti.com</a></p>
        </div>
    </div>
</body>
</html>
