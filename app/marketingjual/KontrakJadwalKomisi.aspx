<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakJadwalKomisi" CodeFile="KontrakJadwalKomisi.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadKontrak" Src="HeadKontrak.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavKontrak" Src="NavKontrak.ascx" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Jadwal Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Jadwal Komisi">
		<script type="text/javascript" src="/Js/Pop.js"></script>
	</HEAD>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
            
            <div class="content-header">
                <uc1:navkontrak id="NavKontrak1" runat="server" aktif="5"></uc1:navkontrak>
            </div>
			<div class="tabdata">
                <div id="NoData" runat="server" visible="false">
                    <br />    
                    <p><b>&nbsp;&nbsp;&nbsp;
                        No Kontrak Belum Melakukan Komisi.</b></p>
                </div>

				<div class="pad" id="datas" runat="server" visible="true">
					<uc1:headkontrak id="HeadKontrak1" runat="server"></uc1:headkontrak>
                    

					<div style="WIDTH: 100%">
                        
						<div style="FLOAT: left">
							<table cellspacing="5">
								<tr>
									<td>
										<input type="button" id="edit" runat="server" value="Edit Komisi" class="btn btn-blue"  style="WIDTH:100px"
											name="edit" accesskey="e">
									</td>
									<td>
										<input type="button" id="data" runat="server" value="Data Penerima Overriding" class="btn"  style="WIDTH:150px;display:none;"
											name="edit" accesskey="e">
									</td>
									<td style="PADDING-LEFT:10px">
										<p class="feed">
											<asp:label id="feed" runat="server"></asp:label>
										</p>
									</td>
								</tr>
							</table>
						</div>
						<div style="FLOAT: right">
							<table cellspacing="5" style="display:none;">
								<tr>
									<td>
										<input type="button" id="solve" runat="server" value="Solve Komisi" class="btn" style="WIDTH:100px"
											name="solve">
									</td>
								</tr>
							</table>
						</div>
					</div>
					<table cellspacing="5">

						<tr>
							<td>
								Nilai Kontrak :
								<br>
								<asp:label id="nilai" runat="server" font-bold="True"></asp:label>
							</td>
                            <td width="250">
								Skema :
								<br>
								<asp:label id="skema" runat="server" font-bold="True"></asp:label>
							</td>
						</tr>
						<tr>
							<td>
								Nilai DPP :
								<br>
								<asp:label id="nilaidpp" runat="server" font-bold="True"></asp:label>
							</td>
						</tr>
						<tr>
							<td>
								Pelunasan sudah mencapai :
								<br>
								<asp:label id="persenlunas" runat="server" font-bold="True" font-size="18"></asp:label>
							</td>
						</tr>
					</table>
					<br/>
					<asp:table id="rpt" runat="server" cssclass="blue-skin tb" cellspacing="1">
                        <asp:tablerow horizontalalign="Left">
                        </asp:tablerow>
					</asp:table>
                    <br />
                    <asp:table id="rpt2" runat="server" class="tb blue-skin" cellspacing="1">
                        <asp:tablerow horizontalalign="Left">
                        </asp:tablerow>
                    </asp:table>
				</div>
			</div>
			<script type="text/javascript">
			function popKomisiBayar(NoKontrak, NoUrut, Baris) {
				openModal('KomisiBayar.aspx?NoKontrak=' + NoKontrak + '&NoUrut=' + NoUrut + '&Baris=' + Baris , '920', '650');
			}
			</script>
		</form>
	</body>
</HTML>
