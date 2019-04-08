<%@ Page Language="c#" Inherits="ISC064.COLLECTION.CustomerInfo" CodeFile="CustomerInfo.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadCIF" Src="HeadCIF.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCIF" Src="NavCIF.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Customer Information File (Tagihan dan Pelunasan)</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Customer Information File - Tagihan dan Pelunasan">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header" style="width:145%">
            <uc1:NavCIF ID="NavCIF1" runat="server" Aktif="1"></uc1:NavCIF>
        </div>
        <div class="tabdata" style="width: 145%">
            <div class="pad">
                <uc1:HeadCIF ID="HeadCIF1" runat="server"></uc1:HeadCIF>
                <br>
                <table>
                    <tr>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog" style="text-align:left"
                                    accesskey="l">
                            </label>
                        </td>
                    </tr>
                </table>
                <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
                    <asp:TableRow HorizontalAlign="Left">
                        <asp:TableHeaderCell Wrap="false" RowSpan="2">No.</asp:TableHeaderCell>
                        <asp:TableHeaderCell Wrap="false" RowSpan="2">Tagihan</asp:TableHeaderCell>
                        <asp:TableHeaderCell Wrap="false" RowSpan="2">Tgl.</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" RowSpan="2">Pokok</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" RowSpan="2">Pelunasan</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" RowSpan="2">Sisa</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" ColumnSpan="4">Denda</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" RowSpan="2">Lebih Bayar</asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="Left">
                        <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false">Denda</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false">Realisasi Denda</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false">Putih Denda</asp:TableHeaderCell>
                        <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false">Sisa Denda</asp:TableHeaderCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </div>
    </form>
</body>
</html>
