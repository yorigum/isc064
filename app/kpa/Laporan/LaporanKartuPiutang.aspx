<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.LaporanKartuPiutang" CodeFile="LaporanKartuPiutang.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="../Head.ascx" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Laporan Kartu Piutang</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Kartu Piutang - Hal.1">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" cellspacing="3" width="100%" runat="server">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title">Laporan Kartu Piutang
                    </h1>
                    <table cellspacing="0" cellpadding="0">
                        <tr valign="top">
                            <td>
                                <p class="pparam">
                                    <strong>No. Kontrak</strong>
                                    <br />
                                    <asp:TextBox ID="dari" runat="server" ReadOnly="True" CssClass="txt_center" Width="85"></asp:TextBox><input class="btn" onclick="popDaftarKontrak('dari');" type="button" value="...">
                                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                </p>
                            </td>
                            <td width="20px"></td>
                        </tr>
                    </table>
                    <br />
                    <div class="ins">
                       <table>
							    <tr>
								    <td style="min-width:auto; padding-right:10px">
									    <asp:LinkButton id="scr" accesskey="s" runat="server" cssclass="btn btn-blue" onclick="scr_Click"><i class="fa fa-search"></i> Preview</asp:LinkButton>
									</td>
                                    <td>
                                        <asp:button id="xls" accesskey="e" runat="server" text="Download Excel" cssclass="btn btn-green" onclick="xls_Click"></asp:button>
								    </td>
							    </tr>
						    </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:PlaceHolder ID="report" runat="server"></asp:PlaceHolder>
        <asp:Table ID="rpt" runat="server"></asp:Table>
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="rp" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
