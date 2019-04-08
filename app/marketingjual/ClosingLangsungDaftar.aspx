<%@ Reference Page="~/Customer.aspx" %>
<%@ Reference Page="~/Unit.aspx" %>
<%@ Reference Page="~/Skema.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.ClosingLangsungDaftar" CodeFile="ClosingLangsungDaftar.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Closing Langsung</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Pendaftaran Closing Langsung (Hal. 2)">
    <style type="text/css">
        H3 {
            font-size: 9pt;
        }
    </style>
</head>
<body class="body-padding">

    <script type="text/javascript" type="text/javascript" src="/Js/NumberFormat.js"></script>

    <form class="cnt" id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Closing Langsung</h1>
        <p><b><i>Halaman 2 dari 3</i></b></p>
        <br />
        <div style="display: none">
            <input class="btn" id="ilus" type="button" value="Ilustrasi" name="ilus" runat="server" />
            <input class="btn" id="reserv" type="button" value="Reservasi" name="reserv" runat="server" />
            <input class="btn" id="closing" type="button" value="Closing Langsung" name="closing"
                runat="server" />
            <%--onserverclick="closing_ServerClick">--%>
        </div>
        <div id="dclosing" runat="server">
            <table cellpadding="2">
                <tr>
                    <td colspan="3">
                        <h3>
                            <span style="width: 30px">1.</span> DATA PEMESAN</h3>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>NUP
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="noqueue" runat="server" Width="50" CssClass="txt_center"></asp:TextBox>
                        <asp:Label ID="noqueuec" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Sumber Data
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="sumberdata" runat="server">
                            <asp:ListItem>WALK IN</asp:ListItem>
                            <asp:ListItem>CALL IN</asp:ListItem>
                            <asp:ListItem>CANVAS</asp:ListItem>
                            <asp:ListItem>IKLAN</asp:ListItem>
                            <asp:ListItem>BUYER GET BUYER</asp:ListItem>
                            <asp:ListItem>REFERENSI</asp:ListItem>
                            <asp:ListItem>PEMBELI LAMA</asp:ListItem>
                            <asp:ListItem>PAMERAN</asp:ListItem>
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
                        <asp:RadioButton ID="perorangan" runat="server" Text="PERORANGAN" GroupName="tipe2"
                            Checked="True" OnCheckedChanged="gantitipe" AutoPostBack="true" />
                        <asp:RadioButton ID="badanhukum" runat="server" Text="BADAN HUKUM" GroupName="tipe2"
                            OnCheckedChanged="gantitipe" AutoPostBack="true"></asp:RadioButton>
                    </td>
                </tr>
                <tr>
                    <td width="200px">Kewarganegaraan
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButton ID="wni" runat="server" Text="WNI" GroupName="tipe" Checked="True"></asp:RadioButton>
                        <asp:RadioButton ID="wna" runat="server" Text="WNA" GroupName="tipe"></asp:RadioButton>
                        <asp:RadioButton ID="kori" runat="server" Text="KORPORASI INDONESIA" GroupName="tipe"></asp:RadioButton>
                        <asp:RadioButton ID="kora" runat="server" Text="KORPORASI ASING" GroupName="tipe"></asp:RadioButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table id="korp" runat="server" visible="false" cellspacing="0" width="100%">
                            <tr width="100%">
                                <td width="200px">Nama Penanggungjawab Korporasi
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="penanggungjawab" runat="server" Width="" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="penanggungjawabc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Jabatan Korporasi
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="jabatan" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="jabatanc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>No. SK Korporasi
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="nosk" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="noskc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Bentuk Korporasi
                                </td>
                                <td>:
                                </td>
                                <td>
                                    <asp:TextBox ID="bentuk" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                    <asp:Label ID="bentukc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <tr>
                        <td>Nama Lengkap
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="nama" runat="server" Width="250px" MaxLength="100" CssClass="txt"></asp:TextBox><asp:Label
                                ID="namac" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <p>
                                <br />
                                <b>Kartu Identitas</b></p>
                        </td>
                    </tr>
                        <tr>
                            <td>
                                No. Identitas
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="noktp" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="Label1" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Alamat
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ktp1" runat="server" CssClass="txt igroup" Width="250" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="ktp1c" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                RT/RW
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ktp2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kelurahan
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ktp3" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kecamatan
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ktp4" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kotamadya/Kabupaten
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="ktp5" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kode Pos
                            </td>
                            <td>
                                :
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
                                <asp:TextBox ID="tglktp" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                                <label for="tglktp" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                <asp:Label ID="tglktpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                <tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="sedup" runat="server" Text="Seumur Hidup" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3">
                            <p>
                                <br />
                                <b>Data Pribadi</b></p>
                        </td>
                    </tr>
                    <tr>
                        <td>Tempat, Tanggal Lahir
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:TextBox ID="tempat" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox>
                            <asp:TextBox ID="tgllahir" runat="server" MaxLength="50" CssClass="txt_center" Width="80px"></asp:TextBox><label for="tgllahir" class="btn btn-cal"><i class="fa fa-calendar"></i></label><asp:Label ID="tgllahirc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">Status
                        </td>
                        <td valign="top">:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="marital" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>MENIKAH</asp:ListItem>
                                <asp:ListItem Selected="True">BELUM MENIKAH</asp:ListItem>
                                <asp:ListItem>CERAI</asp:ListItem>
                                <asp:ListItem>LAIN-LAIN</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>Agama
                        </td>
                        <td>:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="agama" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True">ISLAM</asp:ListItem>
                                <asp:ListItem>KRISTEN</asp:ListItem>
                                <asp:ListItem>KATOLIK</asp:ListItem>
                                <asp:ListItem>BUDHA</asp:ListItem>
                                <asp:ListItem>HINDU</asp:ListItem>
                                <asp:ListItem>KONGHUCU</asp:ListItem>
                                <asp:ListItem>LAINNYA</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>    
                    <tr>
                        <td valign="top">Alamat Surat Menyurat
                        </td>
                        <td valign="top">:
                        </td>
                        <td>
                             <input type="radio" id="rbSama1" name="pilihcara2" onclick="javascript: samaisi()" />SAMA dengan KTP</label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">Alamat Surat Menyurat
                        </td>
                        <td valign="top">:
                        </td>
                        <td>
                            <asp:TextBox ID="alamat1" runat="server" Width="250" MaxLength="50" CssClass="txt"></asp:TextBox>
                            <asp:Label ID="alamatc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                            <td>
                                RT/RW
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="alamat2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kelurahan
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="alamat3" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kecamatan
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="alamat4" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kotamadya/Kabupaten
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="alamat5" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Telp
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="telp" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox>
                                <asp:Label ID="telpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                No. HP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="hp" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                No. HP 2
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="hp2" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Fax
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="fax" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Alamat Email
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="email" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                                <td colspan="2">
                                    <p>
                                        <br />
                                        <b>Data NPWP</b></p>
                                </td>
                                <td>
                                    <br />
                                    <input type="radio" id="rbSama1" name="pilihcara2" onclick="javascript: samaisi2()" />SAMA dengan KTP</label>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                Nama NPWP
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="namanpwp" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>                        
                            <td>NPWP
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp" runat="server" CssClass="txt" MaxLength="50" Width="150px"
                                    OnTextChanged="npwp_TextChanged" AutoPostBack="true">00.000.000.0-000.000</asp:TextBox>
                                <asp:Label ID="npwpc" runat="server" CssClass="err"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Alamat NPWP
                            </td>
                            <td valign="top">:
                            </td>
                            <td>
                                <asp:TextBox ID="npwp1" runat="server" Width="250" MaxLength="100" CssClass="txt"></asp:TextBox>
                                <asp:Label ID="npwp1c" runat="server" CssClass="err"></asp:Label>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                RT/RW
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="npwp2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kelurahan
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="npwp3" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kecamatan
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="npwp4" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kotamadya/Kabupaten
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="npwp5" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                               <p>
                                    <br />
                                    <b>Data Kantor</b></p>
                           </td>
                        </tr>
                        <tr>
                            <td>Pekerjaan
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="pekerjaan" runat="server" MaxLength="100" CssClass="txt" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Alamat Kantor
                            </td>
                            <td valign="top">:
                            </td>
                            <td>
                                <asp:TextBox ID="kantor1" runat="server" Width="247px" MaxLength="50" CssClass="txt"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                RT/RW
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="kantor2" runat="server" CssClass="txt igroup" Width="150" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kelurahan
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="kantor3" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kecamatan
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="kantor4" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kotamadya/Kabupaten
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="kantor5" runat="server" CssClass="txt igroup" Width="200" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">No. Telp Kantor
                            </td>
                            <td valign="top">:
                            </td>
                            <td>
                                <asp:TextBox ID="telpk" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox>
                            </td>
                        </tr>
                <tr style="display: none;">
                    <td>Jenis Kelamin
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="jenisKelamin" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">LAKI-LAKI</asp:ListItem>
                            <asp:ListItem>PEREMPUAN</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">Nama Perusahaan
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:TextBox ID="perusahaan" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">No. SIUP/Akte
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:TextBox ID="siup" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">Jenis Usaha
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:TextBox ID="jenisusaha" runat="server" Width="150" MaxLength="50" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td colspan="3">
                        <asp:CheckBox ID="cktp" runat="server" Text="KTP" />
                        <asp:CheckBox ID="cnpwp" runat="server" Text="NPWP" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Orang yang dapat dihubungi </b>
                        <tr>
                            <td class="style1">Nama
                            </td>
                            <td>:
                            </td>
                            <td>
                                <asp:TextBox ID="nmorghub" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </td>
                    <tr>
                        <td class="style1">Hubungan
                        </td>
                        <td>:
                        </td>
                            <td>
                                <asp:RadioButtonList ID="hubungan" RepeatColumns="7" runat="server">
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
                        <td valign="top" class="style1">No. HP
                        </td>
                        <td valign="top">:
                        </td>
                        <td>
                            <asp:TextBox ID="hphub" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox><asp:Label
                                ID="hpc" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style1">Alamat Email
                        </td>
                        <td valign="top">:
                        </td>
                        <td>
                            <asp:TextBox ID="emailhub" runat="server" MaxLength="100" CssClass="txt" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <h3>
                            <span style="width: 30px">2.</span> UNIT YANG DIPESAN</h3>
                    </td>
                </tr>
                <tr>
                    <td>Tanggal Kontrak
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="tglKontrak" runat="server" Width="100px" CssClass="txt_center" ReadOnly="False">
                        </asp:TextBox>&nbsp;<label for="tglKontrak" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglkontrakc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Kode Unit
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="unit" runat="server" Width="100" CssClass="txt" ReadOnly="true"></asp:TextBox>
                        <div style="display: none">
                            <asp:TextBox ID="nostock" runat="server"></asp:TextBox>
                            <asp:TextBox ID="nokontrak" runat="server"></asp:TextBox>
                            <asp:TextBox ID="nokontrakmanual" runat="server"></asp:TextBox>
                        </div>
                        <input visible="false" class="btn" id="btnUnit" onclick="popDaftarUnit2('a')" type="button"
                            value="..." name="btnUnit" runat="server" />
                        <asp:Label ID="unitc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>Price List
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="Pricelist" runat="server" Width="100" ReadOnly="true"></asp:TextBox>&nbsp
                        rupiah
                        <asp:Label ID="pricec" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr valign="top">
                    <td>Skema
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="skema" runat="server" Width="400" AutoPostBack="True"
                            OnSelectedIndexChanged="skema_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Sifat PPN
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="sifatppn" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                            <%--onselectedindexchanged="sifatppn_SelectedIndexChanged">--%>
                            <asp:ListItem Selected="False">Tanpa PPN</asp:ListItem>
                            <asp:ListItem Selected="True">Dengan PPN</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trppn" runat="server">
                    <td colspan="2">&nbsp;
                    </td>
                    <td>
                        <asp:CheckBox ID="roundppn" runat="server" Checked="True" Text="Nilai PPN Dibulatkan"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Diskon Harga Jual
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <%--onselectedindexchanged="sifatppn_SelectedIndexChanged">--%>
                        <div id="lumsum" runat="server" style="display: none">
                            <asp:TextBox ID="diskon" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                                ID="diskonc" runat="server" CssClass="err"></asp:Label>
                        </div>
                        <div id="persentingkat" runat="server">
                            <input class="btn" onclick="popdiskon('diskon2', 'diskonket')" type="button" value="..."
                                id="btnBertingkat" runat="server" />
                            <asp:TextBox ID="diskon2" runat="server" Width="60px" MaxLength="100" CssClass="txt_num"
                                AutoPostBack="true" OnTextChanged="diskon2_TextChanged">0</asp:TextBox>&nbsp;
                            <div style="display: none">
                                <asp:TextBox ID="diskonket" runat="server"></asp:TextBox>
                            </div>
                            <asp:TextBox ID="nilaiDiskon" runat="server" CssClass="txt_num" ReadOnly="True" Width="108px"></asp:TextBox>
                            <%--onselectedindexchanged="sifatppn_SelectedIndexChanged">--%>
                            <asp:Label ID="diskon2c" runat="server" CssClass="err"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Diskon Tambahan
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="jenisDiskon" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="jenisDiskon_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Rp</asp:ListItem>
                            <asp:ListItem>% Bertingkat</asp:ListItem>
                        </asp:RadioButtonList>
                        <div id="divLumpSum" runat="server">
                            <asp:TextBox ID="diskonLumpSum" runat="server" CssClass="txt_num">0</asp:TextBox>
                            <asp:Label ID="diskonLumpSumc" runat="server" CssClass="err" />
                        </div>
                        <div id="divPersenBertingkat" runat="server">
                            <asp:TextBox ID="diskontambahPersen" runat="server" Width="150" MaxLength="100" CssClass="txt">0</asp:TextBox>
                            <input class="btn" onclick="popdiskon('diskontambahPersen', 'diskontambahKet')" type="button"
                                value="..." />
                            <div style="display: none;">
                                <asp:TextBox ID="diskontambahKet" runat="server"></asp:TextBox>
                            </div>
                            <asp:Label ID="diskontambahPersenc" runat="server" CssClass="err" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Bunga
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <div id="persentingakat" runat="server">
                            <input class="btn" onclick="popbunga('bunga2', 'bungaket')" type="button" value="..."
                                id="btnBertingkat2" runat="server">
                            <asp:TextBox ID="bunga2" runat="server" CssClass="txt" Width="50px" MaxLength="100"
                                AutoPostBack="true" OnTextChanged="bunga2_TextChanged"></asp:TextBox>&nbsp;
                            <div style="display: none">
                                <asp:TextBox ID="bungaket" runat="server"></asp:TextBox>
                            </div>
                            <asp:TextBox ID="nilaiBunga" runat="server" CssClass="txt_num" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="bunga2c" runat="server" CssClass="err"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <h3>
                            <span style="width: 30px">3.</span> SALES PERSON</h3>
                    </td>
                </tr>
                <tr>
                    <td>Kode Sales
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="agent" runat="server" Width="400" AutoPostBack="true"
                            OnSelectedIndexChanged="GantiTipeSales">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr><td>Refferator
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="reffcust" runat="server" Width="300"></asp:TextBox>

                    </td>
                </tr>
                <tr><td>Bank Refferator
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="bankreff" runat="server" Width="300"></asp:TextBox>

                    </td>
                </tr>
                <tr><td>No Rek. Refferator
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="norekreff" runat="server" Width="300"></asp:TextBox>

                    </td>
                </tr>
                <tr><td>A/N Bank Reff
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="anreff" runat="server" Width="300"></asp:TextBox>

                    </td>
                </tr>

                <tr><td>NPWP Refferator
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="npwpreff" runat="server" Width="300"></asp:TextBox>

                    </td>
                </tr>
                <tr id="reff" runat="server" visible="false">
                    <td>Refferator
                    </td>
                    <td>:
                    </td>
                    <td>
                        
                        <asp:DropDownList ID="agentreff" runat="server" Width="300" Visible="false">
                            <asp:ListItem>Refferator:</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="rep" runat="server" Width="300"></asp:TextBox>
                        <asp:Label ID="agentreffc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:CheckBox ID="special" runat="server" Text="Special Event" Font-Bold="True" Font-Size="15"></asp:CheckBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td colspan="3">
                        <br>
                        <h3>
                            <span style="display: none">4.</span> BOOKING FEE</h3>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Rekening Bank
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAcc" runat="server" CssClass="ddl" Width="300">
                            <%--<asp:listitem selected="True">- Pilih Rekening Bank -</asp:listitem>--%>
                        </asp:DropDownList>
                        <asp:Label ID="ddlAccErr" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Nilai
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="carabayar" runat="server" CssClass="ddl">
                            <asp:ListItem Value="TN">TN = Tunai</asp:ListItem>
                            <asp:ListItem Value="KK">KK = Kartu Kredit</asp:ListItem>
                            <asp:ListItem Value="KD">KD = Kartu Debit</asp:ListItem>
                            <asp:ListItem Value="TR">TR = Transfer Bank</asp:ListItem>
                            <asp:ListItem Value="BG">BG = Cek Giro</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="nilai" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                            ID="nilaic" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Keterangan TTS
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="kettts" runat="server" Width="200" MaxLength="200" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Bank BG
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="bankbg" runat="server" Width="125" MaxLength="50" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>No. BG
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="nobg" runat="server" Width="125" MaxLength="20" CssClass="txt"></asp:TextBox>&nbsp;
                        Tgl :
                        <asp:TextBox ID="tglbg" runat="server" Width="85" CssClass="txt_center"></asp:TextBox>
                        <label for="tglbg" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="bgc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>No. Kartu
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="nokk" runat="server" Width="125" CssClass="txt" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Bank Kartu
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="bankkk" runat="server" Width="125" CssClass="txt" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br>
                        <h3>
                            <span style="width: 30px">4.</span> KATEGORISASI</h3>
                    </td>
                </tr>
                <tr style="display: none">
                    <td valign="top">Status ROI
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rbROI" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="False">Tanpa ROI</asp:ListItem>
                            <asp:ListItem Selected="True">Dengan ROI</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Sumber Dana
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSumberDana" runat="server" OnSelectedIndexChanged="ddlSumberDana_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="0">Dana Sendiri</asp:ListItem>
                            <asp:ListItem Value="1">Pinjaman Bank</asp:ListItem>
                            <asp:ListItem Value="2">Warisan/Hibah</asp:ListItem>
                            <asp:ListItem Value="3">Lainnya</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="ddlSumberDanac" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr id="trLainnya" runat="server" visible="false">
                    <td valign="top">&nbsp;
                    </td>
                    <td valign="top">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="lainnya" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Tujuan Pembelian
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTujuan" runat="server" OnSelectedIndexChanged="ddlTujuan_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="0">Investasi</asp:ListItem>
                            <asp:ListItem Value="1">Jual Kembali</asp:ListItem>
                            <asp:ListItem Value="2">Dipakai Sendiri</asp:ListItem>
                            <asp:ListItem Value="3">Lainnya</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="ddlTujuanc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr id="trTujuanLain" runat="server" visible="false">
                    <td valign="top">&nbsp;
                    </td>
                    <td valign="top">&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="tujuanlain" runat="server" Width="400px" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display: none">
                    <td valign="top">PPN Ditanggung
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="JenisPPN" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="False">PEMERINTAH</asp:ListItem>
                            <asp:ListItem Selected="True">KONSUMEN</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="JenisPPNc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">Status Titip Jual
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="titipjual" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="False" Value="1">Titip Jual</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">Non Titip Jual</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="titipjualc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td valign="top">Status Paket Investasi
                    </td>
                    <td valign="top">:
                    </td>
                    <td valign="top">
                        <asp:CheckBox ID="paketinvest" runat="server" Text="Merupakan paket investasi" />
                        <br />
                        Tgl Berakhir Paket Investasi :
                        <asp:TextBox ID="tglinv" runat="server" Width="100px" CssClass="txt_center" ReadOnly="False"></asp:TextBox>&nbsp;
                        <label for="tglinv" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <br />
                        <asp:Label ID="tglinvc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td valign="top">Jenis KPR
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="jeniskpr" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="0">KPR</asp:ListItem>
                            <asp:ListItem Value="1">NON-KPR</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="jeniskprc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top">Cara Bayar
                    </td>
                    <td valign="top">:
                    </td>
                    <td>
                        <table id="tablecarabayar" cellspacing="1" cellpadding="1" width="100%" border="0">
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="carabayar2" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem>KPR</asp:ListItem>
                                        <asp:ListItem>CASH BERTAHAP</asp:ListItem>
                                        <asp:ListItem>CASH KERAS</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Label ID="carabayarc" runat="server" CssClass="err"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>Fitting Out
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:TextBox ID="focounter" runat="server" Width="40" CssClass="txt_center">1</asp:TextBox>&nbsp;x&nbsp;Angsuran
                        <asp:TextBox ID="fo" runat="server" CssClass="txt_num">0</asp:TextBox><asp:Label
                            ID="foc" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
            </table>
            <table height="50">
                <tr>
                    <td>
                        <asp:Button ID="ok" runat="server" Width="75" CssClass="btn btn-blue" Text="OK" OnClick="ok_Click"></asp:Button>
                    </td>
                    <td>
                        <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
                            runat="server" onclick="javascript: history.go(-1)">
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function samaisi2() {
                document.getElementById('namanpwp').value = document.getElementById('nama').value;
                document.getElementById('npwp1').value = document.getElementById('ktp1').value;
                document.getElementById('npwp2').value = document.getElementById('ktp2').value;
                document.getElementById('npwp3').value = document.getElementById('ktp3').value;
                document.getElementById('npwp4').value = document.getElementById('ktp4').value;
                document.getElementById('npwp5').value = document.getElementById('ktp5').value;
            }
            function samaisi() {
                document.getElementById('alamat1').value = document.getElementById('ktp1').value;
                document.getElementById('alamat2').value = document.getElementById('ktp2').value;
                document.getElementById('alamat3').value = document.getElementById('ktp3').value;
                document.getElementById('alamat4').value = document.getElementById('ktp4').value;
                document.getElementById('alamat5').value = document.getElementById('ktp5').value;
            }
            function call(nomor, nounit) {
                document.getElementById('nostock').value = nomor;
                document.getElementById('unit').value = nounit;
            }
            function callSource(nomor, source) {
                document.getElementById('spgabungan').value = nomor;
            }
            function popdiskon(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaDiskon.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }
            function popbunga(d1, d2) {
                foo1 = document.getElementById(d1);
                foo2 = document.getElementById(d2);
                openModal('SkemaBunga.aspx?t1=' + foo1.value + '&t2=' + foo2.value + '&d1=' + d1 + '&d2=' + d2, '450', '360');
            }
            function recaldisc(discTxt) {
                disc = discTxt.value.split("+");

                discTxt.value = "";

                for (i = 0; i < disc.length; i++) {
                    if (!isNaN(disc[i]) && disc[i] != "") {
                        if (discTxt.value != "") discTxt.value = discTxt.value + "+";
                        discTxt.value = discTxt.value + disc[i];
                    }
                }
            }
            function cvtnum(foo) {
                return foo.replace(/,/gi, "");
            }
            function recal() {

            }
        </script>

    </form>
</body>
</html>
