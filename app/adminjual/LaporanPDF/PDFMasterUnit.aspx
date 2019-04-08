<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.MasterUnit" CodeFile="PDFMasterUnit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Master Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Unit">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
   <form id="Form1" method="post" runat="server">
        <div id="headReport" runat="server">
            <table id="param" runat="server" width="100%" cellspacing="3">
                <tr>
                    <td colspan="2">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title title-line">Laporan Master Unit
                        </h1>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
             <asp:TableRow VerticalAlign="Bottom">
				    <asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">No.</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">No. Stock</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Tipe</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Lokasi</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Tipe Properti</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Unit</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Luas SG</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Luas Netto</asp:tableheadercell>
                    <asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Arah Hadap</asp:tableheadercell>
                    <asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Panorama</asp:tableheadercell>
                    <asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Status</asp:tableheadercell>
                    <asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">No.Kontrak</asp:tableheadercell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
