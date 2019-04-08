<%@ Page Language="c#" Inherits="ISC064.KOMISI.Lap" CodeFile="Lap.aspx.cs" %>

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
        .list td {
            width: 220px;
            padding-left: 50px;
        }

        h2 {
            font: bold 10pt;
        }

        .left50 {
            margin-left: 50px;
        }
    </style>
</head>
<body class="body-padding">
    <form id="Form1" method="post" runat="server" class="cnt">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Laporan
        </h1>
        <br />
        <div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/MasterKomisi.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Komisi</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/MasterKomisiDetail.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Komisi</p>
                        <p>Detail</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/MasterCF.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Closing Fee</p>
                    </div>
                </a>
            </div>
            <div class="box-list">
                <a href="javascript:openLaporan('Laporan/MasterReward.aspx')">
                    <div class="box-content">
                        <span>Laporan</span>
                        <p>Master Reward</p>
                    </div>
                </a>
            </div>
        </div>
    </form>
</body>
</html>
