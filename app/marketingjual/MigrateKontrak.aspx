<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.MigrateKontrak" CodeFile="MigrateKontrak.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Approval Kontrak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Migrate - Approval Kontrak (Hal. 1)">
	</head>
	<body>
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1>Approval Kontrak</h1>
			<%--<p>Halaman 1 dari 4</p>--%>
			<br>
			<table cellspacing="0" cellpadding="0">
				<tr valign="top">
					<td width="370">
						<table style="border:1px solid #DCDCDC" cellspacing="5">
							<tr>
								<td>No. Kontrak</td>
								<td>:</td>
								<td>
									<asp:textbox id="nokontrak" runat="server" cssclass="txt" width="145"></asp:textbox>
									<asp:button id="display" runat="server" cssclass="btn" text="Display" onclick="display_Click"></asp:button>
								</td>
							</tr>
						</table>
						<p class="feed">
					        <asp:label id="feed" runat="server"></asp:label>
				        </p>
						<br>
						<p style="padding:5;">
							<u>Data yang ditampilkan adalah
								data yang belum di-approve.</u>
						</p>
					</td>
					<%--<td width="30"></td>
					<td>
						<p><b>Terbaru :</b></p>
						<asp:listbox id="baru" rows="10" runat="server" width="200" cssclass="ddl"></asp:listbox>
					</td>--%>
				</tr>
			</table>
			<br>
			<div id="hasil" runat="server">
				<%--<p style="font:8pt;padding-left:3">
					Harga price list adalah dalam rupiah per meter persegi per bulan.
					<br>
					Luas adalah dalam meter persegi.
				</p>--%>
				<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="3">
					<asp:tablerow>
						<asp:tableheadercell>&nbsp;</asp:tableheadercell>
						<asp:tableheadercell width="100" horizontalalign="Left">No. Kontrak</asp:tableheadercell>
						<asp:tableheadercell width="120" horizontalalign="Left">Tgl. Kontrak</asp:tableheadercell>
						<asp:tableheadercell width="50" horizontalalign="Left">Status</asp:tableheadercell>
						<asp:tableheadercell width="100" horizontalalign="Left">Unit</asp:tableheadercell>
						<asp:tableheadercell width="150" horizontalalign="Left">Customer</asp:tableheadercell>
						<asp:tableheadercell width="150" horizontalalign="Left">Agent</asp:tableheadercell>
						<asp:tableheadercell>&nbsp;</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
			</div>
			<script language="javascript">
			    function hapus(nokontrak, nourut) {
			        if (confirm('Apakah anda ingin menghapus kontrak : ' + nokontrak + '?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
			            location.href = 'MigrateKontrakDel.aspx?NoKontrak=' + nokontrak;
			        }
			    }
			</script>
		</form>
	</body>
</html>
