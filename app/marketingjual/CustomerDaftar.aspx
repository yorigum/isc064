<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.CustomerDaftar" CodeFile="CustomerDaftar.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Pendaftaran Customer Baru</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Customer - Pendaftaran Customer Baru">
    <style type="text/css">
        .sm TD {
            font-weight: normal;
            font-size: 8pt;
            line-height: normal;
            font-style: normal;
            font-variant: normal;
        }
    </style>
</head>
<body class="body-padding" onkeyup="if(document.getElementById('cancel')){if(event.keyCode==27)document.getElementById('cancel').click()}">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div style="display: none">
            <asp:CheckBox ID="dariReservasi" runat="server"></asp:CheckBox>
            <asp:CheckBox ID="dariClosing" runat="server" />
        </div>
        <h1 class="title title-line">Pendaftaran Customer Baru</h1>
        <br>
        <table cellspacing="0">
            <tr valign="top">
                <td style="padding-right: 10px">
                    <p>
                        <b>Terbaru :</b>
                    </p>
                    <asp:ListBox ID="baru" Rows="25" runat="server" Width="200" CssClass="ddl"></asp:ListBox>
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
                <td style="padding-right: 10px; padding-left: 15px; padding-bottom: 0px; padding-top: 5px">
                    <img src="/Media/line_vert.gif">
                </td>
                <td>
                    <table cellspacing="5">
                        <tr>
                            <td colspan="3">
                                <p>
                                    <b>Identitas</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Reservasi / No. NUP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nourut" runat="server" CssClass="txt igroup" Width="75" ReadOnly="True"
                                    Font-Bold="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Customer
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nocustomer" runat="server" CssClass="txt igroup" Width="75" ReadOnly="True"
                                    Font-Bold="True" Text="#AUTO#"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Project</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="project" runat="server" CssClass="ddl igroup" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Sumber Data
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:DropDownList ID="sumberdata" runat="server" CssClass="ddl igroup">
                                    <asp:ListItem>WALK IN</asp:ListItem>
                                    <asp:ListItem>CALL IN</asp:ListItem>
                                    <asp:ListItem>CANVAS</asp:ListItem>
                                    <asp:ListItem>IKLAN</asp:ListItem>
                                    <asp:ListItem>BUYER GET BUYER</asp:ListItem>
                                    <asp:ListItem>REFERENSI</asp:ListItem>
                                    <asp:ListItem>PEMBELI LAMA</asp:ListItem>
                                    <asp:ListItem>PAMERAN</asp:ListItem>
                                    <asp:ListItem>eSales</asp:ListItem>
                                    <asp:ListItem>LAINNYA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Tipe
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:RadioButton ID="perorangan" class="igroup-radio" runat="server" Text="PERORANGAN" GroupName="tipe2"
                                    Checked="True" OnCheckedChanged="gantitipe" AutoPostBack="true" />
                                <asp:RadioButton ID="badanhukum" class="igroup-radio" runat="server" Text="BADAN HUKUM" GroupName="tipe2"
                                    OnCheckedChanged="gantitipe" AutoPostBack="true"></asp:RadioButton>
                            </td>
                        </tr>
                        <tr>
                            <td width="33%">Kewarganegaraan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:RadioButton ID="wni" class="igroup-radio" runat="server" Text="WNI" GroupName="tipe" Checked="True"></asp:RadioButton>
                                <asp:RadioButton ID="wna" class="igroup-radio" runat="server" Text="WNA" GroupName="tipe"></asp:RadioButton>
                                <asp:RadioButton ID="kori" class="igroup-radio" runat="server" Text="KORPORASI INDONESIA" GroupName="tipe"></asp:RadioButton>
                                <asp:RadioButton ID="kora" class="igroup-radio" runat="server" Text="KORPORASI ASING" GroupName="tipe"></asp:RadioButton>
                            </td>
                        </tr>
                        <tr id="korp1" runat="server" visible="false">
                            <td>Nama Penanggungjawab<br />
                                Korporasi
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="penanggungjawab" CssClass="igroup" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="penanggungjawabc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr id="korp2" runat="server" visible="false">
                            <td>Jabatan Korporasi
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="jabatan" CssClass="igroup" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="jabatanc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr id="korp3" runat="server" visible="false">
                            <td>No. SK Korporasi
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nosk" CssClass="igroup" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="noskc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr id="korp4" runat="server" visible="false">
                            <td>Bentuk Korporasi
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="bentuk" CssClass="igroup" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="bentukc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Nama
                            </td>
                            <td>:
                            </td>
                            <td style="width: 400px">
                                <asp:TextBox ID="nama" runat="server" CssClass="txt igroup" Width="250" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Nama Customer 2
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nama2" runat="server" CssClass="txt igroup" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Salutation
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="salutation" runat="server" CssClass="txt igroup" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                <p>
                                    <b>Kartu Identitas</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Identitas
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="noktp" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="noktpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Alamat
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="ktp1" runat="server" CssClass="txt igroup" Width="250" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="ktp1c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>RT/RW
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="ktp2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kelurahan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="ktp3" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kecamatan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="ktp4" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kotamadya/Kabupaten
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="ktp5" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kode Pos
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="kodepos1" runat="server" CssClass="txt igroup" Width="130" MaxLength="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Tanggal KTP Berakhir
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="tglktp" runat="server" CssClass="txt_center igroup" Width="85"></asp:TextBox>
                                <label class="btn btn-cal" for="tglktp">
                                    <i class="fa fa-calendar"></i>
                                </label>
                                <asp:Label ID="tglktpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="sedup" runat="server" Text=" Seumur Hidup" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                <p>
                                    <b>Data Pribadi</b>
                                </p>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>Agama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="agama" RepeatColumns="3" runat="server">
                                    <asp:ListItem class="igroup-radio" Selected="True">ISLAM</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">KRISTEN</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">KATOLIK</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">BUDHA</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">HINDU</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">KONGHUCU</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">LAINNYA</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>Tempat
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="tempatlahir" runat="server" CssClass="txt igroup" Width="150" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Tanggal Lahir
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="tgllahir" runat="server" CssClass="txt_center igroup" Width="85"></asp:TextBox>
                                <label class="btn btn-cal" for="tgllahir">
                                    <i class="fa fa-calendar"></i>
                                </label>
                                <asp:Label ID="tgllahirc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Status
                            </td>
                            <td valign="top">:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="marital" runat="server" RepeatColumns="2" RepeatDirection="vertical">
                                    <asp:ListItem class="igroup-radio">MENIKAH</asp:ListItem>
                                    <asp:ListItem class="igroup-radio" Selected="True">BELUM MENIKAH</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">CERAI</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">LAIN-LAIN</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Nama Bisnis
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="namabisnis" runat="server" CssClass="txt igroup" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <p>
                                    <b>Data NPWP</b>
                                </p>
                            </td>
                            <td>
                                <br />
                                <input type="checkbox" id="rbSama" name="pilihcara2" onclick="javascript: samaisi()" />
                                SAMA dengan KTP
                            </td>
                        </tr>
                        <tr>
                            <td>Nama NPWP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="namanpwp" runat="server" CssClass="txt igroup" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>NPWP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp" runat="server" CssClass="txt igroup" Width="250" MaxLength="15"></asp:TextBox>
                                <asp:Label ID="npwpc" runat="server" CssClass="err" Width="250"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Alamat NPWP
                            </td>
                            <td valign="top">:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp1" runat="server" Width="250" MaxLength="100" CssClass="txt igroup"></asp:TextBox>
                                <asp:Label ID="npwp1c" runat="server" CssClass="err" Visible="false"></asp:Label>
                                <asp:TextBox ID="npwp6" runat="server" Width="150" Visible="false" MaxLength="100" CssClass="txt igroup"></asp:TextBox>
                                <asp:TextBox ID="npwp7" runat="server" Width="150" MaxLength="100" Visible="false" CssClass="txt igroup"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>RT/RW
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kelurahan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp3" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kecamatan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp4" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kotamadya
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp5" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>

                        <tr style="display: none">
                            <td>Jenis Bisnis
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="jenisbisnis" runat="server" CssClass="txt igroup" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td>Merek Bisnis
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="merekbisnis" runat="server" CssClass="txt igroup" Width="150" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br>
                                <p>
                                    <b>Kontak</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Telepon
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="notelp" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="notelpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>No. HP 1
                                <br />
                                <b style="font-size: 10px">( untuk di SMS. )</b>
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="kodehp" ReadOnly="true" CssClass="txt igroup" Width="30" MaxLength="50">+62</asp:TextBox>
                                <asp:TextBox ID="nohp" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="nohpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>No. HP 2
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nohp2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Fax
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nofax" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Alamat Email
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="email" runat="server" CssClass="txt igroup" Width="175" MaxLength="100"></asp:TextBox>
                                <asp:Label ID="emailc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <br />
                                <p>
                                    <b>Alamat Surat Menyurat</b>
                                </p>
                            </td>
                            <td>
                                <br />
                                <input type="checkbox" id="rbSama1" name="pilihcara2" onclick="javascript: samaisi2()" />SAMA dengan KTP
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>Alamat Surat Menyurat
                            </td>
                            <td>:
                            </td>
                            <td>
                                <p>
                                    <asp:TextBox ID="alamat1" runat="server" CssClass="txt igroup" Width="230" MaxLength="50"></asp:TextBox>
                                    <asp:Label ID="alamat1c" runat="server" CssClass="err" Visible="false"></asp:Label>
                                </p>
                                <p>
                                    <asp:TextBox ID="alamat6" runat="server" CssClass="txt igroup" Width="230" MaxLength="50" Visible="false"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:TextBox ID="alamat7" runat="server" CssClass="txt igroup" Width="120" MaxLength="50" Visible="false"></asp:TextBox>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>RT/RW
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="alamat2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kelurahan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="alamat3" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kecamatan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="alamat4" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kotamadya
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="alamat5" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                <p>
                                    <b>Data Pekerjaan</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>Pekerjaan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="pekerjaan" runat="server" MaxLength="100" CssClass="txt igroup" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Telepon Kantor
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="tlpkantor" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Alamat Kantor
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="kantor1" runat="server" CssClass="txt igroup" Width="230" MaxLength="50"></asp:TextBox></p>
                                    <asp:TextBox ID="kantor6" runat="server" CssClass="txt igroup" Width="230" MaxLength="50" Visible="false"></asp:TextBox></p>
                                    <asp:TextBox ID="kantor7" runat="server" CssClass="txt igroup" Width="120" MaxLength="50" Visible="false"></asp:TextBox></p>
                            </td>
                        </tr>
                        <tr>
                            <td>RT/RW
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="kantor2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kelurahan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="kantor3" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kecamatan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="kantor4" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kotamadya
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="kantor5" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3">
                                <br />
                                <p>
                                    <b>Orang Yang Dapat Dihubungi</b>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>Nama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="namahub" runat="server" CssClass="txt igroup" Width="250" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td>Hubungan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="hubungan" RepeatColumns="3" runat="server">
                                    <asp:ListItem class="igroup-radio" Selected="True">Orang Tua</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">Suami</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">Istri</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">Anak</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">Saudara</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">Teman</asp:ListItem>
                                    <asp:ListItem class="igroup-radio">Lainnya</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>HP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="hphub" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Alamat Email
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="emailhub" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="emailhubc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td colspan="3">
                                <br>
                                <p>
                                    <b>Kartu Tanda Penduduk</b>
                                </p>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td colspan="3">
                                <br>
                                <p>
                                    <b>Existing Customer</b>
                                </p>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Unit Lama / Luas
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="unitlama" runat="server" CssClass="txt igroup" Width="100" MaxLength="20"></asp:TextBox>
                                /
                                <asp:TextBox ID="luaslama" runat="server" CssClass="txt_num igroup" Width="70">0</asp:TextBox>
                                <asp:Label ID="luaslamac" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Nama Toko Lama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="tokolama" runat="server" CssClass="txt igroup" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Zoning Lama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="zoninglama" runat="server" CssClass="txt igroup" Width="250" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Gedung Lama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="gedunglama" runat="server" CssClass="txt igroup" Width="200" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Telepon Lama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="teleponlama" runat="server" CssClass="txt igroup" Width="150" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td>Akte Lama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="aktelama" runat="server" CssClass="txt igroup" Width="150" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table height="50">
                        <tr>
                            <td>
                                <asp:LinkButton ID="save" runat="server" CssClass="btn btn-blue" Width="75" OnClick="save_Click">
                                <i class="fa fa-share"></i> OK
                                </asp:LinkButton>
                            </td>
                            <td>
                                <input type="button" id="cancel" class="btn btn-red" value="Cancel" runat="server" style="width: 75px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
