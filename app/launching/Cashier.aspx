<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cashier.aspx.cs" Inherits="ISC064.LAUNCHING.Cashier" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Batavianet Business Application :: Launching</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP">
</head>
<style>
    .nav, .navsub
    {
        border: 0px;
        background-color: #EEEEEE;
        font: 8pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 190;
        height: 18px;
    }
    .nav2
    {
        border: 0px;
        background-color: #EEEEEE;
        font: 14pt Trebuchet MS;
        padding-left: 7;
        text-align: left;
        width: 200;
        height: 30px;
    }
    .nav2:hover
    {
        border: 1px;
        background-color: #666;
    }
    a
    {
        text-decoration: none !important;
        color: #666 !important;
    }
    a:hover
    {
        color: Black !important;
    }
    .masuk
    {
        border: none;
    }
    .masuk:hover
    {
        border: 1px solid black;
    }
</style>
<body onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
    <div style="float: left; width: 40%;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_prev_c.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Back" class="nav2" style="width: 80px;" onmouseover="over(this)"
                        onclick="window.location='Index.html';" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right; width: 10%;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_gateway2.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Gateway" class="nav2" onclick="top.location.href='/Gateway.aspx'"
                        style="width: 100px" onmouseover="over(this)" onmouseout="out(this)">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20px">
                    <img src="/Media/icon_out.gif" style="width: 30px; height: 30px;">
                </td>
                <td>
                    <input type="button" value="Sign-Out" class="nav2" onclick="if(confirm('Apakah anda ingin melakukan sign-out?\nProgram dan absensi aktif anda akan ditutup.')){top.location.href='SignOut.aspx'}"
                        style="width: 100px" onmouseover="over(this)" onmouseout="out(this)">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div style="width: 90%; margin: 0 auto;" id="pilih" runat="server">
        <table style="width: 100%;" class="tbPilihan">
            <tr>
                <td style="width: 30%; text-align: center; font-size: 20pt;" class="masuk">
                    <a href="TTSRegistrasi.aspx">
                        <img src="Image/cashier.png" style="width: 80px; height: 80px;">
                        <br />
                        Cashier </a>
                </td>
            </tr>
        </table>
    </div>

    <script language="javascript">
        function call(nonup) {
            document.getElementById('nonup').value = nopriority;
            document.getElementById('next').click();
        }
    </script>

    </form>
</body>
</html>
