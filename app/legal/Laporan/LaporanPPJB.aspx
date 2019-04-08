<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.LEGAL.Laporan.LaporanPPJB" CodeFile="LaporanPPJB.aspx.cs" %>

<!DOCTYPE html>
<html>
<head>
    <title>Laporan PPJB</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
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
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td width="20"></td>
                            <td>
                                <p>Tanggal PPJB</p>
                                <table>
                                    <tr>
                                        <td>dari</td>
                                        <td>
                                            <asp:TextBox ID="dari" runat="server" CssClass="tgl" Width="85" style="text-align:center"></asp:TextBox>
                                            <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        </td>
                                        <td rowspan="2">&nbsp;&nbsp;</td>
                                        <td>sampai</td>
                                        <td>
                                            <asp:TextBox ID="sampai" runat="server" CssClass="tgl" Width="85" style="text-align:center"></asp:TextBox>
                                            <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                                        <td colspan="3">
                                            <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Project </td>
                                        <td>
                                            <asp:DropDownList ID="project" runat="server" Width="175" AutoPostBack="true" OnSelectedIndexChanged="project_SelectedIndexChanged">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Perusahaan </td>
                                        <td>
                                            <asp:DropDownList ID="pers" runat="server" Width="175">
                                                <asp:ListItem>SEMUA</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <div class="ins">
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											    <i class="fa fa-search"></i> Screen Preview
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="xls" runat="server" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click">
											    Download Excel
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="pdf" runat="server" CssClass="btn btn-orange" AccessKey="e" OnClick="pdf_Click">
											    Download PDF
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div id="rpt" runat="server" visible="false"> 
        <div id="headReport" runat="server">
        </div>
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
