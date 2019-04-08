<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.LaporanDetilPembayaran" CodeFile="LaporanDetilPembayaran.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan Pembayaran Detail</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Pembayaran Detail">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Detail Pembayaran
                    </h1>
                    <asp:RadioButton ID="tglinput" runat="server" Text="Tanggal Kuitansi" Font-Bold="True" Font-Size="14px"
                        GroupName="tgl" Checked="True"></asp:RadioButton>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="font-size: 12px; margin-top: 10px">Dari</p>
                                <div class="item">
                                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: -50px;">
                                        <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 55%; height: 20px; font-size: 12px"></asp:TextBox>
                                        <span class="input-group-btn" style="height: 34px; display: block">
                                            <label for="dari" class="btn-a btn-cal"><i class="fa fa-calendar"></i></label>
                                        </span>
                                    </div>
                                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                </div>
                                <p class="lbl" style="font-size: 12px; margin-top: 10px">Sampai</p>
                                <div class="item">
                                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: -30px;">
                                        <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 55%; height: 20px; font-size: 12px"></asp:TextBox>
                                        <span class="input-group-btn" style="height: 34px; display: block">
                                            <label for="sampai" class="btn-a btn-cal"><i class="fa fa-calendar"></i></label>
                                        </span>
                                    </div>
                                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <table style="margin-left:5px">
                        <tr>
                            <td><b>Perusahaan</b></td>
                            <td>
                                <asp:DropDownList ID="pers" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Project</b></td>
                            <td>
                                <asp:DropDownList ID="project" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Lokasi</b></td>
                            <td>
                                <asp:DropDownList ID="lokasi" runat="server" Width="175">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
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
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">NO</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">KONTRAK</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">UNIT</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">CUSTOMER</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">BF</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">BANK (BF)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">DP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">BANK (DP)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">ANG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">BANK (ANG)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">ADM</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">BANK (ADM)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">SALDO AWAL</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">MEMO</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">AKUMULASI PEMBAYARAN</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">AKUMULASI PEMBAYARAN (SALDO AWAL)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">AKUMULASI PEMBAYARAN (MEMO)</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">HARGA JUAL INC PPN</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Center" Wrap="false" BackColor="#1E90FF" ForeColor="White">%</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
