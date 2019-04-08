<%@ Page Language="c#" Inherits="ISC064.LAUNCHING.NUPDaftar2" CodeFile="NUPDaftar2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>NUP - Registrasi Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="NUP - Registrasi Customer (Hal. 2)">
</head>
<style>
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
</style>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
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
                    <img src="/Media/icon_gateway.gif" style="width: 30px; height: 30px;">
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
    <div style="width:100%; margin: 0 auto;">
    <h1>
        Registrasi NUP</h1>
    <p style="font-size: 8pt; color: #666;">
        Halaman 2 dari 2</p>
    <br />
    <br />
    <h2 style="color: Brown; border: 1 solid silver; padding: 10">
        Pendaftaran NUP Berhasil
    </h2>
    <br />
    <table cellspacing="5">
        <tr>
            <td>
                NUP
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="nonup" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Customer
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Agent
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <h1 style="display:none">
        <a id="asp" runat="server">Print Form NUP</a></h1>
    <br />
    <p style="padding: 5px">
        <b>Daftar NUP Baru :</b>
    </p>
    <ul>
        <li><a style="font-weight: bold; font-size: 10pt; line-height: normal; font-style: normal;
            font-variant: normal" id="adaftar" runat="server">Daftar</a>
            <br />
            Registrasi NUP baru.
            <br />
            <br />
        </li>
        <%--<li>
					<a style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal"
						id="aTagihan" runat="server">Customize Tagihan</a>
					<br>
					Membuat jadwal tagihan diluar skema cara bayar yang berlaku. Prosedur tidak 
					tersedia untuk surat pesanan yang sudah memiliki jadwal tagihan.
				</li>--%>
    </ul>
    </div>
    </form>
</body>
</html>
