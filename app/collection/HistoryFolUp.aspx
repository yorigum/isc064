<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryFolUp.aspx.cs" Inherits="ISC064.COLLECTION.HistoryFolUp" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<!DOCTYPE html>

<html>
<head>
    <title>Master Follow Up</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="P. Jatuh Tempo - Master Pemberitahuan Follow Up(Marketing)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin membatalkan proses registrasi?')) document.getElementById('cancel').click();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <h1 class="title title-line">Master Follow Up </h1>                
        <table cellspacing="5">
            <tr>
                <td><b>No Kontrak</b></td>
                <td><b>:</b></td>
                <td style="width: 400px;">
                    <asp:Label ID="nokontrak" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Nilai Kontrak</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="nilaikontrak" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Customer</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Tagihan</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="tagihan" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Unit</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Adm</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="adm" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Marketing</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="marketing" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Tagihan + Adm</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="tagadm" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>No HP</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="hp1" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Pembayaran</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="pembayaran" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>No HP 2</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="hp2" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td><b>Pelunasan (10 %)</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="lunas" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr style="display:none">
                <td><b>Tipe</b></td>
                <td><b>:</b></td>
                <td style="width: 200px;">
                    <asp:Label ID="tipe" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><b>Alamat</b></td>
                <td><b>:</b></td>
                <td>
                    <asp:Label ID="alamat" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
        <hr size="1" noshade color="silver">
        <br>
        <table class="tb blue-skin" cellspacing="1" style="width:100%">
				<tr align="left" valign="bottom">
					<th style="text-align:center; vertical-align:middle" rowspan="2">
						No.</th>
					<th style="text-align:center; vertical-align:middle" rowspan="2">
						Tagihan</th>
					<th style="text-align:center; vertical-align:middle" rowspan="2">
						Tipe</th>
					<th style="text-align:center; vertical-align:middle" rowspan="2">
						Jatuh Tempo</th>
                    <th style="text-align:center; vertical-align:middle" rowspan="2">
						Tagihan
					</th>
                    <th style="text-align:center; vertical-align:middle" rowspan="2">
						Pelunasan
					</th>
					<th style="text-align:center; vertical-align:middle" rowspan="2">
						Sisa Tagihan
					</th rowspan="2">
                    <th style="text-align:center" colspan="6">History</th>
				</tr>
                <tr>
                    <th>Tgl Follow Up</th>
                    <th>Kategori</th>
                    <th>Keterangan</th>
                    <th>Tgl Janji Bayar</th>
                    <th>Collector</th>
                    <th>Action</th>
                </tr>
                <asp:placeholder id="list" runat="server"></asp:placeholder>
		</table>
    </form>
</body>
    <script type="text/javascript">
        function Hapus(nokontrak,nofu){
            location.href = 'FollowUpDel.aspx?NoKontrak='+nokontrak+'&NoFU='+nofu+'';
        }
    </script>

</html>
