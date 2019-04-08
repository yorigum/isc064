<%@ Page language="c#" Inherits="ISC064.LEGAL.ST" CodeFile="ST.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
	<head>
		<title>Serah Terima</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="/Media/Style.css" type="text/css" rel="stylesheet">
		<meta name="ctrl" content="1">
		<meta name="sec" content="ST">
	</head>
	<body class="body-padding">
		<form id="Form1" method="post" runat="server" class="cnt">
			<uc1:head id="Head1" runat="server"></uc1:head>
			<input type="text" style="display:none">
			<h1 class="title title-line">Serah Terima</h1>
			<br>
			<table style="border:1px solid #DCDCDC" cellspacing="10">
				<tr>
					<td>
						<input type="button" class="btn btn-blue" value="Search" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx' id="search"
							runat="server" name="search" accesskey="s">
					</td>
					<td>
						<asp:textbox id="dari" runat="server" cssclass="txt_center tgl" width="160"></asp:textbox>
						<label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="daric" runat="server" cssclass="err"></asp:label>
					</td>
					<td>
                        <asp:textbox id="sampai" runat="server" cssclass="txt_center tgl" width="160"></asp:textbox>
						<label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
						<asp:label id="sampaic" runat="server" cssclass="err"></asp:label>
					</td>
                    <td>
                        <asp:dropdownlist id="status" runat="server" width="200">
								<asp:listitem>Status :</asp:listitem>
								<asp:listitem>Aktif</asp:listitem>
								<asp:listitem>Batal</asp:listitem>
                        </asp:dropdownlist>
					</td>
                    <td>
                        <asp:dropdownlist id="project" runat="server" width="200">
								<asp:listitem>Project :</asp:listitem>
                        </asp:dropdownlist>
					</td>
                    <td>
						<asp:button id="display" runat="server" cssclass="btn btn-blue" text="Display" onclick="display_Click"></asp:button>
					</td>	
                </tr>				
			</table>
			<br>
            <%--<div class="peach">
			    <p style="padding:3px;font:8pt">
				    Cara Bayar : TN = Tunai / KD = Kartu Debit / KK = Kartu Kredit / TR = Transfer 
				    Bank / BG = Cek Giro
			    </p>
            </div>--%>
        <asp:GridView ID="tb" runat="server" SkinID="pager" OnPageIndexChanging="tb_PageIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="No. Kontrak" DataField="Kontrak" />
                <asp:BoundField HeaderText="Unit " DataField="Unit" />
                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" />
                <asp:BoundField HeaderText="Project" DataField="Project" />
            </Columns>
        </asp:GridView>
        <script type="text/javascript">
            function call(nomor) {
                popKontrakSTEdit(nomor);
            }
        </script>
    </form>
</body>
</html>

