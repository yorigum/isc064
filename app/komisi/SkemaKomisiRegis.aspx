<%@ Page language="c#" Inherits="ISC064.KOMISI.SkemaKomisiRegis" CodeFile="SkemaKomisiRegis.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Setup Skema Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Setup Skema Komisi">
	    
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
			<h1 class="title title-line">Setup Skema Komisi</h1>
			<br>
        <asp:ScriptManager runat="server" ID="script" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
            <ContentTemplate>
			<table cellpadding="0" cellspacing="0">
				<tr valign="top">
					<td>
						<h2 style="padding-left:5;padding-bottom:5">Pendaftaran Skema Baru</h2>
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
                                    <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">                 
                                        <asp:ListItem>Project : </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
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
                                <td width="20%" valign="top">Periode Komisi</td>
								<td width="1%" valign="top">:</td>
							    <td colspan="2">
							        <table>
										<tr>
										    <td>dari</td>
											<td>
                                                <asp:textbox id="dari" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
											<td rowspan="2">&nbsp;&nbsp;</td>
											<td>sampai</td>
											<td>
                                                <asp:textbox id="sampai" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
										</tr>
										<tr>
										    <td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
											<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
										</tr>
										<tr>
										    <td colspan="5">
                                                <asp:Label ID="tglc" runat="server" Visible="false" cssclass="err"></asp:Label></td>
										</tr>
									</table>
							    </td>
							</tr>
							<tr>
                                <td>Rumus Komisi</td>
                                <td>:</td>
                                <td>
                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rumus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="param_SelectedIndexChanged">
                                        <asp:ListItem Value="UNIT" Selected="True" class="igroup-radio">Satuan per Unit</asp:ListItem>
                                        <asp:ListItem Value="KUMULATIF" class="igroup-radio">Target Kumulatif</asp:ListItem>
                                        <asp:ListItem Value="PROGRESIF" class="igroup-radio">Progresif</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>Dasar Perhitungan</td>
                                <td>:</td>
                                <td>
                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="dasarhitung" runat="server">
                                        <asp:ListItem Value="DPP" Selected="True" class="igroup-radio">DPP</asp:ListItem>
                                        <asp:ListItem Value="KONTRAK" class="igroup-radio">Nilai Kontrak</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>Termin</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="termin" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="termin_SelectedIndexChanged">                 
                                        <asp:ListItem>Termin : </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                        <table cellspacing="5" id="TerminVisible" runat="server" visible="false">
                            <tr>
                                <td>
                                    <asp:Table ID="rptTerm" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                                        <asp:TableRow>
                                            <asp:TableHeaderCell ColumnSpan="11">Kondisi Termin</asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableHeaderCell RowSpan="2">Level</asp:TableHeaderCell>
                                            <asp:TableHeaderCell RowSpan="2">Nama</asp:TableHeaderCell>
                                            <asp:TableHeaderCell RowSpan="2">% Cair</asp:TableHeaderCell>
                                            <asp:TableHeaderCell ColumnSpan="7">Syarat Cair</asp:TableHeaderCell>
                                            <asp:TableHeaderCell RowSpan="2">Tipe Syarat Cair</asp:TableHeaderCell>
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
                                    </asp:Table>
                                </td>
                            </tr>
                        </table>
                        
                        <table cellspacing="5">						
							<tr>
								<td colspan="6">
                                    <asp:Table ID="tbRumus1" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                                        <asp:TableRow>
                                            <asp:TableHeaderCell ColumnSpan="4"><span style="font-size:large;">Rumus Komisi</span></asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableHeaderCell>Level</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Tipe Tarif</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Tarif Komisi</asp:TableHeaderCell>
                                            <asp:TableHeaderCell></asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:tablerow horizontalalign="Left">
                                            <asp:TableCell ColumnSpan="4">
                                                <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
                                            </asp:TableCell>
                                        </asp:tablerow>
                                        <asp:tablerow horizontalalign="Left">
                                            <asp:TableCell width="75" ColumnSpan="4">
                                                <asp:Button ID="add" runat="server" Text="Tambah Baris" Width="150" class="btn btn-blue" OnClick="add_Click"
                                                AccessKey="s" />
                                                &nbsp;
                                                <asp:Button ID="del" runat="server" Text="Hapus Baris" Width="150" class="btn btn-blue" OnClick="del_Click"
                                                AccessKey="s" />
                                            </asp:TableCell>                    
                                        </asp:tablerow>
                                    </asp:Table>
                                    <asp:Table ID="tbRumus2" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                                        <asp:TableRow>
                                            <asp:TableHeaderCell ColumnSpan="7"><span style="font-size:large;">Rumus Komisi</span></asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableHeaderCell>Level</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Tipe Target</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Target Bawah</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Target Atas</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Tipe Tarif</asp:TableHeaderCell>
                                            <asp:TableHeaderCell>Tarif Komisi</asp:TableHeaderCell>
                                            <asp:TableHeaderCell></asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:tablerow horizontalalign="Left">
                                            <asp:TableCell ColumnSpan="7">
                                                <asp:PlaceHolder ID="list2" runat="server"></asp:PlaceHolder>
                                            </asp:TableCell>
                                        </asp:tablerow>
                                        <asp:tablerow horizontalalign="Left">
                                            <asp:TableCell width="75" ColumnSpan="7">
                                                <asp:Button ID="add2" runat="server" Text="Tambah Baris" Width="150" class="btn btn-blue" OnClick="add2_Click"
                                                AccessKey="s" />
                                                &nbsp;
                                                <asp:Button ID="del2" runat="server" Text="Hapus Baris" Width="150" class="btn btn-blue" OnClick="del2_Click"
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ok" />
            </Triggers>
        </asp:UpdatePanel>
		</form>
	</body>
</html>
