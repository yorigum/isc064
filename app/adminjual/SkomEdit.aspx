<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.SkomEdit" CodeFile="SkomEdit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadSkom" Src="HeadSkom.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavSkom" Src="NavSkom.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Edit Skema Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="5">
		<meta name="sec" content="Setup Skema Komisi - Edit Skema Komisi">
	</head>
	<body class="default-content" onkeyup="if(event.keyCode==27) window.close()" >
		<form id="Form1" method="post" runat="server">
			<uc1:navskom id="NavSkom1" runat="server" aktif="1"></uc1:navskom>
			<div class="tabdata">
				<div class="pad">
					<uc1:headskom id="HeadSkom1" runat="server"></uc1:headskom>
					<table cellspacing="5">
						<tr>
							<td width="100%"></td>
							<td>
								<input type="button" class="btn" value="  Log  " id="btnlog" runat="server" name="btnlog"
									accesskey="l">
							</td>
							<td>
								<input type="button" class="btn" value="Delete" id="btndel" runat="server" name="btndel"
									accesskey="d">
							</td>
						</tr>
					</table>
					<p class="feed" style="padding-left:5">
						<asp:label id="feed" runat="server"></asp:label>
					</p>
					<table cellspacing="5">
						<tr>
							<td>
								<asp:radiobutton id="aktif" runat="server" text="Aktif" font-size="12" font-bold="True" forecolor="green"
									groupname="status"></asp:radiobutton>
								<asp:radiobutton id="inaktif" runat="server" text="Inaktif" font-size="12" font-bold="True" forecolor="red"
									groupname="status"></asp:radiobutton>
							</td>
							<td width="20"></td>
							<td>Nama</td>
							<td>:</td>
							<td><asp:textbox id="nama" runat="server" width="250" maxlength="100" cssclass="txt"></asp:textbox>
								<asp:label id="namac" runat="server" cssclass="err" width="50"></asp:label>
							</td>
						</tr>
					</table>
					<table>
					    <tr>
					        <td colspan="5"><b>Periode Komisi</b></td>
					    </tr>
										<tr>
											<td>dari</td>
											<td><asp:textbox id="dari" runat="server" cssclass="txt_center" width="85"></asp:textbox> <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
											<td rowspan="2">&nbsp;&nbsp;</td>
											<td>sampai</td>
											<td><asp:textbox id="sampai" runat="server" cssclass="txt_center" width="85"></asp:textbox> <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
											</td>
										</tr>
										<tr>
											<td colspan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
											<td colspan="3"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
										</tr>
										<tr>
										    <td colspan="7">
                                                <asp:Label ID="tglc" runat="server" Visible="false" cssclass="err"></asp:Label></td>
										</tr>
										<tr>
										    <td>
										        <b>Nilai Komisi :</b>
										    </td>
										    <td>
                                                <asp:TextBox ID="NilaiKomisi" runat="server" style="text-align:right"></asp:TextBox>
                                                %</td>
										</tr>
									</table>
				
                                    <asp:Table ID="tbRumus" runat="server" Width="90%" Border="1"  CellSpacing="0" CellPadding="4" align="center">
                                        <asp:TableRow>
                                            <asp:TableHeaderCell ColumnSpan="8" BackColor="LightGray"><span style="font-size:large;">Rumus Komisi</span></asp:TableHeaderCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableHeaderCell BackColor="LightGray">Grade</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Target</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Nilai Target</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Tipe Target</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Nilai Komisi</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Tipe Nilai Komisi</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Komisi Closing Fee</asp:TableHeaderCell>
                                            <asp:TableHeaderCell BackColor="LightGray">Tipe Komisi Closing Fee</asp:TableHeaderCell>
                                        </asp:TableRow>
                                    </asp:Table>
                    <br />
					<asp:Table ID="tbTerm" runat="server" Border="1"  CellSpacing="0" CellPadding="4" align="center">
                    </asp:Table>
					<br />
					<table height="50">
						<tr>
							<td>
								<asp:button id="ok" runat="server" cssclass="btn" text="OK" width="75" onclick="ok_Click"></asp:button>
							</td>
							<td>
								<input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel">
							</td>
							<td>
								<asp:Button ID="save" runat="server" CssClass="btn btn-blue" text="Apply" width="75" accesskey="a" onclick="save_Click"></asp:button>
							</td>
						</tr>
					</table>
				</div>
			</div>
			<script language="javascript">
			function hapusbaris(nomor, baris)
			{
				if(confirm('Hapus satu baris detail ini dari skema?\nPerhatian bahwa data akan dihapus secara PERMANEN.')){
				    location.href = 'SkomEdit.aspx?Act=del&Tipe=Skema&Nomor=' + nomor + '&Baris=' + baris;
				}
            }
            function hapusbarisTerm(nomor, baris) {
                if (confirm('Hapus satu baris termin ini dari skema?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
                    //SkomEdit2.aspx?Act=del&Tipe=Term&Nomor="+ Nomor +"&Baris="+ d["Baris"] +"'
                    location.href = 'SkomEdit.aspx?Act=del&Tipe=Term&Nomor=' + nomor + '&Baris=' + baris;
                }
            }
			</script>
		</form>
	</body>
</html>
