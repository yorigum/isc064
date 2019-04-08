<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.Skom" CodeFile="Skom.aspx.cs" %>
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
			<h1>Setup Skema Komisi Internal</h1>
			<br>
			<table cellpadding="0" cellspacing="0">
				<tr valign="top">
					<td width="220" style="display:none;">
						<p style="font:bold 10pt">Skema Aktif</p>
						<ul id="aktif" runat="server" class="plike">
						</ul>
						<br>
						<p style="font:bold 10pt">Skema Inaktif</p>
						<ul id="inaktif" runat="server" class="plike">
						</ul>
					</td>
					<td style="padding:5 10 0 15;display:none;"><img src="/Media/line_vert.gif"></td>
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
							    <td class="style1">Periode Komisi</td>
							    <td width="1%">:</td>
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
							    	</table>
							    </td>
							</tr>
							<tr>
							    <td class="style1"></td>
							    <td></td>
								<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
								<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
							</tr>
							<tr>
							    <td colspan="7">
                                    <asp:Label ID="tglc" runat="server" Visible="false" cssclass="err"></asp:Label></td>
							</tr>
							<tr>
								<td>
									
								    Nilai Komisi</td>
								<td>:</td>
								<td>
                                    <asp:TextBox ID="NilaiKomisi" runat="server"></asp:TextBox>
                                &nbsp;%</td>
							</tr>						
							<tr>
								<td colspan="6">
                                    <asp:Table ID="tbRumus" runat="server" Width="100%" Border="1"  CellSpacing="0" CellPadding="4">
                                        <asp:TableRow>
                                            <asp:TableHeaderCell ColumnSpan="8" BackColor="LightGray"><span style="font-size:large;">Rumus Komisi</span></asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableHeaderCell BackColor="LightGray">Grade</asp:TableHeaderCell>
                                            <%--<asp:TableHeaderCell BackColor="LightGray">Target</asp:TableHeaderCell>--%>
                                            <asp:TableHeaderCell BackColor="LightGray">Nilai Target</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Tipe Target</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Nilai Komisi</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Tipe Nilai Komisi</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Komisi Closing Fee</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Tipe Komisi Closing Fee</asp:TableHeaderCell>
                                        </asp:TableRow>
                                    </asp:Table>
								    <br />
                                    <br />
								</td>
							</tr>
						</table>
                        <asp:Table ID="tbTerm" runat="server" Width="100%" Border="1"  CellSpacing="0" CellPadding="4">
                        </asp:Table>
                        <br />
						<br />
						<table height="50">
							<tr>
								<td>
									<asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" text="OK" width="75" onclick="ok_Click">
										<i class="fa fa-share"></i> OK
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
