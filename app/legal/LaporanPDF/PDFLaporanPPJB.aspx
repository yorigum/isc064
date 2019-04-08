<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LEGAL.Laporan.LaporanPPJB" CodeFile="PDFLaporanPPJB.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan PPJB</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan PPJB">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title title-line">Laporan PPJB
                    </h1>
                </td>
            </tr>
        </table>
        <%--<asp:Label ID="lblHeader" runat="server"></asp:Label>--%>
        <div id="headReport" runat="server"></div>
        <div id="rpt" runat="server" visible="false"> 
        <table class="tb blue-skin" cellspacing="1">
            <tr>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    No. Urut
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Nama Pembeli
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Unit
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Blok
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Lantai
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Luas Semi Gross
                </td>
                <td colspan="4" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Status Dokumen PPJB
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Nomor PPJB System
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Nomor PPJB Manual
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Nomor PPJB yang Digunakan
                </td>
                    <asp:PlaceHolder ID="col1" runat="server" />
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    NPWP
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    No. SKPU
                </td>
 <%--               <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Denah Unit
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Denah Lantai
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Spesifikasi Material
                </td>--%>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Lengkap / Tidak Lengkap
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Kekurangan
                </td>
                <td rowspan="2" style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Tgl. Lengkap
                </td>
            </tr>
            <tr>
                <td style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    PPJB
                </td>
                <td style="background-color:#5c9bd1; vertical-align:middle; color:white;"> 
                    Tgl PPJB
                </td>
                <td style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Tgl Cetak
                </td>
                <td style="background-color:#5c9bd1; vertical-align:middle; color:white;">
                    Tgl TTD
                </td>
            </tr>
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </table>
            </div>
    </form>
</body>
</html>
