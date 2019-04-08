<%@ Page language="c#" Inherits="ISC064.KOMISI.TerminKomisiRegis" CodeFile="TerminKomisiRegis.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Setup Termin Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Termin Komisi">
	    
	    <style type="text/css">
            .style1
            {
                width: 212px;
            }
        </style>
	    
	</head>
	<body class="default-content">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<h1 class="title title-line">Setup Termin Komisi</h1>
			<br>
			<table cellpadding="0" cellspacing="0">
				<tr valign="top">
					<td>
						<h2 style="padding-left:5;padding-bottom:5">Pendaftaran Termin Baru</h2>
						<table cellspacing="5">
							<tr>
								<td colspan="4">
									<p><b>Rumus Global</b></p>
								</td>
							</tr>
							<tr>
								<td width="20%">Nama</td>
								<td width="1%">:</td>
								<td colspan="2">
									<asp:textbox id="nama" runat="server" width="250" maxlength="100" cssclass="txt"></asp:textbox>
									<asp:label id="namac" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
                            <tr>
                                <td>Project</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="project" runat="server" Width="200" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">                 
                                    </asp:DropDownList>
                            </tr>
                            <tr>
                                <td>Tipe Marketing</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="tipe" runat="server" Width="300" AutoPostBack="true" OnSelectedIndexChanged="gantitipe">
                                        <asp:ListItem Value="0">Tipe Marketing :</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:label id="tipec" runat="server" cssclass="err"></asp:label>
                                </td>
                            </tr>
							<tr>
                                <td>Cara Bayar</td>
                                <td>:</td>
                                <td>
                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="carabayar" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="ALL" Selected="True" Class="igroup-radio">Semua</asp:ListItem>
                                        <asp:ListItem Value="CASH KERAS" Class="igroup-radio">Cash Keras</asp:ListItem>
                                        <asp:ListItem Value="CASH BERTAHAP" Class="igroup-radio">Cash Bertahap</asp:ListItem>
                                        <asp:ListItem Value="KPR" Class="igroup-radio">KPA/KPR</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>					
							<tr>
								<td colspan="6">
                                    <asp:Table ID="tbRumus1" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                                        <asp:TableRow>
                                            <asp:TableHeaderCell ColumnSpan="15"><span style="font-size:large;">Kondisi Termin</span></asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableHeaderCell RowSpan="2">Level</asp:TableHeaderCell>
                                            <asp:TableHeaderCell RowSpan="2">Nama</asp:TableHeaderCell>
                                            <asp:TableHeaderCell RowSpan="2">% Cair</asp:TableHeaderCell>
                                            <asp:TableHeaderCell ColumnSpan="7">Syarat Cair</asp:TableHeaderCell>
                                            <asp:TableHeaderCell RowSpan="2">Tipe Syarat Cair</asp:TableHeaderCell>
                                            <asp:TableHeaderCell RowSpan="2"></asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableHeaderCell>% Lunas</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>% BF</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>% DP</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>% ANG</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>PPJB</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>AJB</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>AKAD</asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:tablerow horizontalalign="Left">
                                            <asp:TableCell ColumnSpan="16">
                                                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                                            </asp:TableCell>
                                        </asp:tablerow>
                                        <asp:tablerow horizontalalign="Left">
                                            <asp:TableCell width="75" ColumnSpan="16">
                                                <asp:Button ID="add" runat="server" Text="Tambah Baris" Width="150" class="btn btn-blue" OnClick="add_Click"
                                                AccessKey="s" />
                                                &nbsp;
                                                <asp:Button ID="del" runat="server" Text="Hapus Baris" Width="150" class="btn btn-blue" OnClick="del_Click"
                                                AccessKey="s" />
                                            </asp:TableCell>                    
                                        </asp:tablerow>
                                    </asp:Table>
								    <br />
                                    <br />
								</td>
							</tr>
						</table>
                        <br />
						<br />
						<table height="50">
							<tr>
								<td>
                                    <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                                    </asp:LinkButton>
								</td>
								<td style="padding-left:10">
									<p class="feed">
										<asp:label id="feed" runat="server"></asp:label>
									</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
