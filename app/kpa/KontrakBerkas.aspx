<%@ Page Language="c#" Inherits="ISC064.KPA.KontrakBerkas" CodeFile="KontrakBerkas.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Checklist Berkas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Kontrak - Checklist Berkas">
    <meta http-equiv="pragma" content="no-cache">
    <base target="_self">

    <script language="javascript" type="text/javascript" src="/Js/Common.js"></script>

</head>
<body class="body-padding" onkeyup="if(event.keyCode==27) window.close();">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <div id="pilih" runat="server">
            <h1 class="title title-line">Checklist Berkas</h1>
            <p>
                Halaman 1 dari 2
            </p>
            <br />
            <table style="border-right: #dcdcdc 1px solid; border-top: #dcdcdc 1px solid; border-left: #dcdcdc 1px solid; border-bottom: #dcdcdc 1px solid"
                cellspacing="5">
                <tr>
                    <td>No. Kontrak :
                    </td>
                    <td>
                        <asp:TextBox ID="nokontrak" runat="server" Width="100" CssClass="txt"></asp:TextBox>
                        <button class="btn btn-orange" id="btnpop" show-modal='#ModalPopUp' modal-title='Daftar Kontrak' modal-url='DaftarKontrak.aspx?status=a&kpr=1' type="button" value="..." name="btnpop" runat="server">
                            <i class="fa fa-search"></i>
                        </button>
                    </td>
                    <td>
                        <asp:LinkButton ID="next" runat="server" CssClass="btn btn-blue" OnClick="next_Click">Next<i class="fa fa-arrow-right"></i>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <p class="feed">
                <asp:Label ID="feed" runat="server"></asp:Label>
            </p>
        </div>
        <div id="frm" runat="server">
            <input type="text" style="display: none" />
            <div id="pageof" runat="server">
                <h1 class="title title-line">Checklist Berkas
                </h1>
                <p>
                    Halaman 2 dari 2
                </p>
                <br />
            </div>
            <table cellspacing="5">
                <tr>
                    <td>No. Kontrak
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="kontrak" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Unit
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="unit" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Customer
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:Label ID="customer" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <br>
            <table cellspacing="5">
                <tr>
                    <td colspan="3">
                        <u>PERSYARATAN UMUM:</u>
                        <asp:CheckBoxList ID="umum" runat="server">
                            <asp:ListItem>FOTO COPY KTP PEMOHON + SUAMI / ISTRI</asp:ListItem>
                            <asp:ListItem>FOTO COPY KARTU KELUARGA</asp:ListItem>
                            <asp:ListItem>PAS PHOTO UKURAN 3X4 PEMOHON  + SUAMI / ISTRI</asp:ListItem>
                            <asp:ListItem>FOTO COPY SURAT NIKAH / SURAT CERAI / SURAT KEMATIAN</asp:ListItem>
                            <asp:ListItem>FOTO COPY BUKU TABUNGAN / BATARA (SALDO + NOMOR REKENING)</asp:ListItem>
                            <asp:ListItem>NPWP PRIBADI</asp:ListItem>
                        </asp:CheckBoxList>
                        <u>PNS / ABRI / BUMN:</u>
                        <asp:CheckBoxList ID="pn" runat="server">
                            <asp:ListItem>FOTO COPY KARTU PEGAWAI / KARTU ANGGOTA</asp:ListItem>
                            <asp:ListItem>FOTO COPY KARTU PESERTA TASPEN / ASABRI</asp:ListItem>
                            <asp:ListItem>FOTO COPY SK PENGANGKATAN DAN SK TERAKHIR</asp:ListItem>
                            <asp:ListItem>KETERANGAN PERINCIAN GAJI / SLIP GAJI</asp:ListItem>
                            <asp:ListItem>FOTO COPY FORMULIR JAMSOSTEK</asp:ListItem>
                        </asp:CheckBoxList>
                        <u>KARYAWAN SWASTA:</u>
                        <asp:CheckBoxList ID="swasta" runat="server">
                            <asp:ListItem>FOTO COPY KARTU PEGAWAI / NAME TAG</asp:ListItem>
                            <asp:ListItem>KETERANGAN PENGHASILAN / SLIP GAJI DILEGALISIR</asp:ListItem>
                            <asp:ListItem>REKENING KORAN 3 BULAN TERAKHIR</asp:ListItem>
                            <asp:ListItem>FOTO COPY SIUP, NPWP, TDP / SURAT KETERANGAN</asp:ListItem>
                            <asp:ListItem>FOTO COPY FORMULIR JAMSOSTEK</asp:ListItem>
                            <asp:ListItem>SURAT KETERANGAN AKTIF KERJA</asp:ListItem>
                        </asp:CheckBoxList>
                        <u>WIRASWASTA:</u>
                        <asp:CheckBoxList ID="wira" runat="server">
                            <asp:ListItem>KETERANGAN PENGHASILAN DARI DESA / KELURAHAN</asp:ListItem>
                            <asp:ListItem>FOTO COPY SIUP, NPWP, TDP</asp:ListItem>
                            <asp:ListItem>REKENING KORAN 3 BULAN TERAKHIR</asp:ListItem>
                            <asp:ListItem>NERACA RUGI / LABA</asp:ListItem>
                            <asp:ListItem>FOTO COPY AKTE PENDIRIAN</asp:ListItem>
                            <asp:ListItem>FOTO COPY BUKU TABUNGAN / BATARA (SALDO + NOMOR REKENING)</asp:ListItem>
                        </asp:CheckBoxList>
                        <u style="display: none;">UNTUK KPR DI ATAS RP. 50.000.000,-:</u>
                        <asp:CheckBoxList ID="lain" runat="server" Style="display: none;">
                            <asp:ListItem>NPWP PRIBADI</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>Status
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="status" CssClass="radio" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="status_SelectedIndexChanged">
                            <asp:ListItem Value="0" Selected="True" style="padding-right:40px">BELUM LENGKAP</asp:ListItem>
                            <asp:ListItem Value="1">SUDAH LENGKAP</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 25px">Tgl. Selesai Berkas
                    </td>
                    <td style="padding-top: 25px">:
                    </td>
                    <td>
                        <asp:TextBox ID="tgl" CssClass="tgl txt_center" runat="server" type="text"></asp:TextBox>
                        <label for="tgl" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                        <asp:Label ID="tglc" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:LinkButton ID="ok" runat="server" CssClass="btn btn-blue" Width="50" OnClick="ok_Click">
                    <i class="fa fa-share"></i> OK
                        </asp:LinkButton>
                        <input class="btn btn-red" id="cancel" style="width: 75px" type="button" value="Cancel" name="cancel"
                            runat="server" />
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function call(nokontrak) {
                document.getElementById('nokontrak').value = nokontrak;
                document.getElementById('next').click();
            }
        </script>

    </form>
</body>
</html>
