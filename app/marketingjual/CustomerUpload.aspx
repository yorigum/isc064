<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.CustomerUpload" CodeFile="CustomerUpload.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Upload Customer</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Customer - Upload Customer">
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
        <br />
        <asp:DropDownList ID="project" runat="server" OnSelectedIndexChanged="project_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
        <br />
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
                <asp:TableCell>Tipe</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">PERORANGAN/BADAN HUKUM</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>2.</asp:TableCell>
                <asp:TableCell>Nama</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>3.</asp:TableCell>
                <asp:TableCell>Sumber Data</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">WALK IN/CALL IN/CANVAS/IKLAN/BUYER GET BUYER/REFERENSI/PEMBELI LAMA/PAMERAN/eSales/LAINNYA</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>4.</asp:TableCell>
                <asp:TableCell>Agama</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">ISLAM/KRISTEN/KATOLIK/BUDHA/HINDU/KONGHUCU/LAINNYA</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>5.</asp:TableCell>
                <asp:TableCell>Tempat Lahir</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>6.</asp:TableCell>
                <asp:TableCell>Tanggal Lahir</asp:TableCell>
                <asp:TableCell>TANGGAL</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>7.</asp:TableCell>
                <asp:TableCell>Status</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">MENIKAH/BELUM MENIKAH/CERAI/LAIN-LAIN</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>8.</asp:TableCell>
                <asp:TableCell>Telepon</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>9.</asp:TableCell>
                <asp:TableCell>HP 1</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>10.</asp:TableCell>
                <asp:TableCell>HP 2</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>11.</asp:TableCell>
                <asp:TableCell>Fax</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>12.</asp:TableCell>
                <asp:TableCell>Email</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>13.</asp:TableCell>
                <asp:TableCell>Nomor KTP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>14.</asp:TableCell>
                <asp:TableCell>KTP Alamat</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>15.</asp:TableCell>
                <asp:TableCell>KTP RT/RW</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>16.</asp:TableCell>
                <asp:TableCell>KTP Kelurahan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>17.</asp:TableCell>
                <asp:TableCell>KTP Kecamatan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>18.</asp:TableCell>
                <asp:TableCell>KTP Kotamadya</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>19.</asp:TableCell>
                <asp:TableCell>KTP Kode Pos</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>20.</asp:TableCell>
                <asp:TableCell>Tanggal KTP Berakhir</asp:TableCell>
                <asp:TableCell>TANGGAL</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>21.</asp:TableCell>
                <asp:TableCell>Alamat Surat Menyurat</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>150</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>22.</asp:TableCell>
                <asp:TableCell>Alamat Surat Menyurat RT/RW</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>23.</asp:TableCell>
                <asp:TableCell>Alamat Surat Menyurat Kelurahan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>24.</asp:TableCell>
                <asp:TableCell>Alamat Surat Menyurat Kecamatan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>25.</asp:TableCell>
                <asp:TableCell>Alamat Surat Menyurat Kotamadya</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>26.</asp:TableCell>
                <asp:TableCell>Pekerjaan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>27.</asp:TableCell>
                <asp:TableCell>Telepon Kantor</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm">No. telepon kantor</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>28.</asp:TableCell>
                <asp:TableCell>Alamat Kantor</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>150</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>29.</asp:TableCell>
                <asp:TableCell>Alamat Kantor RT/RW</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>30.</asp:TableCell>
                <asp:TableCell>Alamat Kantor Kelurahan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>31.</asp:TableCell>
                <asp:TableCell>Alamat Kantor Kecamatan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>32.</asp:TableCell>
                <asp:TableCell>Alamat Kantor Kotamadya</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>33.</asp:TableCell>
                <asp:TableCell>Nama NPWP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>34.</asp:TableCell>
                <asp:TableCell>NPWP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>15</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>35.</asp:TableCell>
                <asp:TableCell>Alamat NPWP</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>36.</asp:TableCell>
                <asp:TableCell>Alamat NPWP RT/RW</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>37.</asp:TableCell>
                <asp:TableCell>Alamat NPWP Kelurahan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>38.</asp:TableCell>
                <asp:TableCell>Alamat NPWP Kecamatan</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>39.</asp:TableCell>
                <asp:TableCell>Alamat NPWP Kotamadya</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>40.</asp:TableCell>
                <asp:TableCell>Kewarganegaraan</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">WNI/WNA/KORPORASI INDONESIA/KORPORASI ASING</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>41.</asp:TableCell>
                <asp:TableCell>Nama Penanggung Jawab Korporasi</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm">Tipe Badan Hukum</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>42.</asp:TableCell>
                <asp:TableCell>Jabatan Korporasi</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm">Tipe Badan Hukum</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>43.</asp:TableCell>
                <asp:TableCell>No. SK Korporasi</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm">Tipe Badan Hukum</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>44.</asp:TableCell>
                <asp:TableCell>Bentuk Korporasi</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>100</asp:TableCell>
                <asp:TableCell CssClass="sm">Tipe Badan Hukum</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>45.</asp:TableCell>
                <asp:TableCell>Nama Kerabat</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>46.</asp:TableCell>
                <asp:TableCell>Hubungan Kerabat</asp:TableCell>
                <asp:TableCell>RANGE</asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="sm">Orang Tua/Suami/Istri/Anak/Saudara/Teman/Lainnya</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>47.</asp:TableCell>
                <asp:TableCell>HP Kerabat</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>48.</asp:TableCell>
                <asp:TableCell>Email Kerabat</asp:TableCell>
                <asp:TableCell>TEKS</asp:TableCell>
                <asp:TableCell>50</asp:TableCell>
                <asp:TableCell CssClass="sm"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <p style="border-bottom: 1px solid gray; padding-bottom: 10px">
            <a href="Template\Customer.xls">Download Template...</a>
        </p>
        <br>
        <table cellspacing="5">
            <tr>
                <td>File Excel</td>
                <td>:</td>
                <td>
                    <input type="file" id="file" runat="server" class="txt" style="width: 600" name="file">
                </td>
            </tr>
        </table>
        <table height="50">
            <tr>
                <td>
                    <asp:LinkButton ID="upload" runat="server" CssClass="btn btn-blue" Width="75" Text="OK" OnClick="upload_Click">
							<i class="fa fa-share"></i> OK
                    </asp:LinkButton>
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
