<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.LaporanAkad" CodeFile="LaporanAkad.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Akad</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Akad">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <div class="underline" runat="server">
                        <p class="comp" id="comp" runat="server"></p>
                        <h1 id="judul" runat="server" class="title">Laporan Akad
                        </h1>
                    </div>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 130px; font-size:10pt;font-weight:bold">Perusahaan<b style="padding-left: 47px;">:</b></p>
                                <asp:DropDownList ID="pers" runat="server" CssClass="ddl" Font-Size="12px" Width="200" Style="margin-left: 40px" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 130px; font-size:10pt;font-weight:bold">Project<b style="padding-left: 78px;">:</b></p>
                                <asp:DropDownList ID="project" runat="server" CssClass="ddl" Font-Size="12px" Width="200" Style="margin-left: 40px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 150px; font-size:10pt;font-weight:bold">Lokasi<b style="padding-left:85px">:</b></p>
                                <asp:ListBox ID="lokasi" runat="server" Width="200" CssClass="ddl" Rows="10" style="margin-left:20px">
                                    <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                </asp:ListBox>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 150px; font-size:10pt;font-weight:bold"">View By<b style="padding-left:75px;">:</b></p>
                                <asp:RadioButton ID="tbTgl" runat="server" Checked="True" GroupName="tgl" Font-Size="9"
                                    Font-Bold="True" Text="Tanggal Akad" style="margin-left:20px;padding-right:40px"></asp:RadioButton>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <table>
                                <tr>
                                    <td style="float: none; width: 30px;">
                                        <p class="lbl" style=""></p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <p class="lbl" style="margin-left:30px;font-size:12px">Dari</p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <asp:TextBox ID="dari" runat="server" type="text" style="margin-left:-70px"></asp:TextBox>
                                        <label for="dari" class="btn-a btn-cal" style="height:100%"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <p class="lbl" style="margin-left:10px;font-size:12px">Sampai</p>
                                    </td>
                                    <td style="float: none; min-width: 0px;">
                                        <asp:TextBox ID="sampai" runat="server" type="text" style="margin-left:-50px"></asp:TextBox>
                                        <label for="sampai" class="btn-a btn-cal" style="height:100%"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 150px; font-size:10pt;font-weight:bold">Rekening Bank<b style="padding-left: 29px;">:</b></p>
                                <asp:ListBox ID="rekening" runat="server" Rows="10" CssClass="ddl" Width="250" Style="margin-left: 20px">
                                    <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                </asp:ListBox>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 150px;font-size:10pt;font-weight:bold">Status FOBO<b style="padding-left:45px;">:</b></p>
                                    <asp:RadioButton ID="statusS" runat="server" Text="SEMUA" GroupName="status"
                                        Checked="True" style="margin-left:20px;padding-right:20px; font-size:9pt"></asp:RadioButton>
                                <asp:RadioButton ID="statusB" runat="server" GroupName="status" style="margin-left:50px;padding-right:20px; font-size:9pt"
                                            Text="BELUM"></asp:RadioButton>
                                <asp:RadioButton ID="statusA" runat="server"
                                                GroupName="status" Text="SUDAH" style="margin-left:50px;padding-right:20px; font-size:9pt"></asp:RadioButton>
                                </div>
                            </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl" style="width: 150px; font-size:10pt;font-weight:bold">Status AKAD<b style="padding-left:45px;">:</b></p>
                                    <asp:RadioButton ID="S" runat="server" Text="SEMUA" GroupName="status2"
                                        Checked="True" style="margin-left:20px;padding-right:20px; font-size:9pt"></asp:RadioButton>
                                <asp:RadioButton ID="B" runat="server" GroupName="status2"
                                            Text="BELUM" style="margin-left:50px;padding-right:20px; font-size:9pt"></asp:RadioButton>
                                <asp:RadioButton ID="D" runat="server"
                                                GroupName="status2" Text="SUDAH" style="margin-left:50px;padding-right:20px; font-size:9pt"></asp:RadioButton>
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
                        </div>
                </td>
            </tr>
        </table>
        <div id="headReport" runat="server"></div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">#</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Akad</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Tgl. Akad</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">NPWP</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Alamat</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Agent</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" BackColor="#1E90FF" ForeColor="White">Luas</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Jenis</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Bank KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Harga Jual Awal</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Diskon Harga Jual</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Nilai Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" BackColor="#1E90FF" ForeColor="White">Potensi KPR</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="left" BackColor="#1E90FF" ForeColor="White">Realisasi Akad</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
