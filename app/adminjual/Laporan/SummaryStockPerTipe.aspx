<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.Laporan.SummaryStockPerTipe" CodeFile="SummaryStockPerTipe.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Summary Stock Per Tipe</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Summary Stock Per Tipe">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server">
                    </p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Summary Stock Per Tipe
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="width: 100px"><p class="pparam"><b>Project :</b>
                                <asp:DropDownList ID="project" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList></p>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>
                                <p class="pparam">
                                    <b>Lokasi :</b>
                                    <br>
                                    <asp:ListBox ID="lokasi" runat="server" Rows="10" CssClass="ddl" Width="200">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:ListBox>
                                </p>
                            </td>
                            <td style="width: 20px"></td>
                            <td style="width: 300px">
                                <p class="pparam">
                                    <asp:CheckBox ID="jenisCheck" runat="server" Text="<b>Jenis</b><b style='margin-left:20px'>:</b>" Checked="True"
                                        AutoPostBack="True" OnCheckedChanged="jenisCheck_CheckedChanged"></asp:CheckBox>
                                    <asp:Label ID="jenisc" runat="server" CssClass="err"></asp:Label>
                                </p>
                                <br />
                                <asp:CheckBoxList ID="jenis" runat="server" CssClass="igroup-checkbox">
                                </asp:CheckBoxList>
                                <br />
                                <table>
                                    <tr style="display: none">
                                        <td><b>Perusahaan</b></td>
                                        <td><b>:</b></td>
                                        <td>
                                            <asp:DropDownList ID="pers" runat="server">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="xls" runat="server" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click">
											Download Excel
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											Download PDF
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
            <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Tower</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Available</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Sold</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Hold Internal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" BackColor="#1E90FF" ForeColor="White">Total</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
