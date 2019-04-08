<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.CustomerEdit" CodeFile="CustomerEdit.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="HeadCustomer" Src="HeadCustomer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NavCustomer" Src="NavCustomer.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Edit Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="5">
    <meta name="sec" content="Customer - Edit Customer">
</head>
<body onkeyup="if(event.keyCode==27) window.close()">
    <form id="Form1" method="post" runat="server">
        <div class="content-header">
            <uc1:NavCustomer ID="NavCustomer1" runat="server" Aktif="1"></uc1:NavCustomer>
        </div>
        <div class="tabdata">
            <div class="pad">
                <uc1:HeadCustomer ID="HeadCustomer1" runat="server"></uc1:HeadCustomer>
                <table cellspacing="5">
                    <tr>
                        <td width="100%"></td>
                        <td>
                            <label class="ibtn ibtn-file">
                                <input type="button" class="btn btn-blue btn-ico" value="  Log  " id="btnlog" runat="server" name="btnlog"
                                    accesskey="l">
                            </label>
                        </td>
                        <td>
                            <label class="ibtn ibtn-remove">
                                <input type="button" class="btn btn-red btn-ico" value="Delete" id="btndel" runat="server" name="btndel"
                                    accesskey="d">
                            </label>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="stamp">Input :
                        <asp:Label ID="tglInput" runat="server"></asp:Label>
                        </td>
                        <td class="stamp">Edit :
                        <asp:Label ID="tglEdit" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="5">
                    <tr>
                        <td>Status :
                        </td>
                        <td>
                            <asp:RadioButton ID="aktif" runat="server" Text="Aktif" Font-Size="12" Font-Bold="True"
                                ForeColor="green" GroupName="status"></asp:RadioButton>
                        </td>
                        <td>
                            <asp:RadioButton ID="inaktif" runat="server" Text="Inaktif" Font-Size="12" Font-Bold="True"
                                ForeColor="red" GroupName="status"></asp:RadioButton>
                        </td>
                        <td style="padding-left: 25px">Tanggal Transaksi :
                        <asp:Label ID="tgltransaksi" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="padding-left: 25px">Sifat :
                        <asp:Label ID="sifat" runat="server" Font-Bold="True" Font-Size="12"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p style="font: 8pt; padding-left: 7px; padding-bottom: 7px">
                    Customer yang tidak pernah melakukan transaksi lagi selama 1 tahun akan menjadi
                inaktif secara otomatis.
                </p>
                <table cellpadding="0" cellspacing="0">
                    <tr valign="top">
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
                                    <td>No. Customer
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nocustomer" runat="server" CssClass="txt" Width="75" ReadOnly="True"
                                            Font-Bold="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Refferator
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="reff" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Project
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="project" runat="server" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Sumber Data
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="sumberdata" runat="server" CssClass="ddl">
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
                                        <asp:RadioButton ID="perorangan" runat="server" class="igroup-radio" Text="PERORANGAN" GroupName="tipe2"
                                            Checked="True" OnCheckedChanged="gantitipe" AutoPostBack="true" />
                                        <asp:RadioButton ID="badanhukum" runat="server" class="igroup-radio" Text="BADAN HUKUM" GroupName="tipe2"
                                            OnCheckedChanged="gantitipe" AutoPostBack="true"></asp:RadioButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="33%">Kewarganegaraan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="wni" runat="server" class="igroup-radio" Text="WNI" GroupName="tipe" Checked="True"></asp:RadioButton>
                                        <asp:RadioButton ID="wna" runat="server" class="igroup-radio" Text="WNA" GroupName="tipe"></asp:RadioButton>
                                        <asp:RadioButton ID="kori" runat="server" class="igroup-radio" Text="KORPORASI INDONESIA" GroupName="tipe"></asp:RadioButton>
                                        <asp:RadioButton ID="kora" runat="server" class="igroup-radio" Text="KORPORASI ASING" GroupName="tipe"></asp:RadioButton>
                                    </td>
                                </tr>
                                <tr id="korp1" runat="server">
                                    <td>Nama Penanggungjawab<br />
                                        Korporasi
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="penanggungjawab" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="penanggungjawabc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="korp2" runat="server">
                                    <td>Jabatan Korporasi
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="jabatan" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="jabatanc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="korp3" runat="server">
                                    <td>No. SK Korporasi
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nosk" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="noskc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="korp4" runat="server">
                                    <td>Bentuk Korporasi
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="bentuk" runat="server" Width="250" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="bentukc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nama
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nama" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="namac" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <p>
                                            <br />
                                            <b>Data NPWP</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Nama NPWP
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="namanpwp" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. NPWP
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="npwp" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                        <asp:Label ID="npwpc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>Alamat NPWP
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="npwp1" runat="server" CssClass="txt" Width="230" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>RT/RW
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="npwp2" runat="server" CssClass="txt" Width="230" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>Kelurahan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="npwp3" runat="server" CssClass="txt" Width="230" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kecamatan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="npwp4" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kotamadya
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="npwp5" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Nama Customer 2
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nama2" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Agama
                                    </td>
                                    <td>:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="agama" RepeatColumns="4" runat="server" CssClass="sm">
                                            <asp:ListItem class="igroup-radio">ISLAM</asp:ListItem>
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
                                    <td>Tempat Lahir
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tempatlahir" runat="server" CssClass="txt" Width="150" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tanggal Lahir
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tgllahir" runat="server" CssClass="txt_center igroup" Width="85"></asp:TextBox>
                                        <label for="tgllahir" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        <asp:Label ID="tgllahirc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">Status
                                    </td>
                                    <td valign="top">:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="marital" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                            <asp:ListItem class="igroup-radio">MENIKAH</asp:ListItem>
                                            <asp:ListItem class="igroup-radio" Selected="True">BELUM MENIKAH</asp:ListItem>
                                            <asp:ListItem class="igroup-radio">CERAI</asp:ListItem>
                                            <asp:ListItem class="igroup-radio">LAIN-LAIN</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <p>
                                            <br />
                                            <b>Orang yang dapat dihubungi</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">Nama
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nmorghub" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">Hubungan
                                    </td>
                                    <td>:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:RadioButtonList ID="hubungan" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
                                            <asp:ListItem class="igroup-radio" Selected="True">ORANG TUA</asp:ListItem>
                                            <asp:ListItem class="igroup-radio">SUAMI</asp:ListItem>
                                            <asp:ListItem class="igroup-radio">ISTRI</asp:ListItem>
                                            <asp:ListItem class="igroup-radio">ANAK</asp:ListItem>
                                            <asp:ListItem class="igroup-radio">SAUDARA</asp:ListItem>
                                            <asp:ListItem class="igroup-radio">TEMAN</asp:ListItem>
                                            <asp:ListItem class="igroup-radio">LAINNYA</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td valign="top" class="style1">No. Telp
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tlphub" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox>
                                        <asp:Label ID="tlpc" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="style1">No. HP
                                    </td>
                                    <td valign="top">:
                                    </td>
                                    <td>                                        
                                        <asp:TextBox ID="hphub" runat="server" MaxLength="50" CssClass="txt"></asp:TextBox>
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
                                <tr style="display: none;">
                                    <td>Salutation
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="salutation" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Nama Bisnis
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="namabisnis" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Jenis Bisnis
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="jenisbisnis" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Merek Bisnis
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="merekbisnis" runat="server" CssClass="txt" Width="150" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table height="50">
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="75" OnClick="ok_Click"><i class="fa fa-share"></i> OK
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="save" runat="server" CssClass="btn btn-orange" Width="75" AccessKey="a"
                                            OnClick="save_Click"><i class="fa fa-check"></i>Apply</asp:LinkButton>
                                    </td>
                                    <td>
                                        <input id="cancel" type="button" onclick="window.close()" class="btn btn-red" value="Cancel"
                                            style="width: 75px">
                                    </td>
                                    <td style="padding-left: 10px">
                                        <p class="feed">
                                            <asp:Label ID="feed" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="30"></td>
                        <td>
                            <table cellspacing="5">
                                <tr>
                                    <td colspan="3">
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
                                        <asp:TextBox ID="notelp" runat="server" CssClass="txt" Width="150" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. HP
                                <br />
                                        <b style="font-size: 10px">( untuk di SMS. )</b>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="kodehp" ReadOnly="true" CssClass="txt" Width="30" MaxLength="50">+62</asp:TextBox>
                                        <asp:TextBox ID="nohp" runat="server" CssClass="txt" Width="150" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. HP 2
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nohp2" runat="server" CssClass="txt" Width="150" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>No. Fax
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nofax" runat="server" CssClass="txt" Width="150" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Alamat Email
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="email" runat="server" CssClass="txt" Width="175" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>Alamat Surat Menyurat
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="alamat1" runat="server" CssClass="txt" Width="230" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>RT/RW
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="alamat2" runat="server" CssClass="txt" Width="230" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kelurahan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="alamat3" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kecamatan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="alamat4" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kotamadya
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="alamat5" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <p>
                                            <br />
                                            <b>Data Pekerjaan  </b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pekerjaan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pekerjaan" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. Telepon Kantor
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="nokantor" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td>Alamat Kantor
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="kantor1" runat="server" CssClass="txt" Width="230" MaxLength="50"></asp:TextBox>
                                        <asp:TextBox ID="kantor6" runat="server" CssClass="txt" Width="230" MaxLength="50" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="kantor7" runat="server" CssClass="txt" Width="120" MaxLength="50" Visible="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>RT/RW 
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="kantor2" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kelurahan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="kantor3" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kecamatan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="kantor4" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kotamadya
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="kantor5" runat="server" CssClass="txt" Width="120" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br>
                                        <p>
                                            <b>Kartu Tanda Penduduk</b>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>No. KTP
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="noktp" runat="server" CssClass="txt" Width="140" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Alamat
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ktp1" runat="server" CssClass="txt" Width="200" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>RT/RW
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ktp2" runat="server" CssClass="txt" Width="200" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kelurahan
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ktp3" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <tr>
                                        <td>Kecamatan
                                        </td>
                                        <td>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ktp4" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                <tr>
                                    <td>Kotamadya
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ktp5" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Kodepos
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="kodepos1" runat="server" CssClass="txt" Width="120" MaxLength="50"></asp:TextBox>
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
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="sedup" runat="server" Text="Seumur Hidup" />
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
                                        <asp:TextBox ID="unitlama" runat="server" CssClass="txt" Width="100" MaxLength="20"></asp:TextBox>
                                        /
                                    <asp:TextBox ID="luaslama" runat="server" CssClass="txt_num" Width="70">0</asp:TextBox>
                                        <asp:Label ID="luaslamac" runat="server" CssClass="err"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Nama Toko Lama
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tokolama" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Zoning Lama
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="zoninglama" runat="server" CssClass="txt" Width="250" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Gedung Lama
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="gedunglama" runat="server" CssClass="txt" Width="200" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Telepon Lama
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="teleponlama" runat="server" CssClass="txt" Width="150" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td>Akte Lama
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="aktelama" runat="server" CssClass="txt" Width="150" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
