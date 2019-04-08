<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.LaporanOutstandingTagihan" CodeFile="OutstandingTagihan.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Outstanding Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Outstanding Tagihan">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Outstanding Tagihan
                    </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label2" runat="server"><b>Perusahaan</b><b style="padding-left:11px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="pers" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label1" runat="server"><b>Project</b><b style="padding-left:48px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="project" runat="server" Width="200" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right: 0px">
                                    Lokasi<b style="margin-left: 52px; vertical-align: top">:</b>
                                </p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10" style="margin-left:12px">
                                                    <asp:ListItem>SEMUA</asp:ListItem>
                                                </asp:ListBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right: 10px">
                                    Status<b style="padding-left: 52px">:</b>
                                </p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status" Style="margin-left:4px;padding-right: 50px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" Style="padding-right: 50px" Checked="True"></asp:RadioButton>
                                                <asp:RadioButton ID="statusB" Text="BATAL" runat="server" GroupName="status" Style="padding-right: 50px"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="padding-right: 10px">Tanggal<b style="padding-left: 40px">:</b></p>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="dari" runat="server" type="text" CssClass="txt_center" style="margin-left:4px"></asp:TextBox>
                                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                            <td>
                                                <b style="padding-right: 10px; padding-left: 10px">s/d</b>
                                                <asp:TextBox ID="sampai" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col pparam sub">
                            <div class="ins">
                                <table>
                                    <tr>
                                        <td style="min-width: auto; padding-right: 10px">
                                            <asp:LinkButton ID="scr" AccessKey="s" runat="server" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Middle">
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Surat Pesanan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl Surat Pesanan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">No.Telp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">No.Hp</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Sales</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Nett Price</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tagihan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl Jatuh Tempo</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="3" Wrap="false" BackColor="#1E90FF" ForeColor="White">Billing</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" RowSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl Lunas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" ColumnSpan="2" Wrap="false" BackColor="#1E90FF" ForeColor="White">Settlement</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="total1" runat="server" Visible="False" BackColor="#1E90FF" ForeColor="White">0</asp:Label>
        <asp:Label ID="total2" runat="server" Visible="False" BackColor="#1E90FF" ForeColor="White">0</asp:Label>
        <asp:Label ID="total3" runat="server" Visible="False" BackColor="#1E90FF" ForeColor="White">0</asp:Label>
        <asp:Label ID="total4" runat="server" Visible="False" BackColor="#1E90FF" ForeColor="White">0</asp:Label>
        <asp:Label ID="total5" runat="server" Visible="False" BackColor="#1E90FF" ForeColor="White">0</asp:Label>
    </form>
</body>
</html>
