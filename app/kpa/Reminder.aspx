<%@ Page Language="c#" Inherits="ISC064.KPA.Reminder" CodeFile="Reminder.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Reminder</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Reminder">
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Reminder</h1>
        <br />
        <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>
        <br />
        <table class="blue-list-skin">
            <tr>
                <td class="remind-td-num">
                    <a href="" id="berkas" runat="server">
                        <asp:Label ID="countBerkas" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="berkas2" runat="server">Berkas KPR Belum Lengkap</a>
                    <p class="remind-span">
                        Daftar kontrak yang kelengkapan berkas-berkas KPR-nya belum selesai.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="wawancara" runat="server">
                        <asp:Label ID="lblReminderWawancara1" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="wawancara2" runat="server">Wawancara Belum Ditentukan</a>
                    <p class="remind-span">
                        Daftar wawancara belum ditentukan.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="remwawancara" runat="server">
                        <asp:Label ID="lblReminderWawancara2" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="remwawancara2" runat="server">Wawancara Sudah Dijadwalkan Belum Selesai</a>
                    <p class="remind-span">
                        Daftar wawancara sudah dijadwalkan belum selesai.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="ots" runat="server">
                        <asp:Label ID="countOTS1" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="ots2" runat="server">OTS Belum Ditentukan</a>
                    <p class="remind-span">
                        Daftar OTS belum ditentukan.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="otsjadwal" runat="server">
                        <asp:Label ID="countOTS2" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="otsjadwal2" runat="server">OTS Sudah Dijadwalkan Belum Selesai</a>
                    <p class="remind-span">
                        Daftar OTS sudah dijadwalkan belum selesai.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="otstolak" runat="server">
                        <asp:Label ID="countOTS3" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="otstolak2" runat="server">OTS Ditolak</a>
                    <p class="remind-span">
                        Daftar OTS ditolak.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="lpa" runat="server">
                        <asp:Label ID="lblReminderLPA1" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="lpa2" runat="server">LPA Belum Ditentukan</a>
                    <p class="remind-span">
                        Daftar LPA belum ditentukan.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="lpajadwal" runat="server">
                        <asp:Label ID="lblReminderLPA2" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="lpajadwal2" runat="server">LPA Sudah Dijadwalkan Belum Selesai</a>
                    <p class="remind-span">
                        Daftar LPA sudah dijadwalkan belum selesai.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="sp3k" runat="server">
                        <asp:Label ID="lblReminderSP3K1" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="sp3k2" runat="server">SP3K Belum Ditentukan</a>
                    <p class="remind-span">
                        Daftar SPK belum ditentukan.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="sp3kjadwal" runat="server">
                        <asp:Label ID="lblReminderSP3K2" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="sp3kjadwal2" runat="server">SP3K Sudah Dijadwalkan Belum Diajukan</a>
                    <p class="remind-span">
                        Daftar SP3K sudah dijadwalkan belum diajukan.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="sp3kblm" runat="server">
                        <asp:Label ID="lblReminderSP3K3" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="sp3kblm2" runat="server">SP3K Sudah Diajukan Belum Diputuskan</a>
                    <p class="remind-span">
                        Daftar SP3K sudah diajukan belum diputuskan.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="sp3ktolak" runat="server">
                        <asp:Label ID="lblReminderSP3K4" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="sp3ktolak2" runat="server">SP3K Ditolak</a>
                    <p class="remind-span">
                        Daftar SP3K ditolak.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="akad" runat="server">
                        <asp:Label ID="lblReminderAkad1" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="akad2" runat="server">Akad Belum Ditentukan</a>
                    <p class="remind-span">
                        Daftar Akad belum ditentukan.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="akadjadwal" runat="server">
                        <asp:Label ID="lblReminderAkad2" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="akadjadwal2" runat="server">Akad Sudah Dijadwalkan Belum Selesai</a>
                    <p class="remind-span">
                        Daftar akad sudah dijadwalkan belum selesai.
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
