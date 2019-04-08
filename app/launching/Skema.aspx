<%@ Page language="c#" Inherits="ISC064.LAUNCHING.Skema" CodeFile="Skema.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Skema Cara Bayar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Skema Cara Bayar">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<script language="javascript" src="/Js/Pop.js"></script>
		<script language="javascript" src="/Js/NumberFormat.js"></script>
		<form id="Form1" method="post" runat="server">
			<h1 style="padding:5">Skema Cara Bayar</h1>
			<table cellspacing="0" cellpadding="0" id="pilih" runat="server">
				<tr valign="top">
					<td>
						<table cellspacing="5">
							<tr valign="top">
								<td>
									Cara Bayar :
									<br>
									<asp:listbox id="carabayar" runat="server" cssclass="ddl" rows="12" width="300"></asp:listbox>
								</td>
							</tr>
							<tr>
								<td>
									Tgl. Kontrak :
									<asp:textbox id="tglkontrak" runat="server" cssclass="txt_center" width="85"></asp:textbox>
									<input type="button" value="..." class="btn" onclick="openCalendar('tglkontrak')">
									<asp:label id="tglkontrakc" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
						</table>
					</td>
					<td width="20"></td>
					<td>
						<table cellspacing="5">
							<tr>
								<td>Total (Gross)</td>
								<td>:</td>
								<td align="right">
									<asp:textbox id="gross" runat="server" cssclass="txt_num" width="125"></asp:textbox>
								</td>
								<td style="font:8pt;padding-left:10">
									rupiah
								</td>
							</tr>
							<tr>
								<td>Diskon</td>
								<td>:</td>
								<td align="right">
									<asp:textbox id="disc" runat="server" cssclass="txt_num" width="100"></asp:textbox>
								</td>
								<td style="font:8pt;padding-left:10">
									persen bertingkat
								</td>
							</tr>
							<tr>
								<td colspan="3" align="right">
									<hr size="1" noshade>
								</td>
								<td style="font:bold">-</td>
							</tr>
							<tr>
								<td>Nilai Kontrak</td>
								<td>:</td>
								<td align="right">
									<asp:textbox id="nilai" runat="server" tabindex="99" cssclass="txt_num" width="125"></asp:textbox>
								</td>
								<td style="font:8pt;padding-left:10">
									rupiah
									<asp:label id="nilaic" runat="server" cssclass="err"></asp:label>
								</td>
							</tr>
							<tr>
								<td colspan="3">
									<asp:button id="hitung" runat="server" cssclass="btn" text="Hitung" onclick="hitung_Click"></asp:button>
									<input type="button" onclick="window.close()" value="Cancel" class="btn" style="width:55">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div id="hasil" runat="server">
				<p style="padding:5">
					<a href="javascript:history.back(-1)">Back...</a>
				</p>
				<asp:table id="rpt" runat="server" cssclass="tb" cellspacing="5">
					<asp:tablerow horizontalalign="Left">
						<asp:tableheadercell>No.</asp:tableheadercell>
						<asp:tableheadercell width="300">Keterangan</asp:tableheadercell>
						<asp:tableheadercell width="100">Jatuh Tempo</asp:tableheadercell>
						<asp:tableheadercell horizontalalign="Right" width="120">Jumlah</asp:tableheadercell>
					</asp:tablerow>
				</asp:table>
			</div>
			<div id="noskema" runat="server" style="padding:5px;margin:40px">
				<h2>Skema Belum Tersedia</h2>
				<p style="font:8pt">Silakan menghubungi administrasi untuk melakukan setup skema 
					cara bayar.</p>
			</div>
			<script language="javascript">
			function load() {
				Tgl = document.getElementById('tglkontrak').value;
				PriceList = cvtnum(document.getElementById('gross').value);
				
				foo = document.getElementById('carabayar');
				Nomor = foo.options[foo.selectedIndex].value;
				
				location.href='?Nomor='+Nomor+'&pl='+PriceList+'&tgl='+Tgl
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
						nett = nett - (nett * (disc[i]/100) * -1);
						if(discTxt.value!="") discTxt.value = discTxt.value + "+";
						discTxt.value = discTxt.value + disc[i];
					}
				}
				
				n = Math.round(100*nett)/100;
				eval("nettTxt.value = FinalFormat('"+n+"')");
			}
			function nohitung() {
				document.getElementById('hitung').disabled = true;
			}
			function okhitung() {
				document.getElementById('hitung').disabled = false;
			}
			function cvtnum(foo){
				return foo.replace(/,/gi ,"");
			}
			</script>
		</form>
	</body>
</html>
