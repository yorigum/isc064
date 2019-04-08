<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.StokBG" CodeFile="StokBG.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Stok Cek Giro</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Stok Cek Giro">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Stok Cek Giro
                    </h1>
                    <div class="form-model">
                    <div class="form-inline col">
                        <div class="pparam">
                            <asp:Label ID="Label2" runat="server" Font-Size="12"><b>Perusahaan</b></asp:Label>
                        </div>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <br />
                                        <asp:DropDownList ID="pers" runat="server" Width="175" Style="margin-left: 22px" OnSelectedIndexChanged="pers_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="form-inline col">
                        <div class="pparam">
                            <asp:Label ID="Label1" runat="server" Font-Size="12"><b>Project</b></asp:Label>
                        </div>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <br />
                                        <asp:DropDownList ID="project" runat="server" Width="175" Style="margin-left: 60px">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right: 25px">Status</p>
                                <div class="item">
                                    <asp:DropDownList ID="metode" runat="server" Font-Size="12px">
                                        <asp:ListItem>Semua</asp:ListItem>
                                        <asp:ListItem Value="1">BG Normal</asp:ListItem>
                                        <asp:ListItem Value="2">BG Normal, Belum Cair</asp:ListItem>
                                        <asp:ListItem Value="3">BG Normal, Sudah Cair</asp:ListItem>
                                        <asp:ListItem Value="4">BG Bermasalah</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right: 25px">Tahun</p>
                                <div class="item">
                                    <asp:DropDownList ID="tahun" runat="server" Width="110" Font-Size="12px"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right: 20px">Perhitungan</p>
                                <div class="item">
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton Checked="True" ID="kuantitas" runat="server" GroupName="p" Text="KUANTITAS"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rupiah" runat="server" GroupName="p" Text="RUPIAH"></asp:RadioButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br>
                    <div class="form-inline col pparam sub">
                        <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" Text="Screen Preview" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>
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
            <asp:TableRow CssClass="tb blue-skin">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Bulan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Total</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">1</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">2</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">3</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">4</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">5</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">6</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">7</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">8</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">9</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">10</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">11</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">12</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">13</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">14</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">15</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">16</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">17</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">18</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">19</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">20</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">21</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">22</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">23</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">24</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">25</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">26</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">27</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">28</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">29</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">30</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Width="50" BackColor="#1E90FF" ForeColor="White">31</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
