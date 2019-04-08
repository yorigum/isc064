<%@ Page language="c#" Inherits="ISC064.LEGAL.KontrakDiskon" CodeFile="KontrakDiskon.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Prosedur Diskon</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Prosedur Diskon">
	</HEAD>
	<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		<script type="text/javascript">
		    function kaliLuas(gimmick, lainlain, pricelist) {
		        var vargimmick = parseFloat(gimmick.value.replace(/,/g, ""));
		        var varlainlain = parseFloat(lainlain.value.replace(/,/g, ""));
		        var varpricelist = parseFloat(pricelist.value.replace(/,/g, ""));

		        var result = vargimmick + varlainlain + varpricelist;

		        hitungHarga(result);
		    }

		    function hitungHarga(x) {
		        if (x == 0) { x = 0; }
		        //alert(x);
		        document.getElementById("totalharga").value = x;
		        CalcBlur(document.getElementById("totalharga"));
		    }
		</script>
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<div style="DISPLAY: none"><asp:checkbox id="dariDaftar" runat="server"></asp:checkbox></div>
			<input style="DISPLAY: none">
			<div id="pilih" runat="server">
				<h1 class="title title-line">Diskon</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br>
				<table style="BORDER-RIGHT: #dcdcdc 1px solid; BORDER-TOP: #dcdcdc 1px solid; BORDER-LEFT: #dcdcdc 1px solid; BORDER-BOTTOM: #dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td>No. Kontrak :</td>
						<td><asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt"></asp:textbox><input class="btn" id="btnpop" onclick="popDaftarKontrak('a')" type="button" value="..."
								name="btnpop" runat="server">
						</td>
						<td><asp:button id="next" runat="server" cssclass="btn" text="Next..." onclick="next_Click"></asp:button></td>
					</tr>
				</table>
				<p class="feed"><asp:label id="feed" runat="server"></asp:label></p>
				<input class="btn" id="backbtn" style="MARGIN: 5px" onclick="history.back(-1)" type="button"
					value="Cancel" name="backbtn" runat="server">
			</div>
			<div id="frm" runat="server">
				<h1 class="title title-line">Diskon</h1>
				<p><b><i>Halaman 2 dari 2</i></b></p>
				<br>
				<table cellspacing="0" cellpadding="0">
					<tr valign="top">
						<td width="400">
							<table cellspacing="5">
								<tr>
									<td colspan="4">
										<p><b>Perhitungan Harga</b></p>
										<asp:label id="skema" runat="server" font-underline="True"></asp:label></td>
								</tr>
								<tr>
									<td>Price List (Gross)</td>
									<td>:</td>
									<td align="right"><asp:textbox id="gross" runat="server" width="125" cssclass="txt_num" readonly="True"></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah</td>
								</tr>
								<tr>
									<td>Biaya Gimmick</td>
									<td>:</td>
									<td align="right"><asp:textbox id="gimmick" runat="server" width="125" cssclass="txt_num"></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="gimmickc" runat="server" cssclass="err"></asp:label></td>
								</tr>
								<tr>
									<td>Biaya Lain - Lain</td>
									<td>:</td>
									<td align="right"><asp:textbox id="lainlain" runat="server" width="125" cssclass="txt_num"></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="lainlainc" runat="server" cssclass="err"></asp:label></td>
								</tr>
									<tr>
									<td align="right" colspan="3">
										<hr noshade size="1">
									</td>
								</tr>
								<tr>
									<td>Total Harga</td>
									<td>:</td>
									<td align="right"><asp:textbox id="totalharga" runat="server" width="125" cssclass="txt_num" ReadOnly=true></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
								    </td>
								</tr>
								<tr>
									<td>Bunga</td>
									<td>:</td>
									<td align="right"><asp:textbox id="bunga" runat="server" width="125" cssclass="txt_num"></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="errbunga" runat="server" cssclass="err"></asp:label></td>
								</tr>
								<tr>
									<td>Diskon</td>
									<td>:</td>
									<td align="right"><asp:textbox id="disc" runat="server" width="125" cssclass="txt_num"></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="discc" runat="server" cssclass="err"></asp:label></td>
								</tr>
								<tr>
									<td>Diskon Tambahan</td>
									<td>:</td>
									<td align="right"><asp:textbox id="diskontambahan" runat="server" width="125" cssclass="txt_num"></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="diskontambahanc" runat="server" cssclass="err"></asp:label></td>
								</tr>
								<tr style="DISPLAY: none">
									<td align="right" colspan="3">
										<hr noshade size="1">
									</td>
								</tr>
								<tr style="DISPLAY: none">
									<td align="right" colspan="3">
										<hr noshade size="1">
									</td>
								</tr>
								<tr style="DISPLAY: none">
									<td>DPP</td>
									<td>:</td>
									<td align="right"><asp:textbox id="dpp" tabindex="0" runat="server" width="125" cssclass="txt_num" readonly="true"></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
									</td>
								</tr>
								<tr>
									<td>Nilai Kontrak</td>
									<td>:</td>
									<td align="right"><asp:textbox id="nilai" tabindex="0" runat="server" width="125" cssclass="txt_num" readonly="true"></asp:textbox></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
									</td>
								</tr>
								<tr>
									<td>PPN</td>
									<td>:</td>
									<td align="right"><asp:Label ID=ppnlabel runat=server></asp:Label></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="ppnc" runat="server" cssclass="err"></asp:label></td>
								</tr>
								<tr style="display:none;">
									<td align="right" colspan="3"><asp:radiobutton id="rdInclude" runat="Server" groupname="StatusPPN" text="Include"></asp:radiobutton><asp:radiobutton id="rdExclude" runat="Server" groupname="StatusPPN" text="Exclude"></asp:radiobutton></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal"><asp:label id="InExc" runat="server" cssclass="err"></asp:label></td>
								</tr>
								<tr>
									<td align="right" colspan="3">
										<hr noshade size="1">
									</td>
								</tr>
								<tr>
									<td>Harga Minimum</td>
									<td>:</td>
									<td align="right"><asp:label id="pricemin" runat="server"></asp:label></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
									</td>
								</tr>
								<tr>
									<td colspan="3"><asp:label id="nilaic" runat="server" cssclass="err"></asp:label></td>
								</tr>
								<tr>
									<td colspan="4"><asp:label id="discinfo" runat="server" forecolor="Red"></asp:label></td>
								</tr>
							</table>
							<h2 id="warningkomisi" style="PADDING-RIGHT:5px; DISPLAY:none; PADDING-LEFT:5px; PADDING-BOTTOM:5px; COLOR:red; PADDING-TOP:5px"
								runat="server">PERHITUNGAN KOMISI SUDAH DIKELUARKAN</h2>
							<table height="50">
								<tr>
									<td><asp:button id="save" runat="server" width="75" cssclass="btn btn-blue" text="OK" onclick="save_Click"></asp:button></td>
									<td><input class="btn btn-red" id="cancel" style="width: 75px" onclick="location.href='?'" type="button"
											value="Cancel" name="cancel" runat="server">
									</td>
								</tr>
							</table>
						</td>
						<td style="PADDING-RIGHT: 10px; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; PADDING-TOP: 10px"><img src="/Media/line_vert.gif"></td>
						<td>
							<table cellspacing="5">
								<tr>
									<td>No. Kontrak :
										<br>
										<asp:label id="nokontrakl" runat="server" font-bold="True"></asp:label></td>
								</tr>
								<tr>
									<td>Unit :
										<br>
										<asp:label id="unit" runat="server" font-bold="True"></asp:label></td>
								</tr>
								<tr>
									<td>Customer :
										<br>
										<asp:label id="customer" runat="server" font-bold="True"></asp:label></td>
								</tr>
								<tr>
									<td>Sales :
										<br>
										<asp:label id="agent" runat="server" font-bold="True"></asp:label></td>
								</tr>
							</table>
							<br>
							<table cellspacing="5">
								<tr>
									<td>Nilai Kontrak</td>
									<td>:</td>
									<td align="right"><asp:label id="netto" runat="server" font-bold="True"></asp:label></td>
								</tr>
								<tr>
									<td>Total Tagihan</td>
									<td>:</td>
									<td align="right" width="120"><asp:label id="totaltagihan" runat="server" font-bold="True"></asp:label></td>
								</tr>
								<tr>
									<td align="right" colspan="3">
										<hr noshade size="1">
									</td>
									<td style="FONT-WEIGHT: bold">-</td>
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
			</div>
			<script type="text/javascript">
			function popdiskon(d1,d2){
				foo1 = document.getElementById(d1);
				foo2 = document.getElementById(d2);
				openModal('SkemaDiskon.aspx?t1='+foo1.value+'&t2='+foo2.value+'&d1='+d1+'&d2='+d2,'450','360');
			}
			function recaldisc(baseTxt,discTxt,nettTxt){
				base = cvtnum(baseTxt.value);
				nett = base;
				disc = discTxt.value.split("+");
				
				discTxt.value = "";
				
				for(i=0;i<disc.length;i++)
				{
					if(!isNaN(disc[i]) && disc[i]!="")
					{
						nett = nett - (nett * (disc[i]/100));
						if(discTxt.value!="") discTxt.value = discTxt.value + "+";
						discTxt.value = discTxt.value + disc[i];
					}
				}
				
				n = Math.round(100*nett)/100;
				eval("nettTxt.value = FinalFormat('"+n+"')");
			}
			function cvtnum(foo){
				return foo.replace(/,/gi ,"");
			}
			function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
			}
			function hitungPPN(gross, diskon, bunga)
			{
				var g = gross.value;
				var d = diskon.value;
				var b = bunga.value;
				
				var pajak = 0;
				
				g = g.replace(/,/g,"");
				d = d.replace(/,/g,"");
				b = b.replace(/,/g,"");
				
				pajak = parseFloat(g) - parseFloat(d) + parseFloat(b);
				pajak = Math.round(pajak / 10);
				
				document.getElementById('ppn').value = pajak;
				//alert(pajak);
				//alert(document.getElementById("rdInclude").);
			}
			</script>
		</form>
	</body>
</HTML>