<script type="text/javascript">
    function samaisi() {
        document.getElementById('namanpwp').value = document.getElementById('nama').value;
        document.getElementById('npwp1').value = document.getElementById('ktp1').value;
        document.getElementById('npwp2').value = document.getElementById('ktp2').value;
        document.getElementById('npwp3').value = document.getElementById('ktp3').value;
        document.getElementById('npwp4').value = document.getElementById('ktp4').value;
        document.getElementById('npwp5').value = document.getElementById('ktp5').value;
    }
    function samaisi2() {
        document.getElementById('alamat1').value = document.getElementById('ktp1').value;
        document.getElementById('alamat2').value = document.getElementById('ktp2').value;
        document.getElementById('alamat3').value = document.getElementById('ktp3').value;
        document.getElementById('alamat4').value = document.getElementById('ktp4').value;
        document.getElementById('alamat5').value = document.getElementById('ktp5').value;
    }
    function CheckEmail(txt, err) {
        var inputtxt = document.getElementById(txt);
        var decimal = /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/;

        if (inputtxt.value.match(decimal)) {
            document.getElementById("save").disabled = false;
            document.getElementById(err).innerHTML = "";
            return true;
        }
        else {
            document.getElementById("save").enabled = false;
            document.getElementById(err).innerHTML = "Format Email.";
            return false;
        }
    }
    $("#email").keyup(function () {
        CheckEmail('email', 'emailc');
    });
    $("#emailhub").keyup(function () {
        CheckEmail('emailhub', 'emailhubc');
    });
</script>
