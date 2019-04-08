<%@ Page language="c#" Inherits="ISC064.FINANCEAR.CustomerLunas" CodeFile="CustomerLunas.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadCIF" Src="HeadCIF.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCIF" Src="NavCIF.ascx" %>
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>Edit Alokasi Pelunasan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="5">
		<meta name="sec" content="Customer Information File - Edit Alokasi Pelunasan">
	</HEAD>
	<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Batalkan proses edit alokasi pelunasan?')) window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:navcif id="NavCIF1" runat="server" aktif="3"></uc1:navcif>
            </div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headcif id="HeadCIF1" runat="server"></uc1:headcif>
					<p class="feed" style="PADDING-RIGHT:3px; PADDING-LEFT:3px; PADDING-BOTTOM:3px; PADDING-TOP:3px">
						<asp:label id="feed" runat="server"></asp:label>
					</p>
					<div id="tenant" runat="server" style="WIDTH:100%" align="center">
						<i>Program Alokasi Pelunasan tidak tersedia untuk customer tipe TENANT / PENGHUNI</i>
					</div>
					<div id="frm" runat="server">
						<div class="peach">
                            <p style="PADDING-RIGHT:3px;PADDING-LEFT:3px;FONT-WEIGHT:normal;FONT-SIZE:8pt;PADDING-BOTTOM:3px;LINE-HEIGHT:normal;PADDING-TOP:3px;FONT-STYLE:normal;FONT-VARIANT:normal">
							    Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
							    Bank / MB = Merchant Banking / BG = Cek Giro<br />
							    Tipe Memo : PP = Penghapusan Piutang / DN = Diskon / TG = Tukar Guling
						    </p>
                        </div>
						<table cellspacing="0" class="tb blue-skin">
							<tr align="left" valign="bottom">
								<th width="130">
									Tagihan</th>
								<th width="42">
									TTS/Memo</th>
								<th width="150">
									Cara
									Bayar/<br />Tipe Memo</th>
								<th width="240">
									Keterangan</th>
								<th width="70">
									Tgl</th>
								<th width="90" align="right">
									Nilai</th>
							</tr>
							<asp:placeholder id="list" runat="server"></asp:placeholder>
						</table>
						<table style="height:50px">
							<tr>
								<td>
								    <asp:LinkButton id="ok" runat="server" cssclass="btn btn-blue" width="75" onclick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
                                </td>
                                <td>
								    <asp:Linkbutton id="save" runat="server" cssclass="btn btn-orange" width="75" accesskey="a" onclick="save_Click"><i class="fa fa-check"></i> Apply </asp:Linkbutton>
                                </td>
								<td>
									<input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
										onclick="window.close()">
								</td>								
							</tr>
						</table>
					</div>
				</div>
			</div>
		</form>
	</body>
</HTML>
