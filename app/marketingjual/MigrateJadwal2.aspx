<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.MigrateJadwal2" CodeFile="MigrateJadwal2.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Approval Jadwal Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Migrate - Approval Jadwal Tagihan (Hal. 2)">
		<style type="text/css">
		    h3 { font-size: 11pt; text-transform:uppercase; }
		    tr, td { vertical-align:top; }
	    </style>
	</head>
	<body onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<script language="javascript" src="/Js/NumberFormat.js"></script>
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head><input style="DISPLAY: none" type="text">
			<div>
				<table cellspacing="0" cellpadding="0">
					<tr>
						<td width="500">
							<div id="frm" runat="server">
								<h1>Approval Jadwal Tagihan</h1>
								<p>Halaman 2 dari 3</p>
								<br />
								<table cellspacing="5">
									<tr>
										<td colspan="3">
											<h3>DATA KONTRAK</h3>
										</td>
									</tr>
									<tr>
										<td>No. Kontrak</td>
										<td>:</td>
										<td>
										    <asp:Label ID="nokontrak" runat="server"></asp:Label>
										</td>
									</tr>
									<tr>
									    <td>Unit</td>
									    <td>:</td>
									    <td>
									        <asp:Label ID="nounit" runat="server"></asp:Label>
									    </td>
									</tr>
									<tr>
									    <td>Customer</td>
									    <td>:</td>
									    <td>
									        <asp:Label ID="cust" runat="server"></asp:Label>
									    </td>
									</tr>
									<tr>
									    <td>Agent</td>
									    <td>:</td>
									    <td>
									        <asp:Label ID="agent" runat="server"></asp:Label>
									    </td>
									</tr>
									<tr>
									    <td>Skema</td>
									    <td>:</td>
									    <td>
									        <asp:Label ID="skema" runat="server"></asp:Label>
									    </td>
									</tr>
									<tr>
									    <td>Nilai Kontrak</td>
									    <td>:</td>
									    <td>
									        <asp:Label ID="nilaikontrak" runat="server"></asp:Label>
									    </td>
									</tr>
								</table>
								<br />
								<table cellspacing="5" class="tb">
				                    <tr align="left">
					                    <th>
						                    No.</th>
					                    <th>
						                    Tipe</th>
					                    <th>
						                    Nama</th>
					                    <th>
						                    Tgl</th>
					                    <th>
						                    Nilai</th>
						                <th>
						                    Denda</th>
					                    <th>
						                    KPR</th>
						                <th></th>
				                    </tr>
				                    <asp:placeholder id="list" runat="server"></asp:placeholder>
			                    </table>
								<br />
								<table>
									<tr>
										<td><asp:Button ID="save" runat="server" CssClass="btn btn-blue" text="OK" width="75" onclick="save_Click"></asp:button></td>
										<td>
										    <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
												runat="server">
											<asp:Label ID="cek" runat="server" CssClass="err"></asp:Label>	
										</td>
									</tr>
								</table>
							</div>
						</td>
					</tr>
				</table>
			</div>
			<script language="javascript">
			    function hapus(nokontrak, nourut) {
			        if (confirm('Apakah anda ingin menghapus tagihan : ' + nokontrak + '.' + nourut + '?\nPerhatian bahwa data akan dihapus secara PERMANEN.')) {
			            location.href = 'MigrateJadwalDel.aspx?NoKontrak=' + nokontrak + '&NoUrut=' + nourut;
			        }
			    }
			</script>
		</form>
	</body>
</html>
