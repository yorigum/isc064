<%@ Reference Control="~/PrintKKTemplate.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.PrintKKBatch" CodeFile="PrintKKBatch.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Print Voucher Kas Keluar</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/Media/Print.css" type="text/css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/fontawesome/css/font-awesome.min.css">
    <meta name="ctrl" content="1">
    <meta name="sec" content="Print Voucher Kas Keluar (Batch)">
</head>
<body onkeyup="if(event.keyCode==27&&confirm('Apakah anda ingin menutup layar print ini?')) window.close();">
    <form id="Form1" method="post" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <input type="text" style="display: none">
        <div id="reprint" runat="server">
            <h1 style="border-bottom: 1px solid silver">Print Voucher Kas Keluar</h1>
            <p style="padding: 5px"><b>Tanggal :</b></p>
            <table>
                <tr>
                    <td>dari</td>
                    <td>
                        <asp:TextBox ID="dari" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                        <label for="dari" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    </td>
                    <td>&nbsp;</td>
                    <td>sampai</td>
                    <td>
                        <asp:TextBox ID="sampai" runat="server" CssClass="txt_center" Width="85"></asp:TextBox>
                        <label for="sampai" class="btn btn-cal"><i class="fa fa-calendar"></i></label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="daric" runat="server" CssClass="err"></asp:Label></td>
                    <td colspan="3">
                        <asp:Label ID="sampaic" runat="server" CssClass="err"></asp:Label></td>
                </tr>
            </table>
            <br>
            <div class="ins">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="print" runat="server" CssClass="btn" Text="Print" Width="75" AccessKey="p" OnClick="print_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="print">
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
