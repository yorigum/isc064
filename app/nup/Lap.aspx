<%@ Page Language="c#" Inherits="ISC064.NUP.Lap" CodeFile="Lap.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Laporan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan">
    <style type="text/css">
        .list td {
            width: 220px;
            padding-left: 50px;
        }

        h2 {
            font: bold 10pt;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Laporan</h1>
        <br>
        <div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/SummaryNUP.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>NUP</p>
                    </div>
                </a>
                <a href="javascript:openLaporan('Laporan/LapRevisiNUP.aspx')">
                    <div class="box-content">
                        Laporan
                        <p>
                            Revisi NUP
                        </p>
                    </div>
                </a>
                <a href="javascript:openLaporan('Laporan/LaporanKasirNUP.aspx')">
                    <div class="box-content">
                        Laporan
                        <p>
                            Kasir NUP
                        </p>
                    </div>
                </a>
                <a href="javascript:openLaporan('Laporan/NUPBelumBayar.aspx')">
                    <div class="box-content">
                        Laporan
                        <p>
                            NUP Belum Bayar
                        </p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/TerpilihNUP.aspx')">
                    <div class="box-content">
                        Laporan
                        <p>
                            NUP Terpilih
                        </p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/LaporanRefund.aspx')">
                    <div class="box-content">
                        Laporan
                        <p>
                            Refund NUP
                        </p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/HasilLaunching.aspx')">
                    <div class="box-content">
                        Laporan
                        <p>
                            Hasil Launching
                        </p>
                    </div>
                </a>
            </div>
        </div>
    </form>
</body>
</html>
