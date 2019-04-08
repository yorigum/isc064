<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KomisiGen2" CodeFile="KomisiGen2.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Generate Komisi 2</title>
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
			<h1>Generate Komisi</h1>
			<p>Halaman 1 dari 2</p>
			<br>
			<table style="BORDER-RIGHT: #dcdcdc 1px solid; BORDER-TOP: #dcdcdc 1px solid; BORDER-LEFT: #dcdcdc 1px solid; BORDER-BOTTOM: #dcdcdc 1px solid"
				cellspacing="5">
				<tr>
					<td>No. Sales</td>
					<td colspan="4">:
						<asp:textbox id="noagent" runat="server" cssclass="txt" width="100"></asp:textbox><input id="btnpop" class="btn" onclick="popDaftarAgent('a')" value="..." type="button" name="btnpop" runat="server">
					</td>
				</tr>
				<tr>
					<td colspan="5"><b><u>Periode Kontrak</u></b></td>
				</tr>
				<tr>
					<td>dari</td>
					<td>:
						<asp:textbox id="dari" runat="server" width="85" cssclass="txt_center"></asp:textbox>
						<label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
					</td>
					<td rowspan="2">&nbsp;&nbsp;</td>
					<td>sampai</td>
					<td>
						<asp:textbox id="sampai" runat="server" width="85" cssclass="txt_center"></asp:textbox>
						<label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
					</td>
				</tr>
				<tr style="display:none;">
					<td>Rekening Bank</td>
					<td colspan=4>:
						<asp:dropdownlist id="ddlAcc" runat="server" cssclass="ddl" width="300">
							<asp:listitem selected="True">- Pilih Rekening Bank -</asp:listitem>
						</asp:dropdownlist>
						<asp:label id="ddlAccErr" runat="server" cssclass="err" />
					</td>
				</tr>
				<tr style="display:none;">
					<td>Proyek</td>
					<td colspan=4>:
					 <asp:dropdownlist id="dept" runat="server" width="300" />
					</td>
				</tr>
				<tr>
					<td>
					    <asp:button id="next" onclick="next_Click" runat="server" cssclass="btn" width="75" text="Next"></asp:button>
					    <asp:Button ID="clear" OnClick="clear_Click" runat="server" CssClass="btn" Text="CLEAR FALSE KOMISI" />
						<p class="feed">
										<asp:Label ID="feed" runat="server"></asp:Label></p>
					</td>
				</tr>
			</table>
			<script language="javascript">
			function call(noagent)
			{
				document.getElementById('noagent').value = noagent;
			}
			</script>
		</form>
	</body>
</HTML>
