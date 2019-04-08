<%@ Page Language="c#" Inherits="ISC064.COLLECTION.Laporan.KartuPiutang" CodeFile="KartuPiutang.aspx.cs" %>

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
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan Karttu Piutang - Lama">
    <script type="text/javascript">
        function callSource(nokontrak, source) {
            document.getElementById(source).value = nokontrak;
        }
    </script>
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
        <div style="display: none">
            <uc1:Head ID="Head1" runat="server"></uc1:Head>
        </div>
        <table id="param" runat="server" width="100%" cellspacing="3">
            <tr>
                <td>
                    <p class="comp" id="comp" runat="server"></p>
                    <h1 id="judul" runat="server" class="title">Laporan Kartu Piutang
                    </h1>
                    <table cellpadding="0" cellspacing="0">
                        <tr valign="top">
                            <td>
                                <p class="pparam">
                                    <strong>No. Kontrak</strong>
                                    <br>
                                    Dari
										<asp:TextBox ID="dari" runat="server" Width="85" CssClass="txt_center"></asp:TextBox><input type="button" value="..." onclick="popDaftarKontrak('dari');" class="btn">
                                    <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label>
                                    &nbsp;&nbsp; Sampai
										<asp:TextBox ID="sampai" runat="server" Width="85" CssClass="txt_center"></asp:TextBox><input type="button" value="..." onclick="popDaftarKontrak('sampai');" class="btn">
                                    <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label>
                                </p>
                            </td>
                            <td width="20"></td>
                        </tr>
                    </table>
                    <br>
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
    </form>
</body>
</html>
