<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.LaporanOTS" CodeFile="LaporanOTS.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan OTS</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan OTS">
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
                        <h1 id="judul" runat="server" class="title">Laporan OTS
                        </h1>
                    </div>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 100px; font-size: 10pt; font-weight: bold">Perusahaan<b style="padding-left: 38px;">:</b></p>
                                <asp:DropDownList ID="pers" runat="server" Font-Size="12px" CssClass="select-dropdown" Width="200" Style="margin-left: 50px" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-inline col">
                                <div class="pparam">
                                    <p class="lbl" style="width: 100px; font-size: 10pt; font-weight: bold">Project<b style="padding-left: 70px;">:</b></p>
                                    <asp:DropDownList ID="project" runat="server" Font-Size="12px" CssClass="select-dropdown" Width="200" Style="margin-left: 50px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                        <asp:ListItem>SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-inline col">
                                    <div class="pparam">
                                        <p class="lbl" style="width: 150px; font-size: 10pt; font-weight: bold">Lokasi<b style="padding-left: 65px">:</b></p>
                                        <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10">
                                            <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                        </asp:ListBox>
                                    </div>
                                </div>
                                <div class="form-inline col">
                                    <div class="pparam">
                                        <p class="lbl" style="width: 150px; font-size: 10pt; font-weight: bold">View By<b style="padding-left: 62px;">:</b></p>
                                        <asp:RadioButton ID="tbTarget" runat="server" Text="Target OTS" Font-Bold="True"
                                            Font-Size="9" GroupName="tgl" Checked="True" Style="padding-right: 44px"></asp:RadioButton>
                                        <asp:RadioButton ID="tbTgl" runat="server" Text="Tanggal Hasil OTS" Font-Bold="True"
                                            Font-Size="9" GroupName="tgl" Style="padding-right: 20px"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="form-inline col">
                                    <table>
                                        <tr>
                                            <td style="float: none; width: 30px;">
                                                <p class="lbl" style=""></p>
                                            </td>
                                            <td style="float: none; min-width: 0px;">
                                                <p class="lbl" style="margin-left: 10px; font-size: 12px">Dari</p>
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
                                <div class="form-inline col">
                                    <div class="pparam">
                                        <p class="lbl" style="width: 120px; font-size: 10pt; font-weight: bold">Status OTS<b style="padding-left: 45px;">:</b></p>
                                        <asp:DropDownList ID="ddlStatus" runat="server" Font-Size="12px" CssClass="select-dropdown" Width="200">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                            <asp:ListItem>BELUM DITENTUKAN</asp:ListItem>
                                            <asp:ListItem>DIJADWALKAN</asp:ListItem>
                                            <asp:ListItem>SELESAI</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-inline col" style="margin-bottom: 5px">
                                    <div class="pparam">
                                        <p class="lbl" style="width: 120px; font-size: 10pt; font-weight: bold">Hasil OTS<b style="padding-left: 53px;">:</b></p>
                                        <asp:DropDownList ID="ddlHasil" runat="server" Font-Size="12px" CssClass="select-dropdown" Width="200">
                                            <asp:ListItem>SEMUA</asp:ListItem>
                                            <asp:ListItem>MENUNGGU</asp:ListItem>
                                            <asp:ListItem>TOLAK</asp:ListItem>
                                            <asp:ListItem>SETUJU</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-inline col">
                                    <div class="pparam">

                                        <p class="lbl" style="width: 150px; font-size: 10pt; font-weight: bold">Rekening Bank<b style="padding-left: 20px;">:</b></p>
                                        <asp:ListBox ID="rekening" runat="server" Width="250" CssClass="ddl" Rows="10">
                                            <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                        </asp:ListBox>
                                    </div>
                                </div>
                                <br />
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
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Status OTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Target OTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. Hasil OTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Hasil SP3K</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Potensi KPR</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
