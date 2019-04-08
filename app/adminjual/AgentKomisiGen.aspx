<%@ Page Language="C#" Inherits="ISC064.ADMINJUAL.AgentKomisiGen" CodeFile="AgentKomisiGen.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="HeadAgent" Src="HeadAgent.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavAgent" Src="NavAgent.ascx" %>
<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Generate Komisi</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
    	<link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Sales - Generate Komisi">
	</head>
	<body onkeyup="if(event.keyCode==27) window.close()">
		<form id="Form1" method="post" runat="server">
			<uc1:navagent id="NavAgent1" runat="server" aktif="4"></uc1:navagent>
			<div class="tabdata">
				<div class="pad">
					<uc1:headagent id="HeadAgent1" runat="server"></uc1:headagent>
					<div style="padding:5">


                        <div style="DISPLAY: none"><asp:checkbox id="dariDaftar" runat="server"></asp:checkbox><asp:checkbox id="dariReminder" runat="server"></asp:checkbox></div>
			<div id="pilih2" runat="server">
				<h1>Generate Komisi</h1>
				<p>Halaman 1 dari 2</p>
				<br>
				<table style="BORDER-RIGHT: #dcdcdc 1px solid; BORDER-TOP: #dcdcdc 1px solid; BORDER-LEFT: #dcdcdc 1px solid; BORDER-BOTTOM: #dcdcdc 1px solid"
					cellSpacing="5">
					<tr vAlign="top">
						<td></td>
						<td>
							<p class="pparam">
								<table>
									<tr>
										<td><strong>Periode:</strong></td>
										<td>dari</td>
										<td><asp:textbox id="dari" runat="server" width="85" cssclass="txt_center"></asp:textbox><label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
										</td>
										<td rowSpan="3">&nbsp;&nbsp;</td>
										<td>sampai</td>
										<td><asp:textbox id="sampai" runat="server" width="85" cssclass="txt_center"></asp:textbox><label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
										</td>
									</tr>
									<tr>
										<td colSpan="3"><asp:label id="daric" runat="server" cssclass="err"></asp:label></td>
										<td colSpan="2"><asp:label id="sampaic" runat="server" cssclass="err"></asp:label></td>
									</tr>
									<tr>
										<td colSpan="3">
                        <asp:button id="next" runat="server" cssclass="btn" text="Next  " 
                    onclick="next_Click"></asp:button>
			                            </td>
										<td colSpan="2">&nbsp;</td>
									</tr>
								</table>
							</p>
						</td>
					</tr>
				</table>
			</div>
			<div id="pilih" runat="server">
				<p class="feed"><asp:label id="feed" runat="server"></asp:label></p>
				<%--<input class="btn" id="backbtn" style="MARGIN: 5px" onclick="history.back(-1)" type="button"
					value="" name="backbtn" runat="server">--%>
			</div>
			<div id="frm" runat="server">
				<h1>Generate Komisi</h1>
				<p>Halaman 2 dari 2</p>
				<br>
				<asp:dropdownlist id="daftarskema" runat="server" visible="False"></asp:dropdownlist>
				<%--<table cellSpacing="5">
					<!--		<tr>
						<td>No. Kontrak :<br>
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
					</tr>-->
					<tr>
						<td>Sales :
							<br>
							<asp:label id="agent" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Principal :
							<br>
							<asp:label id="principal" runat="server" font-bold="True"></asp:label></td>
					</tr>
				</table>--%>
				
                <table style="BORDER: #dcdcdc 1px solid; margin:5px">
                    <tr>
                        <td>Sales :
						<br>
						<asp:label id="agent" runat="server" font-bold="True"></asp:label></td>
                    </tr>
                    <tr>
                        <td>Periode Komisi :
						<br>
						<asp:label id="lbldari" runat="server" font-bold="True"></asp:label>&nbsp;-
						<asp:label id="lblsampai" runat="server" font-bold="True"></asp:label></td>
                    </tr>
                </table><hr SIZE="1">
				<!--<table cellSpacing="5">
					<tr>
						<td>Nilai Kontrak</td>
						<td>:</td>
						<td><asp:label id="netto" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Nilai Kumulatif</td>
						<td>:</td>
						<td><asp:label id="kumulatif" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Skema Berlaku</td>
						<td>:</td>
						<td><asp:label id="skemaid" runat="server" visible="False"></asp:label><asp:label id="skema" runat="server" font-bold="True"></asp:label></td>
					</tr>
					<tr>
						<td>Tipe Komisi</td>
						<td>:</td>
						<td><asp:label id="lblTipeKomisi" runat="server"></asp:label></td>
					</tr></table>-->
				<asp:table id="rpt" runat="server" cssclass="blue-skin tb" cellspacing="0">
					<asp:tablerow horizontalalign="Left">
						<asp:tableheadercell>No. Kontrak</asp:tableheadercell>
						<asp:tableheadercell>Tgl. Kontrak</asp:tableheadercell>
						<asp:tableheadercell width="100">Unit</asp:tableheadercell>
						<asp:tableheadercell width="200">Customer</asp:tableheadercell>
						<asp:tableheadercell width="100">Price List</asp:tableheadercell>
						<%--<asp:tableheadercell width="100">DPP</asp:tableheadercell>--%>
						<asp:tableheadercell width="100">Nilai Kontrak</asp:tableheadercell>
					</asp:tablerow>
				</asp:table><%--<asp:tableheadercell width="100">DPP</asp:tableheadercell>--%>
				<table height="50">
					<tr>
						<td><asp:button id="save" runat="server" width="75" cssclass="btn" text="GENERATE" 
                                onclick="save_Click"></asp:button></td>
						<td><input class="btn" id="btnCancel" style="MARGIN: 5px" 
                                onclick="history.back(-1)" type="button"
					         value="Cancel" name="backbtn" runat="server">
						</td>
						<td style="PADDING-LEFT: 7px"><asp:label id="noskema" runat="server" cssclass="err"></asp:label></td>
					</tr>
				</table>
			
			   <script type = "text/javascript">
			       function call(nomor) {
			           popEditKontrak(nomor);
			       }
			        </script>
					
				</div>
			</div>
		</form>
	</body>
</html>
