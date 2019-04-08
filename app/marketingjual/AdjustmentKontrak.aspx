<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.AdjustmentKontrak" CodeFile="AdjustmentKontrak.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Prosedur Adjustment Kontrak</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Adjustment Kontrak">
		<style type="text/css">
		    tr, td { vertical-align:top; }
		</style>
	</HEAD>
	<body class="body-padding" onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
		
		<form class="cnt" id="Form1" method="post" runat="server">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<div style="DISPLAY: none"><asp:checkbox id="dariDaftar" runat="server"></asp:checkbox></div>
			<input style="DISPLAY: none">
			<div id="pilih" runat="server">
				<h1 class="title title-line">Adjustment Kontrak</h1>
				<p><b><i>Halaman 1 dari 2</i></b></p>
				<br />
				<table style="BORDER-RIGHT: #dcdcdc 1px solid; BORDER-TOP: #dcdcdc 1px solid; BORDER-LEFT: #dcdcdc 1px solid; BORDER-BOTTOM: #dcdcdc 1px solid"
					cellspacing="5">
					<tr>
						<td style="padding-top:8px;"><b>No. Kontrak :</b></td>
						<td>
                            <asp:textbox id="nokontrak" runat="server" width="100" cssclass="txt" />
                            <input class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a' type="button" value="&#xf002;" style="font-family: 'fontawesome'"
								name="btnpop" runat="server" />
						</td>
						<td>
                            <asp:LinkButton id="next" runat="server" cssclass="btn btn-blue" onclick="next_Click">
                                Next <i class="fa fa-arrow-right"></i>
                            </asp:LinkButton>
						</td>
					</tr>
				</table>
				<p class="feed"><asp:label id="feed" runat="server" /></p>
                <p class="adj">
					<asp:label id="adj" runat="server"></asp:label>
				</p>
				<input class="btn" id="backbtn" style="MARGIN: 5px" onclick="history.back(-1)" type="button"
					value="Cancel" name="backbtn" runat="server">
			</div>
			<div id="frm" runat="server">
				<h1 class="title title-line">Adjustment Kontrak</h1>
				<p><b><i>Halaman 2 dari 2</i></b></p>
				<br />
				<table cellspacing="0" cellpadding="0">
					<tr>
						<td width="400">
							<table cellspacing="5">
							    <tr style="display:none">
									<td>Project</td>
									<td>:</td>
									<td colspan="2">
                                        <asp:DropDownList runat="server" ID="project" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>                        
									</td>
								</tr>
							    <tr>
									<td>Skema</td>
									<td>:</td>
									<td colspan="2">
									    <asp:textbox id="skema" runat="server" width="200" cssclass="txt" />
									    <br />
									    <asp:label id="skemac" runat="server" cssclass="err" />
									    <br />
									</td>
								</tr>
								<tr>
									<td align="right" colspan="4">
										<hr noshade size="1">
									</td>
								</tr>
								<tr>
								    <td colspan="4"><b>Perincian Kontrak Sebelum Adjustment</b>
								    </td>
							    </tr>
							    <tr>
									<td>Harga Minimum</td>
									<td>:</td>
									<td align="right"><asp:label id="pricemin" runat="server" /></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
									</td>
								</tr>
								<tr>
									<td>Nilai Kontrak</td>
									<td>:</td>
									<td align="right"><asp:label id="nilai" runat="server" /></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
									</td>
								</tr>
								<tr>
									<td align="right" colspan="3">
										<hr noshade size="1">
									</td>
									<td></td>
								</tr>
							    <tr>
								    <td>DPP</td>
								    <td>:</td>
								    <td align="right"><asp:label id="lblDPP" runat="server" font-bold="True" /></td>
								    <td style="PADDING-LEFT:10px;FONT-WEIGHT:normal;FONT-SIZE:8pt;LINE-HEIGHT:normal;FONT-STYLE:normal;FONT-VARIANT:normal">
									    rupiah
								    </td>
							    </tr>
							    <tr>
								    <td>PPN</td>
								    <td>:</td>
								    <td align="right"><asp:label id="lblPPN" runat="server" font-bold="True" /></td>
								    <td style="PADDING-LEFT:10px;FONT-WEIGHT:normal;FONT-SIZE:8pt;LINE-HEIGHT:normal;FONT-STYLE:normal;FONT-VARIANT:normal">
									    rupiah
								    </td>
							    </tr>
								
								<tr>
									<td align="right" colspan="4">
										<hr noshade size="1">
									</td>
								</tr>
								<tr>
									<td colspan="4">    
										<p><b>Perhitungan Harga Baru</b></p>
									</td>
								</tr>
								<tr>
									<td>Price List (Gross)</td>
									<td>:</td>
									<td align="right"><asp:textbox id="gross" runat="server" width="125" 
                                            cssclass="txt_num" OnTextChanged="gross_TextChanged" AutoPostBack="true"/></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah</td>
								</tr>
                                <tr>
									<td>Bunga</td>
									<td>:</td>
									<td align="right"><asp:textbox id="bunga" runat="server" width="125" 
                                            cssclass="txt_num" ontextchanged="bunga_TextChanged" AutoPostBack="true"/></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="bungac" runat="server" cssclass="err" /></td>
								</tr>
								<tr>
									<td>Diskon Skema</td>
									<td>:</td>
									<td align="right"><asp:textbox id="discSkema" runat="server" width="125" 
                                            cssclass="txt_num" ontextchanged="discSkema_TextChanged" AutoPostBack="true"/></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="discSkemac" runat="server" cssclass="err" /></td>
								</tr>
								<tr>
									<td>Diskon Tambahan</td>
									<td>:</td>
									<td align="right"><asp:textbox id="disc" runat="server" width="125" 
                                            cssclass="txt_num" ontextchanged="disc_TextChanged" AutoPostBack="true" /></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="discc" runat="server" cssclass="err" /></td>
								</tr>
								<tr>
							        <td>Sifat PPN</td>
							        <td>:</td>
							        <td colspan="2" align="right">
								        <asp:RadioButtonList ID="sifatppn" runat="server" RepeatDirection="Horizontal">
									        <asp:ListItem class="igroup-radio" Value="0">Tanpa PPN</asp:ListItem>
									        <asp:ListItem class="igroup-radio" Value="1" Selected="True">Dengan PPN</asp:ListItem>
								        </asp:RadioButtonList>
								        <asp:label id="sifatppnc" runat="server" cssclass="err" />
							        </td>
						        </tr>
						        <tr id="nidpp" runat="server">
						            <td>Nilai DPP</td>
						            <td>:</td>
						            <td align="right"><asp:TextBox ID="nilaidpp" runat="server" AutoPostBack="true" 
                                            ontextchanged="nilaidpp_TextChanged" cssclass="txt_num" Width="125"></asp:TextBox></td>
						            <td>rupiah</td>
						        </tr>
						        <tr id="nippn" runat="server">
						            <td>Nilai PPN</td>
						            <td>:</td>
						            <td align="right"><asp:textbox id="nilaippn" runat="server" width="125" 
                                            cssclass="txt_num" ontextchanged="nilaippn_TextChanged" AutoPostBack="true"/></asp:TextBox></td>
						            <td>
						                rupiah
						                
						            </td>
						        </tr>
						        
						        <tr>
						            <td>Nilai Kontrak</td>
						            <td>:</td>
						            <td align="right"><asp:Label ID="nilaikontrak" runat="server" AutoPostBack="true"></asp:Label></td>
						            <td>rupiah</td>
						        </tr>
								<tr>
									<td colspan="4"><asp:label id="adjustinfo" runat="server" forecolor="Red" /></td>
								</tr>
							    <tr>
							        <td colspan="4">
							            <h2 id="warningkomisi" style="PADDING-RIGHT:5px; DISPLAY:none; PADDING-LEFT:5px; PADDING-BOTTOM:5px; COLOR:red; PADDING-TOP:5px" runat="server">PERHITUNGAN KOMISI SUDAH DIKELUARKAN</h2>
							        </td>
							    </tr>
							    <tr>
							        <td colspan="4">
							            <i>
                                            notes : <br />- Perhitungan Harga Baru tidak boleh dibawah Harga Minimum
                                            <br />- Lakukan Reschedule Tagihan untuk menyesuaikan Nilai Tagihan
							            </i>
                                        <br />
							            <asp:Label runat="server" ID="errorc" CssClass="err" />
							        </td>
							    </tr>
							</table>
							<br />
							<table>
								<tr>
									<td>
                                        <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="save_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
									</td>
									<td><input class="btn btn-red" id="cancel" style="WIDTH: 75px" onclick="location.href='?'" type="button"
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
										<br />
										<asp:label id="nokontrakl" runat="server" font-bold="True" /></td>
								</tr>
								<tr>
									<td>Unit :
										<br />
										<asp:label id="unit" runat="server" font-bold="True" /></td>
								</tr>
								<tr>
									<td>Customer :
										<br />
										<asp:label id="customer" runat="server" font-bold="True" /></td>
								</tr>
								<tr>
									<td>Sales :
										<br />
										<asp:label id="agent" runat="server" font-bold="True" /></td>
								</tr>
							</table>
							<br />
							<table cellspacing="5">
								<tr>
									<td>Nilai Kontrak</td>
									<td>:</td>
									<td align="right"><asp:label id="netto" runat="server" font-bold="True" /></td>
								</tr>
								<tr>
									<td>Total Tagihan</td>
									<td>:</td>
									<td align="right" width="120"><asp:label id="totaltagihan" runat="server" font-bold="True" /></td>
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
									<td align="right"><asp:label id="outofbalance" runat="server" font-bold="True" /></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<script language="javascript">
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
