<%@ Page Language="c#" Inherits="ISC064.KPA.Lap" CodeFile="Lap.aspx.cs" %>

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
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan">
    <style type="text/css">
        .list td
        {
            width: 220px;
            padding-left: 50px;
        }
        h2
        {
            font: bold 12pt;
        }
        .left50
        {
            margin-left: 50px;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
    <uc1:Head ID="Head1" runat="server"></uc1:Head>
    <div>
        <h1 class="title title-line">Laporan</h1>
        <br />
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/LaporanCheckListBerkas.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p> Checklist Berkas</p>
                </div>
            </a>
        </div>
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/LaporanWawancara.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p> Wawancara</p>
                </div>
            </a>
        </div>
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/LaporanOTS.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p> OTS</p>
                </div>
            </a>
        </div>
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/LaporanLPA.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p> LPA</p>
                </div>
            </a>
        </div>
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/LaporanSP3K.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p> SP3K</p>
                </div>
            </a>
        </div>
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/LaporanAkad.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p> Akad</p>
                </div>
            </a>
        </div>
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/SummaryPotensiKPA.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p>Summary Potensi KPR</p>
                </div>
            </a>
        </div>
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/LaporanKartuPiutang2.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p> Kartu Piutang KPR</p>
                </div>
            </a>
        </div>
        <div class="box-list">
            <a href="javascript:openLaporan('Laporan/MasterPiutangBank.aspx')">
                <div class="box-content">
                    <span>Laporan</span>
                    <p> Master Piutang Bank</p>
                </div>
            </a>
        </div>
    </div>
    </form>
</body>
</html>
