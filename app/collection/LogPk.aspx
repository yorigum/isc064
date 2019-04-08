<%@ Page language="c#" Inherits="ISC064.COLLECTION.LogPk" CodeFile="LogPk.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Log File</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Log File Detil per Objek Data">
	</head>
	<body class="body-padding" onkeyup="if(event.keyCode==27)history.back(-1)">
		<form id="Form1" method="post" runat="server" class="cnt">
			<div style="display:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<h1 class="title title-line">Log File</h1>
            <br />
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:RadioButtonList ID="tb" runat="server" Visible="False" AutoPostBack="true" OnSelectedIndexChanged="tb_SelectedIndexChanged" CssClass="radio">
                    <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_FOLLOWUP_LOG" Selected="True">FOLLOW UP</asp:ListItem>
                    <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_REALISASIDENDA_LOG">REALISASI DENDA</asp:ListItem>
                    <asp:ListItem Value="ISC064_MARKETINGJUAL..MS_PUTIHDENDA_LOG">PEMUTIHAN DENDA</asp:ListItem>
                    <asp:ListItem Value="MS_SKL_LOG">SURAT KETERANGAN LUNAS</asp:ListItem>
				<asp:listitem value="MS_PJT_LOG">PEMBERITAHUAN JATUH TEMPO</asp:listitem>
				<asp:listitem value="MS_TUNGGAKAN_LOG">SURAT TUNGGAKAN</asp:listitem>
			</asp:radiobuttonlist>
			<br>
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow horizontalalign="Left">
					<asp:tableheadercell width="65">Tgl</asp:tableheadercell>
					<asp:tableheadercell width="45">Jam</asp:tableheadercell>
					<asp:tableheadercell width="200" columnspan="2">User</asp:tableheadercell>
					<asp:tableheadercell width="50">Aktivitas</asp:tableheadercell>
					<asp:tableheadercell width="120">Referensi</asp:tableheadercell>
					<asp:tableheadercell width="120">Approval</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
			<table height="50">
				<tr>
					<td>
                        <a onclick="history.back(-1)" class="btn btn-blue t-white" style="width:75px">
							<i class="fa fa-share"></i> OK
						</a>
					</td>
				</tr>
			</table>
            </ContentTemplate>
        </asp:UpdatePanel>
		</form>
	</body>
</html>
