<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.Reminder" CodeFile="Reminder.aspx.cs" %>

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
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
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
                    <a href="" id="tts2" runat="server">
                        <asp:Label ID="countPosting" runat="server"></asp:Label></a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="tts" runat="server">Posting Tanda Terima Sementara</a>
                    <p class="remind-span">
                        Tanda Terima Sementara (TTS) yang belum di-posting menjadi Bukti Kas Masuk 
							(BKM).
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="inkonsistengiro2" runat="server">
                        <asp:Label ID="countInkonsisten" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="inkonsistengiro" runat="server">Cek Giro Inkonsisten</a>
                    <p class="remind-span">
                        Cek giro dengan nomor giro yang sama tetapi informasi lainnya seperti tanggal 
							cek giro, berbeda satu dengan yang lain.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="jtgiro2" runat="server">
                        <asp:Label ID="countBGJatuhTempo" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="jtgiro" runat="server">Cek Giro Jatuh Tempo</a>
                    <p class="remind-span">
                        Daftar cek giro tidak bermasalah (status normal) yang sudah bisa dicairkan ke 
							rekening bank.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="masalahgiro2" runat="server">
                        <asp:Label ID="countBGBad" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="masalahgiro" runat="server">Cek Giro Bermasalah</a>
                    <p class="remind-span">
                        Daftar cek giro bermasalah (tolakan, overdraft, dst.) yang harus di-follow up 
							proses penggantiannya.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="unalo2" runat="server">
                        <asp:Label ID="countUnallocated" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="unalo" runat="server">Pelunasan Unallocated</a>
                    <p class="remind-span">
                        Transaksi pelunasan yang tidak teralokasikan ke tagihan. Umumnya terjadi karena 
							perubahan jadwal tagihan di marketing.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="anobaru2" runat="server">
                        <asp:Label ID="countAnonimBaru" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="anobaru" runat="server">Transfer Anonim Baru</a>
                    <p class="remind-span">
                        Transaksi transfer bank yang belum diketahui detil pembayarannya.
                    </p>
                </td>
            </tr>
            <tr>
                <td class="remind-td-num">
                    <a href="" id="anosolve2" runat="server">
                        <asp:Label ID="countAnonimSolve" runat="server"></asp:Label>
                    </a>
                </td>
                <td class="remind-td-text">
                    <a href="" id="anosolve" runat="server">Transfer Anonim Belum Solve</a>
                    <p class="remind-span">
                        Transaksi transfer bank yang sudah diketahui detil pembayarannya namun belum 
							dibuatkan TTS.
                    </p>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
