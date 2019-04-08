<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.SummaryPotensiKPA" CodeFile="SummaryPotensiKPA.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Summary Potensi KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content=" Laporan Summary Potensi KPR">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <div class="underline" runat="server">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title">Laporan Summary Potensi KPR
                        </h1>
                    </div>
                    <div class="form-model">
                        <div class="">
                                <p class="lbl" style="font-size:10pt;font-weight:bold">Perusahaan <b style="margin-left:10px">:</b></p>
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="pers" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="">
                                <p class="lbl" style="font-size:10pt;font-weight:bold">Project <b style="margin-left:40px">:</b></p>
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="project" runat="server" Width="175">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="">
                                <p class="lbl" style="font-size:10pt;vertical-align:middle">Periode <b style="margin-left:36px">:</b></p>
                            <table>
                                <tr>
<%--                                    <td style="min-width:30px;">dari
                                    </td>--%>
                                    <td>
                                        <asp:DropDownList ID="bln" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="thn" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br /><br />
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
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Bank KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Potensi KPR (Rp)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Potensi KPR (Unit)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">SP3K Terbit (Rp)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">SP3K Terbit (Unit)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Realisasi Akad (Rp)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Realisasi Akad (Unit)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">SP3K Terbit Belum Akad (Rp)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">SP3K Terbit Belum Akad (Unit)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">KPR Ditolak (Rp)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">KPR Ditolak (Unit)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Batal (Rp)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Batal (Unit)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Sisa Potensi di Bank (Rp)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Sisa Potensi di Bank (Unit)</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
