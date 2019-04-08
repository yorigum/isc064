<%@ Page Language="c#" Inherits="ISC064.ADMINJUAL.UnitUpload" CodeFile="UnitUpload.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Upload Unit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Unit - Upload Unit">
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
        <h1 class="title title-line">Upload Unit</h1>
        <br>
        <br>
        <asp:ScriptManager ID="scriptmanager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <asp:DropDownList runat="server" ID="project" AutoPostBack="true">
        </asp:DropDownList>
        <br />

        <h2 class="sm">Standard Pengisian</h2>
        <asp:Table ID="rule" runat="server" CssClass="blue-skin tb" CellSpacing="1">
            <asp:TableRow HorizontalAlign="Left">
                <asp:TableHeaderCell>No.</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="200">Kolom</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="75">Format</asp:TableHeaderCell>
                <asp:TableHeaderCell>Panjang</asp:TableHeaderCell>
                <asp:TableHeaderCell Width="300">Keterangan</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>1.</asp:TableCell>
                <asp:TableCell>Tipe</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>2.</asp:TableCell>
                <asp:TableCell>Lokasi</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>3.</asp:TableCell>
                <asp:TableCell>Blok</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>4.</asp:TableCell>
                <asp:TableCell>Nomor</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>3</asp:TableCell>
                <asp:TableCell CssClass="sm">001/002 Hanya 3 Karakter Angka</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>5.</asp:TableCell>
                <asp:TableCell>Luas</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Luas yang digunakan dalam perhitungan harga</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>6.</asp:TableCell>
                <asp:TableCell>Price List Minimum</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Batas bawah negosiasi marketing</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>7.</asp:TableCell>
                <asp:TableCell>Price List Rumah</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>8.</asp:TableCell>
                <asp:TableCell>Price List Kavling</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>9.</asp:TableCell>
                <asp:TableCell>Biaya BPHTB</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>10.</asp:TableCell>
                <asp:TableCell>Biaya Surat</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>11.</asp:TableCell>
                <asp:TableCell>Biaya Proses</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>12.</asp:TableCell>
                <asp:TableCell>Biaya Lain-Lain</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>13.</asp:TableCell>
                <asp:TableCell>Zoning</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>14.</asp:TableCell>
                <asp:TableCell>Panjang</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>15.</asp:TableCell>
                <asp:TableCell>Lebar</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>16.</asp:TableCell>
                <asp:TableCell>Tinggi</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>17.</asp:TableCell>
                <asp:TableCell>Luas Tanah</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>18.</asp:TableCell>
                <asp:TableCell>Luas Lebih Tanah</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>19.</asp:TableCell>
                <asp:TableCell>Luas Bangunan</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <%--            <asp:TableRow>
                <asp:TableCell>14.</asp:TableCell>
                <asp:TableCell>Hadap Atrium</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Ya/Tidak</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>15.</asp:TableCell>
                <asp:TableCell>Hadap Entrance</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Ya/Tidak</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>16.</asp:TableCell>
                <asp:TableCell>Hadap Eskalator</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Ya/Tidak</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>17.</asp:TableCell>
                <asp:TableCell>Hadap Lift</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Ya/Tidak</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>18.</asp:TableCell>
                <asp:TableCell>Hadap Parkir</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Ya/Tidak</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>19.</asp:TableCell>
                <asp:TableCell>Hadap Axis</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Ya/Tidak</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>20.</asp:TableCell>
                <asp:TableCell>Hook</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Ya/Tidak</asp:TableCell>
            </asp:TableRow>--%>
            <asp:TableRow>
                <asp:TableCell>20.</asp:TableCell>
                <asp:TableCell>Lebar Jalan</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Lebar jalan di depan unit properti</asp:TableCell>
            </asp:TableRow>
            <%--            <asp:TableRow>
                <asp:TableCell>22.</asp:TableCell>
                <asp:TableCell>Klasifikasi</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Outdoor/Indoor</asp:TableCell>
            </asp:TableRow>--%>
            <asp:TableRow>
                <asp:TableCell>21.</asp:TableCell>
                <asp:TableCell>Arah Hadap</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>22.</asp:TableCell>
                <asp:TableCell>Panorama</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>23.</asp:TableCell>
                <asp:TableCell>Tipe Properti</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                        <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>24.</asp:TableCell>
                <asp:TableCell>Kategori Unit</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">FLPP/Real Estate/KOMERSIL/High Rise</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>25.</asp:TableCell>
                <asp:TableCell>Nama Jalan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>26.</asp:TableCell>
                <asp:TableCell>Harga Tanah</asp:TableCell>
                <asp:TableCell>ANGKA</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <p style="border-bottom: 1px solid gray; padding-bottom: 10px">
            <a href="Template\Unit4.xls">Download Template...</a>
        </p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>File Excel</td>
                <td>:</td>
                <td>
                    <input type="file" id="file" runat="server" class="btn" style="width: 600px; text-align:left" name="file">
                </td>
            </tr>
        </table>
        <table style="height: 50px;">
            <tr>
                <td>
                    <asp:LinkButton ID="upload" runat="server" CssClass="btn btn-blue" Width="75px" OnClick="upload_Click">
                        <i class="fa fa-share"></i> OK
                    </asp:LinkButton>
                </td>
                <td>
                    <asp:CheckBox ID="overwrite" runat="server" CssClass="" Text="Overwrite"></asp:CheckBox>
                </td>
                <td style="padding-left: 10px">
                    <p class="feed">
                        <asp:Label ID="feed" runat="server"></asp:Label>
                    </p>
                </td>
            </tr>
        </table>
        <br>
        <h2 style="border-top: 1px solid gray; padding-top: 10px">Gagal Upload :</h2>
        <asp:Table ID="gagal" runat="server"></asp:Table>
    </form>
</body>
</html>
