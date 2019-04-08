<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.UploadKontrak" CodeFile="UploadKontrak.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Upload Kontrak</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Migrate - Upload Kontrak">
    <style type="text/css">
        .sm {
            font: 8pt;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server" class="cnt">
        <input type="text" style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Upload Kontrak</h1>
        <br />
        <asp:DropDownList ID="project" runat="server">
        </asp:DropDownList>
        <br>
        <h2>Standard Pengisian</h2>
        <asp:Table ID="rule" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="200">Kolom</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="75">Format</asp:TableHeaderCell>
                <asp:TableHeaderCell>Panjang</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="300">Keterangan</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>1.</asp:TableCell>
                <asp:TableCell>No. Kontrak</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm">Nomor Yang Duplikat Tidak Di-Upload</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>2.</asp:TableCell>
                <asp:TableCell>Status</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">A/B</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>3.</asp:TableCell>
                <asp:TableCell>Nama Customer</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>4.</asp:TableCell>
                <asp:TableCell>No. KTP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>5.</asp:TableCell>
                <asp:TableCell>Alamat KTP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>6.</asp:TableCell>
                <asp:TableCell>Tempat Lahir</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>7.</asp:TableCell>
                <asp:TableCell>Tgl. Lahir</asp:TableCell>
                <asp:TableCell>TANGGAL</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>8.</asp:TableCell>
                <asp:TableCell>Status Marital</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">MENIKAH/BELUM MENIKAH/CERAI/LAIN-LAIN</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>9.</asp:TableCell>
                <asp:TableCell>Agama</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">ISLAM/KRISTEN/KATOLIK/BUDHA/HINDU/KONGHUCU/LAINNYA</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>10.</asp:TableCell>
                <asp:TableCell>No. Telp</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>11.</asp:TableCell>
                <asp:TableCell>No. Fax</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>12.</asp:TableCell>
                <asp:TableCell>No. NPWP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>13.</asp:TableCell>
                <asp:TableCell>Alamat NPWP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>14.</asp:TableCell>
                <asp:TableCell>Alamat Surat Menyurat</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>15.</asp:TableCell>
                <asp:TableCell>Nama Agent</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>16.</asp:TableCell>
                <asp:TableCell>Tgl. Kontrak</asp:TableCell>
                <asp:TableCell>TANGGAL</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>17.</asp:TableCell>
                <asp:TableCell>No. Unit</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>20</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>18.</asp:TableCell>
                <asp:TableCell>Pricelist</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>19.</asp:TableCell>
                <asp:TableCell>Diskon Tambahan</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>20.</asp:TableCell>
                <asp:TableCell>Nilai Kontrak</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>21.</asp:TableCell>
                <asp:TableCell>Skema Bayar</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>150</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>22.</asp:TableCell>
                <asp:TableCell>Cara Bayar</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">KPR/Cash Bertahap/Cash Keras</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>23.</asp:TableCell>
                <asp:TableCell>Jenis PPN</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">PEMERINTAH/KONSUMEN</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>24.</asp:TableCell>
                <asp:TableCell>Nilai PPN</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>25.</asp:TableCell>
                <asp:TableCell>Nilai DPP</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>26.</asp:TableCell>
                <asp:TableCell>Tgl. Batal</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Tanggal Pembatalan Kontrak</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>27.</asp:TableCell>
                <asp:TableCell>Alasan Batal</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Alasan Pembatalan Kontrak</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <p style="border-bottom: 1px dashed gray; padding-bottom: 10">
            <a href="Template\MigrateKontrak.xls">Download Template...</a>
        </p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>File Excel</td>
                <td>:</td>
                <td>
                    <input type="file" id="file" runat="server" class="txt" style="width: 600" name="file" />
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="upload" runat="server" CssClass="btn btn-blue" Width="75" OnClick="upload_Click">
							<i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:CheckBox ID="overwrite" runat="server" Text="Overwrite" Visible="false"></asp:CheckBox>
                </td>
                <td style="padding-left: 10">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
        <br />
        <h2 style="border-top: 1px dashed gray; padding-top: 10">Gagal Upload :</h2>
        <asp:Table ID="gagal" runat="server"></asp:Table>
    </form>
</body>
</html>
