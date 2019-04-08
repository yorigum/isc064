<%@ Reference Control="~/PrintKKTemplate.ascx" %>

<%@ Page Language="c#" Inherits="ISC064.FINANCEAR.PrintKK" CodeFile="PrintKK.aspx.cs" %>

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
    <meta name="ctrl" content="3">
    <meta name="sec" content="Print Voucher Kas Keluar">
    <script type="text/JavaScript" src="/Js/MD5.js"></script>
</head>
<body onkeyup="if(event.keyCode==27&&document.getElementById('cancel')){document.getElementById('cancel').click()}else if(event.keyCode==27){document.getElementById('cancel2').click()}">
    
    <form id="Form1" method="post" runat="server">
        <div id="reprint" runat="server">
            <h1 style="border-bottom: 1px solid silver">Otorisasi Reprint</h1>
            <p style="padding: 5px">
                Jenis Dokumen : <u style="font: bold 10pt">Voucher Kas Keluar</u>
                <br>
                Dokumen sudah di-print sebanyak
					<asp:Label ID="count" runat="server"></asp:Label>
                kali.
            </p>
            <br>
            <table cellspacing="5">
                <tr>
                    <td>Username</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="username" runat="server" CssClass="txt" Width="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Password</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="pass" runat="server" CssClass="txt" Width="150" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br>
            <div class="ins">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btn" runat="server" cssclass="btn btn-green" Text="Authorize" OnClick="btn_Click"></asp:Button>
                        </td>
                        <td>
                            <input type="button" id="cancel" runat="server" class="btn btn-red" value="Cancel" style="width: 75"
                                name="cancel">
                        </td>
                        <td style="padding-left: 10px">
                            <asp:Label ID="salah" runat="server" CssClass="err"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div style="display: none">
            <input type="button" id="cancel2" runat="server" name="cancel2"></div>
        <div id="print">
            <asp:PlaceHolder ID="list" runat="server"></asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
