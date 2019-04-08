<%@ Reference Page="~/SecLevel.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.Laporan.TabelAbsensi" CodeFile="TabelAbsensi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html >
<html>
<head>
    <title>Laporan Tabel Absensi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Tabel Absensi">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==13)document.getElementById('scr').click();if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td colspan="2">
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan Tabel Absensi
                    </h1>
                </td>
            </tr>
            <tr>
                <td style="width: 50px">
                    <p class="report-title">Tanggal Masuk</p>
                    <div class="formgroup">
                        <label class="formlabel txt_center" style="width: 75px">Dari</label>
                        <asp:TextBox ID="dari" runat="server" CssClass="txt_center igroup" Width="150"></asp:TextBox>
                        <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                    </div>
                    <br>
                    <div class="formgroup">
                        <label class="formlabel txt_center" style="width: 75px">Sampai</label>
                        <asp:TextBox ID="sampai" runat="server" CssClass="txt_center igroup" Width="150"></asp:TextBox>
                        <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                    </div>
                </td>
                <td style="width: 50%"></td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <p class="pparam">
                        <b>User :</b>
                        <br>
                        <asp:ListBox ID="user" runat="server" CssClass="ddl" Width="300" Rows="18">
                            <asp:ListItem>SEMUA</asp:ListItem>
                        </asp:ListBox>
                    </p>
                </td>
                <td style="width: 50%; vertical-align: top;">
                    <p class="pparam">
                        <b>Security Level :</b>
                        <br>
                        <asp:ListBox ID="seclevel" runat="server" CssClass="ddl" Width="150" Rows="10">
                            <asp:ListItem>SEMUA</asp:ListItem>
                        </asp:ListBox>
                    </p>
                    <p class="pparam">
                        <b>IP Address :</b>
                        <br>
                        <asp:ListBox ID="ip" runat="server" CssClass="ddl" Width="150" Rows="10">
                            <asp:ListItem>SEMUA</asp:ListItem>
                        </asp:ListBox>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2">
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
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">ID</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="150px" BackColor="#1E90FF" ForeColor="White">Tgl. Jam Masuk</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="150px" BackColor="#1E90FF" ForeColor="White">Tgl. Jam Keluar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="75px" BackColor="#1E90FF" ForeColor="White">Kode User</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="200px" BackColor="#1E90FF" ForeColor="White">Nama User</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="75px" BackColor="#1E90FF" ForeColor="White">Security Level</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">IP Address</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
