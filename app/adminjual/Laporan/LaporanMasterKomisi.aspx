<%@ Page Language="C#" Inherits="ISC064.ADMINJUAL.Laporan.LaporanMasterKomisi" CodeFile="LaporanMasterKomisi.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Komisi</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="/Media/Report.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1" />
    <meta name="sec" content="Laporan - Master Komisi" />
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <p class="comp" id="comp" runat="server">
            <asp:Label ID="lblPT" runat="server"></asp:Label>
        </p>
        <h1>Laporan Master Komisi</h1>
        <br />
        <div>
            <table style="width: 50%; font-size: 13px; font-weight: bold">
                <tr>
                    <td width="100px;">Periode Komisi</td>
                    <td width="5px;">:</td>
                    <td>
                        <b>
                            <asp:Label ID="lblPeriodeKomisi" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td width="100px;">
                        <b>Nama Sales</b></td>
                    <td width="5px;">
                        <b>:</b></td>
                    <td>
                        <b>
                            <asp:Label ID="lblAgent" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td width="100px;">No Sales</td>
                    <td width="5px;">:</td>
                    <td>
                        <asp:Label ID="lblNoAgent" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Principal</b></td>
                    <td>
                        <b>:</b></td>
                    <td>
                        <b>
                            <asp:Label ID="lblPrincipal" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Status</b></td>
                    <td>
                        <b>:</b></td>
                    <td>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Alamat</td>
                    <td>:</td>
                    <td>
                        <asp:Label ID="lblAlamat" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <%--<table style="width:100%;">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td rowspan="2">
                        &nbsp;</td>
                    <td rowspan="2">
                        &nbsp;</td>
                    <td rowspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1" colspan="3">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                    <td class="style1">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>--%>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
        <div>
            <asp:PlaceHolder ID="header" runat="server"></asp:PlaceHolder>
        </div>
        <br />
        <div>
            <asp:PlaceHolder ID="report" runat="server"></asp:PlaceHolder>
        </div>

        <script type="text/javascript">
            function popJadwalKomisi(NoAgent) {
                openModal('AgentJadwalKomisi.aspx?NoAgent=' + NoAgent, '800', '600');
            }
        </script>
    </form>
</body>
</html>
