<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KartuPiutangKPA.aspx.cs" Inherits="Laporan_KartuPiutangKPA" %>--%>
<%@ Page Language="c#" Inherits="ISC064.KPA.Laporan.KartuPiutangKPA" CodeFile="KartuPiutangKPA.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="~/Head.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 4.0 Transitional//EN">

<html>
<head>
    <title>Laporan KPR</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Report.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Laporan SP3K">
</head>
<body onkeyup="if(event.keyCode==27&amp;&amp;confirm('Apakah anda ingin menutup laporan ini?'))window.close()">
    <form id="Form1" method="post" runat="server">
    <div style="display: none">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
    </div>
    <table id="param" runat="server" width="100%" cellspacing="3">
        <tr>
            <td>
                <p class="comp" id="comp" runat="server">
                </p>
                <h1 id="judul" runat="server">
                    Kartu Piutang KPR
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
                                    <td>
                                        <asp:LinkButton ID="scr" runat="server" CssClass="btn btn-blue" AccessKey="s" OnClick="scr_Click">
											    <i class="fa fa-search"></i> Screen Preview
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="xls" runat="server" CssClass="btn btn-green" AccessKey="e" OnClick="xls_Click">
											    Download Excel
                                        </asp:LinkButton>
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

