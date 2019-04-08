<%@ Page Language="c#" Inherits="ISC064.APPROVAL.KontrakApprovADJ2" CodeFile="KontrakApprovADJ2.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Approval Adjustment Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Approval Adjustment">
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div>
            <h1 class="title title-line">Approval Kontrak Diskon</h1>
            <br>
				<table cellspacing="0" cellpadding="0">
					<tr>
						<td>
							<table cellspacing="5">
							    <tr>
									<td>No. Kontrak</td>
									<td>:</td>
									<td colspan="2">
                                        <asp:Label runat="server" ID="nokontrak"></asp:Label>
									</td>
								</tr>
							    <tr>
									<td>Unit</td>
									<td>:</td>
									<td colspan="2">
                                        <asp:Label runat="server" ID="unit"></asp:Label>
									</td>
								</tr>
							    <tr>
									<td>Customer</td>
									<td>:</td>
									<td colspan="2">
                                        <asp:Label runat="server" ID="customer"></asp:Label>
									</td>
								</tr>
							    <tr>
									<td>Sales</td>
									<td>:</td>
									<td colspan="2">
                                        <asp:Label runat="server" ID="agent"></asp:Label>
									</td>
								</tr>
							    <tr>
									<td>Skema</td>
									<td>:</td>
									<td colspan="2">
                                        <asp:Label runat="server" ID="skema"></asp:Label>
									</td>
								</tr>
							    <tr>
									<td>Tanggal Pengajuan</td>
									<td>:</td>
									<td colspan="2">
                                        <asp:Label runat="server" ID="tglpengajuan"></asp:Label>
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
									<td align="right"><asp:Label runat="server" ID="gross"></asp:Label></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah</td>
								</tr>
                                <tr>
									<td>Bunga</td>
									<td>:</td>
									<td align="right"><asp:Label runat="server" ID="bunga"></asp:Label></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="bungac" runat="server" cssclass="err" /></td>
								</tr>
								<tr>
									<td>Diskon Skema</td>
									<td>:</td>
									<td align="right"><asp:Label runat="server" ID="diskonrupiah"></asp:Label></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="discSkemac" runat="server" cssclass="err" /></td>
								</tr>
								<tr>
									<td>Diskon Tambahan</td>
									<td>:</td>
									<td align="right"><asp:Label runat="server" ID="diskontambahan"></asp:Label></td>
									<td style="PADDING-LEFT: 10px; FONT-WEIGHT: normal; FONT-SIZE: 8pt; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">rupiah
										<asp:label id="discc" runat="server" cssclass="err" /></td>
								</tr>
						        <tr id="nidpp" runat="server">
						            <td>Nilai DPP</td>
						            <td>:</td>
						            <td align="right"><asp:Label runat="server" ID="dpp"></asp:Label></td>
						            <td>rupiah</td>
						        </tr>
						        <tr id="nippn" runat="server">
						            <td>Nilai PPN</td>
						            <td>:</td>
						            <td align="right"><asp:Label runat="server" ID="ppn"></asp:Label></td>
						            <td>
						                rupiah						                
						            </td>
						        </tr>						        
						        <tr>
						            <td>Nilai Kontrak</td>
						            <td>:</td>
						            <td align="right"><b><asp:Label ID="nilaikontrak" runat="server"></asp:Label></b></td>
						            <td>rupiah</td>
						        </tr>
						        <tr>
						            <td style="vertical-align:top">Note</td>
						            <td style="vertical-align:top">:</td>
						            <td align="right" colspan="2"><asp:TextBox runat="server" ID="note" TextMode="MultiLine" Height="70px" Width="300px"></asp:TextBox></td>						            
						        </tr>
								<tr>
									<td colspan="4"><asp:label id="adjustinfo" runat="server" forecolor="Red" /></td>
								</tr>
							    <tr>
							        <td colspan="4">
							            <h2 id="warningkomisi" style="PADDING-RIGHT:5px; DISPLAY:none; PADDING-LEFT:5px; PADDING-BOTTOM:5px; COLOR:red; PADDING-TOP:5px" runat="server">PERHITUNGAN KOMISI SUDAH DIKELUARKAN</h2>
							        </td>
							    </tr>
							</table>
							<br />
						</td>
					</tr>
				</table>            <br />
            <asp:LinkButton ID="save" runat="server" Width="75" OnClick="save_Click" CssClass="btn btn-blue" AccessKey="s"><i class="fa fa-share"></i> Approve</asp:LinkButton>
            <asp:LinkButton ID="reject" runat="server" Width="75" OnClick="reject_Click" CssClass="btn btn-red" AccessKey="d"><i class="fa fa-share"></i> Reject</asp:LinkButton>
    </form>
</body>
</html>

