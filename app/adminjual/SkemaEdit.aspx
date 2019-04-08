<%@ Page language="c#" Inherits="ISC064.ADMINJUAL.SkemaEdit" CodeFile="SkemaEdit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadSkema" Src="HeadSkema.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavSkema" Src="NavSkema.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Edit Skema Cara Bayar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
        <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="5">
		<meta name="sec" content="Setup Skema Cara Bayar - Edit Skema CB">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
            <div class="content-header">
			    <uc1:NavSkema id="NavSkema1" runat="server" aktif="1"></uc1:NavSkema>
            </div>
			<div class="tabdata">
				<div class="pad">
					<uc1:headskema id="HeadSkema1" runat="server"></uc1:headskema>
					<table cellspacing="5">
						<tr>
							<td width="100%"></td>
							<td>
                                <label class="ibtn ibtn-file">
								    <input type="button" class="btn btn-blue btn-ico" value="Log" id="btnlog" runat="server" name="btnlog" accesskey="l">
                                </label>
							</td>
							<td>
                                <label class="ibtn ibtn-remove">
                                    <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel" accesskey="d">
                                </label>
							</td>
						</tr>
					</table>
					<p class="feed" style="PADDING-LEFT:5px">
						<asp:label id="feed" runat="server"></asp:label>
					</p>
					<table cellspacing="5">
						<tr>
							<td>
								<asp:radiobutton id="aktif" runat="server" text="Aktif" font-size="12" font-bold="True" forecolor="green"
									groupname="status" style="padding-right:20px"></asp:radiobutton>
								<asp:radiobutton id="inaktif" runat="server" text="Inaktif" font-size="12" font-bold="True" forecolor="red"
									groupname="status"></asp:radiobutton>
							</td>
							<td width="20"></td>
							<td>Nama</td>
							<td>:</td>
							<td><asp:textbox id="nama" runat="server" width="220" maxlength="100" cssclass="txt"></asp:textbox>
								<asp:label id="namac" runat="server" cssclass="err" width="50"></asp:label></td>
							<td>Diskon</td>
							<td>:</td>
							<td>
								<asp:textbox id="diskon" runat="server" width="150" cssclass="txt" maxlength="100"></asp:textbox>
								<input type="button" class="btn" value="..." onclick="popdiskon('diskon','diskonket')">
								<div style="DISPLAY:none">
									<asp:textbox id="diskonket" runat="server"></asp:textbox>
								</div>
								% bertingkat &nbsp;&nbsp;
								<asp:checkbox id="round" runat="server" text="Pembulatan Nilai"></asp:checkbox>
							</td>
						</tr>
						<tr>
						    <td colspan="2">&nbsp;</td>
                            <td>Project</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="project" runat="server"></asp:DropDownList>
                            </td>
						    <td valign="top">Present Value</td>
			                <td valign="top">:</td>
			                <td>
			                    <div id="persentingakat" runat="server">
				                    <input class="btn" onclick="popbunga('bunga2','bungaket')" type="button" value="..." id="btn2" runat="server">
				                    <asp:textbox id="bunga2" runat="server" cssclass="txt" width="50px" Text='0'>0</asp:textbox>&nbsp;
                                <div style="DISPLAY: none"><asp:textbox id="bungaket" runat="server"></asp:textbox></div>
				                    <asp:label id="bunga2c" runat="server" cssclass="err"></asp:label>
				                </div>
			                </td>
						</tr>
						<tr>
						<td colspan=20>Jenis : <asp:radiobuttonlist id="jenis" runat="server" repeatdirection="Horizontal">
											<asp:listitem style="padding-right:20px">KPR</asp:listitem>
											<asp:listitem style="padding-right:20px">CASH BERTAHAP</asp:listitem>
											<asp:listitem style="padding-right:20px">CASH KERAS</asp:listitem>
										</asp:radiobuttonlist></td>
						</tr>
					</table>
					<table cellspacing="1" class="tb blue-skin">
						<tr align="left">
							<th>
								No.</th>
							<th>
								Tipe</th>
							<th>
								Nama</th>
							<th>
								Nilai</th>
							<th>
								Jadwal</th>
							<th>
								Setelah</th>
							<th>
								BF</th>
							<th>
								KPR</th>
                            <th></th>
						</tr>
						<asp:placeholder id="list" runat="server"></asp:placeholder>
						<tr>
							<td>Baru:</td>
							<td>
								<asp:radiobutton id="barubf" runat="server" groupname="barutipe" text="BF" checked="True"></asp:radiobutton>&nbsp;
								<asp:radiobutton id="barudp" runat="server" groupname="barutipe" text="DP"></asp:radiobutton>&nbsp;
								<asp:radiobutton id="baruang" runat="server" groupname="barutipe" text="ANG"></asp:radiobutton>
							</td>
							<td>
								<asp:textbox id="barunama" runat="server" cssclass="txt" maxlength="50"></asp:textbox>
							</td>
							<td>
								<asp:radiobutton id="barupersen" runat="server" groupname="barutipenominal" text="%" checked="True"></asp:radiobutton>&nbsp;
								<asp:radiobutton id="barurupiah" runat="server" groupname="barutipenominal" text="F"></asp:radiobutton>&nbsp;
								<asp:textbox id="barunominal" runat="server" cssclass="txt_num"></asp:textbox>
							</td>
							<td>
								<asp:radiobutton id="barubln" runat="server" groupname="barujadwal" text="M" checked="True"></asp:radiobutton>&nbsp;
								<asp:radiobutton id="baruhr" runat="server" groupname="barujadwal" text="D"></asp:radiobutton>&nbsp;
								<asp:radiobutton id="barufix" runat="server" groupname="barujadwal" text="F"></asp:radiobutton>&nbsp;
								<asp:textbox id="barulama" runat="server" cssclass="txt_center" width="85"></asp:textbox>
								<input type="button" class="btn" value="..."  onclick="openCalendar('barulama')">
							</td>
							<td>
								<asp:textbox id="barureferensi" runat="server" width="40" cssclass="txt_center"></asp:textbox>
							</td>
							<td>
								<asp:checkbox id="barupotong" runat="server"></asp:checkbox>
							</td>
							<td>
								<asp:checkbox id="kpr" runat="server" />
							</td>
                            <td></td>
						</tr>
					</table>
					<table height="50">
						<tr>
							<td>
                                <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK</asp:LinkButton>
								<%--<asp:button id="ok" runat="server" cssclass="btn btn-blue" text="OK" width="75" onclick="ok_Click"></asp:button>--%>
							</td>
							<td>
                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a" OnClick="save_Click"><i class="fa fa-check"></i> Apply </asp:LinkButton>								
							</td>
							<td>
								<input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel" style="width: 75px">
							</td>
						</tr>
					</table>
				</div>
			</div>
			<script language="javascript">
			function popdiskon(d1,d2){
				foo1 = document.getElementById(d1);
				foo2 = document.getElementById(d2);
				openModal('SkemaDiskon.aspx?t1='+foo1.value+'&t2='+foo2.value+'&d1='+d1+'&d2='+d2,'450','360');
			}
			function popbunga(d1, d2) {
			    foo1 = document.getElementById(d1);
			    foo2 = document.getElementById(d2);
			    openModal('SkemaBunga.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
			}
			function recaldisc(discTxt){
				disc = discTxt.value.split("+");
				
				discTxt.value = "";
				
				for(i=0;i<disc.length;i++)
				{
					if(!isNaN(disc[i]) && disc[i]!="")
					{
						if(discTxt.value!="") discTxt.value = discTxt.value + "+";
						discTxt.value = discTxt.value + disc[i];
					}
				}
			}
			function hapusbaris(nomor, baris)
			{
				if(confirm('Hapus satu baris detail ini dari skema?\nPerhatian bahwa data akan dihapus secara PERMANEN.')){
					location.href='SkemaDelBaris.aspx?Nomor=' + nomor + '&Baris=' + baris;
				}
			}
			</script>
		</form>
	</body>
</html>
