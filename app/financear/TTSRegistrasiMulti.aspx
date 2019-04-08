<%@ Reference Page="~/Customer.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.TTSRegistrasiMulti" CodeFile="TTSRegistrasiMulti.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Registrasi Multiple Receipt</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Tanda Terima Sementara - Registrasi TTS Multiple">
</head>
<body onkeyup="keyup();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1>Registrasi Multiple Receipt</h1>
        <p>Halaman 2 dari 2</p>
        <br />
        <table cellspacing="5">
            <tr>
                <td>Tipe</td>
                <td>:</td>
                <td width="100">
                    <asp:Label ID="lblTipe" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td>Unit</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblUnit" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ref.</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblReferensi" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td>Customer</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lblCustomer" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <hr size="1" noshade color="silver" />
        <br />
        <table class="tb" cellspacing="5">
            <tr>
                <th align="left">No. Tagihan</th>
                <th align="left">Tagihan</th>
                <th align="right">Sisa Tagihan</th>
                <th align="left">Cara Bayar</th>
                <th align="left">Tgl TTS</th>
                <th align="left">Keterangan</th>
                <th align="left">Rekening Bank</th>
                <th align="left">No. BG</th>
                <th align="left">Tgl BG</th>
                <th align="right" colspan="2">Nilai Pembayaran</th>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:Button ID="save" runat="server" Width="75" CssClass="btn" Text="OK" OnClick="save_Click"></asp:Button></td>
                <td>
                    <input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='TTSRegistrasi.aspx'"
                        type="button" value="Cancel" name="cancel" runat="server">
                </td>
            </tr>
        </table>
        <script type="text/javascript">
			function keyup()
			{
				if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?'))
				{
					if(document.getElementById('cancel'))
						document.getElementById('cancel').click();
				}
			}
        </script>
    </form>
</body>
</html>
