<%@ Page Language="c#" Inherits="ISC064.MARKETINGJUAL.Reminder" CodeFile="Reminder.aspx.cs" %>

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
        <br>    
        
        <asp:DropDownList runat="server" ID="project" AutoPostBack="true"></asp:DropDownList>
            
        <table class="blue-list-skin">
           <%-- <tr>
                <td class="remind-td-num">
                    <a href="ReminderCus.aspx">
                        <asp:Label ID="countCus" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderCus.aspx">Data Customer Tidak Lengkap</a>
                    <p class="remind-span">
                        Customer yang data dirinya belum dilengkapi.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="ReminderCusNPWP.aspx">
                        <asp:Label ID="countNPWP" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderCusNPWP.aspx">Data NPWP Customer Tidak Lengkap</a>
                    <p class="remind-span">
                        Customer yang data NPWP-nya belum dilengkapi.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="ReminderObs.aspx">
                        <asp:Label ID="countObs" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderObs.aspx">Reservasi Obsolete</a>
                    <p class="remind-span">
                        Waiting list yang unitnya sudah tidak lagi available.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="ReminderExpire.aspx">
                        <asp:Label ID="countExpire" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderExpire.aspx">Reservasi Expire</a>
                    <p class="remind-span">
                        Reservasi urutan pertama telah melewati batas waktu pemesanan.
                    </p>
                </td>
            </tr>--%>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="ppjb2" runat="server">
                        <asp:Label ID="countPPJB" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="ppjb" runat="server">Belum PPJB</a>
                    <p class="remind-span">
                        Customer yang belum melakukan PPJB sedangkan pelunasan sudah mencapai
                        <label id="persenppjb" runat="server"></label>
                        %.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" runat="server" id="ajb">
                        <asp:Label ID="countAJB" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="ajb2" runat="server">Belum AJB</a>
                    <p class="remind-span">
                        Customer yang belum melakukan AJB sedangkan pelunasan sudah mencapai
                        <label id="persenajb" runat="server"></label>
                        %.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="st" runat="server">
                        <asp:Label ID="countST" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="st1" runat="server">Terlambat Serah Terima</a>
                    <p class="remind-span">
                        Customer yang belum melakukan serah terima sedangkan target serah terima sudah 
							lewat.
                    </p>
                </td>
            </tr>
            <tr style="display: none">
                <td class="remind-td-num">
                    <a href="ReminderOB.aspx">
                        <asp:Label ID="countOB" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderOB.aspx">Selisih Kontrak Tagihan</a>
                    <p class="remind-span">
                        Kontrak yang masih memiliki selisih antara nilai kontrak dengan total jadwal 
							tagihan.
                    </p>
                </td>
            </tr>
            <tr style="display: none">
                <td class="remind-td-num">
                    <a href="ReminderGross.aspx">
                        <asp:Label ID="countGross" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderGross.aspx">Mutasi Price List</a>
                    <p class="remind-span">
                        Kontrak yang mengalami perubahan nilai price list diluar kondisi normal.
                    </p>
                </td>
            </tr>
            <tr style="display: none">
                <td class="remind-td-num">
                    <a href="ReminderKom.aspx">
                        <asp:Label ID="countKom" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderKom.aspx">Komisi Pending</a>
                    <p class="remind-span">
                        Daftar kontrak yang perhitungan komisinya belum dikeluarkan.
                    </p>
                </td>
            </tr>
            <tr style="display: none">
                <td class="remind-td-num">
                    <a href="ReminderBF.aspx">
                        <asp:Label ID="countBF" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderBF.aspx">Kontrak yang Baru Bayar Sampai BF</a>
                    <p class="remind-span">
                        Daftar kontrak yang pembayarannya baru sampai BF (Booking Fee).
                    </p>
                </td>
            </tr>
            <tr style="display: none">
                <td class="remind-td-num">
                    <a href="ReminderBF.aspx">
                        <asp:Label ID="countKomisi" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderAnomaliKomisi.aspx">Penjualan Berubah Setelah Komisi Dikeluarkan</a>
                </td>
            </tr>
            <tr style="display: none">
                <td class="remind-td-num">
                    <a href="ReminderPaketInvest.aspx">
                        <asp:Label ID="countPaketInvest" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="ReminderPaketInvest.aspx">Paket Investasi Expired </a>
                    <p class="remind-span">
                        Daftar kontrak yang melebihi batas tanggal paket investasi serta kontrak yang tanggal paket investasinya akan expired dalam 30 hari.
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
