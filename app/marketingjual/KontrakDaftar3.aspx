<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakDaftar3" CodeFile="KontrakDaftar3.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Pendaftaran Surat Pesanan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Pendaftaran Surat Pesanan (Hal. 3)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Pendaftaran Surat Pesanan</h1>
        <p><b><i>Halaman 4 dari 4</i></b></p>
        <br>
        <br>
        <h2 style="border-right: silver 1px solid; padding-right: 10px; border-top: silver 1px solid; padding-left: 10px; padding-bottom: 10px; border-left: silver 1px solid; color: brown; padding-top: 10px; border-bottom: silver 1px solid">Surat Pesanan Selesai
        </h2>
        <br>
        <table cellspacing="5">
            <tr>
                <td>No. Kontrak</td>
                <td>:</td>
                <td>
                    <asp:Label ID="nokontrakl" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Unit</td>
                <td>:</td>
                <td>
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Customer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Sales</td>
                <td>:</td>
                <td>
                    <asp:Label ID="agent" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <br>
        <h1><a id="asp" runat="server">Print KPU + SP + SKEMA</a></h1>
        <p style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
            <b>Prosedur 
					Lanjutan :</b>
        </p>
        <ul>
            <li>
                <a style="font-weight: bold; font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal"
                    id="aDiskon" runat="server">Diskon</a>
                <br>
                Memberikan diskon tambahan diluar diskon skema cara bayar.
					<br>
                <br>
                <li>
                    <a style="font-weight: bold; font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal"
                        id="aRt" runat="server">Reset Tagihan</a>
                    <br>
                    Ganti jadwal tagihan secara keseluruhan.
					<br>
                    <br>
                    <li>
                        <a style="font-weight: bold; font-size: 10pt; line-height: normal; font-style: normal; font-variant: normal"
                            id="aTagihan" runat="server">Customize Tagihan</a>
                        <br>
                        Membuat jadwal tagihan diluar skema cara bayar yang berlaku. Prosedur tidak 
					tersedia untuk surat pesanan yang sudah memiliki jadwal tagihan.
					<br>
                        <br>
                        <%--<li>
					<a style="FONT-WEIGHT:bold; FONT-SIZE:10pt; LINE-HEIGHT:normal; FONT-STYLE:normal; FONT-VARIANT:normal"
						id="aKom" runat="server">Generate Komisi</a>
					<br>
					Membuat jadwal komisi.
				</li>--%>
        </ul>
        <br>
        <table height="50">
            <tr>
                <td>
                    <input id="cancel" onclick="location.href = 'KontrakDaftar.aspx'" type="button" class="btn btn-blue"
                        value="OK" style="width: 75px">
                </td>
                <td>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
