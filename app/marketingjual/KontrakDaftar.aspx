<%@ Page language="c#" Inherits="ISC064.MARKETINGJUAL.KontrakDaftar" CodeFile="KontrakDaftar.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Pendaftaran Surat Pesanan</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="Kontrak - Pendaftaran Surat Pesanan (Hal. 1)">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1 class="title title-line">Pendaftaran Surat Pesanan</h1>
			<p><b><i>Halaman 1 dari 4</i></b></p>
			<br>
			<table cellspacing="0" cellpadding="0">
				<tr valign="top">
					<td width="400" style="padding-top:5px;">
						<table style="border:1px solid #DCDCDC;" cellspacing="5">
                            <tr>
                                <td><b>Project</b></td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="project" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
							<tr>
								<td><b>Unit</b></td>
								<td>:</td>
								<td>
									<asp:textbox id="unit" runat="server" cssclass="txt" width="145"></asp:textbox>
                                </td>
                                <td>
									<asp:button id="display" runat="server" cssclass="btn btn-blue" text="Display" onclick="display_Click"></asp:button>
								</td>
							</tr>
						</table>
						<br>
						<p style="padding:5;">
							<u>Reservasi yang ditampilkan adalah reservasi
								<br>
								dengan nomor urut PERTAMA.</u>
						</p>
					</td>
                    <td width="30"></td>
                    <td class="verticalline"></td>					
					<td>
						<p><b>Terbaru :</b></p>
						<asp:listbox id="baru" rows="10" runat="server" width="200" cssclass="ddl"></asp:listbox>
					</td>
				</tr>
			</table>
			<br>
			<div id="hasil" runat="server">
				<div class="peach">
                    <p style="font:8pt;padding-left:3px">
					    Harga price list adalah dalam rupiah per meter persegi per bulan.
					    <br>
                    Luas adalah dalam meter persegi.
                </p>
            </div>
            <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="Next" />
                    <asp:BoundField HeaderText="No.Stock" DataField="No" />
                    <asp:BoundField HeaderText="Unit" DataField="Unit" />
                    <asp:BoundField HeaderText="Customer" DataField="Customer" />
                    <asp:BoundField HeaderText="Sales" DataField="Sales" />
                    <asp:BoundField HeaderText="Tgl. Reservasi" DataField="Tgl" />
                    <asp:BoundField HeaderText="Batas Waktu" DataField="Exp" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
