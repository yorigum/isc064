<%@ Page Language="c#" Inherits="ISC064.NUP.CustomerUploadNUP" CodeFile="CustomerUploadNUP.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Upload Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Customer - Upload Customer NUP">
    <style type="text/css">
        .sm {
            font: 8pt;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <input type="text" style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Upload Customer</h1>
        <br>
        <h2>Standard Pengisian</h2>
        <asp:Table ID="rule" runat="server" CssClass="tb blue-skin" CellSpacing="1">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="150">Kolom</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="75">Format</asp:TableHeaderCell>
                <asp:TableHeaderCell>Panjang</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="350">Keterangan</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>1.</asp:TableCell>
                <asp:TableCell>No.NUP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>20</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>2.</asp:TableCell>
                <asp:TableCell>Sumber Data</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">WALK IN/CALL IN/CANVAS/IKLAN/LAINNYA</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>3.</asp:TableCell>
                <asp:TableCell>Tipe</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">PERORANGAN/BADAN HUKUM/CORPORATE</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>4.</asp:TableCell>
                <asp:TableCell>NamaAgent</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>5.</asp:TableCell>
                <asp:TableCell>Nama</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>6.</asp:TableCell>
                <asp:TableCell>Telepon</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>7.</asp:TableCell>
                <asp:TableCell>HP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>8.</asp:TableCell>
                <asp:TableCell>Email</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>9.</asp:TableCell>
                <asp:TableCell>Nomor KTP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>10.</asp:TableCell>
                <asp:TableCell>KTP Alamat</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>11.</asp:TableCell>
                <asp:TableCell>KTP RT/RW</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>12.</asp:TableCell>
                <asp:TableCell>KTP Kecamatan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>13.</asp:TableCell>
                <asp:TableCell>KTP Kotamadya</asp:TableCell>
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
                <asp:TableCell>Alamat Surat Menyurat RT/RW</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>16.</asp:TableCell>
                <asp:TableCell>Alamat Surat Menyurat Kelurahan / Kecamatan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>17.</asp:TableCell>
                <asp:TableCell>Alamat Surat Menyurat Kotamadya</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>18.</asp:TableCell>
                <asp:TableCell>Rekening Bank</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>19.</asp:TableCell>
                <asp:TableCell>Rekening Cabang</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>20.</asp:TableCell>
                <asp:TableCell>No Rekening</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>21.</asp:TableCell>
                <asp:TableCell>Rekening Atas Nama</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>22.</asp:TableCell>
                <asp:TableCell>No.NPWP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>200</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>23.</asp:TableCell>
                <asp:TableCell>Tgl. Transfer NUP</asp:TableCell>
                <asp:TableCell>TANGGAL</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>24.</asp:TableCell>
                <asp:TableCell>Rekening NUP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>20</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>25.</asp:TableCell>
                <asp:TableCell>Cara Bayar NUP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>20</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>26.</asp:TableCell>
                <asp:TableCell>Nilai NUP</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>27.</asp:TableCell>
                <asp:TableCell>Jenis Properti</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>28.</asp:TableCell>
                <asp:TableCell>Tanggal Lahir</asp:TableCell>
                <asp:TableCell>TANGGAL</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <p style="border-bottom: 1px dashed gray; padding-bottom: 10">
            <a href="Template\CustomerPP.xls">Download Template...</a>
        </p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>File Excel
                </td>
                <td>:
                </td>
                <td>
                    <input type="file" id="file" runat="server" class="txt" style="width: 600" name="file">
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:Button ID="upload" runat="server" CssClass="btn" Width="75" Text="OK" OnClick="upload_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10">
                    <p class="feed">
                        <asp:Label ID="feedaa" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 10">
                    <p class="feed">
                        <asp:Label ID="feed2aa" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
        <br>
        <h2 style="border-top: 1px dashed gray; padding-top: 10">Gagal Upload :</h2>
        <asp:Table ID="gagal" runat="server">
        </asp:Table>
    </form>
</body>
</html>
