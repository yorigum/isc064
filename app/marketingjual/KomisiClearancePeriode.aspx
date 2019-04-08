<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KomisiClearancePeriode.aspx.cs"
    Inherits="ISC064.MARKETINGJUAL.KomisiClearance" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Clear Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Clear Komisi Periode">

    <script type="text/javascript">
    	function call(nokontrak)
			{
				document.getElementById('nokontrak').value = nokontrak;
				document.getElementById('next').click();
			}
    </script>

</head>
<body onkeyup="&#13;&#10;&#9;if(document.getElementById('backbtn')){if(event.keyCode==27)document.getElementById('backbtn').click()};&#13;&#10;&#9;if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div id="pilih" runat="server" style="padding: 10px;">
        <h1 class="title title-line">Clear Komisi</h1>
        <p>
            Halaman 1 dari 2</p>
        <br />
        <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid;
            border-bottom: #dcdcdc 1px solid" cellspacing="5">
            <tr>
                <td colspan="10">
                    Periode Kontrak :
                </td>
            </tr>
            <tr>
                <td>
                    Dari
                </td>
                <td>
                    &nbsp;<asp:TextBox ID="dari" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                    <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    Sampai
                </td>
                <td>
                    <asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                    <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                </td>
                <td>
                    <asp:LinkButton ID="next" runat="server" CssClass="btn  btn-blue" OnClick="next_Click">
                        Next <i class="fa fa-arrow-right"></i>
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <p class="feed">
            <asp:Label ID="feed" runat="server"></asp:Label>
        </p>
    </div>
    <div id="frm" runat="server" visible="false" style="padding: 10px;">
        <h1 class="title title-line">
            Clear Komisi</h1>
        <p>
            Halaman 2 dari 2</p>
        <br />
        Hanya menampilkan komisi yang belum dibayar
        <asp:Table ID="rpt" runat="server" GridLines="Both"
            CssClass="blue-skin">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell>Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell>Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell>Nilai DPP</asp:TableHeaderCell>
                <asp:TableHeaderCell>Jenis Komisi</asp:TableHeaderCell>
                <asp:TableHeaderCell>Penerima</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right">Nilai</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        <br />
        <asp:Button ID="save" runat="server" Text="Clear" OnClick="save_Click" cssclass="btn btn-blue" />
    </div>
    </form>
</body>
</html>
