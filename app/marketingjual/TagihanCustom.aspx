<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.TagihanCustom" CodeFile="TagihanCustom.aspx.cs" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Customize Tagihan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Tagihan - Customize Tagihan">
	</HEAD>
	<body class="body-padding" onkeyup="cancelclick();">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="DISPLAY:none">
			<div style="DISPLAY:none">
				<asp:checkbox id="dariDaftar" runat="server"></asp:checkbox>
			</div>
			<div id="pilih" runat="server">
				<h1 class="title title-line">Customize Tagihan</h1>
				<p><b><i>Halaman 1 dari 3</i></b></p>
				<br>
				<table style="BORDER-RIGHT:#dcdcdc 1px solid; BORDER-TOP:#dcdcdc 1px solid; BORDER-LEFT:#dcdcdc 1px solid; BORDER-BOTTOM:#dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td><b>No. Kontrak :</b></td>
						<td>
							<asp:textbox id="nokontrak1" runat="server" width="100"></asp:textbox>
							<input type="button" value="&#xf002;" style="font-family: 'fontawesome'" class="btn btn-orange" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&tag=1' id="btnpop" runat="server" name="btnpop" />
						</td>
						<td>
							<asp:LinkButton id="next" runat="server" cssclass="btn btn-blue" onclick="next_Click">
                                Next <i class="fa fa-arrow-right"></i>
							</asp:LinkButton>
						</td>
					</tr>
				</table>
				<p class="feed">
					<asp:label id="feed" runat="server"></asp:label>
				</p>
                <p class="feed1">
					<asp:label id="feed1" runat="server"></asp:label>
				</p>
				<input type="button" id="backbtn" runat="server" onclick="history.back(-1)" value="Cancel"
					class="btn" style="MARGIN:5px" name="backbtn">
			</div>
			<div id="frm" runat="server">
				<h1 class="title title-line">Customize Tagihan</h1>
				<p><b><i>Halaman 2 dari 3</i></b></p>
				<br>
				<table cellpadding="0" cellspacing="0">
					<tr valign="top">
						<td width="600px">
							<table cellspacing="5">
								<tr>
									<td colspan="8">
										<p><b>Rumus Nilai</b></p>
									</td>
								</tr>
								<tr>
									<td>Nilai Kontrak</td>
									<td>:</td>
									<td colspan="5">
										<asp:textbox id="netto" runat="server" cssclass="txt" readonly="True"></asp:textbox>
									</td>
									<td>
										<asp:checkbox id="rounding" runat="server" text="Pembulatan nilai" checked="True"></asp:checkbox>
									</td>
								</tr>
								<tr>
									<td>Booking Fee</td>
									<td>:</td>
									<td><asp:textbox id="bfkali" runat="server" width="40" cssclass="txt_center">1</asp:textbox>
										x</td>
									<td>&nbsp;</td>
									<td><asp:radiobutton id="bfrupiah" runat="server" groupname="bftipe" text="Rp" checked="True"></asp:radiobutton></td>
									<td><asp:radiobutton id="bfpersen" runat="server" groupname="bftipe" text="%"></asp:radiobutton></td>
									<td>&nbsp;</td>
									<td>
										<asp:textbox id="bfjumlah" runat="server" cssclass="txt_num">10,000,000</asp:textbox>
										<asp:label id="bfc" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
								<tr>
									<td>DP</td>
									<td>:</td>
									<td><asp:textbox id="dpkali" runat="server" width="40" cssclass="txt_center">3</asp:textbox>
										x</td>
									<td>&nbsp;</td>
									<td><asp:radiobutton id="dprupiah" runat="server" groupname="dptipe" text="Rp"></asp:radiobutton></td>
									<td><asp:radiobutton id="dppersen" runat="server" groupname="dptipe" text="%" checked="True"></asp:radiobutton></td>
									<td>&nbsp;</td>
									<td>
										<asp:textbox id="dpjumlah" runat="server" cssclass="txt_num">30</asp:textbox>
										<asp:label id="dpc" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
								<tr>
									<td>Angsuran</td>
									<td>:</td>
									<td><asp:textbox id="angkali" runat="server" width="40" cssclass="txt_center">20</asp:textbox>
										x</td>
									<td>&nbsp;</td>
									<td><asp:radiobutton id="angrupiah" runat="server" groupname="angtipe" text="Rp"></asp:radiobutton></td>
									<td><asp:radiobutton id="angpersen" runat="server" groupname="angtipe" text="%" checked="True"></asp:radiobutton></td>
									<td>&nbsp;</td>
									<td>
										<asp:textbox id="angjumlah" runat="server" cssclass="txt_num">70</asp:textbox>
										<asp:label id="angc" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
							</table>
							<br />
							<table cellpadding="5">
							    <tr>
									<td colspan="3">
										<p><b>Skema Cara Bayar</b></p>
									</td>
								</tr>
							    <tr>
									<td>Skema</td>
									<td>:</td>
									<td>
