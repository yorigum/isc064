<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanMasterStock"
    CodeFile="LaporanMasterStock.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Master Stock Per Tipe Property</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Stock Per Tipe Property">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server">
                    </p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Master Stock Per Tipe Property</h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td></td>
                            <td>
                                <p class="pparam">
                                    <table>
                                        <tr>
                                            <td colspan="5">
                                                <b>Project :</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:DropDownList ID="project" runat="server" Width="200" CssClass="ddl" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>SEMUA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <b>Perusahaan :</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:DropDownList ID="pers" runat="server" Width="200" CssClass="ddl">
                                                    <asp:ListItem>SEMUA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <b>Tipe Property :</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:DropDownList ID="tipe" runat="server">
                                                    <asp:ListItem>SEMUA</asp:ListItem>
                                                    <%--                                                    <asp:ListItem>Apartment</asp:ListItem>
                                                    <asp:ListItem>Service Apartment</asp:ListItem>
                                                    <asp:ListItem>Medical Clinic</asp:ListItem>
                                                    <asp:ListItem>Office</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <b>Status Titip Jual :</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:DropDownList ID="titipjual" runat="server" Width="200" CssClass="ddl">
                                                    <asp:ListItem Selected="True" Value="SEMUA">SEMUA</asp:ListItem>
                                                    <asp:ListItem Value="1">Titip Jual</asp:ListItem>
                                                    <asp:ListItem Value="0">Non Titip Jual</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <b>Status Paket Investasi :</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:DropDownList ID="papen" runat="server" Width="200" CssClass="ddl">
                                                    <asp:ListItem Selected="True" Value="SEMUA">SEMUA</asp:ListItem>
                                                    <asp:ListItem Value="1">Paket Investasi</asp:ListItem>
                                                    <asp:ListItem Value="0">Non Paket Investasi</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </p>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click">
                                    <i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>

                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
            <%--            <h2 id="project" runat="server"></h2>
            <h1 class="title">Laporan Penjualan Master Stock</h1>
            <br />
            <asp:Label ID="filter" runat="server"></asp:Label>--%>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Center" VerticalAlign="Middle" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">Stock</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">Sold</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White" VerticalAlign="Middle">Deviasi STU Sold</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="White">Available</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White" VerticalAlign="Middle">Project</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai (excl. ppn)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai (excl. ppn)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Total Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Nilai (excl. ppn)</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">SGA</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">SGA</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Net</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">SGA</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
