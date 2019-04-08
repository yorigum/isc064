<%@ Page language="c#" Inherits="ISC064.LAUNCHING.NUPDaftarBayar" CodeFile="NUPDaftarBayar.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>NUP - Pembayaran Registrasi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="NUP - Pembayaran Registrasi (Hal. 1)">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1>Pembayaran Registrasi</h1>
            <p style="font-size:8pt; color:#666;">Halaman 1 dari 3</p>
            <br /><br />
            <table style="border:1px solid #DCDCDC" cellspacing="5">
				<tr>
					<td>
						<asp:textbox id="keyword" runat="server" cssclass="txt" width="150"></asp:textbox>
					</td>
					<td>
						<asp:button id="display" runat="server" cssclass="btn" text="Display" 
                            onclick="display_Click"></asp:button>
					</td>
				</tr>
			</table>
			<br>
			Daftar NUP yang belum melakukan pembayaran pertama.
			<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
				<asp:tablerow horizontalalign="Left" verticalalign="Bottom">
					<asp:tableheadercell width="75">NUP</asp:tableheadercell>
					<asp:tableheadercell width="150">Customer</asp:tableheadercell>
					<asp:tableheadercell width="150">Agent</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<script language="javascript">
			function call(nomor)
			{
				popNUP(nomor);
			}
			</script>
		</form>
	</body>
</html>
