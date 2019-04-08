<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.LaporanCheckListBerkas" CodeFile="LaporanCheckListBerkas.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Checklist Berkas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Checklist Berkas">
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
                        <h1 id="judul" runat="server" class="title">Laporan Checklist Berkas
                        </h1>
                    </div>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 125px; font-size: 10pt; font-weight: bold">Perusahaan<b style="margin-left: 24px">:</b></p>
                                <asp:DropDownList ID="pers" runat="server" CssClass="" Width="200" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 125px; font-size: 10pt; font-weight: bold">Project<b style="margin-left: 55px">:</b></p>
                                <asp:DropDownList ID="project" runat="server" CssClass="" Width="200" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 130px; font-size: 10pt; font-weight: bold">Lokasi<b style="padding-left: 60px">:</b></p>
                                <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10">
                                    <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                </asp:ListBox>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 130px; font-size: 10pt; font-weight: bold">View By<b style="padding-left: 50px">:</b></p>
                                <asp:RadioButton ID="tbKontrak" runat="server" repeatdirection="vertical" Text="Tanggal Kontrak" Font-Bold="True"
                                    Font-Size="9" GroupName="tgl" Checked="True" Style="padding-right: 20px"></asp:RadioButton>
                                <asp:RadioButton ID="tbBerkas" runat="server" repeatdirection="vertical" Text="Tanggal Selesai Berkas" Font-Bold="True"
                                    Font-Size="9" GroupName="tgl" Style="padding-right: 40px; margin-right: 20px"></asp:RadioButton>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <table>
                                <tr>
                                    <td style="float: none; width: 30px;">
                                        <p class="lbl" style=""></p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <p class="lbl" style="margin-left: -10px; font-size: 12px">Dari</p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <asp:TextBox ID="dari" runat="server" type="text" Style="margin-left: -70px"></asp:TextBox>
                                        <label for="dari" class="btn-a btn-cal" style="height: 100%"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <p class="lbl" style="margin-left: 10px; font-size: 12px">Sampai</p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <asp:TextBox ID="sampai" runat="server" type="text" Style="margin-left: -50px"></asp:TextBox>
                                        <label for="sampai" class="btn-a btn-cal" style="height: 100%"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 125px; font-size: 10pt; font-weight: bold">Status Berkas <b style="padding-left: 10px">:</b></p>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="" Width="200" Font-Size="12px">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                    <asp:ListItem Value="0">BELUM LENGKAP</asp:ListItem>
                                    <asp:ListItem Value="1">SUDAH LENGKAP</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                    </div>
                    <br />
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
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">#</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Bank KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Status Berkas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl Selesai Berkas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Checklist Dokumen</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
