<%@ Reference Page="~/SecLevel.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.Laporan.DaftarUser" CodeFile="PDFLapDaftarUser.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
    <title>Laporan Daftar Username</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Daftar Username">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==13)document.getElementById('scr').click();if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div id="headReport" runat="server">
            <table id="param" runat="server" width="100%" cellspacing="3">
                <tr>
                    <td colspan="2">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title title-line">Laporan Daftar Username
                        </h1>
                    </td>
                </tr>
            </table>
        </div>
        <br />
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow VerticalAlign="Bottom">
					<asp:tableheadercell horizontalalign="Left">No</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">User Name</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Nama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Sec Level</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left">Status</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
    </form>
</body>
</html>
