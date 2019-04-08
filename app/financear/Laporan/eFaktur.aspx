<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Laporan.eFaktur" CodeFile="eFaktur.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html >
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
    <meta name="sec" content="Laporan e-Faktur">
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
                        <h1 id="judul" class="title title-line" runat="server">Laporan e-Faktur
                        </h1>
                    <div class="form-model">
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Perusahaan</p>
                                <div class="item">
                                    <asp:DropDownList ID="pers" Font-Size="12px" runat="server" Width="175" OnSelectedIndexChanged="pers_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Project</p>
                                <div class="item">
                                    <asp:DropDownList ID="project" Font-Size="12px" runat="server" Width="175" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True">SEMUA</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Status</p>
                                <asp:RadioButton ID="statusS" runat="server" GroupName="status" Font-Size="10" Text="SEMUA" Style="display: none"></asp:RadioButton>
                                <asp:RadioButton ID="statusB" runat="server" GroupName="status" Font-Size="10" Text="BARU" Style="display: none"></asp:RadioButton>
                                <asp:RadioButton Checked="True" ID="statusP" runat="server" GroupName="status" Font-Size="10"
                                    Text="POST"></asp:RadioButton>
                                <asp:RadioButton ID="statusV" runat="server" GroupName="status" Font-Size="10" Text="VOID" Style="display: none"></asp:RadioButton>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Tipe</p>
                                <div class="item">
                                    <asp:Label ID="tipec" runat="server" CssClass="err"></asp:Label>
                                    <asp:CheckBox ID="tipeCheck" runat="server" Text="Semua" Checked="True" AutoPostBack="True"
                                        OnCheckedChanged="tipeCheck_CheckedChanged"></asp:CheckBox>
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
                                    <asp:CheckBox ID="carabayarCheck" runat="server" Text="Semua" Checked="True"
                                        AutoPostBack="True" OnCheckedChanged="carabayarCheck_CheckedChanged"></asp:CheckBox>
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
                                                    <asp:RadioButton ID="tgltts" runat="server" Text="Tanggal TTS" Font-Bold="True" Font-Size="12px"
                                                        GroupName="tgl" Checked="True"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="tglinput" runat="server" Text="Tanggal Input" Font-Bold="True"
                                                        Font-Size="12px" GroupName="tgl"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="tglbkm" runat="server" Text="Tanggal BKM" Font-Bold="True" Font-Size="12px"
                                                        GroupName="tgl"></asp:RadioButton>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="tglbg" runat="server" Text="Tanggal BG" Font-Bold="True" Font-Size="12px"
                                                        GroupName="tgl"></asp:RadioButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline col">
                            <div class="pparam">
                                <p class="lbl">Lokasi</p>
                                <div class="item">
                                    <asp:DropDownList ID="ddlLokasi" Font-Size="12px" runat="server" Width="175">
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
                                            <label for="sampai" class="btn-a btn-cal"><i class="fa fa-calendar"></i></label>
                                        </span>
                                    </div>
                                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                </div>
                                <p class="lbl">Sampai</p>
                                <div class="item">
                                    <div class="input-group input-medium" style="margin-top: 0px; margin-left: 0px;">
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
                            <asp:CheckBox Visible="false" ID="detil" runat="server" Text="Tampilkan detil alokasi pembayaran"></asp:CheckBox>
                        </p>
                    </div>
                    <br />
                    <div class="form-inline col pparam sub">
                        <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" AccessKey="s" runat="server" Text="Screen Preview"
                                        CssClass="btn btn-blue" OnClick="scr_Click"><i class="fa fa-search"></i> Screen Preview</asp:LinkButton>
                                    <asp:Button ID="xls" AccessKey="e" runat="server" Text="Download Excel"
                                        CssClass="btn btn-green" OnClick="xls_Click"></asp:Button>
                                    <asp:Button ID="btncsv" AccessKey="e" runat="server" Text="Download Csv"
                                        CssClass="btn" OnClick="btncsv_Click"></asp:Button>
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
            <asp:TableRow VerticalAlign="Bottom" CssClass="tb blue-skin">
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">FK<br /> LT<br />OF<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">KD_JENIS_TRANSAKSI<br />NPWP<br />KODE_OBJEK<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">FG_PENGGANTI<br />NAMA<br />NAMA<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">NOMOR_FAKTUR<br />JALAN<br />HARGA_SATUAN<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">MASA_PAJAK<br />BLOK<br />JUMLAH_BARANG<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">TAHUN_PAJAK<br />NOMOR<br />HARGA_TOTAL<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">TANGGAL_FAKTUR<br />RT<br />DISKON<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">NPWP<br />RW<br />DPP<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">NAMA<br />KECAMATAN<br />PPN<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">ALAMAT_LENGKAP<br />KELURAHAN<br />TARIF_PPNBM<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">JUMLAH_DPP<br />KABUPATEN<br />PPNBM<br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">JUMLAH_PPN<br />PROPINSI<br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">JUMLAH_PPNBM<br />KODE_POS<br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">ID_KETERANGAN_TAMBAHAN<br />NOMOR_TELEPON<br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">FG_UANG_MUKA<br /><br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">UANG_MUKA_DPP<br /><br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">UANG_MUKA_PPN<br /><br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">UANG_MUKA_PPNBM<br /><br /><br /></asp:TableHeaderCell>
                <asp:TableHeaderCell HorizontalAlign="Left" Wrap="false" BackColor="#1E90FF" ForeColor="White">REFERENSI<br /><br /><br /></asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
