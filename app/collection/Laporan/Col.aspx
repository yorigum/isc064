<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.Col" CodeFile="Col.aspx.cs" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Tagihan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Proyeksi Penerimaan">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Proyeksi Penerimaan
                    </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status</p>
                                <table>
                                    <tbody>
                                        <tr>                                            
                                            <td>
                                                <b style="padding-left:35px; padding-right:10px;font-size:14px">:</b>
                                                <asp:RadioButton ID="statusS" Text="SEMUA" runat="server" GroupName="status" style="padding-right:30px"></asp:RadioButton>
                                                <asp:RadioButton ID="statusA" Text="AKTIF" runat="server" GroupName="status" style="padding-right:30px" Checked="True"></asp:RadioButton>
                                                <asp:RadioButton ID="statusB" Text="BATAL" runat="server" GroupName="status" style="padding-right:30px"></asp:RadioButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <table>
                                    <tbody>
                                        <tr>                                            
                                            <td>
                                                <b style="font-size:16px">Tgl. Jatuh Tempo :</b>
                                            </td>
                                            <td>
                                                <span style="padding-left:10px;font-size:14px">Dari</span>
                                                <asp:TextBox ID="dari" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                            <td>
                                                <span style="padding-left:10px;font-size:14px">Sampai</span>
                                                <asp:TextBox ID="sampai" runat="server" type="text" CssClass="txt_center"></asp:TextBox>
                                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                                <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <asp:Label ID="Label2" runat="server"><b>Perusahaan</b><b style="padding-left:45px;font-size:12px">:</b></asp:Label>
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
                                <asp:Label ID="Label1" runat="server"><b>Project</b><b style="padding-left:82px;font-size:12px">:</b></asp:Label>
                            </div>
                            <table>
                                <tbody>
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:DropDownList ID="project" runat="server" Width="200">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
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
        <asp:Label ID="headJudul" runat="server"></asp:Label>
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow BackColor="LightGray">
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">No.</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">No. Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Harga<br />Pokok</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">PPN 10%</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Total<br />Harga</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Cara<br />Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">DP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" BackColor="#1E90FF" ForeColor="White">Rencana Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Total<br />Pembayaran</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" RowSpan="3" BackColor="#1E90FF" ForeColor="White">Sisa<br />Pembayaran</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
