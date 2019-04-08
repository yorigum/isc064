<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.MasterTTS" CodeFile="MasterTTS.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan Master Tanda Terima Sementara</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Master Tanda Terima Sementara">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==13)document.getElementById('scr').click();if(event.keyCode==27&&confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div id="tab1" runat="server">
            <div style="display: none">
                <uc1:Head ID="Head1" runat="server"></uc1:Head>
            </div>
            <table id="param" runat="server" width="100%" cellspacing="3">
                <tr>
                    <td>
                        <div class="underline">
                            <p class="comp" id="comp" runat="server"></p>
                            <h1 id="judul" runat="server" class="title">Laporan Master Tanda Terima Sementara
                            </h1>
                        </div>
                        <div class="form-model">
                            <div class="form-inline col" style="display:none">
                                <div class="pparam">
                                    <asp:Label ID="Label2" runat="server"><b>Perusahaan</b></asp:Label>
                                </div>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <br />
                                                <asp:DropDownList ID="pers" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="pers_SelectedIndexChanged">
                                                    <asp:ListItem>SEMUA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="form-inline col">
                                <div class="pparam">
                                    <asp:Label ID="Label1" runat="server"><b>Project</b></asp:Label>
                                </div>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <br />
                                                <asp:DropDownList ID="project" runat="server" Width="175" Style="margin-left: 38px" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">SEMUA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="form-inline col">
                                <div class="pparam">
                                    <p class="lbl">Status</p>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="statusS" runat="server" GroupName="status" Text="SEMUA" Checked="True"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="statusB" runat="server" GroupName="status" Text="BARU"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="statusP" runat="server" GroupName="status" Text="POST"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="statusV" runat="server" GroupName="status" Text="VOID"></asp:RadioButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-inline col">
                                <div class="pparam">
                                    <p class="lbl">Alokasi</p>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="alokasiS" runat="server" GroupName="alokasi" Text="SEMUA" Checked="True"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="alokasiYes" runat="server" GroupName="alokasi" Text="TERALOKASI"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="alokasiNo" runat="server" GroupName="alokasi" Text="TIDAK TERALOKASI"></asp:RadioButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-inline col" style="display:none">
                                <div class="pparam">
                                    <p class="lbl">Tipe</p>
                                    <div class="item">
                                        <asp:Label ID="tipec" runat="server" CssClass="err"></asp:Label>
                                        <asp:CheckBox ID="tipeCheck" runat="server" Text="SEMUA" Font-Size="14px" Checked="True" AutoPostBack="True" OnCheckedChanged="tipeCheck_CheckedChanged"></asp:CheckBox>
                                    </div>
                                </div>
                                <br />
                                <asp:CheckBoxList ID="tipe" runat="server"></asp:CheckBoxList>
                            </div>
                            <div class="form-inline col">
                                <div class="pparam">
                                    <p class="lbl">Cara Bayar</p>
                                    <div class="item">
                                        <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label>
                                        <asp:CheckBox ID="carabayarCheck" runat="server" Text="SEMUA" Font-Size="14px" Checked="True" AutoPostBack="True" OnCheckedChanged="carabayarCheck_CheckedChanged"></asp:CheckBox>
                                    </div>
                                </div>
                                <br />
                                <asp:CheckBoxList ID="carabayar" runat="server" RepeatColumns="7">
                                    <asp:ListItem Selected="True" Value="TN">Tunai</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="KK">Kartu Kredit</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="KD">Kartu Debit</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="TR">Transfer Bank</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="BG">Cek Giro</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="UJ">Uang Jaminan</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="DN">Diskon</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                            <div class="form-inline col">
                                <div class="pparam">
                                    <p class="lbl">Tanggal</p>
                                    <div class="item">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton ID="tgltts" runat="server" Text="Tanggal TTS" Font-Bold="True"
                                                            GroupName="tgl" Checked="True"></asp:RadioButton>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="tglinput" runat="server" Text="Tanggal Input" Font-Bold="True"
                                                            GroupName="tgl"></asp:RadioButton>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="tglbkm" runat="server" Text="Tanggal BKM" Font-Bold="True"
                                                            GroupName="tgl"></asp:RadioButton>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="tglbg" runat="server" Text="Tanggal BG" Font-Bold="True"
                                                            GroupName="tgl"></asp:RadioButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="form-inline col" style="display:none">
                                <div class="pparam">
                                    <p class="lbl">Lokasi</p>
                                    <div class="item" style="font-size: 12px">
                                        <asp:DropDownList ID="ddlLokasi" runat="server" Width="175" Style="margin-left: 4px">
                                            <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-inline col">
                                <div class="pparam">
                                    <p class="lbl">Dari</p>
                                    <div class="item">
                                        <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                            <asp:TextBox ID="dari" runat="server" type="text" CssClass="form-control" Style="width: 55%; height: 20px; font-size: 12px"></asp:TextBox>
                                            <span class="input-group-btn" style="height: 34px; display: block">
                                                <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            </span>
                                        </div>
                                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                    </div>
                                    <p class="lbl">Sampai</p>
                                    <div class="item" style="font-size: 12px">
                                        <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
                                            <asp:TextBox ID="sampai" runat="server" type="text" CssClass="form-control" Style="width: 55%; height: 20px; font-size: 12px"></asp:TextBox>
                                            <span class="input-group-btn" style="height: 34px; display: block">
                                                <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                            </span>
                                        </div>
                                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div>
                            <p class="pparam">
                                <b>Kasir :</b>
                                <br />
                                <asp:ListBox ID="kasir" runat="server" CssClass="ddl" Width="300" Rows="12">
                                    <asp:ListItem>SEMUA</asp:ListItem>
                                </asp:ListBox>
                            </p>
                            <p class="pparam">
                                <b>Rekening Bank :</b>
                                <br />
                                <asp:ListBox ID="lbAcc" runat="server" CssClass="ddl" Width="250" Rows="12">
                                    <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                </asp:ListBox>
                                <br />
                                <asp:CheckBox ID="detil" runat="server" Text="Tampilkan detil alokasi pembayaran"></asp:CheckBox>
                            </p>
                        </div>
                        <br>
                        <div class="form-inline col pparam sub">
                            <div class="ins">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="scr" AccessKey="s" runat="server" Text="Screen Preview" CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                            <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel" CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                            <%--<asp:Button ID="pdf" runat="server" Text="Download PDF" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click"></asp:Button>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>

        </div>
        <div id="headReport" runat="server">
        </div>
        <br />
        <asp:Table ID="rpt" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow VerticalAlign="Bottom">
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="true" BackColor="#1E90FF" ForeColor="White">No. </asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. TTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. KWT</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Status</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. TTS</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. KWT</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Kasir</asp:TableHeaderCell>
                <asp:TableHeaderCell Wrap="false" BackColor="#1E90FF" ForeColor="White">Tipe</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. Kontrak</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Unit</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Customer</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Cara Bayar</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Keterangan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">No. BG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Tgl. BG</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Rekening</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">Bank</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pelunasan Piutang</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Pembulatan</asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Right" Wrap="false" BackColor="#1E90FF" ForeColor="White">Total Pembayaran</asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>

</body>
</html>
