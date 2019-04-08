<%@ Reference Page="~/SecLevel.aspx" %>

<%@ Page Language="c#" Inherits="ISC064.SECURITY.Laporan.SecurityLog" CodeFile="SecurityLog.aspx.cs" Debug="true"%>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Security Log</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Security Log">
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
                    <h1 id="judul" runat="server" class="title title-line">Laporan Security Log
                    </h1>
                </td>
            </tr>
            <tr>
                <td style="width: 50%" valign="top">
                    <p class="report-title">Tanggal Log</p>
                    <div class="formgroup" style="width: 75%">
                        <label class="formlabel txt_center">Dari</label>
                        <asp:TextBox ID="dari" runat="server" CssClass="txt_center igroup" Width="150"></asp:TextBox>
                        <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                    </div>
                    <div class="formgroup" style="width: 75%">
                        <label class="formlabel txt_center">Sampai</label>
                        <asp:TextBox ID="sampai" runat="server" CssClass="txt_center igroup" Width="150"></asp:TextBox>
                        <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                    </div>
                </td>
                <td style="width: 50%">
                    <p class="report-title">User</p>
                    <asp:ListBox ID="user" runat="server" CssClass="ddl" Width="300" Rows="12">
                        <asp:ListItem>SEMUA</asp:ListItem>
                    </asp:ListBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <p class="pparam">
                        <asp:CheckBox ID="aktivitasCheck" runat="server" Text="<b>Aktivitas :</b>" AutoPostBack="True"
                            Checked="True" OnCheckedChanged="aktivitasCheck_CheckedChanged"></asp:CheckBox>
                        <asp:Label ID="aktivitasc" runat="server" CssClass="err"></asp:Label>
                    </p>
                    <asp:CheckBoxList ID="aktivitas" runat="server">
                        <asp:ListItem Selected="True" Value="L">L = Log-In Normal</asp:ListItem>
                        <asp:ListItem Selected="True" Value="S">S = Sign-Out Normal</asp:ListItem>
                        <asp:ListItem Selected="True" Value="DL">DL = Double Login</asp:ListItem>
                        <asp:ListItem Selected="True" Value="SP">SP = Salah Password</asp:ListItem>
                        <asp:ListItem Selected="True" Value="B">B = Blokir</asp:ListItem>
                        <asp:ListItem Selected="True" Value="A">A = Aktivasi</asp:ListItem>
                        <asp:ListItem Selected="True" Value="GP">GP = Ganti Password</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td style="width: 50%">
                    <div style="width: auto; float: left; padding: 5px;">
                        <b>Security Level :</b>
                        <br>
                        <asp:ListBox ID="seclevel" runat="server" CssClass="ddl" Width="150" Rows="10">
                            <asp:ListItem>SEMUA</asp:ListItem>
                        </asp:ListBox>
                    </div>
                    <div style="width: auto; float: left; padding: 5px;">
                        <b>IP Address :</b>
                        <br>
                        <asp:ListBox ID="ip" runat="server" CssClass="ddl" Width="150" Rows="10">
                            <asp:ListItem>SEMUA</asp:ListItem>
                        </asp:ListBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td class="btn-holder">
                                    <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											<i class="fa fa-search"></i> Screen preview
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="xls" runat="server" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click">
											Download Excel
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											Download PDF
                                    </asp:LinkButton>
                                </td>
                                <style type="text/css">
                                    .btn-holder > a {
                                        text-decoration: none;
                                    }
                                </style>
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
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="75" BackColor="#1E90FF" ForeColor="White">Tgl. Log</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="75" BackColor="#1E90FF" ForeColor="White">Jam Log</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="200" BackColor="#1E90FF" ForeColor="White">Aktivitas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="75" BackColor="#1E90FF" ForeColor="White">Kode User</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="200" BackColor="#1E90FF" ForeColor="White">Nama User</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" Width="75" BackColor="#1E90FF" ForeColor="White">Security Level</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">IP Address</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
