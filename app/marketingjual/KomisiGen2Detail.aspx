<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KomisiGen2Detail" CodeFile="KomisiGen2Detail.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>KomisiGen2Detail</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" type="text/css" href="/Media/Style.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Komisi - Generate Komisi 2">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head><input style="DISPLAY: none">
			<h1>Generate Komisi Detail</h1>
			<p>Halaman 2 dari 2</p>
			<br>
			<table cellspacing="5">
				<tr>
					<td>Sales :
						<br>
						<asp:label id="agent" runat="server" font-bold="True"></asp:label></td>
				</tr>
			</table>
			<br>
			<h2>KONTRAK YANG BELUM DIGENERATE KOMISI UNTUK AGEN</h2>
			<table id="rpt" runat="server" class="tb" cellspacing="3">
	            <tr>
	                <th>No. Kontrak</th>
					<th>Tgl. Kontrak</th>
					<th width="100">Unit</th>
					<th width="200">Customer</th>
					<th align="Right" width="120">Nilai DPP</th>
					<th align="Right" width="70">% Lunas</th>
					<th>Komisi</th>
					<th>Tipe Reff</th>
					<th>Nama Reff</th>
					<th>Komisi Reff (%)</th>
	            </tr>
	        </table>
			<br>
			<br>
			<tr>
				<td>
				    <asp:Button ID="save" runat="server" CssClass="btn btn-blue" text="GENERATE" width="75" onclick="Save_Click"></asp:button>
                </asp:Button>
				</td>
			</tr>
			</TBODY></TABLE>
			<script language="javascript">
			function call(nomor)
			{
				popEditKontrak(nomor);
			}
			</script>
		</form>
		</TR></TBODY></TABLE></FORM></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