<%--                                        <asp:DropDownList runat="server" ID="skema" Width="300" AutoPostBack="true" OnSelectedIndexChanged="skema_SelectedIndexChanged"></asp:DropDownList>--%>
										<asp:textbox id="skema" runat="server" cssclass="txt" Width="300"></asp:textbox>
									</td>
								</tr>
								<tr>
						<td valign="top">Cara Bayar</td>
						<td valign="top">:</td>
						<td>
							<table id="tablecarabayar" cellspacing="1" cellpadding="1" width="100%" border="0">
								<tr>
									<td>
										<asp:radiobuttonlist id="carabayar2" runat="server" repeatdirection="Horizontal">
                                            <asp:ListItem Selected="True" Value="CASH KERAS">CASH KERAS</asp:ListItem>
                                            <asp:ListItem Value="CASH BERTAHAP">CASH BERTAHAP</asp:ListItem>
                                            <asp:ListItem Value="KPR">KPR</asp:ListItem>
										</asp:radiobuttonlist></td>
									<td><asp:label id="carabayarc" runat="server" cssclass="err"></asp:label></td>
								</tr>
							</table>
						</td>
				    </tr>
							</table>
							<br>
							<table cellspacing="5" width="100%">
								<tr>
									<td colspan="9">
										<p><b>Rumus Jadwal</b></p>
									</td>
								</tr>
								<tr>
									<td>Tgl. Kontrak</td>
									<td>:</td>
									<td colspan="7">
										<asp:textbox id="tgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
										<label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
										<asp:label id="tglc" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
								<tr>
									<td colspan="2"></td>
									<td colspan="3">
										<u>Interval</u>
									</td>
									<td></td>
									<td colspan="3">
										<u>Pertama</u>
									</td>
									<td>
										<u>Tgl. Mulai</u>
									</td>
								</tr>
								<tr>
									<td>Booking Fee</td>
									<td>:</td>
									<td><asp:textbox id="bflama1" runat="server" width="40" cssclass="txt_center">7</asp:textbox></td>
									<td><asp:radiobutton id="bfbln1" runat="server" text="Bln" groupname="bf1"></asp:radiobutton></td>
									<td><asp:radiobutton id="bfhari1" runat="server" text="Hari" groupname="bf1" checked="True"></asp:radiobutton></td>
									<td>&nbsp;</td>
									<td>
									    <asp:textbox id="bflama2" runat="server" width="40" cssclass="txt_center">0</asp:textbox>
									</td>
									<td><asp:radiobutton id="bfbln2" runat="server" text="Bln" groupname="bf2"></asp:radiobutton></td>
									<td>
									    <asp:radiobutton id="bfhari2" runat="server" text="Hari" groupname="bf2" checked="True"></asp:radiobutton>
									    <asp:label id="bf2c" runat="server" cssclass="err"></asp:label>
									</td>
									<td>
									    <asp:textbox id="bftgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                                        <label for="bftgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>                                         
										<asp:label id="bftglc" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
								<tr>
									<td>DP</td>
									<td>:</td>
									<td><asp:textbox id="dplama1" runat="server" width="40" cssclass="txt_center">1</asp:textbox></td>
									<td><asp:radiobutton id="dpbln1" runat="server" text="Bln" groupname="dp1" checked="True"></asp:radiobutton></td>
									<td><asp:radiobutton id="dphari1" runat="server" text="Hari" groupname="dp1"></asp:radiobutton></td>
									<td>&nbsp;</td>
									<td><asp:textbox id="dplama2" runat="server" width="40" cssclass="txt_center">1</asp:textbox>
									</td>
									<td><asp:radiobutton id="dpbln2" runat="server" text="Bln" groupname="dp2" checked="True"></asp:radiobutton></td>
									<td><asp:radiobutton id="dphari2" runat="server" text="Hari" groupname="dp2"></asp:radiobutton>
										<asp:label id="dp2c" runat="server" cssclass="err"></asp:label>
									</td>
									<td>
									    <asp:textbox id="dptgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                                        <label for="dptgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
										<asp:label id="dptglc" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
								<tr>
									<td>Angsuran</td>
									<td>:</td>
									<td><asp:textbox id="anglama1" runat="server" width="40" cssclass="txt_center">1</asp:textbox></td>
									<td><asp:radiobutton id="angbln1" runat="server" text="Bln" groupname="ang1" checked="True"></asp:radiobutton></td>
									<td><asp:radiobutton id="anghari1" runat="server" text="Hari" groupname="ang1"></asp:radiobutton></td>
									<td>&nbsp;</td>
									<td><asp:textbox id="anglama2" runat="server" width="40" cssclass="txt_center">1</asp:textbox>
									</td>
									<td><asp:radiobutton id="angbln2" runat="server" text="Bln" groupname="ang2" checked="True"></asp:radiobutton></td>
									<td><asp:radiobutton id="anghari2" runat="server" text="Hari" groupname="ang2"></asp:radiobutton>
										<asp:label id="ang2c" runat="server" cssclass="err"></asp:label>
									</td>
									<td>
									    <asp:textbox id="angtgl" runat="server" cssclass="txt_center" width="85"></asp:textbox>
                                        <label for="angtgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
										<asp:label id="angtglc" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
							</table>
							<br>
							<table cellspacing="5">
								<tr>
									<td>
										Booking Fee dipotong di :
										<br>
										<asp:radiobutton id="dp1potong" runat="server" groupname="bfpotong" text="DP 1" checked="True"></asp:radiobutton>
										&nbsp;&nbsp;
										<asp:radiobutton id="ang1potong" runat="server" groupname="bfpotong" text="Angsuran 1"></asp:radiobutton>
										&nbsp;&nbsp;
										<asp:radiobutton id="dpspotong" runat="server" groupname="bfpotong" text="DP disebar"></asp:radiobutton>
										&nbsp;&nbsp;
										<asp:radiobutton id="angspotong" runat="server" groupname="bfpotong" text="Angsuran disebar"></asp:radiobutton>
										&nbsp;&nbsp;
										<asp:radiobutton id="tidakpotong" runat="server" groupname="bfpotong" text="BF tidak disebar"></asp:radiobutton>
									</td>
								</tr>
								<tr>
									<td>
										<asp:label id="cc" runat="server" cssclass="err"></asp:label>
									</td>
								</tr>
							</table>
						</td>
						<td style="PADDING-RIGHT:10px; PADDING-LEFT:10px; PADDING-BOTTOM:10px; PADDING-TOP:10px"><img src="/Media/line_vert.gif"></td>
						<td>
							<table cellspacing="5">
								<tr>
									<td>
										No. Kontrak :<br>
										<asp:label id="nokontrak2" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										Unit :
										<br>
										<asp:label id="unit" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										Customer :
										<br>
										<asp:label id="nama" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										Sales :
										<br>
										<asp:label id="agent" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<table height="50">
					<tr>
						<td>
                            <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
						</td>
						<td><input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='?'" type="button"
								value="Cancel" name="cancel" runat="server"></td>
					</tr>
				</table>
			</div>
			<div id="hasil" runat="server">
				<h1 class="title title-line">Customize Tagihan</h1>
				<p><b><i>Halaman 3 dari 3</i></b></p>
				<br>
				<table cellpadding="0" cellspacing="0">
					<tr valign="top">
						<td width="440">
							<asp:table id="rpt" runat="server" cssclass="tb blue-skin" cellspacing="0" enableviewstate="True">
								<asp:tablerow horizontalalign="Left">
									<asp:tableheadercell>No.</asp:tableheadercell>
									<asp:tableheadercell>Tipe</asp:tableheadercell>
									<asp:tableheadercell width="100">Keterangan</asp:tableheadercell>
									<asp:tableheadercell width="40">Tanggal</asp:tableheadercell>
									<asp:tableheadercell width="120" horizontalalign="Right">Jumlah</asp:tableheadercell>
									<asp:tableheadercell>BF</asp:tableheadercell>
								</asp:tablerow>                                
							</asp:table>
						</td>
						<td style="PADDING-RIGHT:10px; PADDING-LEFT:10px; PADDING-BOTTOM:10px; PADDING-TOP:10px"><img src="/Media/line_vert.gif"></td>
						<td valign="top">
							<table cellspacing="5">
								<tr>
									<td>
										No. Kontrak :<br>
										<asp:label id="nokontrakdetail" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										Unit :
										<br>
										<asp:label id="unitdetail" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										Customer :
										<br>
										<asp:label id="namadetail" runat="server" font-bold="True"></asp:label>
									</td>
								</tr>
							</table>
							<br>
							<table cellspacing="5" style="display:none;">
								<tr>
									<td>Nilai Kontrak</td>
									<td>:</td>
									<td align="right">
										<asp:label id="nettodetail" runat="server" font-bold="True"></asp:label></td>
								</tr>
								<tr>
									<td>Total Tagihan</td>
									<td>:</td>
									<td align="right" width="120">
										<asp:label id="totaldetail" runat="server" font-bold="True"></asp:label></td>
								</tr>
								<tr>
									<td colspan="3" align="right">
										<hr size="1" noshade>
									</td>
									<td style="FONT-WEIGHT:bold">-</td>
								</tr>
								<tr>
									<td>Selisih</td>
									<td>:</td>
									<td align="right"><asp:label id="outofbalance" runat="server" font-bold="True"></asp:label></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<asp:checkbox id="alokasi" runat="server" text="Auto Alokasi TTS" checked="True"></asp:checkbox>
				<table height="50">
					<tr>
						<td>
							<asp:LinkButton id="insert" runat="server" cssclass="btn btn-blue" width="75" onclick="insert_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
						</td>
						<td>
							<input class="btn" id="cancel2" style="WIDTH: 75px" onclick="javascript:history.back();"
								type="button" value="Cancel" name="cancel2" runat="server">
						</td>
					</tr>
				</table>
			</div>
			<script language="javascript">
			function cancelclick()
			{
				if(event.keyCode==27)
				{
					if(document.getElementById('cancel'))
						document.getElementById('cancel').click();
					else if(document.getElementById('cancel2'))
						document.getElementById('cancel2').click();
					else if(document.getElementById('backbtn'))
						document.getElementById('backbtn').click();
				}
			}
			function call(nokontrak)
			{
				document.getElementById('nokontrak1').value = nokontrak;
				document.getElementById('next').click();
			}
			</script>
		</form>
	</body>
</HTML>
