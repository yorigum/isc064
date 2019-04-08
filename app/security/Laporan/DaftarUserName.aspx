<%@ Reference Page="~/SecLevel.aspx" %>
<%@ Page Language="C#" CodeFile="DaftarUserName.aspx.cs" Inherits="ISC064.SECURITY.Laporan.DaftarUserName" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
	<head>
		<title>Laporan Data User</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Report.css" type="text/css" rel="stylesheet">
        <link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Laporan Data User">
	</head>
	<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
		<form id="Form1" method="post" runat="server">
			<div style="display:none"><uc1:head id="Head1" runat="server"></uc1:head></div>
			<table id="param" runat="server" width="100%" cellspacing="3">
				<tr>
					<td>
						<p class="comp" id="comp" runat="server"></p>
						<h1 id="judul" class="title title-line" runat="server">
							Laporan Data User
						</h1>
                        <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status :</p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
									            <asp:RadioButtonList id="rblstatus" runat="server" RepeatDirection="Horizontal" Font-Size="14px">
										            <asp:listitem selected="True" value="S" style="font-size:14px">Semua</asp:listitem>
										            <asp:listitem value="A" style="font-size:14px">Aktif</asp:listitem>
										            <asp:listitem value="B" style="font-size:14px">Blokir</asp:listitem>
									            </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        </div>
						<br><br />
						<div class="ins">
							<table>
								<tr>
									<td>
                                        <asp:LinkButton id="scr" accesskey="s" runat="server" cssclass="btn btn-blue" onclick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
										</asp:LinkButton>										
                                        <asp:button id="xls" runat="server" text="Download Excel" cssclass="btn btn-green" 
                                            accesskey="e" onclick="xls_Click" ></asp:button>
                                        <asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											Download PDF
                                        </asp:LinkButton>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
        <div id="headReport" runat="server">
        </div>
        <br />
			<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="1">
				<asp:tablerow VerticalAlign="Bottom">
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">No</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">User Name</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Nama</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Sec Level</asp:tableheadercell>
					<asp:tableheadercell horizontalalign="Left" BackColor="#1E90FF" ForeColor="White">Status</asp:tableheadercell>
				</asp:tablerow>
			</asp:table>
		</form>
	</body>
</html>
