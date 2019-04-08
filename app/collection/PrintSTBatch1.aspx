<%@ Reference Control="~/PrintSTTemplate.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.COLLECTION.PrintSTBatch1" CodeFile="PrintSTBatch1.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Print Surat Peringatan</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link href="/Media/Style.css" type="text/css" rel="stylesheet">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Print Surat Peringatan (Batch)">
</head>
<body class="body-padding" onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar print ini?')) window.close();">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
       <%-- <div id="reprint" runat="server">
        <h1 class="title title-line" style="">Print Surat Peringatan</h1>
            <p style="padding: 5px"><b>Tanggal :</b></p>
            <table>
                <tr>
                    <td><b>Dari</b></td>
                    <td>
                        <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                        <button type="button" class="btn" onclick="openCalendar('dari');">
                            <i class="fa fa-calendar"></i>
                        </button>
                        <asp:Label ID="Label1" runat="server" CssClass="err"></asp:Label>
                    </td>
                    <td><b style="padding-left: 10px;">Sampai</b></td>
                    <td>
                        <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="135"></asp:TextBox>
                        <button type="button" class="btn" onclick="openCalendar('sampai');">
                            <i class="fa fa-calendar"></i>
                        </button>
                        <asp:Label ID="Label2" runat="server" CssClass="err"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                    <td colspan="3">
                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                </tr>
            </table>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="print" runat="server" CssClass="btn btn-blue" Text="Print" Width="75" AccessKey="p" OnClick="print_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        <input type="text" style="display: none">
            </div>--%>
        <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
