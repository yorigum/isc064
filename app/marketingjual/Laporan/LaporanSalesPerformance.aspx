<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Laporan.LaporanSalesPerformance"
    CodeFile="LaporanSalesPerformance.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Sales Performance</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Sales Performance">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Sales Performance
                    </h1>
                    <br />
                    <div style="">
                        <b>Project :</b>
                        <asp:DropDownList ID="project" runat="server" CssClass="ddl" Width="200" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div style="">
                        <b>Sales <b style="margin-left:10px">:</b></b>
                        <asp:DropDownList ID="ddlAgent" runat="server" CssClass="ddl" Width="200">
                            <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <br />
                    <div>
                        <b>Status Sales:</b>
                        <asp:RadioButtonList ID="status" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="A">Aktif</asp:ListItem>
                            <asp:ListItem Value="I">Inaktif</asp:ListItem>
                            <asp:ListItem Value="S" Selected="True">Semua</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <br />
                    <div>
                        <p class="pparam" style="display: none;">
                            <b>As of :</b>
                            <br />
                            <asp:TextBox ID="tgl" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                            <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:Label ID="tglc" runat="server" CssClass="err"></asp:Label>
                        </p>
                        <b>Range : </b>
                        <br />
                        <asp:RadioButtonList ID="filtertgl" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                            OnSelectedIndexChanged="filtertgl_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True">Range</asp:ListItem>
                            <asp:ListItem Value="1">As Of</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div>
                        <p id="range" runat="server">
                            <table>
                                <tr>
                                    <td>dari
                                    </td>
                                    <td>
                                        <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                        <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                    </td>
                                    <td rowspan="2">&nbsp;&nbsp;
                                    </td>
                                    <td>sampai
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                        <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="periodec" runat="server" CssClass="err"></asp:Label>
                        </p>
                        <p id="asof" runat="server" visible="false">
                            As Of :
                            <asp:TextBox ID="tglasof" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                            <label for="tglasof" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                            <asp:Label ID="asofc" runat="server" CssClass="err"></asp:Label>
                        </p>
                        <p class="pparam" style="display: none;">
                            <b>Perhitungan :</b>
                            <br>
                            <asp:RadioButton Checked="True" ID="kuantitas" runat="server" GroupName="p" Text="KUANTITAS"
                                Font-Size="14"></asp:RadioButton>
                            <asp:RadioButton ID="rupiah" runat="server" GroupName="p" Text="RUPIAH" Font-Size="14"></asp:RadioButton>
                        </p>
                    </div>
                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click">
                                    <i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e"
                                        runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
                                    </asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server">
            <asp:Label ID="lblHeader" runat="server" Font-Size="12pt" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="lblSubHeader" runat="server" Font-Size="9pt" Font-Bold="True"></asp:Label>
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="3" BackColor="#1E90FF" ForeColor="white">No. Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="3" BackColor="#1E90FF" ForeColor="white">Nama Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="3" BackColor="#1E90FF" ForeColor="white">Principal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="3" BackColor="#1E90FF" ForeColor="white">Project</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Jan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Feb</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Mar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Apr</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">May</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Jun</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Jul</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Aug</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Sep</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Oct</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Nov</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" ColumnSpan="4" BackColor="#1E90FF" ForeColor="white">Dec</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="2" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="2" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Batal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" RowSpan="2" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Grand Total</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Target</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    ColumnSpan="2">Riil</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white"
                    RowSpan="2">Persentase</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Kuantitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="center" BackColor="#1E90FF" ForeColor="white">Rupiah</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <%--    <asp:Chart ID="Chart1" runat="server">
        <Series>
            <asp:Series Name="Series1">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>--%>
        <%--</div>--%>
    </form>
</body>
</html>
