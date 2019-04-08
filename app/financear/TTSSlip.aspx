<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TTSSlip" CodeFile="TTSSlip.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Slip Setoran</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Sementara - Slip Setoran">
</head>
<body onkeyup="if(event.keyCode==27)document.getElementById('cancel').click()">
    <script type="text/javascript" src="/Js/Common.js"></script>
    <form id="Form1" method="post" runat="server">
        <input type="text" style="display: none">
        <h1>Slip Setoran</h1>
        <br>
        <p style="font-size: 10pt; padding-left: 5px"><b>Kondisi Sekarang</b></p>
        <table cellspacing="5">
            <tr>
                <td>No. Slip</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="noslipl" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:Label ID="nosliplc" runat="server" CssClass="err"></asp:Label>
                    0 = Tidak digabung dalam slip setoran
                </td>
            </tr>
            <tr>
                <td>Tanggal Setoran</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="tglsetoranl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <label for="tglsetoranl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="tglsetoranlc" runat="server" CssClass="err"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Rekening</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="bankl" runat="server" CssClass="txt" Width="150" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <input type="button" class="btn" id="btnedit" runat="server" value=" Edit " style="width: 75px" onserverclick="btnedit_ServerClick">
                </td>
                <td>
                    <input type="button" class="btn btn-red" value="Cancel" style="width: 75px" id="cancel" runat="server"
                        name="cancel">
                </td>
            </tr>
        </table>
        <hr size="1" noshade>
        <p style="font-size: 10pt; padding-left: 5px"><b>Buat Slip Setoran Baru</b></p>
        <table cellspacing="5">
            <tr>
                <td>Tanggal Setoran</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="tglsetoran" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                    <label for="tglsetoran" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    <asp:Label ID="tglsetoranc" runat="server" CssClass="err"></asp:Label>
                </td>
                <td width="10"></td>
                <td>Rekening</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="bank" runat="server" CssClass="txt" Width="150" MaxLength="20"></asp:TextBox>
                </td>
                <td>
                    <input type="button" class="btn" id="btnbaru" runat="server" value=" OK " style="width: 50px" onserverclick="btnbaru_ServerClick">
                </td>
            </tr>
        </table>
        <hr size="1" noshade>
        <p style="font-size: 10pt; padding-left: 5px"><b>Gabung Slip Setoran</b></p>
        <p style="padding-left: 5px"><i>Daftar hanya menampilkan 20 slip setoran terbaru saja</i></p>
        <asp:RadioButtonList ID="sliplama" runat="server"></asp:RadioButtonList>
        <input type="button" class="btn" id="btnlama" runat="server" value=" OK " style="width: 50px" onserverclick="btnlama_ServerClick">
    </form>
</body>
</html>
